using System.Text.Json.Serialization;
using WasabiCli.Models.Info;

namespace WasabiCli.Models.Results;

public class RpcGetNewAddressResult : Rpc
{
    [JsonPropertyName("result")]
    [JsonRequired]
    public AddressInfo? Result { get; set; }
}
