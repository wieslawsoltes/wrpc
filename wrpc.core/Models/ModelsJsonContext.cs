using System.Collections.Generic;
using System.Text.Json.Serialization;
using WasabiRpc.Models.App;
using WasabiRpc.Models.Info;
using WasabiRpc.Models.Params.Build;
using WasabiRpc.Models.Params.Send;
using WasabiRpc.Models.Params.Transactions;
using WasabiRpc.Models.Rpc;
using WasabiRpc.Models.Rpc.Methods;
using WasabiRpc.Models.Rpc.Results;

namespace WasabiRpc.Models;

// app
[JsonSerializable(typeof(State))]
[JsonSerializable(typeof(List<string>))]
[JsonSerializable(typeof(Job))]
[JsonSerializable(typeof(Batch))]
[JsonSerializable(typeof(List<Job>))]
[JsonSerializable(typeof(List<Batch>))]
// rpc
[JsonSerializable(typeof(RpcMethod))]
[JsonSerializable(typeof(List<RpcMethod>))]
[JsonSerializable(typeof(RpcMethod[]))]
// methods
[JsonSerializable(typeof(BroadcastRpcMethod))]
[JsonSerializable(typeof(BuildRpcMethod))]
[JsonSerializable(typeof(CancelTransactionRpcMethod))]
[JsonSerializable(typeof(CreateWalletRpcMethod))]
[JsonSerializable(typeof(ExcludeFromCoinJoinRpcMethod))]
[JsonSerializable(typeof(GetFeeRatesRpcMethod))]
[JsonSerializable(typeof(GetHistoryRpcMethod))]
[JsonSerializable(typeof(GetNewAddressRpcMethod))]
[JsonSerializable(typeof(GetStatusRpcMethod))]
[JsonSerializable(typeof(GetWalletInfoRpcMethod))]
[JsonSerializable(typeof(ListCoinsRpcMethod))]
[JsonSerializable(typeof(ListKeysRpcMethod))]
[JsonSerializable(typeof(ListUnspentCoinsRpcMethod))]
[JsonSerializable(typeof(ListWalletsRpcMethod))]
[JsonSerializable(typeof(LoadWalletRpcMethod))]
[JsonSerializable(typeof(PayInCoinjoinRpcMethod))]
[JsonSerializable(typeof(RecoverWalletRpcMethod))]
[JsonSerializable(typeof(SendRpcMethod))]
[JsonSerializable(typeof(SpeedUpTransactionRpcMethod))]
[JsonSerializable(typeof(StartCoinJoinSweepRpcMethod))]
[JsonSerializable(typeof(StartCoinJoinRpcMethod))]
[JsonSerializable(typeof(StopCoinJoinRpcMethod))]
[JsonSerializable(typeof(StopRpcMethod))]
// rpc
[JsonSerializable(typeof(Rpc.Rpc))]
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
// listpaymentsincoinjoin
[JsonSerializable(typeof(PaymentInCoinjoinStateInfo))]
[JsonSerializable(typeof(List<PaymentInCoinjoinStateInfo>))]
[JsonSerializable(typeof(PaymentInCoinjoinInfo))]
[JsonSerializable(typeof(List<PaymentInCoinjoinInfo>))]
[JsonSerializable(typeof(ListPaymentsInCoinjoinInfo))]
[JsonSerializable(typeof(RpcListPaymentsInCoinjoinResult))]
// cancelpaymentincoinjoin
[JsonSerializable(typeof(RpcCancelPaymentInCoinjoinResult))]
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
