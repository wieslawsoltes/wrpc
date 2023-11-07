using CommunityToolkit.Mvvm.ComponentModel;
using WasabiRpc.Models.Services;

namespace WasabiRpc.ViewModels.Info;

public partial class AccountInfoViewModel : RoutableViewModel
{
    [ObservableProperty] 
    private string? _name;

    [ObservableProperty] 
    private string? _publicKey;

    [ObservableProperty] 
    private string? _keyPath;

    public AccountInfoViewModel(
        IRpcServiceViewModel rpcService, 
        INavigationService navigationService,
        INavigationService detailsNavigationService)
        : base(rpcService, navigationService, detailsNavigationService)
    {
    }
}
