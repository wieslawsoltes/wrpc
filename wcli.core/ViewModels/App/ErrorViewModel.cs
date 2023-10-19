using CommunityToolkit.Mvvm.ComponentModel;
using WasabiCli.Models.Services;

namespace WasabiCli.ViewModels.App;

public partial class ErrorViewModel : RoutableViewModel
{
    [ObservableProperty] 
    private string? _message;

    public ErrorViewModel(IRpcServiceViewModel rpcService, INavigationService navigationService)
        : base(rpcService, navigationService)
    {
    }
}