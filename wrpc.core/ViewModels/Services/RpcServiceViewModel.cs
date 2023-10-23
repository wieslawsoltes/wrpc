using System;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using WasabiRpc.Models;
using WasabiRpc.Models.App;
using WasabiRpc.Models.Results;
using WasabiRpc.Models.Services;

namespace WasabiRpc.ViewModels.Services;

public partial class RpcServiceViewModel : ViewModelBase, IRpcServiceViewModel
{
    private readonly IHttpService _httpService;
    [ObservableProperty] private string? _serverPrefix;
    [ObservableProperty] private bool _batchMode;

    public RpcServiceViewModel(
        IHttpService httpService, 
        string serverPrefix, 
        bool batchMode)
    {
        _httpService = httpService;

        ServerPrefix = serverPrefix;
        BatchMode = batchMode;
    }

    public async Task<object?> Send<TResult>(Job job, INavigationService navigationService) where TResult: class
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
