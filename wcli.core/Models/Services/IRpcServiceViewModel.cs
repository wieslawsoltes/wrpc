using System.Collections.Generic;
using System.Threading.Tasks;
using WasabiCli.Models.App;

namespace WasabiCli.Models.Services;

public interface IRpcServiceViewModel
{
    Task<object?> Send<TResult>(Job job) where TResult: class;

    string? ServerPrefix { get; set; }

    bool BatchMode { get; set; }

    IList<Batch>? Batches { get; set; }
}
