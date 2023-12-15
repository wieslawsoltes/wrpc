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
using WasabiRpc.Models.App;
using WasabiRpc.Models.BatchMode;
using WasabiRpc.Models.Services;
using WasabiRpc.ViewModels.BatchMode;

namespace WasabiRpc.ViewModels.Factories;

public static class BatchModeFactory
{
    public static IBatchManager ToViewModel(
        this IList<Batch> batches, 
        int selectedBatchIndex, 
        IRpcServiceViewModel rpcService, 
        INavigationService navigationService,
        INavigationService detailsNavigationService)
    {
        var batchesList = new ObservableCollection<IBatch>();

        foreach (var batch in batches)
        {
            batchesList.Add(batch.ToViewModel(rpcService, navigationService, detailsNavigationService));
        }

        var selectedBatch = selectedBatchIndex < batchesList.Count && selectedBatchIndex >= 0
            ? batchesList[selectedBatchIndex]
            : null;

        return new BatchManagerViewModel(rpcService, navigationService, detailsNavigationService)
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

    public static ObservableCollection<IJob> ToViewModel(
        this IList<Job> jobs, 
        IRpcServiceViewModel rpcService, 
        INavigationService navigationService,
        INavigationService detailsNavigationService)
    {
        var jobsList = new ObservableCollection<IJob>();

        foreach (var job in jobs)
        {
            jobsList.Add(job.ToViewModel(rpcService, navigationService, detailsNavigationService));
        }

        return jobsList;
    }

    public static IBatch ToViewModel(
        this Batch batch, 
        IRpcServiceViewModel rpcService, 
        INavigationService navigationService,
        INavigationService detailsNavigationService)
    {
        var jobs = batch.Jobs?.ToViewModel(rpcService, navigationService, detailsNavigationService);

        var selectedJob = batch.SelectedJobIndex < jobs?.Count && batch.SelectedJobIndex >= 0
            ? jobs[batch.SelectedJobIndex]
            : null;

        return new BatchViewModel(rpcService, navigationService, detailsNavigationService)
        {
            Name = batch.Name,
            Jobs = jobs,
            SelectedJob = selectedJob
        };
    }

    public static IJob ToViewModel(
        this Job job, 
        IRpcServiceViewModel rpcService, 
        INavigationService navigationService,
        INavigationService detailsNavigationService)
    {
        return new JobViewModel(rpcService, navigationService, detailsNavigationService, job);
    }
}
