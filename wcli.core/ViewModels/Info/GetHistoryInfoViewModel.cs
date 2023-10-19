using System.Collections.Generic;
using CommunityToolkit.Mvvm.ComponentModel;
using WasabiCli.Models.Services;

namespace WasabiCli.ViewModels.Info;

public partial class GetHistoryInfoViewModel : RoutableViewModel
{
    [ObservableProperty] 
    private List<TransactionInfoViewModel>? _transactions;

    public GetHistoryInfoViewModel(IRpcServiceViewModel rpcService, INavigationService navigationService)
        : base(rpcService, navigationService)
    {
    }

    // TODO:
}
