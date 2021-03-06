// This file was generated by typhen-api

using System;
using System.Collections.Generic;

namespace TyphenApi.WebApi.Base
{
    public abstract class Submarine : TyphenApi.WebApiBase<TyphenApi.Type.Submarine.Error>
    {

        protected Submarine(string baseUri) : base(baseUri)
        {
        }

        [MessagePack.MessagePackObject]
        [Newtonsoft.Json.JsonObject(Newtonsoft.Json.MemberSerialization.OptIn)]
        class PingRequestBody : TyphenApi.TypeBase<PingRequestBody>
        {
            [TyphenApi.QueryStringProperty("message", false)]
            [MessagePack.Key("message")]
            [Newtonsoft.Json.JsonProperty("message")]
            [Newtonsoft.Json.JsonRequired]
            public string Message { get; set; }
        }

        public TyphenApi.WebApiRequest<TyphenApi.Type.Submarine.PingObject, TyphenApi.Type.Submarine.Error> Ping(string message)
        {
            var requestBody = new PingRequestBody();
            requestBody.Message = message;

            var request = new TyphenApi.WebApiRequest<TyphenApi.Type.Submarine.PingObject, TyphenApi.Type.Submarine.Error>(this);
            request.Uri = new Uri(BaseUri, "ping");
            request.Method = HttpMethod.Post;
            request.Body = requestBody;
            request.NoAuthenticationRequired = true;
            return request;
        }

        [MessagePack.MessagePackObject]
        [Newtonsoft.Json.JsonObject(Newtonsoft.Json.MemberSerialization.OptIn)]
        class SignUpRequestBody : TyphenApi.TypeBase<SignUpRequestBody>
        {
            [TyphenApi.QueryStringProperty("name", false)]
            [MessagePack.Key("name")]
            [Newtonsoft.Json.JsonProperty("name")]
            [Newtonsoft.Json.JsonRequired]
            public string Name { get; set; }
        }

        public TyphenApi.WebApiRequest<TyphenApi.Type.Submarine.SignUpObject, TyphenApi.Type.Submarine.Error> SignUp(string name)
        {
            var requestBody = new SignUpRequestBody();
            requestBody.Name = name;

            var request = new TyphenApi.WebApiRequest<TyphenApi.Type.Submarine.SignUpObject, TyphenApi.Type.Submarine.Error>(this);
            request.Uri = new Uri(BaseUri, "sign_up");
            request.Method = HttpMethod.Post;
            request.Body = requestBody;
            request.NoAuthenticationRequired = true;
            return request;
        }

        [MessagePack.MessagePackObject]
        [Newtonsoft.Json.JsonObject(Newtonsoft.Json.MemberSerialization.OptIn)]
        class LoginRequestBody : TyphenApi.TypeBase<LoginRequestBody>
        {
            [TyphenApi.QueryStringProperty("auth_token", false)]
            [MessagePack.Key("auth_token")]
            [Newtonsoft.Json.JsonProperty("auth_token")]
            [Newtonsoft.Json.JsonRequired]
            public string AuthToken { get; set; }
        }

        public TyphenApi.WebApiRequest<TyphenApi.Type.Submarine.LoginObject, TyphenApi.Type.Submarine.Error> Login(string auth_token)
        {
            var requestBody = new LoginRequestBody();
            requestBody.AuthToken = auth_token;

            var request = new TyphenApi.WebApiRequest<TyphenApi.Type.Submarine.LoginObject, TyphenApi.Type.Submarine.Error>(this);
            request.Uri = new Uri(BaseUri, "login");
            request.Method = HttpMethod.Post;
            request.Body = requestBody;
            request.NoAuthenticationRequired = true;
            return request;
        }

