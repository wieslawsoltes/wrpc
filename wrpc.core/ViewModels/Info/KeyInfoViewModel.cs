using CommunityToolkit.Mvvm.ComponentModel;
using WasabiRpc.Models.Services;

namespace WasabiRpc.ViewModels.Info;

public partial class KeyInfoViewModel : RoutableViewModel
{
    [ObservableProperty] 
    private string? _fullKeyPath;

    [ObservableProperty] 
    private bool? _internal;

    [ObservableProperty] 
    private int? _keyState;

    [ObservableProperty] 
    private string? _label;

    [ObservableProperty] 
    private string? _scriptPubKey;

    [ObservableProperty] 
    private string? _pubKey;

    [ObservableProperty] 
    private string? _pubKeyHash;

    [ObservableProperty] 
    private string? _address;

    public KeyInfoViewModel(IRpcServiceViewModel rpcService, INavigationService navigationService)
        : base(rpcService, navigationService)
    {
    }
}
