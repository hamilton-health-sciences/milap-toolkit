using System.Text.Json.Serialization;

namespace orthancsv.Models
{
    public class LevelQuery
    {
        [JsonPropertyName("Level")]
        public string Level { get; set; }
        [JsonPropertyName("Query")]
        public Query Query { get; set; }
    }
}
