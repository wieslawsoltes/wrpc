using System;
using System.Text.Json;
using System.Text.Json.Serialization.Metadata;
using System.Threading;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using WasabiCli.Models;
using WasabiCli.Models.RpcJson;
using WasabiCli.Services;

namespace WasabiCli.ViewModels.RpcJson;

public partial class RpcServiceViewModel : ViewModelBase
{
    [ObservableProperty] private string? _rpcServerPrefix;

    public RpcServiceViewModel(string rpcServerPrefix)
    {
        RpcServerPrefix = rpcServerPrefix;
    }

    public async Task<object?> SendRpcMethod<T>(RpcMethod rpcMethod, string rpcServerUri, JsonTypeInfo<T> jsonTypeInfo) 
        where T: class
    {
        string? responseBodyJson;

        try
        {
            var requestBodyJson = JsonSerializer.Serialize(rpcMethod, RpcJsonContext.Default.RpcMethod);
            var cts = new CancellationTokenSource();
            var rpcService = new RpcService();
            responseBodyJson = await rpcService.GetResponseDataAsync(rpcServerUri, requestBodyJson, true, cts.Token);
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
            return JsonSerializer.Deserialize(responseBodyJson, RpcJsonContext.Default.RpcErrorResult);
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
