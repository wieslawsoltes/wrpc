using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using WasabiRpc.ViewModels.Factories;
using WasabiRpc.Models.App;
using WasabiRpc.Models.BatchMode;
using WasabiRpc.Models.Info;
using WasabiRpc.Models.Params.Transactions;
using WasabiRpc.Models.Results;
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

    public SpeedUpTransactionViewModel(IRpcServiceViewModel rpcService, INavigationService navigationService, IBatchManager batchManager, string walletName)
        : base(rpcService, navigationService, batchManager)
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
        var job = CreateJob();

        if (RpcService.BatchMode)
        {
            OnBatch(job);
            return;
        }

        var result = await RpcService.Send<RpcSpeedUpTransactionResult>(job.RpcMethod, job.RpcServerUri, NavigationService);
        if (result is RpcSpeedUpTransactionResult { Result: not null } rpcSpeedUpTransactionResult)
        {
            OnRpcSuccess(rpcSpeedUpTransactionResult);
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
        if (rpcResult is RpcSpeedUpTransactionResult rpcSpeedUpTransactionResult)
        {
            NavigationService.ClearAndNavigateTo(new BuildInfo { Tx = rpcSpeedUpTransactionResult.Result }.ToViewModel(RpcService, NavigationService));
        }
    }

    public override Job CreateJob()
    {
        var requestBody = new RpcMethod
        {
            Method = "speeduptransaction",
            Params = new SpeedUpTransaction
            {
                TxId = TxId,
                Password = WalletPassword
            }
        };

        var rpcServerUri = $"{RpcService.ServerPrefix}/{WalletName}";

        return new Job("speeduptransaction", requestBody, rpcServerUri, typeof(RpcSpeedUpTransactionResult));
    }
}
