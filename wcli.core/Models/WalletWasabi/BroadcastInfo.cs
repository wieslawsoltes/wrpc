using System.Text.Json.Serialization;

namespace WasabiCli.Models.WalletWasabi;

public class BroadcastInfo
{
    [JsonPropertyName("txid")]
    public string? TxId { get; set; }
}
