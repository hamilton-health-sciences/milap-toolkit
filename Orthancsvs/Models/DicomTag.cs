using System.Text.Json.Serialization;

namespace orthancsv.Models
{
    public class DicomTag
    {
        [JsonPropertyName("AccessionNumber")]
        public string AccessionNum { get; set; }
    }
}
