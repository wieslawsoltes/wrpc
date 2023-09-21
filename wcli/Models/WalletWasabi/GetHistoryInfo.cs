using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace WasabiCli.Models.WalletWasabi;

public class GetHistoryInfo
{
    [JsonPropertyName("transactions")]
    public List<TransactionInfo>? Transactions { get; set; }
}
