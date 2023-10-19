using CommunityToolkit.Mvvm.ComponentModel;
using WasabiCli.Models.Services;

namespace WasabiCli.ViewModels.Info;

public partial class PayInCoinjoinInfoViewModel : RoutableViewModel
{
    [ObservableProperty] 
    private string? _paymentId;

    public PayInCoinjoinInfoViewModel(IRpcServiceViewModel rpcService, INavigationService navigationService)
        : base(rpcService, navigationService)
    {
    }
}
