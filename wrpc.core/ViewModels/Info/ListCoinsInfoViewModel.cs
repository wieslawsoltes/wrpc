using System.Collections.Generic;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using WasabiRpc.Models.Services;

namespace WasabiRpc.ViewModels.Info;

public partial class ListCoinsInfoViewModel : RoutableViewModel
{
    [ObservableProperty] 
    private List<CoinInfoViewModel>? _coins;

    public ListCoinsInfoViewModel(
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