        [MessagePack.MessagePackObject]
        [Newtonsoft.Json.JsonObject(Newtonsoft.Json.MemberSerialization.OptIn)]
        class FindUserRequestBody : TyphenApi.TypeBase<FindUserRequestBody>
        {
            [TyphenApi.QueryStringProperty("name", false)]
            [MessagePack.Key("name")]
            [Newtonsoft.Json.JsonProperty("name")]
            [Newtonsoft.Json.JsonRequired]
            public string Name { get; set; }
        }

        public TyphenApi.WebApiRequest<TyphenApi.Type.Submarine.FindUserObject, TyphenApi.Type.Submarine.Error> FindUser(string name)
        {
            var requestBody = new FindUserRequestBody();
            requestBody.Name = name;

            var request = new TyphenApi.WebApiRequest<TyphenApi.Type.Submarine.FindUserObject, TyphenApi.Type.Submarine.Error>(this);
            request.Uri = new Uri(BaseUri, "find_user");
            request.Method = HttpMethod.Post;
            request.Body = requestBody;
            request.NoAuthenticationRequired = false;
            return request;
        }

        [MessagePack.MessagePackObject]
        [Newtonsoft.Json.JsonObject(Newtonsoft.Json.MemberSerialization.OptIn)]
        class CreateRoomRequestBody : TyphenApi.TypeBase<CreateRoomRequestBody>
        {
        }

        public TyphenApi.WebApiRequest<TyphenApi.Type.Submarine.CreateRoomObject, TyphenApi.Type.Submarine.Error> CreateRoom()
        {
            var requestBody = new CreateRoomRequestBody();

            var request = new TyphenApi.WebApiRequest<TyphenApi.Type.Submarine.CreateRoomObject, TyphenApi.Type.Submarine.Error>(this);
            request.Uri = new Uri(BaseUri, "create_room");
            request.Method = HttpMethod.Post;
            request.Body = requestBody;
            request.NoAuthenticationRequired = false;
            return request;
        }

        [MessagePack.MessagePackObject]
        [Newtonsoft.Json.JsonObject(Newtonsoft.Json.MemberSerialization.OptIn)]
        class GetRoomsRequestBody : TyphenApi.TypeBase<GetRoomsRequestBody>
        {
        }

        public TyphenApi.WebApiRequest<TyphenApi.Type.Submarine.GetRoomsObject, TyphenApi.Type.Submarine.Error> GetRooms()
        {
            var requestBody = new GetRoomsRequestBody();

            var request = new TyphenApi.WebApiRequest<TyphenApi.Type.Submarine.GetRoomsObject, TyphenApi.Type.Submarine.Error>(this);
            request.Uri = new Uri(BaseUri, "get_rooms");
            request.Method = HttpMethod.Post;
            request.Body = requestBody;
            request.NoAuthenticationRequired = false;
            return request;
        }

        [MessagePack.MessagePackObject]
        [Newtonsoft.Json.JsonObject(Newtonsoft.Json.MemberSerialization.OptIn)]
        class JoinIntoRoomRequestBody : TyphenApi.TypeBase<JoinIntoRoomRequestBody>
        {
            [TyphenApi.QueryStringProperty("room_id", false)]
            [MessagePack.Key("room_id")]
            [Newtonsoft.Json.JsonProperty("room_id")]
            [Newtonsoft.Json.JsonRequired]
            public long RoomId { get; set; }
        }

        public TyphenApi.WebApiRequest<TyphenApi.Type.Submarine.JoinIntoRoomObject, TyphenApi.Type.Submarine.Error> JoinIntoRoom(long room_id)
        {
            var requestBody = new JoinIntoRoomRequestBody();
            requestBody.RoomId = room_id;

            var request = new TyphenApi.WebApiRequest<TyphenApi.Type.Submarine.JoinIntoRoomObject, TyphenApi.Type.Submarine.Error>(this);
            request.Uri = new Uri(BaseUri, "join_into_room");
            request.Method = HttpMethod.Post;
            request.Body = requestBody;
            request.NoAuthenticationRequired = false;
            return request;
        }

    }
}
