using CommunityToolkit.Mvvm.ComponentModel;
using WasabiCli.Models.Services;

namespace WasabiCli.ViewModels.App;

public partial class SuccessViewModel : RoutableViewModel
{
    [ObservableProperty] 
    private string? _message;

    public SuccessViewModel(IRpcServiceViewModel rpcService, INavigationService navigationService)
        : base(rpcService, navigationService)
    {
    }
}
