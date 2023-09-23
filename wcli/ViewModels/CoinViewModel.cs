using CommunityToolkit.Mvvm.ComponentModel;
using WasabiCli.Models.Navigation;
using WasabiCli.Models.WalletWasabi;

namespace WasabiCli.ViewModels;

public partial class CoinViewModel : ViewModelBase
{
    [ObservableProperty] private bool _isSelected;

    public CoinViewModel(RpcServiceViewModel rpcService, INavigationService navigationService, CoinInfo coinInfo)
    {
        RpcService = rpcService;
        NavigationService = navigationService;
        CoinInfo = coinInfo;
        IsSelected = false;
    }

    public RpcServiceViewModel RpcService { get; }

    private INavigationService NavigationService { get; }

    public CoinInfo CoinInfo { get; }
}
