using CommunityToolkit.Mvvm.ComponentModel;
using WasabiCli.Models.Services;

namespace WasabiCli.ViewModels.Info;

public partial class SpeedUpTransactionInfoViewModel : RoutableViewModel
{
    [ObservableProperty] 
    private string? _tx;

    public SpeedUpTransactionInfoViewModel(IRpcServiceViewModel rpcService, INavigationService navigationService)
        : base(rpcService, navigationService)
    {
    }

    // TODO:
}
