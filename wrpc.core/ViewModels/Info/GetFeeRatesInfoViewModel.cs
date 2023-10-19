using System.Collections.Generic;
using CommunityToolkit.Mvvm.ComponentModel;
using WasabiRpc.Models.Services;

namespace WasabiRpc.ViewModels.Info;

public partial class GetFeeRatesInfoViewModel : RoutableViewModel
{
    [ObservableProperty] 
    private Dictionary<int, int>? _feeRates;

    public GetFeeRatesInfoViewModel(IRpcServiceViewModel rpcService, INavigationService navigationService)
        : base(rpcService, navigationService)
    {
    }
}
