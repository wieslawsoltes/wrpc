using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using WasabiCli.Models.RpcJson;
using WasabiCli.Models.WalletWasabi;

namespace WasabiCli.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    [ObservableProperty] private RpcServiceViewModel _rpcService;
    [ObservableProperty] private NavigationServiceViewModel _navigationService;
    [ObservableProperty] private string? _walletName;
    [ObservableProperty] private string? _walletPassword;
    [ObservableProperty] private string? _label;
    [ObservableProperty] private ObservableCollection<RpcMethodViewModel>? _rpcMethods;

    public MainWindowViewModel()
    {
        RpcService = new RpcServiceViewModel("http://127.0.0.1:37128");
        NavigationService = new NavigationServiceViewModel();
        WalletName = "Wallet 1";
        WalletPassword = "";
        Label = "Label 1, Label 2";
        RpcMethods = new ObservableCollection<RpcMethodViewModel>()
        {
            new ("GetStatus", GetStatusCommand),
            new ("CreateWallet", CreateWalletCommand),
            new ("ListCoins", ListCoinsCommand),
            new ("ListUnspentCoins", ListUnspentCoinsCommand),
            new ("GetWalletInfo", GetWalletInfoCommand),
            new ("GetNewAddress", GetNewAddressCommand),
            new ("Send", SendCommand),
            new ("Build", BuildCommand),
            new ("Broadcast", BroadcastCommand),
            new ("GetHistory", GetHistoryCommand),
            new ("ListKeys", ListKeysCommand),
            new ("StartCoinJoin", StartCoinJoinCommand),
            new ("StopCoinJoin", StopCoinJoinCommand),
            new ("Stop", StopCommand)
        };
    }

    [RelayCommand]
    private async Task GetStatus()
    {
        // {"jsonrpc":"2.0","id":"1","method":"getstatus"}
        var requestBody = new RpcMethod()
        {
            Method = "getstatus"
        };
        var rpcServerUri = $"{RpcService.RpcServerPrefix}";
        var rpcResult = await RpcService.SendRpcMethod(requestBody, rpcServerUri, RpcJsonContext.Default.RpcGetStatusResult);
        if (rpcResult is RpcGetStatusResult { Result: not null } rpcGetStatusResult)
        {
            // TODO:
            NavigationService.Navigate(rpcGetStatusResult.Result);
        }
        else if (rpcResult is RpcErrorResult { Error: not null } rpcErrorResult)
        {
            // TODO:
            NavigationService.Navigate(rpcErrorResult.Error);
        }
    }

    [RelayCommand]
    private void CreateWallet()
    {
        NavigationService.Navigate(new CreateWalletViewModel(RpcService, NavigationService));
    }

    [RelayCommand]
    private async Task ListCoins()
    {
        // {"jsonrpc":"2.0","id":"1","method":"listcoins"}
        var requestBody = new RpcMethod()
        {
            Method = "listcoins"
        };
        var rpcServerUri = $"{RpcService.RpcServerPrefix}/{WalletName}";
        var rpcResult = await RpcService.SendRpcMethod(requestBody, rpcServerUri, RpcJsonContext.Default.RpcListCoinsResult);
        if (rpcResult is RpcListCoinsResult { Result: not null } rpcListCoinsResult)
        {
            // TODO:
            NavigationService.Navigate(new ListCoinsInfo { Coins = rpcListCoinsResult.Result });
        }
        else if (rpcResult is RpcErrorResult { Error: not null } rpcErrorResult)
        {
            // TODO:
            NavigationService.Navigate(rpcErrorResult.Error);
        }
    }

    [RelayCommand]
    private async Task ListUnspentCoins()
    {
        // {"jsonrpc":"2.0","id":"1","method":"listunspentcoins"}
        var requestBody = new RpcMethod()
        {
            Method = "listunspentcoins"
        };
        var rpcServerUri = $"{RpcService.RpcServerPrefix}/{WalletName}";
        var rpcResult = await RpcService.SendRpcMethod(requestBody, rpcServerUri, RpcJsonContext.Default.RpcListUnspentCoinsResult);
        if (rpcResult is RpcListUnspentCoinsResult { Result: not null } rpcListUnspentCoinsResult)
        {
            // TODO:
            NavigationService.Navigate(new ListUnspentCoinsInfo { Coins = rpcListUnspentCoinsResult.Result });
        }
        else if (rpcResult is RpcErrorResult { Error: not null } rpcErrorResult)
        {
            // TODO:
            NavigationService.Navigate(rpcErrorResult.Error);
        }
    }

    [RelayCommand]
    private async Task GetWalletInfo()
    {
        // {"jsonrpc":"2.0","id":"1","method":"getwalletinfo"}
        var requestBody = new RpcMethod()
        {
            Method = "getwalletinfo"
        };
        var rpcServerUri = $"{RpcService.RpcServerPrefix}/{WalletName}";
        var rpcResult = await RpcService.SendRpcMethod(requestBody, rpcServerUri, RpcJsonContext.Default.RpcGetWalletInfoResult);
        if (rpcResult is RpcGetWalletInfoResult { Result: not null } rpcGetWalletInfoResult)
        {
            // TODO:
            NavigationService.Navigate(rpcGetWalletInfoResult.Result);
        }
        else if (rpcResult is RpcErrorResult { Error: not null } rpcErrorResult)
        {
            // TODO:
            NavigationService.Navigate(rpcErrorResult.Error);
        }
    }

    [RelayCommand]
    private void GetNewAddress()
    {
        if (WalletName is not null && WalletName.Length > 0)
        {
            NavigationService.Navigate(new GetNewAddressViewModel(RpcService, NavigationService, WalletName));
        }
    }

    [RelayCommand]
    private async Task Send()
    {
        throw new NotImplementedException();
    }

    [RelayCommand]
    private async Task Build()
    {
        throw new NotImplementedException();
    }

    [RelayCommand]
    private async Task Broadcast()
    {
        throw new NotImplementedException();
    }

    [RelayCommand]
    private async Task GetHistory()
    {
        throw new NotImplementedException();
    }

    [RelayCommand]
    private async Task ListKeys()
    {
        throw new NotImplementedException();
    }

    [RelayCommand]
    private async Task StartCoinJoin()
    {
        throw new NotImplementedException();
    }

    [RelayCommand]
    private async Task StopCoinJoin()
    {
        throw new NotImplementedException();
    }

    [RelayCommand]
    private async Task Stop()
    {
        throw new NotImplementedException();
    }
}
