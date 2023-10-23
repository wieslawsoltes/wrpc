using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.Input;
using WasabiRpc.Models.App;

namespace WasabiRpc.Models.BatchMode;

public interface IBatch
{
    string? Name { get; set; }

    ObservableCollection<IJob>? Jobs { get; set; }

    IJob? SelectedJob { get; set; }

    bool IsRunning { get; set; }

    IRelayCommand RemoveJobCommand { get; }

    IRelayCommand<Job> AddJobCommand { get; }

    IAsyncRelayCommand<IJob> RunJobCommand { get; }
}
