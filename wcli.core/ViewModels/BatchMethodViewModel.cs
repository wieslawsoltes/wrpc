using System;
using System.Text.Json;
using WasabiCli.Models;
using WasabiCli.Models.App;
using WasabiCli.Models.RpcJson;
using WasabiCli.Models.Services;

namespace WasabiCli.ViewModels;

public abstract partial class BatchMethodViewModel : ViewModelBase
{
    protected IRpcServiceViewModel RpcService { get; init; }

    protected INavigationService NavigationService { get; init; }

    protected abstract void OnRpcSuccess(Rpc rpcResult);

    protected virtual void OnRpcError(RpcErrorResult rpcErrorResult)
    {
        NavigationService.Navigate(rpcErrorResult.Error);
    }

    protected virtual void OnError(Error error)
    {
        NavigationService.Navigate(error);
    }

    protected virtual void OnBatch(Job job)
    {
        try
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            var json = JsonSerializer.Serialize(job, typeof(Job), new ModelsJsonContext(options));

            NavigationService.Clear();
            NavigationService.Navigate(new Json { Content = json });
        }
        catch (Exception e)
        {
            NavigationService.Navigate(new Error { Message = e.Message });
        }
    }

    public abstract Job CreateJob();
}
