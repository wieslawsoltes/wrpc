using CommunityToolkit.Mvvm.ComponentModel;
using WasabiCli.Models.Navigation;
using WasabiCli.Models.WalletWasabi;

namespace WasabiCli.ViewModels;

public partial class CoinViewModel : ViewModelBase
{
    private readonly CoinInfo _coinInfo;

    [ObservableProperty] private bool _isSelected;

    public CoinViewModel(RpcServiceViewModel rpcService, INavigationService navigationService, CoinInfo coinInfo)
    {
        _coinInfo = coinInfo;
        RpcService = rpcService;
        NavigationService = navigationService;
        IsSelected = false;
    }

    public RpcServiceViewModel RpcService { get; }

    private INavigationService NavigationService { get; }

    public string? TxId => _coinInfo.TxId;

    public int Index => _coinInfo.Index;

    public long Amount => _coinInfo.Amount;
}
