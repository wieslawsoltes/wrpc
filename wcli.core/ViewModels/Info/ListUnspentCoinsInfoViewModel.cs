using System.Collections.Generic;
using CommunityToolkit.Mvvm.ComponentModel;
using WasabiCli.Models.Services;

namespace WasabiCli.ViewModels.Info;

public partial class ListUnspentCoinsInfoViewModel : RoutableViewModel
{
    [ObservableProperty] 
    private List<CoinInfoViewModel>? _coins;

    public ListUnspentCoinsInfoViewModel(IRpcServiceViewModel rpcService, INavigationService navigationService)
        : base(rpcService, navigationService)
    {
    }

    // TODO:
}
