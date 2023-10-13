using System.Text.Json.Serialization;

namespace WasabiCli.Models.WalletWasabi;

public class SpeedUpTransactionInfo
{
    [JsonPropertyName("tx")]
    public string? Tx { get; set; }
}
