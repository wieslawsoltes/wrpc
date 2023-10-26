using System.Collections.Generic;
using CommunityToolkit.Mvvm.ComponentModel;
using WasabiRpc.Models.Services;
using WasabiRpc.ViewModels.Methods.Adapters;

namespace WasabiRpc.ViewModels.Info;

public partial class ListUnspentCoinsInfoViewModel : RoutableViewModel
{
    [ObservableProperty] 
    private List<CoinAdapterViewModel>? _coins;

    public ListUnspentCoinsInfoViewModel(IRpcServiceViewModel rpcService, INavigationService navigationService)
        : base(rpcService, navigationService)
    {
    }
}
