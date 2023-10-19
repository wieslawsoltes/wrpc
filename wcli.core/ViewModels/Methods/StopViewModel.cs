using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Input;
using WasabiCli.Models.App;
using WasabiCli.Models.Results;
using WasabiCli.Models.Services;
using WasabiCli.ViewModels.Factories;

namespace WasabiCli.ViewModels.Methods;

public partial class StopViewModel : RoutableMethodViewModel
{
    public StopViewModel(IRpcServiceViewModel rpcService, INavigationService navigationService)
        : base(rpcService, navigationService)
    {
    }

    [RelayCommand]
    private async Task Stop()
    {
        var job = CreateJob();

        if (RpcService.BatchMode)
        {
            OnBatch(job);
            return;
        }

        var result = await RpcService.Send<string>(job, NavigationService);
        if (result is string)
        {
            OnRpcSuccess(new RpcResult());
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
        NavigationService.NavigateTo(new Success { Message = "Stopped daemon." }.ToViewModel(RpcService, NavigationService));
    }

    public override Job CreateJob()
    {
        var requestBody = new RpcMethod
        {
            Method = "stop"
        };

        var rpcServerUri = $"{RpcService.ServerPrefix}";

        return new Job(requestBody, rpcServerUri);
    }
}
