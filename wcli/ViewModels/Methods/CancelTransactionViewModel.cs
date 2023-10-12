using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using WasabiCli.Models;
using WasabiCli.Models.App;
using WasabiCli.Models.Services;
using WasabiCli.Models.RpcJson;
using WasabiCli.Models.WalletWasabi;
using WasabiCli.Models.WalletWasabi.Transactions;

namespace WasabiCli.ViewModels.Methods;

public partial class CancelTransactionViewModel : BatchMethodViewModel
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

    public CancelTransactionViewModel(IRpcServiceViewModel rpcService, INavigationService navigationService, string walletName)
    {
        RpcService = rpcService;
        NavigationService = navigationService;
        WalletName = walletName;
        WalletPassword = "";
        TxId = "";
    }

    private IRpcServiceViewModel RpcService { get; }

    private INavigationService NavigationService { get; }

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
        var job = CreateJob();
        var result = await RpcService.SendRpcMethod(job.RpcMethod, job.RpcServerUri, ModelsJsonContext.Default.RpcCancelTransactionResult);
        if (result is RpcCancelTransactionResult { Result: not null } rpcCancelTransactionResult)
        {
            OnRpcSuccess(rpcCancelTransactionResult);
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
        if (rpcResult is RpcCancelTransactionResult rpcCancelTransactionResult)
        {
            NavigationService.Clear();
            NavigationService.Navigate(new BuildInfo { Tx = rpcCancelTransactionResult.Result });
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
            Method = "canceltransaction",
            Params = new CancelTransaction
            {
                TxId = TxId,
                Password = WalletPassword
            }
        };

        var rpcServerUri = $"{RpcService.RpcServerPrefix}/{WalletName}";

        return new Job(requestBody, rpcServerUri);
    }
}
