using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using WasabiRpc.ViewModels.Factories;
using WasabiRpc.Models.App;
using WasabiRpc.Models.BatchMode;
using WasabiRpc.Models.Info;
using WasabiRpc.Models.Params.Transactions;
using WasabiRpc.Models.Rpc;
using WasabiRpc.Models.Rpc.Methods;
using WasabiRpc.Models.Rpc.Results;
using WasabiRpc.Models.Services;

namespace WasabiRpc.ViewModels.Methods;

public partial class SpeedUpTransactionViewModel : RoutableMethodViewModel
{
    [NotifyCanExecuteChangedFor(nameof(SpeedUpTransactionCommand))]
    [ObservableProperty] 
    private string? _walletName;

    [NotifyCanExecuteChangedFor(nameof(SpeedUpTransactionCommand))]
    [ObservableProperty]
    private string? _walletPassword;

    [NotifyCanExecuteChangedFor(nameof(SpeedUpTransactionCommand))]
    [ObservableProperty]
    private string? _txId;

    public SpeedUpTransactionViewModel(
        IRpcServiceViewModel rpcService, 
        INavigationService navigationService,
        INavigationService detailsNavigationService, 
        IBatchManager batchManager, string? walletName)
        : base(rpcService, navigationService, detailsNavigationService, batchManager)
    {
        WalletName = walletName;
        WalletPassword = "";
        TxId = "";
    }

    private bool CanSpeedUpTransaction()
    {
        return WalletName is not null 
               && WalletName.Length > 0
               && WalletPassword is not null
               && TxId is not null 
               && TxId.Length > 0;
    }

    [RelayCommand(CanExecute = nameof(CanSpeedUpTransaction))]
    private async Task SpeedUpTransaction()
    {
        await RunCommand();
    }

    public override async Task<IRoutable?> Execute(Job job)
    {
        var result = await RpcService.Send<RpcSpeedUpTransactionResult>(job.RpcMethod, job.RpcServerUri);
        return ToJobResult(result);
    }

    public override IRoutable? ToJobResult(object? result)
    {
        if (result is RpcSpeedUpTransactionResult { Result: not null } rpcSpeedUpTransactionResult)
        {
            return new BuildInfo { Tx = rpcSpeedUpTransactionResult.Result }.ToViewModel(RpcService, NavigationService, DetailsNavigationService);
        }

        if (result is RpcErrorResult { Error: not null } rpcErrorResult)
        {
            return rpcErrorResult.Error?.ToViewModel(RpcService, NavigationService, DetailsNavigationService);
        }

        if (result is Error error)
        {
            return error.ToViewModel(RpcService, NavigationService, DetailsNavigationService);
        }

        return null;
    }

    public override Job CreateJob()
    {
        var requestBody = new SpeedUpTransactionRpcMethod
        {
            Method = "speeduptransaction",
            Params = new SpeedUpTransaction
            {
                TxId = TxId,
                Password = WalletPassword
            }
        };

        var rpcServerUri = $"{RpcService.ServerPrefix}/{WalletName}";

        return new Job("speeduptransaction", requestBody, rpcServerUri);
    }
}
