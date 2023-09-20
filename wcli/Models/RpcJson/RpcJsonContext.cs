using System.Collections.Generic;
using System.Text.Json.Serialization;
using WasabiCli.Models.WalletWasabi;

namespace WasabiCli.Models.RpcJson;

// error
[JsonSerializable(typeof(ErrorInfo))]
[JsonSerializable(typeof(RpcErrorResult))]
// getwalletinfo
[JsonSerializable(typeof(WalletInfo))]
[JsonSerializable(typeof(RpcGetWalletInfoResult))]
// getstatus
[JsonSerializable(typeof(PeerInfo))]
[JsonSerializable(typeof(List<PeerInfo>))]
[JsonSerializable(typeof(StatusInfo))]
[JsonSerializable(typeof(RpcGetStatusResult))]
// rpc
[JsonSerializable(typeof(RpcMethod))]
[JsonSerializable(typeof(Rpc))]
public partial class RpcJsonContext : JsonSerializerContext
{
}
