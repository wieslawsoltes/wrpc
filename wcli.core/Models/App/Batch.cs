using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace WasabiCli.Models.App;

public class Batch
{
    [JsonPropertyName("jobs")]
    public List<Job>? Jobs { get; set; } 
}
