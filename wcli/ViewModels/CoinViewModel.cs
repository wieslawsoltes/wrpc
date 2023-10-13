using CommunityToolkit.Mvvm.ComponentModel;
using WasabiCli.Models.Services;
using WasabiCli.Models.WalletWasabi;

namespace WasabiCli.ViewModels;

public partial class CoinViewModel : ViewModelBase
{
    [ObservableProperty] private bool _isSelected;

    public CoinViewModel(IRpcServiceViewModel rpcService, INavigationService navigationService, CoinInfo coinInfo)
    {
        RpcService = rpcService;
        NavigationService = navigationService;
        CoinInfo = coinInfo;
        IsSelected = false;
    }

    private IRpcServiceViewModel RpcService { get; }

    private INavigationService NavigationService { get; }

    public CoinInfo CoinInfo { get; }
}
