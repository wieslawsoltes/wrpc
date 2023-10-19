using System.Text.Json.Serialization;

namespace WasabiCli.Models.Params.Transactions;

public class SpeedUpTransaction
{
    [JsonPropertyName("txId")]
    public string? TxId { get; set; }

    [JsonPropertyName("password")]
    public string? Password { get; set; }
}
