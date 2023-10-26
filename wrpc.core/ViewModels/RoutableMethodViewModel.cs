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
        var errorInfoViewModel = rpcErrorResult.Error?.ToViewModel(RpcService, NavigationService);
        NavigationService.NavigateTo(errorInfoViewModel);
    }

    protected virtual void OnError(Error error)
    {
        var errorViewModel = error.ToViewModel(RpcService, NavigationService);
        NavigationService.NavigateTo(errorViewModel);
    }

    protected virtual void OnBatch(Job job)
    {
        try
        {
            var addJobViewModel = ToJobViewModel(job);
            NavigationService.ClearAndNavigateTo(addJobViewModel);
        }
        catch (Exception e)
        {
            var errorViewModel = new Error { Message = e.Message }.ToViewModel(RpcService, NavigationService);
            NavigationService.NavigateTo(errorViewModel);
        }
    }

    private AddJobViewModel ToJobViewModel(Job job)
    {
        var options = new JsonSerializerOptions { WriteIndented = true };
        var json = JsonSerializer.Serialize(job, typeof(Job), new ModelsJsonContext(options));
        return new AddJobViewModel(RpcService, NavigationService, BatchManager, job, json);
    }

    public abstract Job CreateJob();

    public abstract Task Execute(Job job);
}
