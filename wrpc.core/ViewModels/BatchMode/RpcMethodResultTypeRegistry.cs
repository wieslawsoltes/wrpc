using System;
using System.Collections.Generic;
using WasabiRpc.Models.Rpc.Results;

namespace WasabiRpc.ViewModels.BatchMode;

public static class RpcMethodResultTypeRegistry
{
    public static Dictionary<string, Type> Results { get; }

    static RpcMethodResultTypeRegistry()
    {
        Results = new ()
        {
            ["broadcast"] = typeof(RpcBroadcastResult),
            ["build"] = typeof(RpcBuildResult),
            ["canceltransaction"] = typeof(RpcCancelTransactionResult),
            ["createwallet"] = typeof(RpcCreateWalletResult),
            ["excludefromcoinjoin"] = typeof(RpcExcludeFromCoinJoinResult),
            ["getfeerates"] = typeof(RpcGetFeeRatesResult),
            ["gethistory"] = typeof(RpcGetHistoryResult),
            ["getnewaddress"] = typeof(RpcGetNewAddressResult),
            ["getstatus"] = typeof(RpcGetStatusResult),
            ["getwalletinfo"] = typeof(RpcGetWalletInfoResult),
            ["listcoins"] = typeof(RpcListCoinsResult),
            ["listkeys"] = typeof(RpcListKeysResult),
            ["listunspentcoins"] = typeof(RpcListUnspentCoinsResult),
            ["listwallets"] = typeof(RpcListWalletsResult),
            ["loadwallet"] = typeof(RpcLoadWalletResult),
            ["payincoinjoin"] = typeof(RpcPayInCoinjoinResult),
            ["listpaymentsincoinjoin"] = typeof(RpcListPaymentsInCoinjoinResult),
            ["cancelpaymentincoinjoin"] = typeof(RpcCancelPaymentInCoinjoinResult),
            ["recoverwallet"] = typeof(RpcRecoverWalletResult),
            ["send"] = typeof(RpcSendResult),
            ["speeduptransaction"] = typeof(RpcSpeedUpTransactionResult),
            ["startcoinjoinsweep"] = typeof(RpcStartCoinJoinSweepResult),
            ["startcoinjoin"] = typeof(RpcStartCoinJoinResult),
            ["stopcoinjoin"] = typeof(RpcStopCoinJoinResult),
            ["stop"] = typeof(string),
        };
    }
}
