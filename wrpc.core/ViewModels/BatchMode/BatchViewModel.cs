using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using WasabiRpc.Models.App;
using WasabiRpc.Models.BatchMode;
using WasabiRpc.Models.Services;

namespace WasabiRpc.ViewModels.BatchMode;

public partial class BatchViewModel : RoutableViewModel, IBatch
{
    [ObservableProperty] 
    private string? _name;

    [NotifyCanExecuteChangedFor(nameof(AddJobCommand))]
    [NotifyCanExecuteChangedFor(nameof(RemoveJobCommand))]
    [ObservableProperty]
    private ObservableCollection<IJob>? _jobs;

    [NotifyCanExecuteChangedFor(nameof(AddJobCommand))]
    [NotifyCanExecuteChangedFor(nameof(RemoveJobCommand))]
    [ObservableProperty] 
    private IJob? _selectedJob;

    [NotifyCanExecuteChangedFor(nameof(AddJobCommand))]
    [NotifyCanExecuteChangedFor(nameof(RemoveJobCommand))]
    [ObservableProperty] 
    private bool _isRunning;

    public BatchViewModel(IRpcServiceViewModel rpcService, INavigationService navigationService)
        : base(rpcService, navigationService)
    {
    }

    private bool CanAddJob()
    {
        return Jobs is not null 
               && IsRunning;
    }

    [RelayCommand(CanExecute = nameof(CanAddJob))]
    private void AddJob(Job job)
    {
        if (Jobs is not null)
        {
            Jobs.Add(new JobViewModel(RpcService, NavigationService, job));
            SelectedJob = Jobs.LastOrDefault();
        }
    }

    private bool CanRemoveJob()
    {
        return Jobs is not null 
               && SelectedJob is not null
               && IsRunning;
    }

    [RelayCommand(CanExecute = nameof(CanRemoveJob))]
    private void RemoveJob()
    {
        if (Jobs is not null && SelectedJob is not null)
        {
            Jobs.Remove(SelectedJob);
            SelectedJob = Jobs.FirstOrDefault();
        }
    }

    private bool CanRunJob()
    {
        return Jobs is not null 
               && SelectedJob is not null
               && IsRunning;
    }

    [RelayCommand(CanExecute = nameof(CanRunJob))]
    private async Task RunJob(IJob job)
    {
        IsRunning = true;

        await Task.Run(() =>
        {
            // TODO:
        });
    }
}
