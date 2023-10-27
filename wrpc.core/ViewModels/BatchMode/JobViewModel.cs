using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using WasabiRpc.Models.App;
using WasabiRpc.Models.BatchMode;
using WasabiRpc.Models.Services;

namespace WasabiRpc.ViewModels.BatchMode;

public partial class JobViewModel : RoutableViewModel, IJob
{
    [ObservableProperty] 
    private bool _isRunning;

    public JobViewModel(IRpcServiceViewModel rpcService, INavigationService navigationService, Job job)
        : base(rpcService, navigationService)
    {
        Job = job;
    }

    public Job Job { get; }

    private bool CanRunJob()
    {
        return !IsRunning;
    }

    [RelayCommand(CanExecute = nameof(CanRunJob))]
    private async Task RunJob()
    {
        IsRunning = true;

        await Task.Run(() =>
        {
            // TODO:
        });

        IsRunning = false;
    }
}
