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

public partial class SpeedUpTransactionViewModel : BatchMethodViewModel
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

    public SpeedUpTransactionViewModel(IRpcServiceViewModel rpcService, INavigationService navigationService, string walletName)
    {
        RpcService = rpcService;
        NavigationService = navigationService;
        WalletName = walletName;
        WalletPassword = "";
        TxId = "";
    }

    private IRpcServiceViewModel RpcService { get; }

    private INavigationService NavigationService { get; }

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
        var result = await RpcService.SendRpcMethod(job.RpcMethod, job.RpcServerUri, ModelsJsonContext.Default.RpcSpeedUpTransactionResult);
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
            NavigationService.Clear();
            NavigationService.Navigate(new BuildInfo { Tx = rpcSpeedUpTransactionResult.Result });
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
            Method = "speeduptransaction",
            Params = new SpeedUpTransaction
            {
                TxId = TxId,
                Password = WalletPassword
            }
        };

        var rpcServerUri = $"{RpcService.RpcServerPrefix}/{WalletName}";

        return new Job(requestBody, rpcServerUri);
    }
}
