using Newtonsoft.Json;
using System.Reflection.PortableExecutable;

namespace HardLaba4
{
    internal class Program
    {
        public class Scheme
        {
            [JsonProperty("name")]
            public string Name { get; set; }

            [JsonProperty("columns")]
            public List<SchemeColumn> Columns { get; set; }
        }
        
        public class SchemeColumn
        {
            [JsonProperty("name")]
            public string Name { get; set; }

            [JsonProperty("type")]
            public string Type { get; set; }
        }

        static void Main(string[] args)
        {
            Scheme f = readJson("FootballMatch.scheme.json");

            Console.OutputEncoding = System.Text.Encoding.UTF8;

            try
            {
                string[] allLinesTable = File.ReadAllLines("FootballMatch.csv");
                for ( int i=0; i<allLinesTable.Length;i++)
                {
                    if (allLinesTable[i].Length > 6)
                    {
                        throw new ArgumentException($"В файле Book.csv в строке {i + 1} столбцов больше чем 6");
                    }

                    if (f.Columns[i].Type == "int")
                    {

                    }
                }
            }
            catch (ArgumentException ex)
            {
                Console.Clear();
                Console.Write("Ошибка:");
                Console.WriteLine(ex.Message);
            }
        }
        private static Scheme readJson(string path)
        {
            return JsonConvert.DeserializeObject<Scheme>(File.ReadAllText(path));
        }

    }
}