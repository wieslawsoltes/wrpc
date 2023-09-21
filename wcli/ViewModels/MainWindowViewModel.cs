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

    [NotifyCanExecuteChangedFor(nameof(GetNewAddressCommand))]
    [ObservableProperty] 
    private string? _walletName;

    [ObservableProperty] private string? _walletPassword;
    [ObservableProperty] private ObservableCollection<RpcMethodViewModel>? _rpcMethods;

    public MainWindowViewModel()
    {
        RpcService = new RpcServiceViewModel("http://127.0.0.1:37128");
        NavigationService = new NavigationServiceViewModel();
        WalletName = "Wallet 1";
        WalletPassword = "";
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

    private bool CanGetNewAddress()
    {
        return WalletName is not null && WalletName.Length > 0;
    }

    [RelayCommand(CanExecute = nameof(CanGetNewAddress))]
    private void GetNewAddress()
    {
        if (WalletName is not null)
        {
            NavigationService.Navigate(new GetNewAddressViewModel(RpcService, NavigationService, WalletName));
        }
    }

    [RelayCommand]
    private async Task Send()
    {
        // {"jsonrpc":"2.0","id":"1","method":"send", "params": { "payments":[ {"sendto": "tb1qgvnht40a08gumw32kp05hs8mny954hp2snhxcz", "amount": 15000, "label": "David" }, {"sendto":"tb1qpyhfrpys6skr2mmnc35p3dp7zlv9ew4k0gn7qm", "amount": 86200, "label": "Michael"} ], "coins":[{"transactionid":"ab83d9d0b2a9873b8ab0dc48b618098f3e7fbd807e27a10f789e9bc330ca89f7", "index":0}], "feeTarget":2, "password": "UserPassword" }}
        throw new NotImplementedException();
    }

    [RelayCommand]
    private async Task Build()
    {
        // {"jsonrpc":"2.0","id":"1","method":"build", "params": { "payments":[ {"sendto": "tb1qgjgy9k7q32rcvdjsp3nhq0x8saqcvyahhy8up2", "amount": 15000, "label": "David" }, ], "coins":[{"transactionid":"cdfda1d9839e71e82ca539a4f60e947b1cdfbeecb198616e1daa5c43e2e6fbb3", "index":0}], "feeTarget":2, "password": "UserPassword" }}
        throw new NotImplementedException();
    }

    [RelayCommand]
    private async Task Broadcast()
    {
        // {"jsonrpc":"2.0","id":"1","method":"broadcast", "params":["020000000001021cb46e8537e18398c382d0d5622ad9e475245cd789e31fb2de285c802e7b56e50100000000fefffffffe0fa22998e2dd1e4d66b09bf7e253d201f7e0b88098973bfd5e6c5ac426398d0000000000feffffff02c315100000000000160014b60bfb6774881b531d70228fb36a5fd60bd36c6ca07a0300000000001600145d576a81f460e7a1ed254fe9bfff075ab3bc45650247304402206b6d7d282b796920bf1742ca996733866321e5b491cd4a50749b9f62192f635202200f7d5a2105da3361f41810868526545c1b01160e6381ab34c56956df2995bd2c012102549fa9f712caffdf63da7d077e6a26dc3d01ca275312e9f64d1d9accf949d2bc0247304402206afcb357fa31aa5d9d241b1365492b85f058f8029ba348ff7ffd2ee15bb972fe022064818c8ab49ce649a4bb37d645d933c5860b1d5a6916fe267f3fb29e6bafca10012102a80b143904f60ede946ba9a44f29df12b7fb16f6ae3dc93a728d6a00ef658e62ad2d2500"]}
        throw new NotImplementedException();
    }

    [RelayCommand]
    private async Task GetHistory()
    {
        // {"jsonrpc":"2.0","id":"1","method":"gethistory"}
        var requestBody = new RpcMethod()
        {
            Method = "gethistory"
        };
        var rpcServerUri = $"{RpcService.RpcServerPrefix}/{WalletName}";
        var rpcResult = await RpcService.SendRpcMethod(requestBody, rpcServerUri, RpcJsonContext.Default.RpcGetHistoryResult);
        if (rpcResult is RpcGetHistoryResult { Result: not null } rpcGetHistoryResult)
        {
            // TODO:
            NavigationService.Navigate(new GetHistoryInfo() { Transactions = rpcGetHistoryResult.Result });
        }
        else if (rpcResult is RpcErrorResult { Error: not null } rpcErrorResult)
        {
            // TODO:
            NavigationService.Navigate(rpcErrorResult.Error);
        }
    }

    [RelayCommand]
    private async Task ListKeys()
    {
        // {"jsonrpc":"2.0","id":"1","method":"listkeys"}
        throw new NotImplementedException();
    }

    [RelayCommand]
    private async Task StartCoinJoin()
    {
        // {"jsonrpc":"2.0","id":"1","method":"startcoinjoin", "params":["UserPassword", "True", "True"]}
        throw new NotImplementedException();
    }

    [RelayCommand]
    private async Task StopCoinJoin()
    {
        // {"jsonrpc":"2.0","id":"1","method":"stopcoinjoin"}
        throw new NotImplementedException();
    }

    [RelayCommand]
    private async Task Stop()
    {
        // {"jsonrpc":"2.0", "method":"stop"}
    }
}
