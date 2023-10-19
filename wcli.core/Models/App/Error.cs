using System.Text.Json.Serialization;

namespace WasabiCli.Models.App;

public class Error
{
    [JsonPropertyName("message")]
    public string? Message { get; set; }
}
