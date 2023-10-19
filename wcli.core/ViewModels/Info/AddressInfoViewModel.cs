using CommunityToolkit.Mvvm.ComponentModel;
using WasabiCli.Models.Services;

namespace WasabiCli.ViewModels.Info;

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

    public AddressInfoViewModel(IRpcServiceViewModel rpcService, INavigationService navigationService)
        : base(rpcService, navigationService)
    {
    }
}
