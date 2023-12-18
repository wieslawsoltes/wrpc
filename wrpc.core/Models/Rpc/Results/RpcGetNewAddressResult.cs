using System.Text.Json.Serialization;
using WasabiRpc.Models.Info;

namespace WasabiRpc.Models.Rpc.Results;

public class RpcGetNewAddressResult : Rpc
{
    [JsonPropertyName("result")]
    [JsonRequired]
    public AddressInfo? Result { get; set; }
}
