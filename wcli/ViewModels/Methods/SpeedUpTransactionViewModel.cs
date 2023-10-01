using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using WasabiCli.Models;
using WasabiCli.Models.Navigation;
using WasabiCli.Models.RpcJson;
using WasabiCli.Models.WalletWasabi;
using WasabiCli.Models.WalletWasabi.Transactions;
using WasabiCli.ViewModels.RpcJson;

namespace WasabiCli.ViewModels.Methods;

public partial class SpeedUpTransactionViewModel : ViewModelBase
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

    public SpeedUpTransactionViewModel(RpcServiceViewModel rpcService, INavigationService navigationService, string walletName)
    {
        RpcService = rpcService;
        NavigationService = navigationService;
        WalletName = walletName;
        WalletPassword = "";
        TxId = "";
    }

    private RpcServiceViewModel RpcService { get; }

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
        var result = await RpcService.SendRpcMethod(requestBody, rpcServerUri, RpcJsonContext.Default.RpcSpeedUpTransactionResult);
        if (result is RpcSpeedUpTransactionResult { Result: not null } rpcSpeedUpTransactionResult)
        {
            NavigationService.Clear();
            NavigationService.Navigate(new BuildInfo { Tx = rpcSpeedUpTransactionResult.Result });
        }
        else if (result is RpcErrorResult { Error: not null } rpcErrorResult)
        {
            NavigationService.Navigate(rpcErrorResult.Error);
        }
        else if (result is Error error)
        {
            NavigationService.Navigate(error);
        }
    }
}
