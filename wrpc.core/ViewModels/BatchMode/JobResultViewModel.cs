using CommunityToolkit.Mvvm.ComponentModel;
using WasabiRpc.Models.BatchMode;
using WasabiRpc.Models.Services;

namespace WasabiRpc.ViewModels.BatchMode;

public partial class JobResultViewModel : RoutableViewModel
{
    [ObservableProperty] 
    private IJob? _job;

    [ObservableProperty] 
    private object? _result;

    [ObservableProperty] 
    private bool _isSuccess;

    public JobResultViewModel(IRpcServiceViewModel rpcService, INavigationService navigationService)
        : base(rpcService, navigationService)
    {
    }   
}
