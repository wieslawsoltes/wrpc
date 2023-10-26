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

public partial class ListUnspentCoinsViewModel : RoutableMethodViewModel
{
    [ObservableProperty] private string? _walletName;

    public ListUnspentCoinsViewModel(IRpcServiceViewModel rpcService, INavigationService navigationService, IBatchManager batchManager, string walletName)
        : base(rpcService, navigationService, batchManager)
    {
        WalletName = walletName;
    }

    private bool CanListUnspentCoins()
    {
        return true;
    }

    [RelayCommand(CanExecute = nameof(CanListUnspentCoins))]
    private async Task ListUnspentCoins()
    {
        await RunCommand();
    }

    public override async Task<IRoutable?> Execute(Job job)
    {
        var result = await RpcService.Send<RpcListUnspentCoinsResult>(job.RpcMethod, job.RpcServerUri, NavigationService);
        if (result is RpcListUnspentCoinsResult  { Result: not null } rpcListUnspentCoinsResult && WalletName is not null)
        {
            return new ListUnspentCoinsInfo { Coins = rpcListUnspentCoinsResult.Result }.ToViewModelAdapter(RpcService, NavigationService, BatchManager, WalletName);
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
            Method = "listunspentcoins"
        };

        var rpcServerUri = $"{RpcService.ServerPrefix}/{WalletName}";

        return new Job("listunspentcoins", requestBody, rpcServerUri, typeof(RpcListUnspentCoinsResult));
    }
}
