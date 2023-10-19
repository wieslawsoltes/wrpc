using System.Collections.Generic;
using CommunityToolkit.Mvvm.ComponentModel;
using WasabiCli.Models.Services;

namespace WasabiCli.ViewModels.Info;

public partial class ListWalletsInfoViewModel : RoutableViewModel
{
    [ObservableProperty] 
    private List<WalletInfoViewModel>? _wallets;

    public ListWalletsInfoViewModel(IRpcServiceViewModel rpcService, INavigationService navigationService)
        : base(rpcService, navigationService)
    {
    }

    // TODO:
}
