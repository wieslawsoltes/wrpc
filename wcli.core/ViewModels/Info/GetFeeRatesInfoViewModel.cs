using System.Collections.Generic;
using CommunityToolkit.Mvvm.ComponentModel;
using WasabiCli.Models.Services;

namespace WasabiCli.ViewModels.Info;

public partial class GetFeeRatesInfoViewModel : RoutableViewModel
{
    [ObservableProperty] 
    private Dictionary<int, int>? _feeRates;

    public GetFeeRatesInfoViewModel(IRpcServiceViewModel rpcService, INavigationService navigationService)
        : base(rpcService, navigationService)
    {
    }

    // TODO:
}
