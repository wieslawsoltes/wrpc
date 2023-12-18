using System.Text.Json.Serialization;

namespace WasabiRpc.Models.Info;

public class CancelTransactionInfo
{
    [JsonPropertyName("tx")]
    public string? Tx { get; set; }
}
