using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using WasabiRpc.Models;
using WasabiRpc.Models.App;
using WasabiRpc.Models.Results;
using WasabiRpc.Models.Services;
using WasabiRpc.ViewModels.BatchMode;

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

    public async Task<object?> Send<TResult>(RpcMethod rpcMethod, string rpcServerUri, INavigationService navigationService) where TResult: class
    {
        string? responseBodyJson;

        try
        {
            var requestBodyJson = JsonSerializer.Serialize(rpcMethod, typeof(RpcMethod), ModelsJsonContext.Default);
            var cts = new CancellationTokenSource();
            responseBodyJson = await _httpService.GetResponseDataAsync(rpcServerUri, requestBodyJson, cts.Token);
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

    public async Task<object?> Send(RpcMethod[] rpcMethods, string rpcServerUri, INavigationService navigationService)
    {
        string? responseBodyJson;

        try
        {
            var requestBodyJson = JsonSerializer.Serialize(rpcMethods, typeof(RpcMethod[]), ModelsJsonContext.Default);
            var cts = new CancellationTokenSource();
            responseBodyJson = await _httpService.GetResponseDataAsync(rpcServerUri, requestBodyJson, cts.Token);
            if (responseBodyJson is null)
            {
                return new Error { Message = "Invalid response."};
            }
        }
        catch (Exception e)
        {
            return new Error { Message = $"{e.Message}"};
        }

        using var jsonDocument = JsonDocument.Parse(responseBodyJson);

        if (jsonDocument.RootElement.ValueKind != JsonValueKind.Array)
        {
            return default;
        }

        var length = jsonDocument.RootElement.GetArrayLength();
        if (length != rpcMethods.Length)
        {
            return default;
        }

        var results = new List<object?>();

        for (var i = 0; i < length; i++)
        {
            var jsonElement = jsonDocument.RootElement[i];

            if (rpcMethods[i].Method is null)
            {
                results.Add(default);
                continue;
            }
    
            RpcMethodResultTypeRegistry.Results.TryGetValue(rpcMethods[i].Method!, out var resultType);
            if (resultType is null)
            {
                results.Add(default);
                continue;
            }

            if (resultType == typeof(string))
            {
                results.Add(responseBodyJson);
                continue;
            }

            try
            {
                var errorResult = jsonElement.Deserialize(typeof(RpcErrorResult), ModelsJsonContext.Default);
                if (errorResult is not null)
                {
                    results.Add(errorResult);
                    continue;
                }
            }
            catch (Exception)
            {
                // ignored
            }

            try
            {
                var okResult = jsonElement.Deserialize(resultType, ModelsJsonContext.Default);
                if (okResult is not null)
                {
                    results.Add(okResult);
                    continue;
                }
            }
            catch (Exception)
            {
                // ignored
            }

            results.Add(default);
        }

        return results;
    }
}
