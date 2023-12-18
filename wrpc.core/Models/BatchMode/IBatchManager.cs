using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.Input;
using WasabiRpc.Models.Services;

namespace WasabiRpc.Models.BatchMode;

public interface IBatchManager : IRoutable
{
    ObservableCollection<IBatch>? Batches { get; set; }

    IBatch? SelectedBatch { get; set; }

    bool IsRunning { get; set; }

    IRelayCommand<IBatch> RemoveBatchCommand { get; }

    IRelayCommand AddBatchCommand { get; } 

    IAsyncRelayCommand<IBatch> RunBatchCommand { get; }
}
