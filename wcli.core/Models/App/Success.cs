using System.Text.Json.Serialization;

namespace WasabiCli.Models.App;

public class Success
{
    [JsonPropertyName("message")]
    public string? Message { get; set; }
}
