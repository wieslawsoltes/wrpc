using CommunityToolkit.Mvvm.ComponentModel;
using WasabiCli.Models.Services;

namespace WasabiCli.ViewModels.Info;

public partial class AccountInfoViewModel : RoutableViewModel
{
    [ObservableProperty] 
    private string? _name;

    [ObservableProperty] 
    private string? _publicKey;

    [ObservableProperty] 
    private string? _keyPath;

    public AccountInfoViewModel(IRpcServiceViewModel rpcService, INavigationService navigationService)
        : base(rpcService, navigationService)
    {
    }
}
