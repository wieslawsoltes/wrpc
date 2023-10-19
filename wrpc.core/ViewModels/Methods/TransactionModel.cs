using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using WasabiRpc.Models.App;
using WasabiRpc.Models.Info;
using WasabiRpc.Models.Results;
using WasabiRpc.Models.Services;
using WasabiRpc.ViewModels.Factories;
using WasabiRpc.ViewModels.Info;

namespace WasabiRpc.ViewModels.Methods;

public partial class TransactionViewModel : RoutableViewModel
{
    [ObservableProperty] private string _walletName;
    [ObservableProperty] private bool _isSelected;

    public TransactionViewModel(IRpcServiceViewModel rpcService, INavigationService navigationService, string walletName, TransactionInfoViewModel transactionInfo)
        : base(rpcService, navigationService)
    {
        WalletName = walletName;
        TransactionInfo = transactionInfo;
        IsSelected = false;
    }

    public TransactionInfoViewModel TransactionInfo { get; }

    [RelayCommand]
    private async Task SpeedUpTransaction()
    {
        var speedUpTransactionViewModel = new SpeedUpTransactionViewModel(RpcService, NavigationService, WalletName)
        {
            TxId = TransactionInfo.Tx,
            // TODO: WalletPassword
            WalletPassword = "",
        };
        var job = speedUpTransactionViewModel.CreateJob();
        var result = await RpcService.Send<RpcSpeedUpTransactionResult>(job, NavigationService);
        if (result is RpcSpeedUpTransactionResult rpcSpeedUpTransactionResult)
        {
            NavigationService.NavigateTo(new BuildInfo { Tx = rpcSpeedUpTransactionResult.Result }.ToViewModel(RpcService, NavigationService));
        }
        else if (result is RpcErrorResult { Error: not null } rpcErrorResult)
        {
            NavigationService.NavigateTo(rpcErrorResult.Error.ToViewModel(RpcService, NavigationService));
        }
        else if (result is Error error)
        {
            NavigationService.NavigateTo(error.ToViewModel(RpcService, NavigationService));
        }
    }

    [RelayCommand]
    private async Task CancelTransaction()
    {
        var cancelTransactionViewModel = new CancelTransactionViewModel(RpcService, NavigationService, WalletName)
        {
            TxId = TransactionInfo.Tx,
            // TODO: WalletPassword
            WalletPassword = "",
        };
        var job = cancelTransactionViewModel.CreateJob();
        var result = await RpcService.Send<RpcCancelTransactionResult>(job, NavigationService);
        if (result is RpcCancelTransactionResult rpcCancelTransactionResult)
        {
            NavigationService.NavigateTo(new BuildInfo { Tx = rpcCancelTransactionResult.Result }.ToViewModel(RpcService, NavigationService));
        }
        else if (result is RpcErrorResult { Error: not null } rpcErrorResult)
        {
            NavigationService.NavigateTo(rpcErrorResult.Error.ToViewModel(RpcService, NavigationService));
        }
        else if (result is Error error)
        {
            NavigationService.NavigateTo(error.ToViewModel(RpcService, NavigationService));
        }
    }
}
