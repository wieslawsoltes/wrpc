using CommunityToolkit.Mvvm.ComponentModel;
using WasabiCli.Models.Services;

namespace WasabiCli.ViewModels.Info;

public partial class BroadcastInfoViewModel : RoutableViewModel
{
    [ObservableProperty] 
    private string? _txId;

    public BroadcastInfoViewModel(IRpcServiceViewModel rpcService, INavigationService navigationService)
        : base(rpcService, navigationService)
    {
    }

    // TODO:
}
