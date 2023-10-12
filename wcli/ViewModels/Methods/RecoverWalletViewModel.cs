using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using WasabiCli.Models;
using WasabiCli.Models.App;
using WasabiCli.Models.Services;
using WasabiCli.Models.RpcJson;

namespace WasabiCli.ViewModels.Methods;

public partial class RecoverWalletViewModel : BatchMethodViewModel
{
    [NotifyCanExecuteChangedFor(nameof(RecoverWalletCommand))]
    [ObservableProperty] 
    private string? _walletName;

    [NotifyCanExecuteChangedFor(nameof(RecoverWalletCommand))]
    [ObservableProperty] 
    private string? _walletMnemonic;

    [ObservableProperty] private string? _walletPassword;

    public RecoverWalletViewModel(IRpcServiceViewModel rpcService, INavigationService navigationService)
    {
        RpcService = rpcService;
        NavigationService = navigationService;
        WalletName = "Wallet";
        WalletMnemonic = "";
        WalletPassword = "";
    }

    private IRpcServiceViewModel RpcService { get; }

    private INavigationService NavigationService { get; }

    private bool CanRecoverWallet()
    {
        return WalletName is not null 
               && WalletName.Length > 0
               && WalletMnemonic is not null 
               && WalletMnemonic.Length > 0;
    }

    [RelayCommand(CanExecute = nameof(CanRecoverWallet))]
    private async Task RecoverWallet()
    {
        var job = CreateJob();
        var result = await RpcService.Send(job, ModelsJsonContext.Default.RpcRecoverWalletResult);
        if (result is RpcRecoverWalletResult rpcRecoverWalletResult)
        {
            OnRpcSuccess(rpcRecoverWalletResult);
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
        NavigationService.Clear();
        NavigationService.Navigate(new Success { Message = $"Recovered wallet {WalletName}" });
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
            Method = "recoverwallet",
            Params = new []
            {
                WalletName,
                WalletMnemonic,
                WalletPassword
            }
        };

        var rpcServerUri = $"{RpcService.ServerPrefix}";

        return new Job(requestBody, rpcServerUri);
    }
}
