using System.Collections.Generic;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using WasabiRpc.Models.Services;

namespace WasabiRpc.ViewModels.Info;

public partial class WalletInfoViewModel : RoutableViewModel
{
    [ObservableProperty] 
    private string? _walletName;

    [ObservableProperty] 
    private string? _walletFile;

    [ObservableProperty] 
    private string? _state;

    [ObservableProperty] 
    private string? _masterKeyFingerprint;

    [ObservableProperty] 
    private List<AccountInfoViewModel>? _accounts;

    [ObservableProperty] 
    private long _balance;

    [ObservableProperty] 
    private int _anonScoreTarget;

    [ObservableProperty] 
    private bool _isWatchOnly;

    [ObservableProperty] 
    private bool _isHardwareWallet;

    [ObservableProperty] 
    private bool _isAutoCoinjoin;

    [ObservableProperty] 
    private bool _isRedCoinIsolation;

    [ObservableProperty] 
    private string? _coinjoinStatus;

    public WalletInfoViewModel(IRpcServiceViewModel rpcService, INavigationService navigationService, ICommand refreshCommand)
        : base(rpcService, navigationService)
    {
        RefreshCommand = refreshCommand;
    }

    public ICommand RefreshCommand { get; }
}
