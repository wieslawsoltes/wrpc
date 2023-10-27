using System.Collections.Generic;
using CommunityToolkit.Mvvm.ComponentModel;
using WasabiRpc.Models.Services;

namespace WasabiRpc.ViewModels.BatchMode;

public partial class BatchResultViewModel : RoutableViewModel
{
    [ObservableProperty] 
    private List<JobResultViewModel>? _results;

    public BatchResultViewModel(IRpcServiceViewModel rpcService, INavigationService navigationService)
        : base(rpcService, navigationService)
    {
    }   
}
