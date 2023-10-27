using CommunityToolkit.Mvvm.Input;
using WasabiRpc.Models.App;
using WasabiRpc.Models.Services;

namespace WasabiRpc.Models.BatchMode;

public interface IJob : IRoutable
{
    Job Job { get; }

    bool IsRunning { get; set; }

    IAsyncRelayCommand RunJobCommand { get; }
}
