using WasabiRpc.Models.App;

namespace WasabiRpc.Models.BatchMode;

public interface IJob
{
    string? Name { get; set; }

    Job Job { get; }
}
