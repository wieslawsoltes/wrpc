using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace WasabiRpc.Models.App;

public class Batch
{
    [JsonPropertyName("jobs")]
    public List<Job>? Jobs { get; set; } 
}
