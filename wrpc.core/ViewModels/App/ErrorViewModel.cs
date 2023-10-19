using CommunityToolkit.Mvvm.ComponentModel;
using WasabiRpc.Models.Services;

namespace WasabiRpc.ViewModels.App;

public partial class ErrorViewModel : RoutableViewModel
{
    [ObservableProperty] 
    private string? _message;

    public ErrorViewModel(IRpcServiceViewModel rpcService, INavigationService navigationService)
        : base(rpcService, navigationService)
    {
    }
}
