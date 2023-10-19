using System.Collections.Generic;
using CommunityToolkit.Mvvm.ComponentModel;
using WasabiRpc.Models.Services;

namespace WasabiRpc.ViewModels.Info;

public partial class ListWalletsInfoViewModel : RoutableViewModel
{
    [ObservableProperty] 
    private List<WalletInfoViewModel>? _wallets;

    public ListWalletsInfoViewModel(IRpcServiceViewModel rpcService, INavigationService navigationService)
        : base(rpcService, navigationService)
    {
    }
}
