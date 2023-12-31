using System;
using System.Text.Json;
using System.Threading.Tasks;
using WasabiRpc.ViewModels.Factories;
using WasabiRpc.Models;
using WasabiRpc.Models.App;
using WasabiRpc.Models.BatchMode;
using WasabiRpc.Models.Services;
using WasabiRpc.ViewModels.App;
using WasabiRpc.ViewModels.BatchMode;
using WasabiRpc.ViewModels.Info;

namespace WasabiRpc.ViewModels;

public abstract partial class RoutableMethodViewModel : RoutableViewModel
{
    protected RoutableMethodViewModel(
        IRpcServiceViewModel rpcService, 
        INavigationService navigationService,
        INavigationService detailsNavigationService, 
        IBatchManager batchManager)
        : base(rpcService, navigationService, detailsNavigationService)
    {
        BatchManager = batchManager;
    }

    protected IBatchManager BatchManager { get; }

    protected virtual async Task RunCommand()
    {
        var job = CreateJob();

        if (RpcService.BatchMode)
        {
            OnBatch(job);
            return;
        }

        var routable = await Execute(job);
        if (routable is not null)
        {
            if (routable is ErrorInfoViewModel errorInfoViewModel)
            {
                OnRpcError(errorInfoViewModel);
            }
            else if (routable is ErrorViewModel errorViewModel)
            {
                OnError(errorViewModel);
            }
            else
            {
                OnRpcSuccess(routable);
            }
        }
    }

    protected virtual void OnRpcSuccess(IRoutable routable)
    {
        DetailsNavigationService.Clear();
        NavigationService.ClearAndNavigateTo(routable);
    }

    protected virtual void OnRpcError(IRoutable routable)
    {
        DetailsNavigationService.Clear();
        NavigationService.NavigateTo(routable);
    }

    protected virtual void OnError(IRoutable routable)
    {
        DetailsNavigationService.Clear();
        NavigationService.NavigateTo(routable);
    }

    protected virtual void OnBatch(Job job)
    {
        try
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            var json = JsonSerializer.Serialize(job, typeof(Job), new ModelsJsonContext(options));
            var addJobViewModel = new AddJobViewModel(RpcService, NavigationService, DetailsNavigationService, BatchManager, job, json);
            DetailsNavigationService.Clear();
            NavigationService.ClearAndNavigateTo(addJobViewModel);
        }
        catch (Exception e)
        {
            var errorViewModel = new Error { Message = e.Message }.ToViewModel(RpcService, NavigationService, DetailsNavigationService);
            DetailsNavigationService.Clear();
            NavigationService.NavigateTo(errorViewModel);
        }
    }

    public abstract IRoutable? ToJobResult(object? result);

    public abstract Job CreateJob();

    public abstract Task<IRoutable?> Execute(Job job);
}
