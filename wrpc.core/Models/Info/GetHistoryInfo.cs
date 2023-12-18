using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace WasabiRpc.Models.Info;

public class GetHistoryInfo
{
    [JsonPropertyName("transactions")]
    public List<TransactionInfo>? Transactions { get; set; }
}
