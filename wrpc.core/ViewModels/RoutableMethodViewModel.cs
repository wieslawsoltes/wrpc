using System;
using System.Text.Json;
using WasabiRpc.ViewModels.Factories;
using WasabiRpc.Models;
using WasabiRpc.Models.App;
using WasabiRpc.Models.Results;
using WasabiRpc.Models.Services;

namespace WasabiRpc.ViewModels;

public abstract partial class RoutableMethodViewModel : RoutableViewModel
{
    protected RoutableMethodViewModel(IRpcServiceViewModel rpcService, INavigationService navigationService)
        : base(rpcService, navigationService)
    {
    }

    protected abstract void OnRpcSuccess(Rpc rpcResult);

    protected virtual void OnRpcError(RpcErrorResult rpcErrorResult)
    {
        NavigationService.NavigateTo(rpcErrorResult.Error?.ToViewModel(RpcService, NavigationService));
    }

    protected virtual void OnError(Error error)
    {
        NavigationService.NavigateTo(error.ToViewModel(RpcService, NavigationService));
    }

    protected virtual void OnBatch(Job job)
    {
        try
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            var json = JsonSerializer.Serialize(job, typeof(Job), new ModelsJsonContext(options));
            NavigationService.ClearAndNavigateTo(new Json { Content = json }.ToViewModel(RpcService, NavigationService));
        }
        catch (Exception e)
        {
            NavigationService.NavigateTo(new Error { Message = e.Message }.ToViewModel(RpcService, NavigationService));
        }
    }

    public abstract Job CreateJob();
}
