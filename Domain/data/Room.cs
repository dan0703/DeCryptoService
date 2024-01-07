using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Domain.data
{
    public class JoinToGame
    {
        [DataMember]
        public string nickname { get; set; }
        [DataMember]
        public int code { get; set; }

        public JoinToGame()
        {

        }
    }
}
