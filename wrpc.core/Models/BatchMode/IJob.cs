using WasabiRpc.Models.App;
using WasabiRpc.Models.Services;

namespace WasabiRpc.Models.BatchMode;

public interface IJob : IRoutable
{
    string? Name { get; set; }

    Job Job { get; }
}
