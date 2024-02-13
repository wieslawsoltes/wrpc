using CommunityToolkit.Mvvm.ComponentModel;
using WasabiRpc.Models.Services;

namespace WasabiRpc.ViewModels.Info;

public partial class PaymentInCoinjoinStateInfoViewModel : RoutableViewModel
{
    [ObservableProperty] 
    private string? _status;

    [ObservableProperty] 
    private string? _round;

    [ObservableProperty] 
    private string? _txId;

    public PaymentInCoinjoinStateInfoViewModel(
        IRpcServiceViewModel rpcService, 
        INavigationService navigationService,
        INavigationService detailsNavigationService)
        : base(rpcService, navigationService, detailsNavigationService)
    {
    }
}
