using System;
using System.Text.Json;
using System.Text.Json.Serialization.Metadata;
using System.Threading;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using WasabiCli.Models.RpcJson;
using WasabiCli.Services;

namespace WasabiCli.ViewModels;

public partial class RpcServiceViewModel : ViewModelBase
{
    [ObservableProperty] private string? _rpcServerPrefix;

    public RpcServiceViewModel(string rpcServerPrefix)
    {
        RpcServerPrefix = rpcServerPrefix;
    }

    public async Task<Rpc?> SendRpcMethod<T>(RpcMethod rpcMethod, string rpcServerUri, JsonTypeInfo<T> jsonTypeInfo) 
        where T: Rpc
    {
        var requestBodyJson = JsonSerializer.Serialize(rpcMethod, RpcJsonContext.Default.RpcMethod);
        var cts = new CancellationTokenSource();
        var rpcService = new RpcService();
        var responseBodyJson = await rpcService.GetResponseDataAsync(rpcServerUri, requestBodyJson, true, cts.Token);
        if (responseBodyJson is null)
        {
            return default;
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

        try
        {
            return JsonSerializer.Deserialize(responseBodyJson, RpcJsonContext.Default.RpcErrorResult);
        }
        catch (Exception)
        {
            // ignored
        }

        return default;
    }
}
