using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Input;
using WasabiRpc.ViewModels.Factories;
using WasabiRpc.Models.App;
using WasabiRpc.Models.BatchMode;
using WasabiRpc.Models.Results;
using WasabiRpc.Models.Services;

namespace WasabiRpc.ViewModels.Methods;

public partial class StopViewModel : RoutableMethodViewModel
{
    public StopViewModel(IRpcServiceViewModel rpcService, INavigationService navigationService, IBatchManager batchManager)
        : base(rpcService, navigationService, batchManager)
    {
    }

    [RelayCommand]
    private async Task Stop()
    {
        await RunCommand();
    }

    public override async Task<IRoutable?> Execute(Job job)
    {
        var result = await RpcService.Send<string>(job.RpcMethod, job.RpcServerUri, NavigationService);
        if (result is string)
        {
            return new Success { Message = "Stopped daemon." }.ToViewModel(RpcService, NavigationService);
        }

        if (result is RpcErrorResult { Error: not null } rpcErrorResult)
        {
            return rpcErrorResult.Error?.ToViewModel(RpcService, NavigationService);
        }

        if (result is Error error)
        {
            return error.ToViewModel(RpcService, NavigationService);
        }

        return null;
    }

    public override Job CreateJob()
    {
        var requestBody = new RpcMethod
        {
            Method = "stop"
        };

        var rpcServerUri = $"{RpcService.ServerPrefix}";

        return new Job("stop", requestBody, rpcServerUri, typeof(string));
    }
}
