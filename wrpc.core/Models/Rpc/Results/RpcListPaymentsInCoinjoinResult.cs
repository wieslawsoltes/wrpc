using System.Collections.Generic;
using System.Text.Json.Serialization;
using WasabiRpc.Models.Info;

namespace WasabiRpc.Models.Rpc.Results;

public class RpcListPaymentsInCoinjoinResult : Rpc
{
    [JsonPropertyName("result")]
    [JsonRequired]
    public List<PaymentInCoinjoinInfo>? Result { get; set; }
}
