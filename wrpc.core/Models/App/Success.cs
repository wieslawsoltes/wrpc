using System.Text.Json.Serialization;

namespace WasabiRpc.Models.App;

public class Success
{
    [JsonPropertyName("message")]
    public string? Message { get; set; }
}
