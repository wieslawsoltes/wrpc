using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using WasabiCli.Models;
using WasabiCli.Models.Navigation;
using WasabiCli.Models.RpcJson;

namespace WasabiCli.ViewModels.Methods;

public partial class StartCoinJoinViewModel : ViewModelBase
{
    [ObservableProperty] private string? _walletName;
    [ObservableProperty] private string? _walletPassword;
    [ObservableProperty] private bool _stopWhenAllMixed;
    [ObservableProperty] private bool _overridePlebStop;

    public StartCoinJoinViewModel(RpcServiceViewModel rpcService, INavigationService navigationService, string walletName)
    {
        RpcService = rpcService;
        NavigationService = navigationService;
        WalletName = walletName;
        WalletPassword = "";
        StopWhenAllMixed = true;
        OverridePlebStop = true;
    }

    private RpcServiceViewModel RpcService { get; }

    private INavigationService NavigationService { get; }

    [RelayCommand]
    private async Task StartCoinJoin()
    {
        var requestBody = new RpcMethod
        {
            Method = "startcoinjoin",
            Params = new []
            {
                WalletPassword,
                $"{StopWhenAllMixed}",
                $"{OverridePlebStop}"
            }
        };
        var rpcServerUri = $"{RpcService.RpcServerPrefix}/{WalletName}";
        var result = await RpcService.SendRpcMethod(requestBody, rpcServerUri, RpcJsonContext.Default.RpcStartCoinJoinResult);
        if (result is RpcStartCoinJoinResult)
        {
            NavigationService.Clear();
            NavigationService.Navigate(new Success { Message = $"Started coinjoin for wallet {WalletName}" });
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
