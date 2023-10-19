using System.Collections.Generic;
using CommunityToolkit.Mvvm.ComponentModel;
using WasabiRpc.Models.Services;

namespace WasabiRpc.ViewModels.Info;

public partial class ListKeysInfoViewModel : RoutableViewModel
{
    [ObservableProperty] 
    private List<KeyInfoViewModel>? _keys;

    public ListKeysInfoViewModel(IRpcServiceViewModel rpcService, INavigationService navigationService)
        : base(rpcService, navigationService)
    {
    }
}
