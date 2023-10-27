using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using WasabiRpc.Models.App;
using WasabiRpc.Models.BatchMode;
using WasabiRpc.Models.Services;
using WasabiRpc.ViewModels.BatchMode;

namespace WasabiRpc.ViewModels.Factories;

public static class BatchModeFactory
{
    public static IBatchManager ToViewModel(this IList<Batch> batches, int selectedBatchIndex, IRpcServiceViewModel rpcService, INavigationService navigationService)
    {
        var batchesList = new ObservableCollection<IBatch>();

        foreach (var batch in batches)
        {
            batchesList.Add(batch.ToViewModel(rpcService, navigationService));
        }

        var selectedBatch = selectedBatchIndex < batchesList.Count && selectedBatchIndex >= 0
            ? batchesList[selectedBatchIndex]
            : null;

        return new BatchManagerViewModel(rpcService, navigationService)
        {
            Batches = batchesList,
            SelectedBatch = selectedBatch
        };
    }

    public static List<Batch> ToBatches(this ObservableCollection<IBatch> batches)
    {
        return batches.Select(batch =>
            {
                var selectedJobIndex = batch.Jobs is not null && batch.SelectedJob is not null
                    ? batch.Jobs.IndexOf(batch.SelectedJob)
                    : -1;

                return new Batch
                {
                    Name = batch.Name,
                    Jobs = batch.Jobs?.Select(job => job.Job).ToList(),
                    SelectedJobIndex = selectedJobIndex
                };
            })
            .ToList();
    }

    public static ObservableCollection<IJob> ToViewModel(this IList<Job> jobs, IRpcServiceViewModel rpcService, INavigationService navigationService)
    {
        var jobsList = new ObservableCollection<IJob>();

        foreach (var job in jobs)
        {
            jobsList.Add(job.ToViewModel(rpcService, navigationService));
        }

        return jobsList;
    }

    public static IBatch ToViewModel(this Batch batch, IRpcServiceViewModel rpcService, INavigationService navigationService)
    {
        var jobs = batch.Jobs?.ToViewModel(rpcService, navigationService);

        var selectedJob = batch.SelectedJobIndex < jobs?.Count && batch.SelectedJobIndex >= 0
            ? jobs[batch.SelectedJobIndex]
            : null;

        return new BatchViewModel(rpcService, navigationService)
        {
            Name = batch.Name,
            Jobs = jobs,
            SelectedJob = selectedJob
        };
    }

    public static IJob ToViewModel(this Job job, IRpcServiceViewModel rpcService, INavigationService navigationService)
    {
        return new JobViewModel(rpcService, navigationService, job);
    }
}
