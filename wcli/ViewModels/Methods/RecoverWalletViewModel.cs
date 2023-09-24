using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using WasabiCli.Models;
using WasabiCli.Models.Navigation;
using WasabiCli.Models.RpcJson;
using WasabiCli.ViewModels.RpcJson;

namespace WasabiCli.ViewModels.Methods;

public partial class RecoverWalletViewModel : ViewModelBase
{
    [NotifyCanExecuteChangedFor(nameof(RecoverWalletCommand))]
    [ObservableProperty] 
    private string? _walletName;

    [NotifyCanExecuteChangedFor(nameof(RecoverWalletCommand))]
    [ObservableProperty] 
    private string? _walletMnemonic;

    [ObservableProperty] private string? _walletPassword;

    public RecoverWalletViewModel(RpcServiceViewModel rpcService, INavigationService navigationService)
    {
        RpcService = rpcService;
        NavigationService = navigationService;
        WalletName = "Wallet";
        WalletMnemonic = "";
        WalletPassword = "";
    }

    private RpcServiceViewModel RpcService { get; }

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
        var rpcServerUri = $"{RpcService.RpcServerPrefix}";
        var result = await RpcService.SendRpcMethod(requestBody, rpcServerUri, RpcJsonContext.Default.RpcRecoverWalletResult);
        if (result is RpcRecoverWalletResult)
        {
            NavigationService.Clear();
            NavigationService.Navigate(new Success { Message = $"Recovered wallet {WalletName}" });
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
