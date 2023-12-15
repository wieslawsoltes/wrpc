/*
 * wrpc A Graphical User Interface for using the Wasabi Wallet RPC.
 * Copyright (C) 2023  Wiesław Šoltés
 *
 * This program is free software: you can redistribute it and/or modify
 * it under the terms of the GNU Affero General Public License as
 * published by the Free Software Foundation, either version 3 of the
 * License, or (at your option) any later version.
 *
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU Affero General Public License for more details.
 *
 * You should have received a copy of the GNU Affero General Public License
 * along with this program.  If not, see <https://www.gnu.org/licenses/>.
 */
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

    public BatchManagerViewModel(
        IRpcServiceViewModel rpcService, 
        INavigationService navigationService,
        INavigationService detailsNavigationService)
        : base(rpcService, navigationService, detailsNavigationService)
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
            Batches.Add(new BatchViewModel(RpcService, NavigationService, DetailsNavigationService)
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
        if (batch.Jobs is null)
        {
            var errorViewModel = new Error { Message = "No jobs to run." }.ToViewModel(RpcService, NavigationService, DetailsNavigationService);
            NavigationService.NavigateTo(errorViewModel);
            return;
        }

        var groupedJobs = batch.Jobs.GroupBy(x => x.Job.RpcServerUri);
        var jobResults = new List<JobResultViewModel>();

        foreach (var groupedJob in groupedJobs)
        {
            var serverPrefix = groupedJob.Key;
            var jobs = groupedJob.ToList();
            var rpcMethods = jobs.Select(x => x.Job.RpcMethod).ToArray();
            var results = await RpcService.Send(rpcMethods, serverPrefix);
            if (results is List<object?> resultsList)
            {
                var jobPartialResults = jobs.Zip(
                    resultsList,
                    (job, result) =>
                    {
                        var routableMethod = RoutableMethodFactory.CreateRoutableMethod(
                            job.Job.Name, 
                            RpcService, 
                            NavigationService,
                            DetailsNavigationService, 
                            this);

                        var resultViewModel = routableMethod?.ToJobResult(result);

                        return new JobResultViewModel(RpcService, NavigationService, DetailsNavigationService)
                        {
                            Job = job,
                            Result = resultViewModel,
                            IsSuccess = resultViewModel is not null && resultViewModel is not ErrorInfoViewModel
                        };
                    });

                jobResults.AddRange(jobPartialResults);
            }
            else if (results is Error error)
            {
                var errorViewModel = error.ToViewModel(RpcService, NavigationService, DetailsNavigationService);
                NavigationService.NavigateTo(errorViewModel);
                return;
            }
            else
            {
                var errorViewModel = new Error { Message = "Invalid send result." }.ToViewModel(RpcService, NavigationService, DetailsNavigationService);
                NavigationService.NavigateTo(errorViewModel);
                return;
            }
        }

        NavigationService.NavigateTo(new BatchResultViewModel(RpcService, NavigationService, DetailsNavigationService)
        {
            Results = jobResults
        });
    }
}
