using System.Collections.Generic;
using System.Text.Json.Serialization;
using WasabiCli.Models.WalletWasabi;

namespace WasabiCli.Models.RpcJson;

// rpc
[JsonSerializable(typeof(RpcMethod))]
[JsonSerializable(typeof(Rpc))]
// error
[JsonSerializable(typeof(ErrorInfo))]
[JsonSerializable(typeof(RpcErrorResult))]
// getstatus
[JsonSerializable(typeof(PeerInfo))]
[JsonSerializable(typeof(List<PeerInfo>))]
[JsonSerializable(typeof(StatusInfo))]
[JsonSerializable(typeof(RpcGetStatusResult))]
// createwallet
[JsonSerializable(typeof(CreateWalletInfo))]
[JsonSerializable(typeof(RpcCreateWalletResult))]
// listcoins
[JsonSerializable(typeof(CoinInfo))]
[JsonSerializable(typeof(List<CoinInfo>))]
[JsonSerializable(typeof(ListCoinsInfo))]
[JsonSerializable(typeof(RpcListCoinsResult))]
// listunspentcoins
[JsonSerializable(typeof(ListUnspentCoinsInfo))]
[JsonSerializable(typeof(RpcListUnspentCoinsResult))]
// getnewaddress
[JsonSerializable(typeof(AddressInfo))]
[JsonSerializable(typeof(List<AddressInfo>))]
[JsonSerializable(typeof(RpcGetNewAddressResult))]
// getwalletinfo
[JsonSerializable(typeof(WalletInfo))]
[JsonSerializable(typeof(RpcGetWalletInfoResult))]
// getwalletinfo
[JsonSerializable(typeof(TransactionInfo))]
[JsonSerializable(typeof(List<TransactionInfo>))]
[JsonSerializable(typeof(GetHistoryInfo))]
[JsonSerializable(typeof(RpcGetHistoryResult))]
public partial class RpcJsonContext : JsonSerializerContext
{
}
