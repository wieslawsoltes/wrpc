using System.Text.Json.Serialization;

namespace WasabiCli.Models.App;

public class Json
{
    [JsonPropertyName("content")]
    public string? Content { get; set; }
}
