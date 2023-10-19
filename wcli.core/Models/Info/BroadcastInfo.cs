using System.Text.Json.Serialization;

namespace WasabiCli.Models.Info;

public class BroadcastInfo
{
    [JsonPropertyName("txid")]
    public string? TxId { get; set; }
}
