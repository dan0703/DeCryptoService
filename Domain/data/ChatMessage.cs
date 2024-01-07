using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Domain.data
{
    [DataContract]
    public class ChatMessageClient
    {
        [DataMember]
        public string nickname { get; set; } 
        [DataMember]
        public string message { get; set; }
        [DataMember]
        public string time { get; set; }
        public ChatMessageClient() { }
    }
}
