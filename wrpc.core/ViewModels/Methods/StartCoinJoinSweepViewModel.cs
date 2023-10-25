using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using WasabiRpc.ViewModels.Factories;
using WasabiRpc.Models.App;
using WasabiRpc.Models.BatchMode;
using WasabiRpc.Models.Results;
using WasabiRpc.Models.Services;

namespace WasabiRpc.ViewModels.Methods;

public partial class StartCoinJoinSweepViewModel : RoutableMethodViewModel
{
    [ObservableProperty] private string? _walletName;
    [ObservableProperty] private string? _walletPassword;
    [ObservableProperty] private string? _outputWalletName;

    public StartCoinJoinSweepViewModel(IRpcServiceViewModel rpcService, INavigationService navigationService, IBatchManager batchManager, string walletName)
        : base(rpcService, navigationService, batchManager)
    {
        WalletName = walletName;
        WalletPassword = "";
        OutputWalletName = "";
    }

    [RelayCommand]
    private async Task StartCoinJoinSweep()
    {
        var job = CreateJob();

        if (RpcService.BatchMode)
        {
            OnBatch(job);
            return;
        }

        await Execute(job);
    }

    public override async Task Execute(Job job)
    {
        var result = await RpcService.Send<RpcStartCoinJoinSweepResult>(job.RpcMethod, job.RpcServerUri, NavigationService);
        if (result is RpcStartCoinJoinSweepResult rpcStartCoinJoinSweepResult)
        {
            OnRpcSuccess(rpcStartCoinJoinSweepResult);
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
        NavigationService.ClearAndNavigateTo(new Success { Message = $"Started coinjoin for wallet {WalletName}" }.ToViewModel(RpcService, NavigationService));
    }

    public override Job CreateJob()
    {
        var requestBody = new RpcMethod
        {
            Method = "startcoinjoinsweep",
            Params = new []
            {
                WalletPassword,
                OutputWalletName,
            }
        };

        var rpcServerUri = $"{RpcService.ServerPrefix}/{WalletName}";

        return new Job("startcoinjoinsweep", requestBody, rpcServerUri, typeof(RpcStartCoinJoinSweepResult));
    }
}
