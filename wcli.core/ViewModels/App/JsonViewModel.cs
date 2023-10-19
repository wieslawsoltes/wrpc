using CommunityToolkit.Mvvm.ComponentModel;
using WasabiCli.Models.Services;

namespace WasabiCli.ViewModels.App;

public partial class JsonViewModel : RoutableViewModel
{
    [ObservableProperty] 
    private string? _content;

    public JsonViewModel(IRpcServiceViewModel rpcService, INavigationService navigationService)
        : base(rpcService, navigationService)
    {
    }
}
