using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.Input;
using WasabiRpc.Models.App;
using WasabiRpc.Models.Services;

namespace WasabiRpc.Models.BatchMode;

public interface IBatch : IRoutable
{
    string? Name { get; set; }

    ObservableCollection<IJob>? Jobs { get; set; }

    IJob? SelectedJob { get; set; }

    IRelayCommand<IJob> RemoveJobCommand { get; }

    IRelayCommand<Job> AddJobCommand { get; }
}
