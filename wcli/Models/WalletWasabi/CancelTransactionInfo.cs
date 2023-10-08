using System.Text.Json.Serialization;

namespace WasabiCli.Models.WalletWasabi;

public class CancelTransactionInfo
{
    [JsonPropertyName("tx")]
    public string? Tx { get; set; }
}
