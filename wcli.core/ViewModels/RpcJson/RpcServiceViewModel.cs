using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text.Json;
using System.Text.Json.Serialization.Metadata;
using System.Threading;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using WasabiCli.Models;
using WasabiCli.Models.App;
using WasabiCli.Models.Services;
using WasabiCli.Models.RpcJson;
using WasabiCli.Services;

namespace WasabiCli.ViewModels.RpcJson;

public partial class RpcServiceViewModel : ViewModelBase, IRpcServiceViewModel
{
    [ObservableProperty] private string? _serverPrefix;
    [ObservableProperty] private bool _batchMode;
    [ObservableProperty] private IList<Batch>? _batches;
    [ObservableProperty] private Batch? _currentBatch;

    public RpcServiceViewModel(string serverPrefix, bool batchMode)
    {
        ServerPrefix = serverPrefix;
        BatchMode = batchMode;
    }

    public async Task<object?> Send<T>(Job job, JsonTypeInfo<T> jsonTypeInfo) 
        where T: class
    {
        string? responseBodyJson;

        try
        {
            var requestBodyJson = JsonSerializer.Serialize(job.RpcMethod, ModelsJsonContext.Default.RpcMethod);
            var cts = new CancellationTokenSource();
            var rpcService = new RpcService();
            responseBodyJson = await rpcService.GetResponseDataAsync(job.RpcServerUri, requestBodyJson, true, cts.Token);
            if (responseBodyJson is null)
            {
                return new Error { Message = "Invalid response."};
            }
        }
        catch (Exception e)
        {
            return new Error { Message = $"{e.Message}"};
        }

        if (jsonTypeInfo.Type == typeof(string))
        {
            return responseBodyJson;
        }
        
        try
        {
            return JsonSerializer.Deserialize(responseBodyJson, ModelsJsonContext.Default.RpcErrorResult);
        }
        catch (Exception)
        {
            // ignored
        }

        try
        {
            var okResult = JsonSerializer.Deserialize(responseBodyJson, jsonTypeInfo);
            if (okResult is not null)
            {
                return okResult;
            }
        }
        catch (Exception)
        {
            // ignored
        }

        return default;
    }
}
