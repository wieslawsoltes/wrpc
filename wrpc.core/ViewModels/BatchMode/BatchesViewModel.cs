using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using WasabiRpc.Models.BatchMode;
using WasabiRpc.Models.Services;

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
               && IsRunning;
    }

    [RelayCommand(CanExecute = nameof(CanAddBatch))]
    private void AddBatch()
    {
        if (Batches is not null)
        {
            Batches.Add(new BatchViewModel(RpcService, NavigationService) { Name = "Batch" });
            SelectedBatch = Batches.LastOrDefault();
        }
    }

    private bool CanRemoveBatch()
    {
        return Batches is not null 
               && SelectedBatch is not null
               && IsRunning;
    }

    [RelayCommand(CanExecute = nameof(CanRemoveBatch))]
    private void RemoveBatch()
    {
        if (Batches is not null && SelectedBatch is not null)
        {
            Batches.Remove(SelectedBatch);
            SelectedBatch = Batches.FirstOrDefault();
        }
    }

    private bool CanRunBatch()
    {
        return Batches is not null 
               && SelectedBatch is not null
               && IsRunning;
    }

    [RelayCommand(CanExecute = nameof(CanRunBatch))]
    private async Task RunBatch()
    {
        IsRunning = true;

        await Task.Run(() =>
        {
            // TODO:
        });
    }
}
