using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Domain.data
{
    [DataContract]
    public class ChatMessage
    {
        [DataMember]
        private string nickname { get; set; }
        [DataMember]
        private string message { get; set; }
        [DataMember]
        private string time { get; set; }
        public ChatMessage() { }
    }
}
