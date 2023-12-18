using System.Text.Json.Serialization;

namespace WasabiRpc.Models.Info;

public class SpeedUpTransactionInfo
{
    [JsonPropertyName("tx")]
    public string? Tx { get; set; }
}
