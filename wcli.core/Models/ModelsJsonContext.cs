using System.Collections.Generic;
using System.Text.Json.Serialization;
using WasabiCli.Models.App;
using WasabiCli.Models.Info;
using WasabiCli.Models.Params.Build;
using WasabiCli.Models.Params.Send;
using WasabiCli.Models.Params.Transactions;
using WasabiCli.Models.Results;

namespace WasabiCli.Models;

// app
[JsonSerializable(typeof(State))]
[JsonSerializable(typeof(List<string>))]
[JsonSerializable(typeof(Job))]
[JsonSerializable(typeof(Batch))]
[JsonSerializable(typeof(List<Job>))]
[JsonSerializable(typeof(List<Batch>))]
// rpc
[JsonSerializable(typeof(RpcMethod))]
[JsonSerializable(typeof(Rpc))]
[JsonSerializable(typeof(RpcResult))]
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
// recoverwallet
[JsonSerializable(typeof(RpcRecoverWalletResult))]
// loadwallet
[JsonSerializable(typeof(RpcLoadWalletResult))]
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
// speeduptransaction
[JsonSerializable(typeof(SpeedUpTransaction))]
[JsonSerializable(typeof(SpeedUpTransactionInfo))]
[JsonSerializable(typeof(RpcCancelTransactionResult))]
// canceltransaction
[JsonSerializable(typeof(CancelTransaction))]
[JsonSerializable(typeof(CancelTransactionInfo))]
[JsonSerializable(typeof(RpcSpeedUpTransactionResult))]
// build
[JsonSerializable(typeof(Build))]
[JsonSerializable(typeof(BuildInfo))]
[JsonSerializable(typeof(RpcBuildResult))]
// payincoinjoin
[JsonSerializable(typeof(PayInCoinjoinInfo))]
[JsonSerializable(typeof(RpcPayInCoinjoinResult))]
// broadcast
[JsonSerializable(typeof(BroadcastInfo))]
[JsonSerializable(typeof(RpcBroadcastResult))]
// getwalletinfo
[JsonSerializable(typeof(WalletInfo))]
[JsonSerializable(typeof(RpcGetWalletInfoResult))]
// gethistory
[JsonSerializable(typeof(TransactionInfo))]
[JsonSerializable(typeof(List<TransactionInfo>))]
[JsonSerializable(typeof(GetHistoryInfo))]
[JsonSerializable(typeof(RpcGetHistoryResult))]
// excludefromcoinjoin
[JsonSerializable(typeof(ExcludeFromCoinJoin))]
[JsonSerializable(typeof(RpcExcludeFromCoinJoinResult))]
// listkeys
[JsonSerializable(typeof(KeyInfo))]
[JsonSerializable(typeof(List<KeyInfo>))]
[JsonSerializable(typeof(ListKeysInfo))]
[JsonSerializable(typeof(RpcListKeysResult))]
// startcoinjoin
[JsonSerializable(typeof(RpcStartCoinJoinResult))]
// startcoinjoinsweep
[JsonSerializable(typeof(RpcStartCoinJoinSweepResult))]
// stopcoinjoin
[JsonSerializable(typeof(RpcStopCoinJoinResult))]
// getfeerates
[JsonSerializable(typeof(GetFeeRatesInfo))]
[JsonSerializable(typeof(Dictionary<int, int>))]
[JsonSerializable(typeof(RpcGetFeeRatesResult))]
// listwallets
[JsonSerializable(typeof(ListWalletsInfo))]
[JsonSerializable(typeof(RpcListWalletsResult))]
// stop, string
[JsonSerializable(typeof(string))]
// string[]
[JsonSerializable(typeof(string[]))]
public partial class ModelsJsonContext : JsonSerializerContext
{
}
