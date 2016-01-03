package connection

import (
	"errors"
	"github.com/gorilla/websocket"
	"net/http"
	"time"
)

var (
	// ErrMessageChannelFull is returned when the connection's envelope channel is full.
	ErrMessageChannelFull = errors.New("Message channel is full")

	closeEnvelope      = &envelope{websocket.CloseMessage, websocket.FormatCloseMessage(websocket.CloseNormalClosure, "")}
	closeErrorEnvelope = &envelope{websocket.CloseMessage, websocket.FormatCloseMessage(websocket.CloseInternalServerErr, "")}
	pingEnvelope       = &envelope{websocket.PingMessage, []byte{}}
)

// Connection represents a web socket connection.
type Connection struct {
	conn                 *websocket.Conn
	Settings             *Settings
	Dialer               *websocket.Dialer
	Upgrader             *websocket.Upgrader
	BinaryMessageHandler func([]byte)
	TextMessageHandler   func([]byte)
	DisconnectHandler    func()
	ErrorHandler         func(error)
	envelope             chan *envelope
}

// New creates a Connection.
func New() *Connection {
	connection := &Connection{
		Settings: newDefaultSettings(),
		Dialer:   new(websocket.Dialer),
		Upgrader: new(websocket.Upgrader),
	}
	return connection
}

// Connect to the peer.
func (c *Connection) Connect(url string, requestHeader http.Header) (*http.Response, error) {
	c.envelope = make(chan *envelope, c.Settings.MessageChannelBufferSize)
	c.Dialer.ReadBufferSize = c.Settings.ReadBufferSize
	c.Dialer.WriteBufferSize = c.Settings.WriteBufferSize
	c.Dialer.HandshakeTimeout = c.Settings.HandshakeTimeout

	conn, response, err := c.Dialer.Dial(url, requestHeader)
	if err != nil {
		return response, err
	}
	c.conn = conn
	c.conn.SetPingHandler(nil)

	go c.writePump()
	go c.readPump()
	return response, nil
}

// UpgradeFromHTTP upgrades HTTP to WebSocket.
func (c *Connection) UpgradeFromHTTP(responseWriter http.ResponseWriter, request *http.Request) error {
	c.envelope = make(chan *envelope, c.Settings.MessageChannelBufferSize)
	c.Upgrader.ReadBufferSize = c.Settings.ReadBufferSize
	c.Upgrader.WriteBufferSize = c.Settings.WriteBufferSize
	c.Upgrader.HandshakeTimeout = c.Settings.HandshakeTimeout

	conn, err := c.Upgrader.Upgrade(responseWriter, request, nil)
	if err != nil {
		return err
	}
	c.conn = conn

	go c.writePump()
	go c.readPump()
	return nil
}

// Close the connection.
func (c *Connection) Close() error {
	return c.postEnvelope(closeEnvelope)
}

// WriteBinaryMessage to the peer.
func (c *Connection) WriteBinaryMessage(data []byte) error {
	return c.postEnvelope(&envelope{websocket.BinaryMessage, data})
}

// WriteTextMessage to the peer.
func (c *Connection) WriteTextMessage(data []byte) error {
	return c.postEnvelope(&envelope{websocket.TextMessage, data})
}

func (c *Connection) postEnvelope(e *envelope) error {
	select {
	case c.envelope <- e:
		return nil
	default:
		return ErrMessageChannelFull
	}
}

func (c *Connection) writeMessage(e *envelope) error {
	c.conn.SetWriteDeadline(time.Now().Add(c.Settings.WriteWait))
	err := c.conn.WriteMessage(e.messageType, e.data)
	if err != nil && c.ErrorHandler != nil {
		c.ErrorHandler(err)
	}
	return err
}

func (c *Connection) writePump() {
	defer c.conn.Close()

	ticker := time.NewTicker(c.Settings.PingPeriod)
	defer ticker.Stop()

loop:
	for {
		select {
		case e, ok := <-c.envelope:
			if !ok {
				c.writeMessage(closeErrorEnvelope)
				break loop
			}
			if err := c.writeMessage(e); err != nil {
				break loop
			}
		case <-ticker.C:
			if err := c.writeMessage(pingEnvelope); err != nil {
				break loop
			}
		}
	}
}

func (c *Connection) readPump() {
	defer c.conn.Close()

	c.conn.SetReadLimit(c.Settings.MaxMessageSize)
	c.conn.SetReadDeadline(time.Now().Add(c.Settings.PongWait))
	c.conn.SetPongHandler(func(string) error {
		c.conn.SetReadDeadline(time.Now().Add(c.Settings.PongWait))
		return nil
	})

	for {
		messageType, data, err := c.conn.ReadMessage()
		if err != nil {
			if c.ErrorHandler != nil {
				c.ErrorHandler(err)
			}
			break
		}

		switch messageType {
		case websocket.BinaryMessage:
			if c.BinaryMessageHandler != nil {
				c.BinaryMessageHandler(data)
			}
		case websocket.TextMessage:
			if c.TextMessageHandler != nil {
				c.TextMessageHandler(data)
			}
		}
	}

	if c.DisconnectHandler != nil {
		c.DisconnectHandler()
	}
}