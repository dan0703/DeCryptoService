using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;


namespace Domain.data
{
    [DataContract]
    public class Account
    {
        [DataMember]
        public int id { get; set; }

        [DataMember] 
        public string email { get; set; }

        [DataMember]
        public string password { get; set; }

        [DataMember]
        public string name { get; set; }

        [DataMember]
        public string nickname {  get; set; }

        [DataMember]
        public bool emailVerify {  get; set; }

        [DataMember]
        public byte[] profileImage { get; set; }
    }
}
