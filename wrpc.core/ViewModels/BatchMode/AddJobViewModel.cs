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

    public AddJobViewModel(IRpcServiceViewModel rpcService, INavigationService navigationService, IBatchManager batchManager, Job job, string json)
        : base(rpcService, navigationService)
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
            var successViewModel = new Success { Message = $"Added job '{_job.Name}'" }.ToViewModel(RpcService, NavigationService);
            NavigationService.ClearAndNavigateTo(successViewModel);
        }
        else
        {
            var errorViewModel = new Error { Message = $"Could not add job '{_job.Name}'" }.ToViewModel(RpcService, NavigationService);
            NavigationService.ClearAndNavigateTo(errorViewModel);
        }
    }
}
