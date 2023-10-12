using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using WasabiCli.Models;
using WasabiCli.Models.App;
using WasabiCli.Models.RpcJson;
using WasabiCli.Models.Services;

namespace WasabiCli.ViewModels.Methods;

public partial class GetWalletInfoViewModel : BatchMethodViewModel
{
    [ObservableProperty] private string? _walletName;

    public GetWalletInfoViewModel(IRpcServiceViewModel rpcService, INavigationService navigationService, string walletName)
    {
        RpcService = rpcService;
        NavigationService = navigationService;
        WalletName = walletName;
    }

    private IRpcServiceViewModel RpcService { get; }

    private INavigationService NavigationService { get; }

    [RelayCommand]
    private async Task GetWalletInfo()
    {
        var job = CreateJob();
        var result = await RpcService.SendRpcMethod(job.RpcMethod, job.RpcServerUri, ModelsJsonContext.Default.RpcGetWalletInfoResult);
        if (result is RpcGetWalletInfoResult { Result: not null } rpcGetWalletInfoResult)
        {
            OnRpcSuccess(rpcGetWalletInfoResult);
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
        if (rpcResult is RpcGetWalletInfoResult rpcGetWalletInfoResult)
        {
            NavigationService.Navigate(rpcGetWalletInfoResult.Result);
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
            Method = "getwalletinfo"
        };

        var rpcServerUri = $"{RpcService.RpcServerPrefix}/{WalletName}";

        return new Job(requestBody, rpcServerUri);
    }
}
