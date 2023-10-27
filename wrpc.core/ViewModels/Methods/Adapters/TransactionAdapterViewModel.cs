using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using WasabiRpc.Models.BatchMode;
using WasabiRpc.Models.Services;
using WasabiRpc.ViewModels.App;
using WasabiRpc.ViewModels.Info;

namespace WasabiRpc.ViewModels.Methods.Adapters;

public partial class TransactionAdapterViewModel : RoutableViewModel
{
    private readonly IBatchManager _batchManager;
    [ObservableProperty] private string? _walletName;
    [ObservableProperty] private string? _walletPassword;
    [ObservableProperty] private bool _isSelected;

    public TransactionAdapterViewModel(IRpcServiceViewModel rpcService, INavigationService navigationService, IBatchManager batchManager, string? walletName, TransactionInfoViewModel transactionInfo)
        : base(rpcService, navigationService)
    {
        _batchManager = batchManager;
        WalletName = walletName;
        // TODO: Set WalletPassword
        WalletPassword = null;
        TransactionInfo = transactionInfo;
        IsSelected = false;
    }

    public TransactionInfoViewModel TransactionInfo { get; }

    [RelayCommand]
    private async Task SpeedUpTransaction()
    {
        var speedUpTransactionViewModel = new SpeedUpTransactionViewModel(RpcService, NavigationService, _batchManager, WalletName)
        {
            TxId = TransactionInfo.Tx,
            WalletPassword = WalletPassword,
        };
        var job = speedUpTransactionViewModel.CreateJob();
        var routable = await speedUpTransactionViewModel.Execute(job);
        if (routable is BuildInfoViewModel buildInfoViewModel)
        {
            NavigationService.NavigateTo(buildInfoViewModel);
        }
        else if (routable is ErrorInfoViewModel errorInfoViewModel)
        {
            NavigationService.NavigateTo(errorInfoViewModel);
        }
        else if (routable is ErrorViewModel errorViewModel)
        {
            NavigationService.NavigateTo(errorViewModel);
        }
    }

    [RelayCommand]
    private async Task CancelTransaction()
    {
        var cancelTransactionViewModel = new CancelTransactionViewModel(RpcService, NavigationService, _batchManager, WalletName)
        {
            TxId = TransactionInfo.Tx,
            WalletPassword = WalletPassword,
        };
        var job = cancelTransactionViewModel.CreateJob();
        var routable = await cancelTransactionViewModel.Execute(job);
        if (routable is BuildInfoViewModel buildInfoViewModel)
        {
            NavigationService.NavigateTo(buildInfoViewModel);
        }
        else if (routable is ErrorInfoViewModel errorInfoViewModel)
        {
            NavigationService.NavigateTo(errorInfoViewModel);
        }
        else if (routable is ErrorViewModel errorViewModel)
        {
            NavigationService.NavigateTo(errorViewModel);
        }
    }
}
