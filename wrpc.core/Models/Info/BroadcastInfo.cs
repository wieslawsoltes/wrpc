using System.Text.Json.Serialization;

namespace WasabiRpc.Models.Info;

public class BroadcastInfo
{
    [JsonPropertyName("txid")]
    public string? TxId { get; set; }
}
