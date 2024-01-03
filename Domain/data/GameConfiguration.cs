using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Domain.data
{
    [DataContract]
    public class GameConfiguration
    {
        [DataMember]
        public string HostNickname { get; set; }
        [DataMember]
        public int roundNumber {  get; set; }
        [DataMember]
        public int numberOfRounds { get; set; }
        [DataMember]
        public RedTeam redTeam{ get; set; }
        [DataMember]
        public BlueTeam blueTeam { get; set; }       

        public GameConfiguration()
        {         
            roundNumber = 1;
        }
    }
    public static class WordLoader
    {
        public static List<string> LoadWordsFromFile(string relativePath)
        {
            List<string> words = new List<string>();
            try
            {
                string fullPath = Path.Combine(Directory.GetCurrentDirectory(), relativePath);
                words.AddRange(File.ReadAllLines(fullPath));               
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading words: {ex.Message}");
            }
            return words;
        }

        public static List<string> GetRandomWordsFromFile(string relativePath, Random random)
        {
            List<string> allWords = LoadWordsFromFile(relativePath);
            List<string> randomWords = new List<string>();
            try
            {
                if (allWords.Count >= 4)
                {
                    for (int i = 0; i < 4; i++)
                    {
                        int randomIndex = random.Next(allWords.Count);
                        string randomWord = allWords[randomIndex];
                        randomWords.Add(randomWord);
                        allWords.RemoveAt(randomIndex);
                    }
                }
                else
                {
                    Console.WriteLine("No hay suficientes palabras en la lista para seleccionar 4 aleatorias.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error obteniendo palabras aleatorias: {ex.Message}");
            }

            return randomWords;
        }
    }
}
