package main

// Room represents a network group for battle.
type Room struct {
	id           uint64
	sessions     map[uint64]*Session
	closeHandler func(*Room)
	Join         chan *Session
	Leave        chan *Session
	Close        chan struct{}
}

func newRoom(id uint64) *Room {
	room := &Room{
		id:       id,
		sessions: make(map[uint64]*Session),
		Join:     make(chan *Session),
		Leave:    make(chan *Session),
		Close:    make(chan struct{}),
	}
	go room.run()
	return room
}

func (r *Room) run() {
loop:
	for {
		select {
		case session := <-r.Join:
			r.join(session)
		case session := <-r.Leave:
			r.leave(session)
		case <-r.Close:
			r.leaveAndCloseAll()
			break loop
		}
	}

	if r.closeHandler != nil {
		r.closeHandler(r)
	}
}

func (r *Room) join(session *Session) {
	r.sessions[session.id] = session
}

func (r *Room) leave(session *Session) {
	delete(r.sessions, session.id)
	session.room = nil
}

func (r *Room) leaveAndCloseAll() {
	for _, session := range r.sessions {
		session.close()
		r.leave(session)
	}
}
