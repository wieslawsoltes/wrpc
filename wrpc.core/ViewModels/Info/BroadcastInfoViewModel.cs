using CommunityToolkit.Mvvm.ComponentModel;
using WasabiRpc.Models.Services;

namespace WasabiRpc.ViewModels.Info;

public partial class BroadcastInfoViewModel : RoutableViewModel
{
    [ObservableProperty] 
    private string? _txId;

    public BroadcastInfoViewModel(IRpcServiceViewModel rpcService, INavigationService navigationService)
        : base(rpcService, navigationService)
    {
    }
}
