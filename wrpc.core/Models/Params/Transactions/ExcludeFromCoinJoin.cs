using System.Text.Json.Serialization;

namespace WasabiRpc.Models.Params.Transactions;

public class ExcludeFromCoinJoin
{
    [JsonPropertyName("transactionId")]
    public string? TransactionId { get; set; }

    [JsonPropertyName("n")]
    public int N { get; set; }

    [JsonPropertyName("exclude")]
    public bool Exclude { get; set; }
}
