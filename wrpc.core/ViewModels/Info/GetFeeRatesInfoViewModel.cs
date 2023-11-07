using System.Collections.Generic;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using WasabiRpc.Models.Services;

namespace WasabiRpc.ViewModels.Info;

public partial class GetFeeRatesInfoViewModel : RoutableViewModel
{
    [ObservableProperty] 
    private Dictionary<int, int>? _feeRates;

    public GetFeeRatesInfoViewModel(
        IRpcServiceViewModel rpcService, 
        INavigationService navigationService,
        INavigationService detailsNavigationService,
        ICommand refreshCommand)
        : base(rpcService, navigationService, detailsNavigationService)
    {
        RefreshCommand = refreshCommand;
    }

    public ICommand RefreshCommand { get; }
}
