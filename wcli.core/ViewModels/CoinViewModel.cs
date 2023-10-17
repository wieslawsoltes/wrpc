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

    public IRpcServiceViewModel RpcService { get; }

    public INavigationService NavigationService { get; }

    public CoinInfo CoinInfo { get; }
}
