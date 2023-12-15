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
using System.Collections.ObjectModel;
using System.Linq;
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

    public BatchViewModel(
        IRpcServiceViewModel rpcService, 
        INavigationService navigationService,
        INavigationService detailsNavigationService)
        : base(rpcService, navigationService, detailsNavigationService)
    {
    }

    private bool CanAddJob()
    {
        return Jobs is not null 
               && !IsRunning;
    }

    [RelayCommand(CanExecute = nameof(CanAddJob))]
    private void AddJob(Job job)
    {
        if (Jobs is not null)
        {
            Jobs.Add(new JobViewModel(RpcService, NavigationService, DetailsNavigationService, job));
            SelectedJob = Jobs.LastOrDefault();
        }
    }

    private bool CanRemoveJob()
    {
        return Jobs is not null 
               && SelectedJob is not null
               && !IsRunning;
    }

    [RelayCommand(CanExecute = nameof(CanRemoveJob))]
    private void RemoveJob(IJob job)
    {
        if (Jobs is not null )
        {
            Jobs.Remove(job);
            SelectedJob = Jobs.FirstOrDefault();
        }
    }
}
