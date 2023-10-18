using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

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
