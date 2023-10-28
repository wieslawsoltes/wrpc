using System.Text.Json.Serialization;
using WasabiRpc.Models.Rpc.Methods;

namespace WasabiRpc.Models.Rpc;

[JsonDerivedType(typeof(BroadcastRpcMethod), "broadcast")]
[JsonDerivedType(typeof(BuildRpcMethod), "build")]
[JsonDerivedType(typeof(CancelTransactionRpcMethod), "canceltransaction")]
[JsonDerivedType(typeof(CreateWalletRpcMethod), "createwallet")]
[JsonDerivedType(typeof(ExcludeFromCoinJoinRpcMethod), "excludefromcoinjoin")]
[JsonDerivedType(typeof(GetFeeRatesRpcMethod), "getfeerates")]
[JsonDerivedType(typeof(GetHistoryRpcMethod), "gethistory")]
[JsonDerivedType(typeof(GetNewAddressRpcMethod), "getnewaddress")]
[JsonDerivedType(typeof(GetStatusRpcMethod), "getstatus")]
[JsonDerivedType(typeof(GetWalletInfoRpcMethod), "getwalletinfo")]
[JsonDerivedType(typeof(ListCoinsRpcMethod), "listcoins")]
[JsonDerivedType(typeof(ListKeysRpcMethod), "listkeys")]
[JsonDerivedType(typeof(ListUnspentCoinsRpcMethod), "listunspentcoins")]
[JsonDerivedType(typeof(ListWalletsRpcMethod), "listwallets")]
[JsonDerivedType(typeof(LoadWalletRpcMethod), "loadwallet")]
[JsonDerivedType(typeof(PayInCoinjoinRpcMethod), "payincoinjoin")]
[JsonDerivedType(typeof(RecoverWalletRpcMethod), "recoverwallet")]
[JsonDerivedType(typeof(SendRpcMethod), "send")]
[JsonDerivedType(typeof(SpeedUpTransactionRpcMethod), "speeduptransaction")]
[JsonDerivedType(typeof(StartCoinJoinSweepRpcMethod), "startcoinjoinsweep")]
[JsonDerivedType(typeof(StartCoinJoinRpcMethod), "startcoinjoin")]
[JsonDerivedType(typeof(StopCoinJoinRpcMethod), "stopcoinjoin")]
[JsonDerivedType(typeof(StopRpcMethod), "stop")]
public abstract class RpcMethod : Rpc
{
    [JsonPropertyName("method")]
    [JsonRequired]
    public string? Method { get; set; }
}
