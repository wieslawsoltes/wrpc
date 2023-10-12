using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Input;
using WasabiCli.Models;
using WasabiCli.Models.App;
using WasabiCli.Models.RpcJson;
using WasabiCli.Models.Services;

namespace WasabiCli.ViewModels.Methods;

public partial class GetStatusViewModel : BatchMethodViewModel
{
    public GetStatusViewModel(IRpcServiceViewModel rpcService, INavigationService navigationService)
    {
        RpcService = rpcService;
        NavigationService = navigationService;
    }

    private IRpcServiceViewModel RpcService { get; }

    private INavigationService NavigationService { get; }

    [RelayCommand]
    private async Task GetStatus()
    {
        var job = CreateJob();
        var result = await RpcService.SendRpcMethod(job.RpcMethod, job.RpcServerUri, ModelsJsonContext.Default.RpcGetStatusResult);
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
            NavigationService.Navigate(rpcGetStatusResult.Result);
        }
    }

    protected override void OnRpcError(RpcErrorResult rpcErrorResult)
    {
        NavigationService.Navigate(rpcErrorResult.Error);
    }

    protected override void OnError(Error error)
    {
        NavigationService.Navigate(error);
    }

    public override Job CreateJob()
    {
        var requestBody = new RpcMethod
        {
            Method = "getstatus"
        };

        var rpcServerUri = $"{RpcService.RpcServerPrefix}";

        return new Job(requestBody, rpcServerUri);
    }
}
