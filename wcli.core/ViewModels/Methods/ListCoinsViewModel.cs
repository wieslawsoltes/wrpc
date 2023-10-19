using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using WasabiCli.Models.App;
using WasabiCli.Models.Info;
using WasabiCli.Models.Results;
using WasabiCli.Models.Services;
using WasabiCli.ViewModels.Factories;

namespace WasabiCli.ViewModels.Methods;

public partial class ListCoinsViewModel : RoutableMethodViewModel
{
    [ObservableProperty] private string? _walletName;

    public ListCoinsViewModel(IRpcServiceViewModel rpcService, INavigationService navigationService, string walletName)
        : base(rpcService, navigationService)
    {
        WalletName = walletName;
    }

    [RelayCommand]
    private async Task ListCoins()
    {
        var job = CreateJob();

        if (RpcService.BatchMode)
        {
            OnBatch(job);
            return;
        }

        var result = await RpcService.Send<RpcListCoinsResult>(job, NavigationService);
        if (result is RpcListCoinsResult { Result: not null } rpcListCoinsResult)
        {
            OnRpcSuccess(rpcListCoinsResult);
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
        if (rpcResult is RpcListCoinsResult rpcListCoinsResult)
        {
            NavigationService.NavigateTo(new ListCoinsInfo { Coins = rpcListCoinsResult.Result }.ToViewModel(RpcService, NavigationService));
        }
    }

    public override Job CreateJob()
    {
        var requestBody = new RpcMethod
        {
            Method = "listcoins"
        };

        var rpcServerUri = $"{RpcService.ServerPrefix}/{WalletName}";

        return new Job(requestBody, rpcServerUri);
    }
}
