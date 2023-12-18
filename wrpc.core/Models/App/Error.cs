using System.Text.Json.Serialization;

namespace WasabiRpc.Models.App;

public class Error
{
    [JsonPropertyName("message")]
    public string? Message { get; set; }
}
