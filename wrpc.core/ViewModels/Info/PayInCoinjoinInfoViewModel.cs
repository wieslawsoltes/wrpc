using CommunityToolkit.Mvvm.ComponentModel;
using WasabiRpc.Models.Services;

namespace WasabiRpc.ViewModels.Info;

public partial class PayInCoinjoinInfoViewModel : RoutableViewModel
{
    [ObservableProperty] 
    private string? _paymentId;

    public PayInCoinjoinInfoViewModel(
        IRpcServiceViewModel rpcService, 
        INavigationService navigationService,
        INavigationService detailsNavigationService)
        : base(rpcService, navigationService, detailsNavigationService)
    {
    }
}
