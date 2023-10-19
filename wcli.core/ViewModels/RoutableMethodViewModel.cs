using System;
using System.Text.Json;
using WasabiCli.Models;
using WasabiCli.Models.App;
using WasabiCli.Models.Results;
using WasabiCli.Models.Services;
using WasabiCli.ViewModels.Factories;

namespace WasabiCli.ViewModels;

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
