using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using WasabiCli.Models;
using WasabiCli.Models.App;
using WasabiCli.Models.Services;
using WasabiCli.Models.RpcJson;
using WasabiCli.Models.WalletWasabi;
using WasabiCli.Models.WalletWasabi.Send;

namespace WasabiCli.ViewModels.Methods;

public partial class BuildViewModel : BatchMethodViewModel
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

    public BuildViewModel(IRpcServiceViewModel rpcService, INavigationService navigationService, string walletName)
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

    private IRpcServiceViewModel RpcService { get; }

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
        var job = CreateJob();
        var result = await RpcService.SendRpcMethod(job.RpcMethod, job.RpcServerUri, ModelsJsonContext.Default.RpcBuildResult);
        if (result is RpcBuildResult { Result: not null } rpcBuildResult)
        {
            OnRpcSuccess(rpcBuildResult);
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
        if (rpcResult is RpcBuildResult rpcBuildResult)
        {
            NavigationService.Clear();
            NavigationService.Navigate(new BuildInfo { Tx = rpcBuildResult.Result });
        }
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
        
        return new Job(requestBody, rpcServerUri);
    }

    [RelayCommand]
    private async Task ListUnspentCoins()
    {
        if (WalletName is null)
        {
            return;
        }

        var listUnspentCoinsViewModel = new ListUnspentCoinsViewModel(RpcService, NavigationService, WalletName);
        var job = listUnspentCoinsViewModel.CreateJob();
        var result = await RpcService.SendRpcMethod(job.RpcMethod, job.RpcServerUri, ModelsJsonContext.Default.RpcListUnspentCoinsResult);
        if (result is RpcListUnspentCoinsResult { Result: not null } rpcListUnspentCoinsResult)
        {
            var coins = rpcListUnspentCoinsResult
                .Result
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
