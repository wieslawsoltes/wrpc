using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace wcli.Models;

// getwalletinfo
[JsonSerializable(typeof(GetWalletInfoResult))]
[JsonSerializable(typeof(RpcGetWalletInfoResult))]
// getstatus
[JsonSerializable(typeof(PeerInfo))]
[JsonSerializable(typeof(List<PeerInfo>))]
[JsonSerializable(typeof(GetStatusResult))]
[JsonSerializable(typeof(RpcGetStatusResult))]
// rpc
[JsonSerializable(typeof(RpcMethod))]
[JsonSerializable(typeof(Rpc))]
public partial class RpcJsonContext : JsonSerializerContext
{
}
