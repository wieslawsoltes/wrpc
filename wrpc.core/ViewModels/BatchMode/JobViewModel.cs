using CommunityToolkit.Mvvm.ComponentModel;
using WasabiRpc.Models.App;
using WasabiRpc.Models.BatchMode;
using WasabiRpc.Models.Services;

namespace WasabiRpc.ViewModels.BatchMode;

public partial class JobViewModel : RoutableViewModel, IJob
{
    [ObservableProperty] 
    private bool _isRunning;

    public JobViewModel(
        IRpcServiceViewModel rpcService, 
        INavigationService navigationService,
        INavigationService detailsNavigationService,
        Job job)
        : base(rpcService, navigationService, detailsNavigationService)
    {
        Job = job;
    }

    public Job Job { get; }
}
