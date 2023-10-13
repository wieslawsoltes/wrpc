using System.Text.Json.Serialization;

namespace WasabiCli.Models.WalletWasabi;

public class ErrorInfo
{
    [JsonPropertyName("code")]
    public int Code { get; set; }

    [JsonPropertyName("message")]
    public string? Message { get; set; }
}
