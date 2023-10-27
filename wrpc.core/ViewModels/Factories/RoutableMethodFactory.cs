using WasabiRpc.Models.BatchMode;
using WasabiRpc.Models.Services;
using WasabiRpc.ViewModels.Methods;

namespace WasabiRpc.ViewModels.Factories;

public static class RoutableMethodFactory
{
    public static RoutableMethodViewModel? CreateRoutableMethod(
        string methodName, 
        IRpcServiceViewModel rpcService, 
        INavigationService navigationService,
        IBatchManager batchManager,
        string? walletName = null)
    {
        return methodName switch
        {
            "broadcast" => new BroadcastViewModel(rpcService, navigationService, batchManager),
            "build" => new BuildViewModel(rpcService, navigationService, batchManager, walletName),
            "canceltransaction" => new CancelTransactionViewModel(rpcService, navigationService, batchManager, walletName),
            "createwallet" => new CreateWalletViewModel(rpcService, navigationService, batchManager),
            "excludefromcoinjoin" => new ExcludeFromCoinJoinViewModel(rpcService, navigationService, batchManager, walletName),
            "getfeerates" => new GetFeeRatesViewModel(rpcService, navigationService, batchManager),
            "gethistory" => new GetHistoryViewModel(rpcService, navigationService, batchManager, walletName),
            "getnewaddress" => new GetNewAddressViewModel(rpcService, navigationService, batchManager, walletName),
            "getstatus" => new GetStatusViewModel(rpcService, navigationService, batchManager),
            "getwalletinfo" => new GetWalletInfoViewModel(rpcService, navigationService, batchManager, walletName),
            "listcoins" => new ListCoinsViewModel(rpcService, navigationService, batchManager, walletName),
            "listkeys" => new ListKeysViewModel(rpcService, navigationService, batchManager, walletName),
            "listunspentcoins" => new ListUnspentCoinsViewModel(rpcService, navigationService, batchManager, walletName),
            "listwallets" => new ListWalletsViewModel(rpcService, navigationService, batchManager),
            "loadwallet" => new LoadWalletViewModel(rpcService, navigationService, batchManager, walletName),
            "payincoinjoin" => new PayInCoinjoinViewModel(rpcService, navigationService, batchManager, walletName),
            "recoverwallet" => new RecoverWalletViewModel(rpcService, navigationService, batchManager),
            "send" => new SendViewModel(rpcService, navigationService, batchManager, walletName),
            "speeduptransaction" => new SpeedUpTransactionViewModel(rpcService, navigationService, batchManager, walletName),
            "startcoinjoinsweep" => new StartCoinJoinSweepViewModel(rpcService, navigationService, batchManager, walletName),
            "startcoinjoin" => new StartCoinJoinViewModel(rpcService, navigationService, batchManager, walletName),
            "stopcoinjoin" => new StopCoinJoinViewModel(rpcService, navigationService, batchManager, walletName),
            "stop" => new StopViewModel(rpcService, navigationService, batchManager),
            _ => null
        };
    }
}
