using CommunityToolkit.Mvvm.ComponentModel;
using WasabiCli.Models.Services;

namespace WasabiCli.ViewModels.Info;

public partial class SendInfoViewModel : RoutableViewModel
{
    [ObservableProperty] 
    private string? _txId;

    [ObservableProperty] 
    private string? _tx;

    public SendInfoViewModel(IRpcServiceViewModel rpcService, INavigationService navigationService)
        : base(rpcService, navigationService)
    {
    }
}
