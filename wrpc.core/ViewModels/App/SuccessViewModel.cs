using CommunityToolkit.Mvvm.ComponentModel;
using WasabiRpc.Models.Services;

namespace WasabiRpc.ViewModels.App;

public partial class SuccessViewModel : RoutableViewModel
{
    [ObservableProperty] 
    private string? _message;

    public SuccessViewModel(IRpcServiceViewModel rpcService, INavigationService navigationService)
        : base(rpcService, navigationService)
    {
    }
}
