using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Input;
using WasabiRpc.ViewModels.Factories;
using WasabiRpc.Models.App;
using WasabiRpc.Models.BatchMode;
using WasabiRpc.Models.Info;
using WasabiRpc.Models.Rpc;
using WasabiRpc.Models.Rpc.Methods;
using WasabiRpc.Models.Rpc.Results;
using WasabiRpc.Models.Services;

namespace WasabiRpc.ViewModels.Methods;

public partial class ListWalletsViewModel : RoutableMethodViewModel
{
    public ListWalletsViewModel(
        IRpcServiceViewModel rpcService, 
        INavigationService navigationService,
        INavigationService detailsNavigationService, 
        IBatchManager batchManager)
        : base(rpcService, navigationService, detailsNavigationService, batchManager)
    {
    }

    [RelayCommand]
    private async Task ListWallets()
    {
        await RunCommand();
    }

    public override async Task<IRoutable?> Execute(Job job)
    {
        var result = await RpcService.Send<RpcListWalletsResult>(job.RpcMethod, job.RpcServerUri);
        return ToJobResult(result);
    }

    public override IRoutable? ToJobResult(object? result)
    {
        if (result is RpcListWalletsResult { Result: not null } rpcListWalletsResult)
        {
            return new ListWalletsInfo { Wallets = rpcListWalletsResult.Result }.ToViewModel(RpcService, NavigationService, DetailsNavigationService, ListWalletsCommand);
        }

        if (result is RpcErrorResult { Error: not null } rpcErrorResult)
        {
            return rpcErrorResult.Error?.ToViewModel(RpcService, NavigationService, DetailsNavigationService);
        }

        if (result is Error error)
        {
            return error.ToViewModel(RpcService, NavigationService, DetailsNavigationService);
        }

        return null;
    }

    public override Job CreateJob()
    {
        var requestBody = new ListWalletsRpcMethod
        {
            Method = "listwallets"
        };

        var rpcServerUri = $"{RpcService.ServerPrefix}";

        return new Job("listwallets", requestBody, rpcServerUri);
    }
}
