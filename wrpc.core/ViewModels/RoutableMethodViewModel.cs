using System;
using System.Text.Json;
using System.Threading.Tasks;
using WasabiRpc.ViewModels.Factories;
using WasabiRpc.Models;
using WasabiRpc.Models.App;
using WasabiRpc.Models.BatchMode;
using WasabiRpc.Models.Results;
using WasabiRpc.Models.Services;
using WasabiRpc.ViewModels.BatchMode;

namespace WasabiRpc.ViewModels;

public abstract partial class RoutableMethodViewModel : RoutableViewModel
{
    protected RoutableMethodViewModel(IRpcServiceViewModel rpcService, INavigationService navigationService, IBatchManager batchManager)
        : base(rpcService, navigationService)
    {
        BatchManager = batchManager;
    }

    protected IBatchManager BatchManager { get; }

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
            NavigationService.ClearAndNavigateTo(new AddJobViewModel(RpcService, NavigationService, BatchManager, job, json));
        }
        catch (Exception e)
        {
            NavigationService.NavigateTo(new Error { Message = e.Message }.ToViewModel(RpcService, NavigationService));
        }
    }

    public abstract Job CreateJob();

    public abstract Task Execute(Job job);
}
