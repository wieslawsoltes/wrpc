using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using WasabiRpc.Models.App;
using WasabiRpc.Models.BatchMode;
using WasabiRpc.Models.Results;
using WasabiRpc.Models.Services;
using WasabiRpc.ViewModels.Factories;
using WasabiRpc.ViewModels.Info;

namespace WasabiRpc.ViewModels.Methods.Adapters;

public partial class CoinAdapterViewModel : RoutableViewModel
{
    private readonly IBatchManager _batchManager;
    [ObservableProperty] private string _walletName;
    [ObservableProperty] private bool _isSelected;

    public CoinAdapterViewModel(IRpcServiceViewModel rpcService, INavigationService navigationService, IBatchManager batchManager, string walletName, CoinInfoViewModel coinInfo)
        : base(rpcService, navigationService)
    {
        _batchManager = batchManager;
        WalletName = walletName;
        CoinInfo = coinInfo;
        IsSelected = false;
    }

    public CoinInfoViewModel CoinInfo { get; }

    [RelayCommand]
    private async Task ExcludeFromCoinJoin()
    {
        await Exclude(true);
    }

    [RelayCommand]
    private async Task RemoveExclusionFromCoinJoin()
    {
        await Exclude(false);
    }

    private async Task Exclude(bool exclude)
    {
        var excludeFromCoinJoinViewModel = new ExcludeFromCoinJoinViewModel(RpcService, NavigationService, _batchManager, WalletName)
        {
            TransactionId = CoinInfo.TxId,
            N = 0,
            Exclude = exclude
        };

        var job = excludeFromCoinJoinViewModel.CreateJob();

        await Execute(job, exclude);
    }

    public async Task Execute(Job job, bool exclude)
    {
        var result = await RpcService.Send<RpcExcludeFromCoinJoinResult>(job.RpcMethod, job.RpcServerUri, NavigationService);
        if (result is RpcExcludeFromCoinJoinResult)
        {
            var successViewModel = new Success
            {
                Message = $"{(exclude ? "Excluded" : "Removed the exclusion")} from coinjoin"
            }.ToViewModel(RpcService, NavigationService);
            NavigationService.NavigateTo(successViewModel);
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
