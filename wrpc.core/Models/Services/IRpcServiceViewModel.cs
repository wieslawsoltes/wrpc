using System.Collections.Generic;
using System.Threading.Tasks;
using WasabiRpc.Models.App;

namespace WasabiRpc.Models.Services;

public interface IRpcServiceViewModel
{
    Task<object?> Send<TResult>(Job job, INavigationService navigationService) where TResult: class;

    string? ServerPrefix { get; set; }

    bool BatchMode { get; set; }

    IList<Batch>? Batches { get; set; }
}
