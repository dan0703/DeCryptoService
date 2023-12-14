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
        public string nicknamePlayer1 { get; set; }
        [DataMember]
        public string nicknamePlayer2 { get; set; }

        [DataMember]
        public int missComunicationsPoints { get; set; }

        [DataMember]
        public int interceptionsPoints { get; set; }

        [DataMember]
        public bool allreadySetGuesses { get; set; }

        [DataMember]
        public List<string>[] clues { get; set; } = new List<string>[4];

        [DataMember]
        public List<string> wordList { get; set; }

        public BlueTeam()
        {
            nicknamePlayer1 = "Player3";
            nicknamePlayer2 = "Player4";
            allreadySetGuesses = false;
        }
    }
}
