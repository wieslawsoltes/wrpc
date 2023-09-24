using System.Collections.ObjectModel;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using WasabiCli.Models;
using WasabiCli.Models.Navigation;
using WasabiCli.Models.RpcJson;
using WasabiCli.Models.WalletWasabi;
using WasabiCli.ViewModels.Methods;

namespace WasabiCli.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    [ObservableProperty] private RpcServiceViewModel _rpcService;
    [ObservableProperty] private ObservableCollection<WalletViewModel>? _wallets;

    [NotifyCanExecuteChangedFor(nameof(ListCoinsCommand))]
    [NotifyCanExecuteChangedFor(nameof(ListUnspentCoinsCommand))]
    [NotifyCanExecuteChangedFor(nameof(GetWalletInfoCommand))]
    [NotifyCanExecuteChangedFor(nameof(GetNewAddressCommand))]
    [NotifyCanExecuteChangedFor(nameof(SendCommand))]
    [NotifyCanExecuteChangedFor(nameof(BuildCommand))]
    [NotifyCanExecuteChangedFor(nameof(GetHistoryCommand))]
    [NotifyCanExecuteChangedFor(nameof(ListKeysCommand))]
    [NotifyCanExecuteChangedFor(nameof(StartCoinJoinCommand))]
    [NotifyCanExecuteChangedFor(nameof(StopCoinJoinCommand))]
    [ObservableProperty] 
    private WalletViewModel? _selectedWallet;

    [ObservableProperty] private ObservableCollection<RpcMethodViewModel>? _rpcMethods;

    public MainWindowViewModel(INavigationService navigationService)
    {
        RpcService = new RpcServiceViewModel("http://127.0.0.1:37128");
        NavigationService = navigationService;

        Wallets = new ObservableCollection<WalletViewModel>
        {
            new () { WalletName = "Wallet 1" },
            new () { WalletName = "Wallet 2" },
            new () { WalletName = "Wallet 3" }
        };

        SelectedWallet = Wallets[0];

        RpcMethods = new ObservableCollection<RpcMethodViewModel>
        {
            new ("GetStatus", GetStatusCommand),
            new ("CreateWallet", CreateWalletCommand),
            new ("RecoverWallet", RecoverWalletCommand),
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

    public INavigationService NavigationService { get; }

    [RelayCommand]
    private async Task GetStatus()
    {
        var requestBody = new RpcMethod
        {
            Method = "getstatus"
        };
        var rpcServerUri = $"{RpcService.RpcServerPrefix}";
        var result = await RpcService.SendRpcMethod(requestBody, rpcServerUri, RpcJsonContext.Default.RpcGetStatusResult);
        if (result is RpcGetStatusResult { Result: not null } rpcGetStatusResult)
        {
            // TODO:
            NavigationService.Navigate(rpcGetStatusResult.Result);
        }
        else if (result is RpcErrorResult { Error: not null } rpcErrorResult)
        {
            // TODO:
            NavigationService.Navigate(rpcErrorResult.Error);
        }
        else if (result is Error error)
        {
            NavigationService.Navigate(error);
        }
    }

    [RelayCommand]
    private void CreateWallet()
    {
        NavigationService.Navigate(new CreateWalletViewModel(RpcService, NavigationService));
    }

    [RelayCommand]
    private void RecoverWallet()
    {
        NavigationService.Navigate(new RecoverWalletViewModel(RpcService, NavigationService));
    }

    private bool CanListCoins()
    {
        return SelectedWallet?.WalletName is not null 
               && SelectedWallet?.WalletName.Length > 0;
    }

    [RelayCommand(CanExecute = nameof(CanListCoins))]
    private async Task ListCoins()
    {
        var requestBody = new RpcMethod
        {
            Method = "listcoins"
        };
        var rpcServerUri = $"{RpcService.RpcServerPrefix}/{SelectedWallet?.WalletName}";
        var result = await RpcService.SendRpcMethod(requestBody, rpcServerUri, RpcJsonContext.Default.RpcListCoinsResult);
        if (result is RpcListCoinsResult { Result: not null } rpcListCoinsResult)
        {
            // TODO:
            NavigationService.Navigate(new ListCoinsInfo { Coins = rpcListCoinsResult.Result });
        }
        else if (result is RpcErrorResult { Error: not null } rpcErrorResult)
        {
            // TODO:
            NavigationService.Navigate(rpcErrorResult.Error);
        }
        else if (result is Error error)
        {
            NavigationService.Navigate(error);
        }
    }

    private bool CanListUnspentCoins()
    {
        return SelectedWallet?.WalletName is not null 
               && SelectedWallet?.WalletName.Length > 0;
    }

    [RelayCommand(CanExecute = nameof(CanListUnspentCoins))]
    private async Task ListUnspentCoins()
    {
        var requestBody = new RpcMethod
        {
            Method = "listunspentcoins"
        };
        var rpcServerUri = $"{RpcService.RpcServerPrefix}/{SelectedWallet?.WalletName}";
        var result = await RpcService.SendRpcMethod(requestBody, rpcServerUri, RpcJsonContext.Default.RpcListUnspentCoinsResult);
        if (result is RpcListUnspentCoinsResult { Result: not null } rpcListUnspentCoinsResult)
        {
            // TODO:
            NavigationService.Navigate(new ListUnspentCoinsInfo { Coins = rpcListUnspentCoinsResult.Result });
        }
        else if (result is RpcErrorResult { Error: not null } rpcErrorResult)
        {
            // TODO:
            NavigationService.Navigate(rpcErrorResult.Error);
        }
        else if (result is Error error)
        {
            NavigationService.Navigate(error);
        }
    }

    private bool CanGetWalletInfo()
    {
        return SelectedWallet?.WalletName is not null 
               && SelectedWallet?.WalletName.Length > 0;
    }

    [RelayCommand(CanExecute = nameof(CanGetWalletInfo))]
    private async Task GetWalletInfo()
    {
        var requestBody = new RpcMethod
        {
            Method = "getwalletinfo"
        };
        var rpcServerUri = $"{RpcService.RpcServerPrefix}/{SelectedWallet?.WalletName}";
        var result = await RpcService.SendRpcMethod(requestBody, rpcServerUri, RpcJsonContext.Default.RpcGetWalletInfoResult);
        if (result is RpcGetWalletInfoResult { Result: not null } rpcGetWalletInfoResult)
        {
            // TODO:
            NavigationService.Navigate(rpcGetWalletInfoResult.Result);
        }
        else if (result is RpcErrorResult { Error: not null } rpcErrorResult)
        {
            // TODO:
            NavigationService.Navigate(rpcErrorResult.Error);
        }
        else if (result is Error error)
        {
            NavigationService.Navigate(error);
        }
    }

    private bool CanGetNewAddress()
    {
        return SelectedWallet?.WalletName is not null 
               && SelectedWallet?.WalletName.Length > 0;
    }

    [RelayCommand(CanExecute = nameof(CanGetNewAddress))]
    private void GetNewAddress()
    {
        if (SelectedWallet?.WalletName is not null)
        {
            NavigationService.Navigate(new GetNewAddressViewModel(RpcService, NavigationService, SelectedWallet.WalletName));
        }
    }

    private bool CanSend()
    {
        return SelectedWallet?.WalletName is not null 
               && SelectedWallet?.WalletName.Length > 0;
    }

    [RelayCommand(CanExecute = nameof(CanSend))]
    private void Send()
    {
        if (SelectedWallet?.WalletName is not null)
        {
            NavigationService.Navigate(new SendViewModel(RpcService, NavigationService, SelectedWallet.WalletName));
        }
    }

    private bool CanBuild()
    {
        return SelectedWallet?.WalletName is not null 
               && SelectedWallet?.WalletName.Length > 0;
    }

    [RelayCommand(CanExecute = nameof(CanBuild))]
    private void Build()
    {
        if (SelectedWallet?.WalletName is not null)
        {
            NavigationService.Navigate(new BuildViewModel(RpcService, NavigationService, SelectedWallet.WalletName));
        }
    }

    [RelayCommand]
    private void Broadcast()
    {
        if (SelectedWallet?.WalletName is not null)
        {
            NavigationService.Navigate(new BroadcastViewModel(RpcService, NavigationService));
        }
    }

    private bool CanGetHistory()
    {
        return SelectedWallet?.WalletName is not null 
               && SelectedWallet?.WalletName.Length > 0;
    }

    [RelayCommand(CanExecute = nameof(CanGetHistory))]
    private async Task GetHistory()
    {
        var requestBody = new RpcMethod
        {
            Method = "gethistory"
        };
        var rpcServerUri = $"{RpcService.RpcServerPrefix}/{SelectedWallet?.WalletName}";
        var result = await RpcService.SendRpcMethod(requestBody, rpcServerUri, RpcJsonContext.Default.RpcGetHistoryResult);
        if (result is RpcGetHistoryResult { Result: not null } rpcGetHistoryResult)
        {
            // TODO:
            NavigationService.Navigate(new GetHistoryInfo { Transactions = rpcGetHistoryResult.Result });
        }
        else if (result is RpcErrorResult { Error: not null } rpcErrorResult)
        {
            // TODO:
            NavigationService.Navigate(rpcErrorResult.Error);
        }
        else if (result is Error error)
        {
            NavigationService.Navigate(error);
        }
    }

    private bool CanListKeys()
    {
        return SelectedWallet?.WalletName is not null 
               && SelectedWallet?.WalletName.Length > 0;
    }

    [RelayCommand(CanExecute = nameof(CanListKeys))]
    private async Task ListKeys()
    {
        var requestBody = new RpcMethod
        {
            Method = "listkeys"
        };
        var rpcServerUri = $"{RpcService.RpcServerPrefix}/{SelectedWallet?.WalletName}";
        var result = await RpcService.SendRpcMethod(requestBody, rpcServerUri, RpcJsonContext.Default.RpcListKeysResult);
        if (result is RpcListKeysResult { Result: not null } rpcListKeysResult)
        {
            // TODO:
            NavigationService.Navigate(new ListKeysInfo { Keys = rpcListKeysResult.Result });
        }
        else if (result is RpcErrorResult { Error: not null } rpcErrorResult)
        {
            // TODO:
            NavigationService.Navigate(rpcErrorResult.Error);
        }
        else if (result is Error error)
        {
            NavigationService.Navigate(error);
        }
    }

    private bool CanStartCoinJoin()
    {
        return SelectedWallet?.WalletName is not null 
               && SelectedWallet?.WalletName.Length > 0;
    }

    [RelayCommand(CanExecute = nameof(CanStartCoinJoin))]
    private void StartCoinJoin()
    {
        if (SelectedWallet?.WalletName is not null)
        {
            NavigationService.Navigate(new StartCoinJoinViewModel(RpcService, NavigationService, SelectedWallet.WalletName));
        }
    }

    private bool CanStopCoinJoin()
    {
        return SelectedWallet?.WalletName is not null 
               && SelectedWallet?.WalletName.Length > 0;
    }

    [RelayCommand(CanExecute = nameof(CanStopCoinJoin))]
    private async Task StopCoinJoin()
    {
        var requestBody = new RpcMethod
        {
            Method = "stopcoinjoin"
        };
        var rpcServerUri = $"{RpcService.RpcServerPrefix}/{SelectedWallet?.WalletName}";
        var result = await RpcService.SendRpcMethod(requestBody, rpcServerUri, RpcJsonContext.Default.RpcStopCoinJoinResult);
        if (result is RpcStopCoinJoinResult)
        {
            // TODO:
            NavigationService.Navigate(new Success { Message = $"Stopped coinjoin for wallet {SelectedWallet?.WalletName}" });
        }
        else if (result is RpcErrorResult { Error: not null } rpcErrorResult)
        {
            // TODO:
            NavigationService.Navigate(rpcErrorResult.Error);
        }
        else if (result is Error error)
        {
            NavigationService.Navigate(error);
        }
    }

    [RelayCommand]
    private async Task Stop()
    {
        var requestBody = new RpcMethod
        {
            Method = "stop"
        };
        var rpcServerUri = $"{RpcService.RpcServerPrefix}";
        var result = await RpcService.SendRpcMethod(requestBody, rpcServerUri, RpcJsonContext.Default.String);
        if (result is string)
        {
            // TODO:
            NavigationService.Navigate(new Success { Message = "Stopped daemon." });
        }
        else if (result is RpcErrorResult { Error: not null } rpcErrorResult)
        {
            // TODO:
            NavigationService.Navigate(rpcErrorResult.Error);
        }
        else if (result is Error error)
        {
            NavigationService.Navigate(error);
        }
    }
}
