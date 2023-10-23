using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.Input;

namespace WasabiRpc.Models.BatchMode;

public interface IBatchManager
{
    ObservableCollection<IBatch>? Batches { get; set; }

    IBatch? SelectedBatch { get; set; }

    bool IsRunning { get; set; }

    IRelayCommand RemoveBatchCommand { get; }

    IRelayCommand AddBatchCommand { get; } 

    IAsyncRelayCommand<IBatch> RunBatchCommand { get; }
}
