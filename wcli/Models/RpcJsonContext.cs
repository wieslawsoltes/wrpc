using System.Collections.Generic;
using System.Text.Json.Serialization;
using WasabiCli.Models.RpcJson;
using WasabiCli.Models.WalletWasabi;
using WasabiCli.Models.WalletWasabi.Send;

namespace WasabiCli.Models;

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
// send
[JsonSerializable(typeof(Coin))]
[JsonSerializable(typeof(Payment))]
[JsonSerializable(typeof(Send))]
[JsonSerializable(typeof(SendInfo))]
[JsonSerializable(typeof(RpcSendResult))]
// getwalletinfo
[JsonSerializable(typeof(WalletInfo))]
[JsonSerializable(typeof(RpcGetWalletInfoResult))]
// getwalletinfo
[JsonSerializable(typeof(TransactionInfo))]
[JsonSerializable(typeof(List<TransactionInfo>))]
[JsonSerializable(typeof(GetHistoryInfo))]
[JsonSerializable(typeof(RpcGetHistoryResult))]
// listkeys
[JsonSerializable(typeof(KeyInfo))]
[JsonSerializable(typeof(List<KeyInfo>))]
[JsonSerializable(typeof(ListKeysInfo))]
[JsonSerializable(typeof(RpcListKeysResult))]
// startcoinjoin
[JsonSerializable(typeof(RpcStartCoinJoinResult))]
// stopcoinjoin
[JsonSerializable(typeof(RpcStopCoinJoinResult))]
// stop
[JsonSerializable(typeof(RpcStopResult))]
// string
[JsonSerializable(typeof(string))]
// string[]
[JsonSerializable(typeof(string[]))]
public partial class RpcJsonContext : JsonSerializerContext
{
}
