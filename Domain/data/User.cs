using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Domain.data
{
    [DataContract]
    public class User
    {
        [DataMember]
        public string name { get; set; }
        [DataMember] public string birthDay { get; set; }
        [DataMember] public string accountNickname { get; set; }
        [DataMember] public int idUser { get; set; }

    }
}
