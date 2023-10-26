using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using WasabiRpc.ViewModels.Factories;
using WasabiRpc.Models.App;
using WasabiRpc.Models.BatchMode;
using WasabiRpc.Models.Info;
using WasabiRpc.Models.Results;
using WasabiRpc.Models.Services;

namespace WasabiRpc.ViewModels.Methods;

public partial class ListCoinsViewModel : RoutableMethodViewModel
{
    [ObservableProperty] private string? _walletName;

    public ListCoinsViewModel(IRpcServiceViewModel rpcService, INavigationService navigationService, IBatchManager batchManager, string walletName)
        : base(rpcService, navigationService, batchManager)
    {
        WalletName = walletName;
    }

    [RelayCommand]
    private async Task ListCoins()
    {
        await RunCommand();
    }

    public override async Task<IRoutable?> Execute(Job job)
    {
        var result = await RpcService.Send<RpcListCoinsResult>(job.RpcMethod, job.RpcServerUri, NavigationService);
        if (result is RpcListCoinsResult { Result: not null } rpcListCoinsResult)
        {
            return new ListCoinsInfo { Coins = rpcListCoinsResult.Result }.ToViewModel(RpcService, NavigationService);
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
            Method = "listcoins"
        };

        var rpcServerUri = $"{RpcService.ServerPrefix}/{WalletName}";

        return new Job("listcoins", requestBody, rpcServerUri, typeof(RpcListCoinsResult));
    }
}
