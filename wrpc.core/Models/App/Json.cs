using System.Text.Json.Serialization;

namespace WasabiRpc.Models.App;

public class Json
{
    [JsonPropertyName("content")]
    public string? Content { get; set; }
}
