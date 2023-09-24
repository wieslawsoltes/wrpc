using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using WasabiCli.Models;
using WasabiCli.Models.Navigation;
using WasabiCli.Models.RpcJson;
using WasabiCli.Models.WalletWasabi;

namespace WasabiCli.ViewModels.Methods;

public partial class CreateWalletViewModel : ViewModelBase
{
    [NotifyCanExecuteChangedFor(nameof(CreateWalletCommand))]
    [ObservableProperty] 
    private string? _walletName;

    [ObservableProperty] private string? _walletPassword;

    public CreateWalletViewModel(RpcServiceViewModel rpcService, INavigationService navigationService)
    {
        RpcService = rpcService;
        NavigationService = navigationService;
        WalletName = "Wallet";
        WalletPassword = "";
    }

    private RpcServiceViewModel RpcService { get; }

    private INavigationService NavigationService { get; }

    private bool CanCreateWallet()
    {
        return WalletName is not null 
               && WalletName.Length > 0;
    }

    [RelayCommand(CanExecute = nameof(CanCreateWallet))]
    private async Task CreateWallet()
    {
        var requestBody = new RpcMethod
        {
            Method = "createwallet",
            Params = new []
            {
                WalletName,
                WalletPassword
            }
        };
        var rpcServerUri = $"{RpcService.RpcServerPrefix}";
        var result = await RpcService.SendRpcMethod(requestBody, rpcServerUri, RpcJsonContext.Default.RpcCreateWalletResult);
        if (result is RpcCreateWalletResult { Result: not null } rpcCreateWalletResult)
        {
            NavigationService.Clear();
            NavigationService.Navigate(new CreateWalletInfo { Mnemonic = rpcCreateWalletResult.Result });
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
