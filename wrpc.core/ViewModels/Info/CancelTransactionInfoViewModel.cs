using CommunityToolkit.Mvvm.ComponentModel;
using WasabiRpc.Models.Services;

namespace WasabiRpc.ViewModels.Info;

public partial class CancelTransactionInfoViewModel : RoutableViewModel
{
    [ObservableProperty] 
    private string? _tx;

    public CancelTransactionInfoViewModel(IRpcServiceViewModel rpcService, INavigationService navigationService)
        : base(rpcService, navigationService)
    {
    }
}
