using System;
using System.Collections.Generic;
using System.Text.Json;
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
    private readonly IHttpService _httpService;
    [ObservableProperty] private string? _serverPrefix;
    [ObservableProperty] private bool _batchMode;
    [ObservableProperty] private IList<Batch>? _batches;
    [ObservableProperty] private Batch? _currentBatch;

    public RpcServiceViewModel(IHttpService httpService, string serverPrefix, bool batchMode)
    {
        _httpService = httpService;
        ServerPrefix = serverPrefix;
        BatchMode = batchMode;
    }

    public async Task<object?> Send<TResult>(Job job) where TResult: class
    {
        string? responseBodyJson;

        try
        {
            var requestBodyJson = JsonSerializer.Serialize(job.RpcMethod, typeof(RpcMethod), ModelsJsonContext.Default);
            var cts = new CancellationTokenSource();
            responseBodyJson = await _httpService.GetResponseDataAsync(job.RpcServerUri, requestBodyJson, cts.Token);
            if (responseBodyJson is null)
            {
                return new Error { Message = "Invalid response."};
            }
        }
        catch (Exception e)
        {
            return new Error { Message = $"{e.Message}"};
        }

        if (typeof(TResult) == typeof(string))
        {
            return responseBodyJson;
        }
        
        try
        {
            return JsonSerializer.Deserialize(responseBodyJson, typeof(RpcErrorResult), ModelsJsonContext.Default);
        }
        catch (Exception)
        {
            // ignored
        }

        try
        {
            var okResult = JsonSerializer.Deserialize(responseBodyJson, typeof(TResult), ModelsJsonContext.Default);
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
