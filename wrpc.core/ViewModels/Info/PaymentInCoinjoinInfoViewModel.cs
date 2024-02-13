using System.Collections.Generic;
using CommunityToolkit.Mvvm.ComponentModel;
using WasabiRpc.Models.Services;

namespace WasabiRpc.ViewModels.Info;

public partial class PaymentInCoinjoinInfoViewModel : RoutableViewModel
{
    [ObservableProperty] 
    private string? _id;

    [ObservableProperty] 
    private long _amount;

    [ObservableProperty] 
    private string? _destination;

    [ObservableProperty] 
    private List<PaymentInCoinjoinStateInfoViewModel>? _state;

    [ObservableProperty] 
    private string? _address;

    public PaymentInCoinjoinInfoViewModel(
        IRpcServiceViewModel rpcService, 
        INavigationService navigationService,
        INavigationService detailsNavigationService)
        : base(rpcService, navigationService, detailsNavigationService)
    {
    }
}
