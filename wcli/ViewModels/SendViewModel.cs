using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using WasabiCli.Models;
using WasabiCli.Models.Navigation;
using WasabiCli.Models.RpcJson;
using WasabiCli.Models.WalletWasabi;
using WasabiCli.Models.WalletWasabi.Send;

namespace WasabiCli.ViewModels;

public partial class SendViewModel : ViewModelBase
{
    [NotifyCanExecuteChangedFor(nameof(SendCommand))]
    [ObservableProperty] 
    private string? _walletName;

    [NotifyCanExecuteChangedFor(nameof(SendCommand))]
    [ObservableProperty]
    private string? _walletPassword;

    [NotifyCanExecuteChangedFor(nameof(SendCommand))]
    [ObservableProperty]
    private string? _sendTo;

    [NotifyCanExecuteChangedFor(nameof(SendCommand))]
    [ObservableProperty]
    private long _amount;

    [NotifyCanExecuteChangedFor(nameof(SendCommand))]
    [ObservableProperty]
    private string? _label;

    [NotifyCanExecuteChangedFor(nameof(SendCommand))]
    [ObservableProperty]
    private int _feeTarget;

    [ObservableProperty]
    private ObservableCollection<CoinViewModel> _coins;

    public SendViewModel(RpcServiceViewModel rpcService, INavigationService navigationService, string walletName)
    {
        RpcService = rpcService;
        NavigationService = navigationService;
        WalletName = walletName;
        WalletPassword = "";
        SendTo = "";
        Amount = 0;
        Label = "Label";
        FeeTarget = 2;
        Coins = new ObservableCollection<CoinViewModel>();
    }

    public RpcServiceViewModel RpcService { get; }

    private INavigationService NavigationService { get; }

    private bool CanSend()
    {
        return WalletName is not null 
               && WalletName.Length > 0
               && WalletPassword is not null
               && SendTo is not null 
               && SendTo.Length > 0
               && Amount > 0
               && Label is not null
               && Label.Length > 0
               && FeeTarget > 0;
    }

    [RelayCommand(CanExecute = nameof(CanSend))]
    private async Task Send()
    {
        var send = new Send
        {
            Payments = new ()
            {
                new Payment
                {
                    SendTo = SendTo,
                    Amount = Amount,
                    Label = Label
                }
            },
            Coins = Coins.Where(x => x.IsSelected).Select(x => new Coin { TransactionId = x.TxId, Index = x.Index }).ToList(),
            FeeTarget = FeeTarget,
            Password = WalletPassword
        };

        // {"jsonrpc":"2.0","id":"1","method":"send", "params": { "payments":[ {"sendto": "tb1qgvnht40a08gumw32kp05hs8mny954hp2snhxcz", "amount": 15000, "label": "David" }, {"sendto":"tb1qpyhfrpys6skr2mmnc35p3dp7zlv9ew4k0gn7qm", "amount": 86200, "label": "Michael"} ], "coins":[{"transactionid":"ab83d9d0b2a9873b8ab0dc48b618098f3e7fbd807e27a10f789e9bc330ca89f7", "index":0}], "feeTarget":2, "password": "UserPassword" }}
        var requestBody = new RpcMethod
        {
            Method = "send",
            Params = send
        };
        var rpcServerUri = $"{RpcService.RpcServerPrefix}/{WalletName}";
        var rpcResult = await RpcService.SendRpcMethod(requestBody, rpcServerUri, RpcJsonContext.Default.RpcSendResult);
        if (rpcResult is RpcSendResult { Result: not null } rpcSendResult)
        {
            // TODO:
            NavigationService.Clear();
            NavigationService.Navigate(rpcSendResult.Result);
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
        var requestBody = new RpcMethod
        {
            Method = "listunspentcoins"
        };
        var rpcServerUri = $"{RpcService.RpcServerPrefix}/{WalletName}";
        var rpcResult = await RpcService.SendRpcMethod(requestBody, rpcServerUri, RpcJsonContext.Default.RpcListUnspentCoinsResult);
        if (rpcResult is RpcListUnspentCoinsResult { Result: not null } rpcListUnspentCoinsResult)
        {
            var coins = rpcListUnspentCoinsResult.Result
                .Select(x => new CoinViewModel(RpcService, NavigationService, x));

            Coins = new ObservableCollection<CoinViewModel>(coins);
        }
        else if (rpcResult is RpcErrorResult { Error: not null } rpcErrorResult)
        {
            // TODO:
            NavigationService.Navigate(rpcErrorResult.Error);
        }
    }
}
