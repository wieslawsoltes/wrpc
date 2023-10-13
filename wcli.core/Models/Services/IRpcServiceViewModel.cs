using System.Collections.Generic;
using System.Text.Json.Serialization.Metadata;
using System.Threading.Tasks;
using WasabiCli.Models.App;

namespace WasabiCli.Models.Services;

public interface IRpcServiceViewModel
{
    Task<object?> Send<T>(Job job, JsonTypeInfo<T> jsonTypeInfo) where T: class;

    string? ServerPrefix { get; set; }

    bool BatchMode { get; set; }

    IList<Batch>? Batches { get; set; }
}
