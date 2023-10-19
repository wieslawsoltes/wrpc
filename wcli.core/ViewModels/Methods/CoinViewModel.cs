using CommunityToolkit.Mvvm.ComponentModel;
using WasabiCli.Models.Services;
using WasabiCli.ViewModels.Info;

namespace WasabiCli.ViewModels;

public partial class CoinViewModel : ViewModelBase
{
    [ObservableProperty] private bool _isSelected;

    public CoinViewModel(IRpcServiceViewModel rpcService, INavigationService navigationService, CoinInfoViewModel coinInfo)
    {
        RpcService = rpcService;
        NavigationService = navigationService;
        CoinInfo = coinInfo;
        IsSelected = false;
    }

    public IRpcServiceViewModel RpcService { get; }

    public INavigationService NavigationService { get; }

    public CoinInfoViewModel CoinInfo { get; }
}
