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
        INavigationService detailsNavigationService,
        IBatchManager batchManager,
        string? walletName = null)
    {
        return methodName switch
        {
            "broadcast" => new BroadcastViewModel(rpcService, navigationService, detailsNavigationService, batchManager),
            "build" => new BuildViewModel(rpcService, navigationService, detailsNavigationService, batchManager, walletName),
            "canceltransaction" => new CancelTransactionViewModel(rpcService, navigationService, detailsNavigationService, batchManager, walletName),
            "createwallet" => new CreateWalletViewModel(rpcService, navigationService, detailsNavigationService, batchManager),
            "excludefromcoinjoin" => new ExcludeFromCoinJoinViewModel(rpcService, navigationService, detailsNavigationService, batchManager, walletName),
            "getfeerates" => new GetFeeRatesViewModel(rpcService, navigationService, detailsNavigationService, batchManager),
            "gethistory" => new GetHistoryViewModel(rpcService, navigationService, detailsNavigationService, batchManager, walletName),
            "getnewaddress" => new GetNewAddressViewModel(rpcService, navigationService, detailsNavigationService, batchManager, walletName),
            "getstatus" => new GetStatusViewModel(rpcService, navigationService, detailsNavigationService, batchManager),
            "getwalletinfo" => new GetWalletInfoViewModel(rpcService, navigationService, detailsNavigationService, batchManager, walletName),
            "listcoins" => new ListCoinsViewModel(rpcService, navigationService, detailsNavigationService, batchManager, walletName),
            "listkeys" => new ListKeysViewModel(rpcService, navigationService, detailsNavigationService, batchManager, walletName),
            "listunspentcoins" => new ListUnspentCoinsViewModel(rpcService, navigationService, detailsNavigationService, batchManager, walletName),
            "listwallets" => new ListWalletsViewModel(rpcService, navigationService, detailsNavigationService, batchManager),
            "loadwallet" => new LoadWalletViewModel(rpcService, navigationService, detailsNavigationService, batchManager, walletName),
            "payincoinjoin" => new PayInCoinjoinViewModel(rpcService, navigationService, detailsNavigationService, batchManager, walletName),
            "listpaymentsincoinjoin" => new ListPaymentsInCoinjoinViewModel(rpcService, navigationService, detailsNavigationService, batchManager, walletName),
            "cancelpaymentincoinjoin" => new CancelPaymentInCoinjoinViewModel(rpcService, navigationService, detailsNavigationService, batchManager, walletName),
            "recoverwallet" => new RecoverWalletViewModel(rpcService, navigationService, detailsNavigationService, batchManager),
            "send" => new SendViewModel(rpcService, navigationService, detailsNavigationService, batchManager, walletName),
            "speeduptransaction" => new SpeedUpTransactionViewModel(rpcService, navigationService, detailsNavigationService, batchManager, walletName),
            "startcoinjoinsweep" => new StartCoinJoinSweepViewModel(rpcService, navigationService, detailsNavigationService, batchManager, walletName),
            "startcoinjoin" => new StartCoinJoinViewModel(rpcService, navigationService, detailsNavigationService, batchManager, walletName),
            "stopcoinjoin" => new StopCoinJoinViewModel(rpcService, navigationService, detailsNavigationService, batchManager, walletName),
            "stop" => new StopViewModel(rpcService, navigationService, detailsNavigationService, batchManager),
            _ => null
        };
    }
}
