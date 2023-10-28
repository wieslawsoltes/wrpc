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

    public StartCoinJoinSweepViewModel(IRpcServiceViewModel rpcService, INavigationService navigationService, IBatchManager batchManager, string? walletName)
        : base(rpcService, navigationService, batchManager)
    {
        WalletName = walletName;
        WalletPassword = "";
        OutputWalletName = "";
    }

    [RelayCommand]
    private async Task StartCoinJoinSweep()
    {
        await RunCommand();
    }

    public override async Task<IRoutable?> Execute(Job job)
    {
        var result = await RpcService.Send<RpcStartCoinJoinSweepResult>(job.RpcMethod, job.RpcServerUri);
        return ToJobResult(result);
    }

    public override IRoutable? ToJobResult(object? result)
    {
        if (result is RpcStartCoinJoinSweepResult)
        {
            return new Success { Message = $"Started coinjoin for wallet {WalletName}" }.ToViewModel(RpcService, NavigationService);
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
        var requestBody = new StartCoinJoinSweepRpcMethod
        {
            Method = "startcoinjoinsweep",
            Params = new []
            {
                WalletPassword,
                OutputWalletName,
            }
        };

        var rpcServerUri = $"{RpcService.ServerPrefix}/{WalletName}";

        return new Job("startcoinjoinsweep", requestBody, rpcServerUri);
    }
}
