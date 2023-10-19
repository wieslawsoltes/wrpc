using CommunityToolkit.Mvvm.ComponentModel;
using WasabiRpc.Models.Services;

namespace WasabiRpc.ViewModels.Info;

public partial class CoinInfoViewModel : RoutableViewModel
{
    [ObservableProperty] 
    private string? _txId;

    [ObservableProperty] 
    private int _index;

    [ObservableProperty] 
    private long _amount;

    [ObservableProperty] 
    private decimal _anonymityScore;

    [ObservableProperty] 
    private bool _confirmed;

    [ObservableProperty] 
    private int _confirmations;

    [ObservableProperty] 
    private string? _keyPath;

    [ObservableProperty] 
    private string? _address;

    [ObservableProperty] 
    private string? _spentBy;

    public CoinInfoViewModel(IRpcServiceViewModel rpcService, INavigationService navigationService)
        : base(rpcService, navigationService)
    {
    }
}
