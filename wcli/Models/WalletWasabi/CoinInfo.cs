using System.Text.Json.Serialization;

namespace WasabiCli.Models.WalletWasabi;

public class CoinInfo
{
    [JsonPropertyName("txid")]
    public string? TxId { get; set; }

    [JsonPropertyName("index")]
    public int Index { get; set; }

    [JsonPropertyName("amount")]
    public int Amount { get; set; }

    [JsonPropertyName("anonymityScore")]
    public decimal AnonymityScore { get; set; }

    [JsonPropertyName("confirmed")]
    public bool Confirmed { get; set; }

    [JsonPropertyName("confirmations")]
    public int Confirmations { get; set; }

    [JsonPropertyName("keyPath")]
    public string? KeyPath { get; set; }

    [JsonPropertyName("address")]
    public string? Address { get; set; }

    [JsonPropertyName("spentBy")]
    public string? SpentBy { get; set; }
}
