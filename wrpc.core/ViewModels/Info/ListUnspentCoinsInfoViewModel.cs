using System.Collections.Generic;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using WasabiRpc.Models.Services;
using WasabiRpc.ViewModels.Methods.Adapters;

namespace WasabiRpc.ViewModels.Info;

public partial class ListUnspentCoinsInfoViewModel : RoutableViewModel
{
    [ObservableProperty] 
    private List<CoinAdapterViewModel>? _coins;

    public ListUnspentCoinsInfoViewModel(IRpcServiceViewModel rpcService, INavigationService navigationService, ICommand refreshCommand)
        : base(rpcService, navigationService)
    {
        RefreshCommand = refreshCommand;
    }

    public ICommand RefreshCommand { get; }
}
