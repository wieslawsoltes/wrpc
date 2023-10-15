using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using WasabiCli.Models;
using WasabiCli.Models.App;
using WasabiCli.Models.RpcJson;
using WasabiCli.Models.Services;

namespace WasabiCli.ViewModels.Methods;

public partial class StopCoinJoinViewModel : RpcMethodViewModel
{
    [ObservableProperty] private string? _walletName;

    public StopCoinJoinViewModel(IRpcServiceViewModel rpcService, INavigationService navigationService, string walletName)
    {
        RpcService = rpcService;
        NavigationService = navigationService;
        WalletName = walletName;
    }

    [RelayCommand]
    private async Task StopCoinJoin()
    {
        var job = CreateJob();

        if (RpcService.BatchMode)
        {
            OnBatch(job);
            return;
        }

        var result = await RpcService.Send<RpcStopCoinJoinResult>(job);
        if (result is RpcStopCoinJoinResult rpcStopCoinJoinResult)
        {
            OnRpcSuccess(rpcStopCoinJoinResult);
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
        NavigationService.Navigate(new Success { Message = $"Stopped coinjoin for wallet {WalletName}" });
    }

    public override Job CreateJob()
    {
        var requestBody = new RpcMethod
        {
            Method = "stopcoinjoin"
        };

        var rpcServerUri = $"{RpcService.ServerPrefix}/{WalletName}";

        return new Job(requestBody, rpcServerUri);
    }
}
