using CommunityToolkit.Mvvm.ComponentModel;
using WasabiCli.Models.Services;

namespace WasabiCli.ViewModels.Info;

public partial class CancelTransactionInfoViewModel : RoutableViewModel
{
    [ObservableProperty] 
    private string? _tx;

    public CancelTransactionInfoViewModel(IRpcServiceViewModel rpcService, INavigationService navigationService)
        : base(rpcService, navigationService)
    {
    }
}
