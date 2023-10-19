using System.Collections.Generic;
using CommunityToolkit.Mvvm.ComponentModel;
using WasabiRpc.Models.Services;
using WasabiRpc.ViewModels.Methods;

namespace WasabiRpc.ViewModels.Info;

public partial class GetHistoryInfoViewModel : RoutableViewModel
{
    [ObservableProperty] 
    private List<TransactionViewModel>? _transactions;

    public GetHistoryInfoViewModel(IRpcServiceViewModel rpcService, INavigationService navigationService)
        : base(rpcService, navigationService)
    {
    }
}
