using Newtonsoft.Json;

namespace HardLaba4
{
    public class Scheme
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("columns")]
        public List<SchemeColumn> Columns { get; set; }
        
        public static Scheme ReadJson(string path)
        {
            return JsonConvert.DeserializeObject<Scheme>(File.ReadAllText(path));
        }
    }  
        
}