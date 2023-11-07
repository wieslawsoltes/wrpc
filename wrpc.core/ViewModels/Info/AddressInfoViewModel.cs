using CommunityToolkit.Mvvm.ComponentModel;
using WasabiRpc.Models.Services;

namespace WasabiRpc.ViewModels.Info;

public partial class AddressInfoViewModel : RoutableViewModel
{
    [ObservableProperty] 
    private string? _address;

    [ObservableProperty] 
    private string? _keyPath;

    [ObservableProperty] 
    private string? _label;

    [ObservableProperty] 
    private string? _publicKey;

    [ObservableProperty] 
    private string? _scriptPubKey;

    public AddressInfoViewModel(
        IRpcServiceViewModel rpcService, 
        INavigationService navigationService,
        INavigationService detailsNavigationService)
        : base(rpcService, navigationService, detailsNavigationService)
    {
    }
}
