using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using WasabiRpc.ViewModels.Factories;
using WasabiRpc.Models.App;
using WasabiRpc.Models.Params.Send;
using WasabiRpc.Models.Results;
using WasabiRpc.Models.Services;

namespace WasabiRpc.ViewModels.Methods;

public partial class SendViewModel : RoutableMethodViewModel
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
    private bool _subtractFee;

    [NotifyCanExecuteChangedFor(nameof(SendCommand))]
    [ObservableProperty]
    private int? _feeTarget;

    [NotifyCanExecuteChangedFor(nameof(SendCommand))]
    [ObservableProperty]
    private int? _feeRate;

    [ObservableProperty]
    private ObservableCollection<CoinViewModel> _coins;

    public SendViewModel(IRpcServiceViewModel rpcService, INavigationService navigationService, string walletName)
        : base(rpcService, navigationService)
    {
        WalletName = walletName;
        WalletPassword = "";
        SendTo = "";
        Amount = 0;
        Label = "Label";
        SubtractFee = false;
        FeeTarget = 2;
        FeeRate = null;
        Coins = new ObservableCollection<CoinViewModel>();
    }

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
        var job = CreateJob();

        if (RpcService.BatchMode)
        {
            OnBatch(job);
            return;
        }

        var result = await RpcService.Send<RpcSendResult>(job, NavigationService);
        if (result is RpcSendResult { Result: not null } rpcSendResult)
        {
            OnRpcSuccess(rpcSendResult);
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
        if (rpcResult is RpcSendResult rpcSendResult)
        {
            NavigationService.ClearAndNavigateTo(rpcSendResult.Result?.ToViewModel(RpcService, NavigationService));
        }
    }

    public override Job CreateJob()
    {
        var requestBody = new RpcMethod
        {
            Method = "send",
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
                FeeRate = FeeRate,
                Password = WalletPassword
            }
        };

        var rpcServerUri = $"{RpcService.ServerPrefix}/{WalletName}";

        return new Job("send", requestBody, rpcServerUri);
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
                .Select(x => new CoinViewModel(RpcService, NavigationService, WalletName, x.ToViewModel(RpcService, NavigationService)));

            Coins = new ObservableCollection<CoinViewModel>(coins);
        }
        else if (result is RpcErrorResult { Error: not null } rpcErrorResult)
        {
            NavigationService.NavigateTo(rpcErrorResult.Error.ToViewModel(RpcService, NavigationService));
        }
        else if (result is Error error)
        {
            NavigationService.NavigateTo(error.ToViewModel(RpcService, NavigationService));
        }
    }
}
