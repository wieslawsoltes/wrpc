using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace WasabiRpc.Models.App;

public class Batch
{
    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("jobs")]
    public List<Job>? Jobs { get; set; }
}
