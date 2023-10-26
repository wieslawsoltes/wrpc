using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using WasabiRpc.Models.App;
using WasabiRpc.Models.BatchMode;
using WasabiRpc.Models.Info;
using WasabiRpc.Models.Results;
using WasabiRpc.Models.Services;
using WasabiRpc.ViewModels.Factories;
using WasabiRpc.ViewModels.Info;

namespace WasabiRpc.ViewModels.Methods.Adapters;

public partial class TransactionAdapterViewModel : RoutableViewModel
{
    private readonly IBatchManager _batchManager;
    [ObservableProperty] private string _walletName;
    [ObservableProperty] private string? _walletPassword;
    [ObservableProperty] private bool _isSelected;

    public TransactionAdapterViewModel(IRpcServiceViewModel rpcService, INavigationService navigationService, IBatchManager batchManager, string walletName, TransactionInfoViewModel transactionInfo)
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

        await Execute(job);
    }

    public async Task Execute(Job job)
    {
        var result = await RpcService.Send<RpcSpeedUpTransactionResult>(job.RpcMethod, job.RpcServerUri, NavigationService);
        if (result is RpcSpeedUpTransactionResult rpcSpeedUpTransactionResult)
        {
            var buildInfoViewModel = new BuildInfo { Tx = rpcSpeedUpTransactionResult.Result }.ToViewModel(RpcService, NavigationService);
            NavigationService.NavigateTo(buildInfoViewModel);
        }
        else if (result is RpcErrorResult { Error: not null } rpcErrorResult)
        {
            var errorInfoViewModel = rpcErrorResult.Error.ToViewModel(RpcService, NavigationService);
            NavigationService.NavigateTo(errorInfoViewModel);
        }
        else if (result is Error error)
        {
            var errorViewModel = error.ToViewModel(RpcService, NavigationService);
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

        await ExecuteCancel(job);
    }

    private async Task ExecuteCancel(Job job)
    {
        var result = await RpcService.Send<RpcCancelTransactionResult>(job.RpcMethod, job.RpcServerUri, NavigationService);
        if (result is RpcCancelTransactionResult rpcCancelTransactionResult)
        {
            var buildInfoViewModel = new BuildInfo { Tx = rpcCancelTransactionResult.Result }.ToViewModel(RpcService, NavigationService);
            NavigationService.NavigateTo(buildInfoViewModel);
        }
        else if (result is RpcErrorResult { Error: not null } rpcErrorResult)
        {
            var errorInfoViewModel = rpcErrorResult.Error.ToViewModel(RpcService, NavigationService);
            NavigationService.NavigateTo(errorInfoViewModel);
        }
        else if (result is Error error)
        {
            var errorViewModel = error.ToViewModel(RpcService, NavigationService);
            NavigationService.NavigateTo(errorViewModel);
        }
    }
}
