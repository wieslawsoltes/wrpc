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
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using WasabiRpc.Models.App;
using WasabiRpc.Models.BatchMode;
using WasabiRpc.Models.Services;
using WasabiRpc.ViewModels.Factories;

namespace WasabiRpc.ViewModels.BatchMode;

public partial class AddJobViewModel : RoutableViewModel
{
    private readonly IBatchManager _batchManager;
    private readonly Job _job;

    [ObservableProperty] 
    private string? _content;

    public AddJobViewModel(
        IRpcServiceViewModel rpcService, 
        INavigationService navigationService,
        INavigationService detailsNavigationService,
        IBatchManager batchManager, 
        Job job, 
        string json)
        : base(rpcService, navigationService, detailsNavigationService)
    {
        _batchManager = batchManager;
        _job = job;
        Content = json;
    }

    private bool CanAddJob()
    {
        return Content is not null
               && Content.Length > 0
               && _batchManager.Batches is not null
               && _batchManager.SelectedBatch is not null;
    }

    [RelayCommand(CanExecute = nameof(CanAddJob))]
    private void AddJob()
    {
        if (_batchManager.Batches is not null && _batchManager.SelectedBatch is not null)
        {
            _batchManager.SelectedBatch.AddJobCommand.Execute(_job);
            var successViewModel = new Success { Message = $"Added job '{_job.Name}'" }.ToViewModel(RpcService, NavigationService, DetailsNavigationService);
            NavigationService.ClearAndNavigateTo(successViewModel);
        }
        else
        {
            var errorViewModel = new Error { Message = $"Could not add job '{_job.Name}'" }.ToViewModel(RpcService, NavigationService, DetailsNavigationService);
            NavigationService.ClearAndNavigateTo(errorViewModel);
        }
    }
}
