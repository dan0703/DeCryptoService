using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Domain.data
{
    [DataContract]
    public class BlueTeam
    {
        [DataMember]
        private string nicknamePlayer1 { get; set; }
        [DataMember]
        private string nicknamePlayer2 { get; set; }

        public BlueTeam()
        {

        }
    }
}
