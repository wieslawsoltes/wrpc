using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using WasabiCli.Models.RpcJson;
using WasabiCli.Models.WalletWasabi;

namespace WasabiCli.ViewModels;

public partial class CreateWalletViewModel : ViewModelBase
{
    [ObservableProperty] private string? _walletName;
    [ObservableProperty] private string? _walletPassword;

    public CreateWalletViewModel(RpcServiceViewModel rpcService, NavigationServiceViewModel navigationService)
    {
        RpcService = rpcService;
        NavigationService = navigationService;
        WalletName = "Wallet";
        WalletPassword = "";
    }

    public RpcServiceViewModel RpcService { get; }

    private NavigationServiceViewModel NavigationService { get; }

    [RelayCommand]
    private async Task CreateWallet()
    {
        // {"jsonrpc":"2.0","id":"1","method":"createwallet","params":["WalletName", "UserPassword"]}'
        var requestBody = new RpcMethod()
        {
            Method = "createwallet",
            Params = new []
            {
                WalletName,
                WalletPassword
            }
        };
        var rpcServerUri = $"{RpcService.RpcServerPrefix}";
        var rpcResult = await RpcService.SendRpcMethod(requestBody, rpcServerUri, RpcJsonContext.Default.RpcCreateWalletResult);
        if (rpcResult is RpcCreateWalletResult { Result: not null } rpcCreateWalletResult)
        {
            // TODO:
            NavigationService.Clear();
            NavigationService.Navigate(new CreateWalletInfo { Mnemonic = rpcCreateWalletResult.Result });
        }
        else if (rpcResult is RpcErrorResult { Error: not null } rpcErrorResult)
        {
            // TODO:
            NavigationService.Navigate(rpcErrorResult.Error);
        }
    }
}
