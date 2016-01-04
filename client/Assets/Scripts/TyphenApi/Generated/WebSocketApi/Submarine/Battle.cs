// This file was generated by typhen-api

using System;
using System.Collections.Generic;

namespace TyphenApi.WebSocketApi.Parts.Submarine
{
    public partial class Battle : TyphenApi.IWebSocketApi
    {
        public enum MessageType
        {
            Ping = -973977363,
            Room = -973911978,
        }

        readonly IWebSocketSession session;

        public event Action<TyphenApi.Type.Submarine.Battle.PingObject> OnPingReceive;
        public event Action<TyphenApi.Type.Submarine.Room> OnRoomReceive;


        public Battle(IWebSocketSession session)
        {
            this.session = session;

        }

        public void SendPing(TyphenApi.Type.Submarine.Battle.PingObject ping)
        {
            session.Send((int)MessageType.Ping, ping);
        }

        public void SendPing(string message)
        {
            session.Send((int)MessageType.Ping, new TyphenApi.Type.Submarine.Battle.PingObject()
            {
                Message = message,
            });
        }
        public void SendRoom(TyphenApi.Type.Submarine.Room room)
        {
            session.Send((int)MessageType.Room, room);
        }

        public void SendRoom(long id, List<TyphenApi.Type.Submarine.User> members)
        {
            session.Send((int)MessageType.Room, new TyphenApi.Type.Submarine.Room()
            {
                Id = id,
                Members = members,
            });
        }

        public TyphenApi.TypeBase DispatchMessageEvent(int messageType, byte[] messageData)
        {
            switch ((MessageType)messageType)
            {
                case MessageType.Ping:
                {
                    var message = session.MessageDeserializer.Deserialize<TyphenApi.Type.Submarine.Battle.PingObject>(messageData);

                    if (OnPingReceive != null)
                    {
                        OnPingReceive(message);
                    }

                    return message;
                }
                case MessageType.Room:
                {
                    var message = session.MessageDeserializer.Deserialize<TyphenApi.Type.Submarine.Room>(messageData);

                    if (OnRoomReceive != null)
                    {
                        OnRoomReceive(message);
                    }

                    return message;
                }
            }


            return null;
        }
    }
}
