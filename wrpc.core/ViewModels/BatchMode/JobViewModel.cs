using CommunityToolkit.Mvvm.ComponentModel;
using WasabiRpc.Models.App;
using WasabiRpc.Models.BatchMode;
using WasabiRpc.Models.Services;

namespace WasabiRpc.ViewModels.BatchMode;

public partial class JobViewModel : RoutableViewModel, IJob
{
    [ObservableProperty] 
    private string? _name;

    public JobViewModel(IRpcServiceViewModel rpcService, INavigationService navigationService, Job job)
        : base(rpcService, navigationService)
    {
        Name = job.Name;
        Job = job;
    }

    public Job Job { get; }
}
