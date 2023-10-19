using CommunityToolkit.Mvvm.ComponentModel;
using WasabiCli.Models.Services;

namespace WasabiCli.ViewModels.Info;

public partial class ErrorInfoViewModel : RoutableViewModel
{
    [ObservableProperty] 
    private int _code;

    [ObservableProperty] 
    private string? _message;

    public ErrorInfoViewModel(IRpcServiceViewModel rpcService, INavigationService navigationService)
        : base(rpcService, navigationService)
    {
    }

    // TODO:
}
