using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using WasabiCli.Models;
using WasabiCli.Models.App;
using WasabiCli.Models.RpcJson;
using WasabiCli.Models.Services;

namespace WasabiCli.ViewModels.Methods;

public partial class LoadWalletViewModel : RpcMethodViewModel
{
    [ObservableProperty] private string? _walletName;

    public LoadWalletViewModel(IRpcServiceViewModel rpcService, INavigationService navigationService, string walletName)
    {
        RpcService = rpcService;
        NavigationService = navigationService;
        WalletName = walletName;
    }

    [RelayCommand]
    private async Task LoadWallet()
    {
        var job = CreateJob();

        if (RpcService.BatchMode)
        {
            OnBatch(job);
            return;
        }

        var result = await RpcService.Send<RpcLoadWalletResult>(job);
        if (result is RpcLoadWalletResult rpcLoadWalletResult)
        {
            OnRpcSuccess(rpcLoadWalletResult);
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
        NavigationService.Navigate(new Success { Message = $"Loaded wallet {WalletName}" });
    }

    public override Job CreateJob()
    {
        var requestBody = new RpcMethod
        {
            Method = "loadwallet",
            Params = new []
            {
                WalletName,
            }
        };

        var rpcServerUri = $"{RpcService.ServerPrefix}";

        return new Job(requestBody, rpcServerUri);
    }
}
