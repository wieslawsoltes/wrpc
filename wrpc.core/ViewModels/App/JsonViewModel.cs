using CommunityToolkit.Mvvm.ComponentModel;
using WasabiRpc.Models.Services;

namespace WasabiRpc.ViewModels.App;

public partial class JsonViewModel : RoutableViewModel
{
    [ObservableProperty] 
    private string? _content;

    public JsonViewModel(IRpcServiceViewModel rpcService, INavigationService navigationService)
        : base(rpcService, navigationService)
    {
    }
}
