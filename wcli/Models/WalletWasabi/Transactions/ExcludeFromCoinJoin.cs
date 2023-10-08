using System.Text.Json.Serialization;

namespace WasabiCli.Models.WalletWasabi.Transactions;

public class ExcludeFromCoinJoin
{
    [JsonPropertyName("transactionId")]
    public string? TransactionId { get; set; }

    [JsonPropertyName("n")]
    public int N { get; set; }

    [JsonPropertyName("exclude")]
    public bool Exclude { get; set; }
}
