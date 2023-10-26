using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Input;
using WasabiRpc.ViewModels.Factories;
using WasabiRpc.Models.App;
using WasabiRpc.Models.BatchMode;
using WasabiRpc.Models.Results;
using WasabiRpc.Models.Services;

namespace WasabiRpc.ViewModels.Methods;

public partial class GetStatusViewModel : RoutableMethodViewModel
{
    public GetStatusViewModel(IRpcServiceViewModel rpcService, INavigationService navigationService, IBatchManager batchManager)
        : base(rpcService, navigationService, batchManager)
    {
    }

    [RelayCommand]
    private async Task GetStatus()
    {
        var job = CreateJob();

        if (RpcService.BatchMode)
        {
            OnBatch(job);
            return;
        }

        await Execute(job);
    }

    public override async Task Execute(Job job)
    {
        var result = await RpcService.Send<RpcGetStatusResult>(job.RpcMethod, job.RpcServerUri, NavigationService);
        if (result is RpcGetStatusResult { Result: not null } rpcGetStatusResult)
        {
            OnRpcSuccess(rpcGetStatusResult);
        }
        else if (result is RpcErrorResult { Error: not null } rpcErrorResult)
        {
            OnRpcError(rpcErrorResult);
        }
        else if (result is Error error)
        {
            OnError(error);
        }
    }

    protected override void OnRpcSuccess(Rpc rpcResult)
    {
        if (rpcResult is RpcGetStatusResult rpcGetStatusResult)
        {
            var statusInfoViewModel = rpcGetStatusResult.Result?.ToViewModel(RpcService, NavigationService);
            NavigationService.NavigateTo(statusInfoViewModel);
        }
    }

    public override Job CreateJob()
    {
        var requestBody = new RpcMethod
        {
            Method = "getstatus"
        };

        var rpcServerUri = $"{RpcService.ServerPrefix}";

        return new Job("getstatus", requestBody, rpcServerUri, typeof(RpcGetStatusResult));
    }
}
