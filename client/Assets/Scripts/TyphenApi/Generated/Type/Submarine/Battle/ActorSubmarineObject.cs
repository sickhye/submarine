// This file was generated by typhen-api

using System.Collections.Generic;

namespace TyphenApi.Type.Submarine.Battle
{
    public partial class ActorSubmarineObject : TyphenApi.TypeBase<ActorSubmarineObject>
    {
        protected static readonly SerializationInfo<ActorSubmarineObject, bool> isUsingPinger = new SerializationInfo<ActorSubmarineObject, bool>("is_using_pinger", false, (x) => x.IsUsingPinger, (x, v) => x.IsUsingPinger = v);
        public bool IsUsingPinger { get; set; }
    }
}
