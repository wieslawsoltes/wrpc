using System.Collections.Generic;
using CommunityToolkit.Mvvm.ComponentModel;
using WasabiRpc.Models.Services;
using WasabiRpc.ViewModels.Methods;

namespace WasabiRpc.ViewModels.Info;

public partial class ListUnspentCoinsInfoViewModel : RoutableViewModel
{
    [ObservableProperty] 
    private List<CoinViewModel>? _coins;

    public ListUnspentCoinsInfoViewModel(IRpcServiceViewModel rpcService, INavigationService navigationService)
        : base(rpcService, navigationService)
    {
    }
}
