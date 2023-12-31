using System.Text.Json.Serialization;

namespace WasabiRpc.Models.Info;

public class ErrorInfo
{
    [JsonPropertyName("code")]
    public int Code { get; set; }

    [JsonPropertyName("message")]
    public string? Message { get; set; }
}
