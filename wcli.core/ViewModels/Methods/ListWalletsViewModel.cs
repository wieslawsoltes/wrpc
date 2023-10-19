using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Input;
using WasabiCli.Models.App;
using WasabiCli.Models.Info;
using WasabiCli.Models.Results;
using WasabiCli.Models.Services;
using WasabiCli.ViewModels.Factories;

namespace WasabiCli.ViewModels.Methods;

public partial class ListWalletsViewModel : RoutableMethodViewModel
{
    public ListWalletsViewModel(IRpcServiceViewModel rpcService, INavigationService navigationService)
        : base(rpcService, navigationService)
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

        return new Job(requestBody, rpcServerUri);
    }
}
