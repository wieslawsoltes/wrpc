using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Input;
using WasabiRpc.ViewModels.Factories;
using WasabiRpc.Models.App;
using WasabiRpc.Models.BatchMode;
using WasabiRpc.Models.Info;
using WasabiRpc.Models.Results;
using WasabiRpc.Models.Services;

namespace WasabiRpc.ViewModels.Methods;

public partial class ListWalletsViewModel : RoutableMethodViewModel
{
    public ListWalletsViewModel(IRpcServiceViewModel rpcService, INavigationService navigationService, IBatchManager batchManager)
        : base(rpcService, navigationService, batchManager)
    {
    }

    [RelayCommand]
    private async Task ListWallets()
    {
        var job = CreateJob();

        if (RpcService.BatchMode)
        {
            OnBatch(job);
            return;
        }

        var result = await RpcService.Send<RpcListWalletsResult>(job, NavigationService);
        if (result is RpcListWalletsResult { Result: not null } rpcListWalletsResult)
        {
            OnRpcSuccess(rpcListWalletsResult);
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
        if (rpcResult is RpcListWalletsResult rpcListWalletsResult)
        {
            NavigationService.NavigateTo(new ListWalletsInfo { Wallets = rpcListWalletsResult.Result }.ToViewModel(RpcService, NavigationService));
        }
    }

    public override Job CreateJob()
    {
        var requestBody = new RpcMethod
        {
            Method = "listwallets"
        };

        var rpcServerUri = $"{RpcService.ServerPrefix}";

        return new Job("listwallets", requestBody, rpcServerUri);
    }
}
