using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using WasabiCli.Models.App;
using WasabiCli.Models.Info;
using WasabiCli.Models.Services;
using WasabiCli.Models.Params.Build;
using WasabiCli.Models.Params.Send;
using WasabiCli.Models.Results;
using WasabiCli.ViewModels.Factories;

namespace WasabiCli.ViewModels.Methods;

public partial class BuildViewModel : RoutableMethodViewModel
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
    private int? _feeTarget;

    [NotifyCanExecuteChangedFor(nameof(BuildCommand))]
    [ObservableProperty]
    private decimal? _feeRate;

    [ObservableProperty]
    private ObservableCollection<CoinViewModel> _coins;

    public BuildViewModel(IRpcServiceViewModel rpcService, INavigationService navigationService, string walletName)
        : base(rpcService, navigationService)
    {
        WalletName = walletName;
        WalletPassword = "";
        SendTo = "";
        Amount = 0;
        Label = "Label";
        SubtractFee = false;
        FeeTarget = 2;
        FeeTarget = null;
        Coins = new ObservableCollection<CoinViewModel>();
    }

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

        if (RpcService.BatchMode)
        {
            OnBatch(job);
            return;
        }

        var result = await RpcService.Send<RpcBuildResult>(job, NavigationService);
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
            NavigationService.ClearAndNavigateTo(new BuildInfo { Tx = rpcBuildResult.Result }.ToViewModel(RpcService, NavigationService));
        }
    }

    public override Job CreateJob()
    {
        var requestBody = new RpcMethod
        {
            Method = "build",
            Params = new Build
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
                FeeRate = FeeRate,
                Password = WalletPassword
            }
        };

        var rpcServerUri = $"{RpcService.ServerPrefix}/{WalletName}";
        
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
        var result = await RpcService.Send<RpcListUnspentCoinsResult>(job, NavigationService);
        if (result is RpcListUnspentCoinsResult { Result: not null } rpcListUnspentCoinsResult)
        {
            var coins = rpcListUnspentCoinsResult
                .Result
                .Select(x => new CoinViewModel(RpcService, NavigationService, x.ToViewModel(RpcService, NavigationService)));

            Coins = new ObservableCollection<CoinViewModel>(coins);
        }
        else if (result is RpcErrorResult { Error: not null } rpcErrorResult)
        {
            NavigationService.NavigateTo(rpcErrorResult.Error?.ToViewModel(RpcService, NavigationService));
        }
        else if (result is Error error)
        {
            NavigationService.NavigateTo(error.ToViewModel(RpcService, NavigationService));
        }
    }
}
