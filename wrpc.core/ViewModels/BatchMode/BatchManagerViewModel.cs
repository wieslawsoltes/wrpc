using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using WasabiRpc.Models.App;
using WasabiRpc.Models.BatchMode;
using WasabiRpc.Models.Services;
using WasabiRpc.ViewModels.Factories;
using WasabiRpc.ViewModels.Info;

namespace WasabiRpc.ViewModels.BatchMode;

public partial class BatchManagerViewModel : RoutableViewModel, IBatchManager
{
    [NotifyCanExecuteChangedFor(nameof(AddBatchCommand))]
    [NotifyCanExecuteChangedFor(nameof(RemoveBatchCommand))]
    [ObservableProperty]
    private ObservableCollection<IBatch>? _batches;

    [NotifyCanExecuteChangedFor(nameof(AddBatchCommand))]
    [NotifyCanExecuteChangedFor(nameof(RemoveBatchCommand))]
    [ObservableProperty] 
    private IBatch? _selectedBatch;

    [NotifyCanExecuteChangedFor(nameof(AddBatchCommand))]
    [NotifyCanExecuteChangedFor(nameof(RemoveBatchCommand))]
    [ObservableProperty] 
    private bool _isRunning;

    public BatchManagerViewModel(IRpcServiceViewModel rpcService, INavigationService navigationService)
        : base(rpcService, navigationService)
    {
    }

    private bool CanAddBatch()
    {
        return Batches is not null 
               && !IsRunning;
    }

    [RelayCommand(CanExecute = nameof(CanAddBatch))]
    private void AddBatch()
    {
        if (Batches is not null)
        {
            Batches.Add(new BatchViewModel(RpcService, NavigationService)
            {
                Name = "Batch",
                Jobs = new ObservableCollection<IJob>()
            });
            SelectedBatch = Batches.LastOrDefault();
        }
    }

    private bool CanRemoveBatch()
    {
        return Batches is not null 
               && SelectedBatch is not null
               && !IsRunning;
    }

    [RelayCommand(CanExecute = nameof(CanRemoveBatch))]
    private void RemoveBatch(IBatch batch)
    {
        if (Batches is not null)
        {
            Batches.Remove(batch);
            SelectedBatch = Batches.FirstOrDefault();
        }
    }

    private bool CanRunBatch()
    {
        return Batches is not null 
               && SelectedBatch is not null
               && !IsRunning;
    }

    [RelayCommand(CanExecute = nameof(CanRunBatch))]
    private async Task RunBatch(IBatch batch)
    {
        IsRunning = true;

        await Task.Run(async () => await Run(batch));

        IsRunning = false;
    }

    private async Task Run(IBatch batch)
    {
        var jobs = batch.Jobs;
        if (jobs is null)
        {
            var errorViewModel = new Error { Message = "No jobs to run." }.ToViewModel(RpcService, NavigationService);
            NavigationService.NavigateTo(errorViewModel);
            return;
        }

        var serverPrefix = jobs.FirstOrDefault()?.Job.RpcServerUri;
        if (serverPrefix is null)
        {
            var errorViewModel = new Error { Message = "No valid server prefix." }.ToViewModel(RpcService, NavigationService);
            NavigationService.NavigateTo(errorViewModel);
            return;
        }

        var rpcMethods = jobs.Select(x => x.Job.RpcMethod).ToArray();
        var results = await RpcService.Send(rpcMethods, serverPrefix);
        if (results is List<object?> resultsList)
        {
            NavigationService.NavigateTo(new BatchResultViewModel(RpcService, NavigationService)
            {
                Results = jobs.Zip(
                    resultsList, 
                    (job, result) =>
                    {
                        var routableMethod = RoutableMethodFactory.CreateRoutableMethod(job.Job.Name, RpcService, NavigationService, this);
                        var resultViewModel = routableMethod?.ToJobResult(result);
                        return new JobResultViewModel(RpcService, NavigationService)
                        {
                            Job = job,
                            Result = resultViewModel,
                            IsSuccess = resultViewModel is not null && resultViewModel is not ErrorInfoViewModel
                        };
                    }).ToList()
            });
        }
        else
        {
            var errorViewModel = new Error { Message = "Invalid send result." }.ToViewModel(RpcService, NavigationService);
            NavigationService.NavigateTo(errorViewModel);
        }
    }
}
