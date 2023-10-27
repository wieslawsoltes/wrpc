using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using WasabiRpc.Models.BatchMode;
using WasabiRpc.Models.Services;
using WasabiRpc.ViewModels.App;
using WasabiRpc.ViewModels.Info;

namespace WasabiRpc.ViewModels.Methods.Adapters;

public partial class CoinAdapterViewModel : RoutableViewModel
{
    private readonly IBatchManager _batchManager;
    [ObservableProperty] private string? _walletName;
    [ObservableProperty] private bool _isSelected;

    public CoinAdapterViewModel(IRpcServiceViewModel rpcService, INavigationService navigationService, IBatchManager batchManager, string? walletName, CoinInfoViewModel coinInfo)
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
        var routable = await excludeFromCoinJoinViewModel.Execute(job);
        if (routable is SuccessViewModel successViewModel)
        {
            NavigationService.NavigateTo(successViewModel);
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
