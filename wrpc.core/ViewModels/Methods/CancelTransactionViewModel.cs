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

public partial class CancelTransactionViewModel : RoutableMethodViewModel
{
    [NotifyCanExecuteChangedFor(nameof(CancelTransactionCommand))]
    [ObservableProperty] 
    private string? _walletName;

    [NotifyCanExecuteChangedFor(nameof(CancelTransactionCommand))]
    [ObservableProperty]
    private string? _walletPassword;

    [NotifyCanExecuteChangedFor(nameof(CancelTransactionCommand))]
    [ObservableProperty]
    private string? _txId;

    public CancelTransactionViewModel(IRpcServiceViewModel rpcService, INavigationService navigationService, IBatchManager batchManager, string walletName)
        : base(rpcService, navigationService, batchManager)
    {
        WalletName = walletName;
        WalletPassword = "";
        TxId = "";
    }

    private bool CanCancelTransaction()
    {
        return WalletName is not null 
               && WalletName.Length > 0
               && WalletPassword is not null
               && TxId is not null 
               && TxId.Length > 0;
    }

    [RelayCommand(CanExecute = nameof(CanCancelTransaction))]
    private async Task CancelTransaction()
    {
        await RunCommand();
    }

    public override async Task<IRoutable?> Execute(Job job)
    {
        var result = await RpcService.Send<RpcCancelTransactionResult>(job.RpcMethod, job.RpcServerUri, NavigationService);
        if (result is RpcCancelTransactionResult { Result: not null } rpcCancelTransactionResult)
        {
            return new BuildInfo { Tx = rpcCancelTransactionResult.Result }.ToViewModel(RpcService, NavigationService);
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
            Method = "canceltransaction",
            Params = new CancelTransaction
            {
                TxId = TxId,
                Password = WalletPassword
            }
        };

        var rpcServerUri = $"{RpcService.ServerPrefix}/{WalletName}";

        return new Job("canceltransaction", requestBody, rpcServerUri, typeof(RpcCancelTransactionResult));
    }
}
