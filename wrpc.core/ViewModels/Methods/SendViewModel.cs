using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using WasabiRpc.ViewModels.Factories;
using WasabiRpc.Models.App;
using WasabiRpc.Models.BatchMode;
using WasabiRpc.Models.Params.Send;
using WasabiRpc.Models.Results;
using WasabiRpc.Models.Services;
using WasabiRpc.ViewModels.App;
using WasabiRpc.ViewModels.Info;
using WasabiRpc.ViewModels.Methods.Adapters;

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
    private ObservableCollection<CoinAdapterViewModel> _coins;

    public SendViewModel(IRpcServiceViewModel rpcService, INavigationService navigationService, IBatchManager batchManager, string walletName)
        : base(rpcService, navigationService, batchManager)
    {
        WalletName = walletName;
        WalletPassword = "";
        SendTo = "";
        Amount = 0;
        Label = "Label";
        SubtractFee = false;
        FeeTarget = 2;
        FeeRate = null;
        Coins = new ObservableCollection<CoinAdapterViewModel>();
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
        await RunCommand();
    }

    public override async Task<IRoutable?> Execute(Job job)
    {
        var result = await RpcService.Send<RpcSendResult>(job.RpcMethod, job.RpcServerUri, NavigationService);
        if (result is RpcSendResult { Result: not null } rpcSendResult)
        {
            return rpcSendResult.Result?.ToViewModel(RpcService, NavigationService);
        }

        if (result is RpcErrorResult { Error: not null } rpcErrorResult)
        {
            return rpcErrorResult.Error?.ToViewModel(RpcService, NavigationService);
        }

        if (result is Error error)
        {
            return error.ToViewModel(RpcService, NavigationService);
        }

        return null;
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

        return new Job("send", requestBody, rpcServerUri, typeof(RpcSendResult));
    }

    [RelayCommand]
    private async Task ListUnspentCoins()
    {
        if (WalletName is null)
        {
            return;
        }

        var listUnspentCoinsViewModel = new ListUnspentCoinsViewModel(RpcService, NavigationService, BatchManager, WalletName);
        var job = listUnspentCoinsViewModel.CreateJob();
        var routable = await listUnspentCoinsViewModel.Execute(job);
        if (routable is ListUnspentCoinsInfoViewModel listUnspentCoinsInfoViewModel)
        {
            if (listUnspentCoinsInfoViewModel.Coins is not null)
            {
                Coins = new ObservableCollection<CoinAdapterViewModel>(listUnspentCoinsInfoViewModel.Coins);
            }
        }
        else if (routable is ErrorInfoViewModel errorInfoViewModel)
        {
            NavigationService.NavigateTo(errorInfoViewModel);
        }
        else if (routable is ErrorViewModel errorViewModel)
        {
            NavigationService.NavigateTo(errorViewModel);
        }
    }
}
