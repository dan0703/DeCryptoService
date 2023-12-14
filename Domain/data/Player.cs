using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Domain.data
{
    [DataContract]
    public class Player
    {
        [DataMember]
        public IFriendsServicesCallback friendsServicesCallback { get; set; }

        [DataMember]
        public IJoinToGameCallback joinToGameCallback { get; set; }

        [DataMember]
        public IGameServiceCallback gameServiceCallback { get; set; }
    }
}
