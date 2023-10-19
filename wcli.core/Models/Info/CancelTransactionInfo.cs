using System.Text.Json.Serialization;

namespace WasabiCli.Models.Info;

public class CancelTransactionInfo
{
    [JsonPropertyName("tx")]
    public string? Tx { get; set; }
}
