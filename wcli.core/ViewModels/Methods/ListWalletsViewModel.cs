using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Input;
using WasabiCli.Models;
using WasabiCli.Models.App;
using WasabiCli.Models.RpcJson;
using WasabiCli.Models.Services;
using WasabiCli.Models.WalletWasabi;

namespace WasabiCli.ViewModels.Methods;

public partial class ListWalletsViewModel : RpcMethodViewModel
{
    public ListWalletsViewModel(IRpcServiceViewModel rpcService, INavigationService navigationService)
    {
        RpcService = rpcService;
        NavigationService = navigationService;
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

        var result = await RpcService.Send<RpcListWalletsResult>(job);
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
            NavigationService.Navigate(new ListWalletsInfo { Wallets = rpcListWalletsResult.Result });
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
