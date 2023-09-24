using System.Collections.Generic;
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

namespace WasabiCli.ViewModels.Methods;

public partial class BuildViewModel : ViewModelBase
{
    [NotifyCanExecuteChangedFor(nameof(BuildCommand))]
    [ObservableProperty] 
    private string? _walletName;

    [NotifyCanExecuteChangedFor(nameof(BuildCommand))]
    [ObservableProperty]
    private string? _walletPassword;

    [NotifyCanExecuteChangedFor(nameof(BuildCommand))]
    [ObservableProperty]
    private string? _sendTo;

    [NotifyCanExecuteChangedFor(nameof(BuildCommand))]
    [ObservableProperty]
    private long _amount;

    [NotifyCanExecuteChangedFor(nameof(BuildCommand))]
    [ObservableProperty]
    private string? _label;

    [NotifyCanExecuteChangedFor(nameof(BuildCommand))]
    [ObservableProperty]
    private bool _subtractFee;

    [NotifyCanExecuteChangedFor(nameof(BuildCommand))]
    [ObservableProperty]
    private int _feeTarget;

    [ObservableProperty]
    private ObservableCollection<CoinViewModel> _coins;

    public BuildViewModel(RpcServiceViewModel rpcService, INavigationService navigationService, string walletName)
    {
        RpcService = rpcService;
        NavigationService = navigationService;
        WalletName = walletName;
        WalletPassword = "";
        SendTo = "";
        Amount = 0;
        Label = "Label";
        SubtractFee = false;
        FeeTarget = 2;
        Coins = new ObservableCollection<CoinViewModel>();
    }

    private RpcServiceViewModel RpcService { get; }

    private INavigationService NavigationService { get; }

    private bool CanBuild()
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

    [RelayCommand(CanExecute = nameof(CanBuild))]
    private async Task Build()
    {
        var requestBody = new RpcMethod
        {
            Method = "build",
            Params = new Send
            {
                Payments = new List<Payment>
                {
                    new ()
                    {
                        SendTo = SendTo,
                        Amount = Amount,
                        Label = Label,
                        SubtractFee = SubtractFee
                    }
                },
                Coins = Coins
                    .Where(x => x.IsSelected)
                    .Select(x => new Coin { TransactionId = x.CoinInfo.TxId, Index = x.CoinInfo.Index })
                    .ToList(),
                FeeTarget = FeeTarget,
                Password = WalletPassword
            }
        };
        var rpcServerUri = $"{RpcService.RpcServerPrefix}/{WalletName}";
        var result = await RpcService.SendRpcMethod(requestBody, rpcServerUri, RpcJsonContext.Default.RpcBuildResult);
        if (result is RpcBuildResult { Result: not null } rpcBuildResult)
        {
            NavigationService.Clear();
            NavigationService.Navigate(new BuildInfo { Tx = rpcBuildResult.Result });
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

    [RelayCommand]
    private async Task ListUnspentCoins()
    {
        var requestBody = new RpcMethod
        {
            Method = "listunspentcoins"
        };
        var rpcServerUri = $"{RpcService.RpcServerPrefix}/{WalletName}";
        var result = await RpcService.SendRpcMethod(requestBody, rpcServerUri, RpcJsonContext.Default.RpcListUnspentCoinsResult);
        if (result is RpcListUnspentCoinsResult { Result: not null } rpcListUnspentCoinsResult)
        {
            var coins = rpcListUnspentCoinsResult.Result
                .Select(x => new CoinViewModel(RpcService, NavigationService, x));

            Coins = new ObservableCollection<CoinViewModel>(coins);
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
