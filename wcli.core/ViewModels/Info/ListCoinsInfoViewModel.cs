using System.Collections.Generic;
using CommunityToolkit.Mvvm.ComponentModel;
using WasabiCli.Models.Services;

namespace WasabiCli.ViewModels.Info;

public partial class ListCoinsInfoViewModel : RoutableViewModel
{
    [ObservableProperty] 
    private List<CoinInfoViewModel>? _coins;

    public ListCoinsInfoViewModel(IRpcServiceViewModel rpcService, INavigationService navigationService)
        : base(rpcService, navigationService)
    {
    }
}
