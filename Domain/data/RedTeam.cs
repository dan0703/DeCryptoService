using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Domain.data
{
    [DataContract]
    public class RedTeam
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
        public bool allreadySetClues {  get; set; }

        [DataMember]
        public List<string>[] clues { get; set; } = new List<string>[4];

        [DataMember]
        public List<string> wordList { get; set; }

        public RedTeam()
        {
            nicknamePlayer1 = "Player1";
            nicknamePlayer2 = "Player2";
            allreadySetGuesses = false;
            allreadySetClues = false;
            interceptionsPoints = 0;
            missComunicationsPoints = 0;
        }
    }
}
