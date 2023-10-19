using System.Collections.Generic;
using CommunityToolkit.Mvvm.ComponentModel;
using WasabiRpc.Models.Services;

namespace WasabiRpc.ViewModels.Info;

public partial class ListCoinsInfoViewModel : RoutableViewModel
{
    [ObservableProperty] 
    private List<CoinInfoViewModel>? _coins;

    public ListCoinsInfoViewModel(IRpcServiceViewModel rpcService, INavigationService navigationService)
        : base(rpcService, navigationService)
    {
    }
}
