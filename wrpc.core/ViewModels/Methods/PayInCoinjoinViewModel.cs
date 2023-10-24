using System.Globalization;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using WasabiRpc.ViewModels.Factories;
using WasabiRpc.Models.App;
using WasabiRpc.Models.BatchMode;
using WasabiRpc.Models.Info;
using WasabiRpc.Models.Results;
using WasabiRpc.Models.Services;

namespace WasabiRpc.ViewModels.Methods;

public partial class PayInCoinjoinViewModel : RoutableMethodViewModel
{
    [NotifyCanExecuteChangedFor(nameof(PayInCoinjoinCommand))]
    [ObservableProperty] 
    private string? _walletName;

    [NotifyCanExecuteChangedFor(nameof(PayInCoinjoinCommand))]
    [ObservableProperty] 
    private string? _address;

    [NotifyCanExecuteChangedFor(nameof(PayInCoinjoinCommand))]
    [ObservableProperty] 
    private long _amount;

    public PayInCoinjoinViewModel(IRpcServiceViewModel rpcService, INavigationService navigationService, IBatchManager batchManager, string walletName)
        : base(rpcService, navigationService, batchManager)
    {
        WalletName = walletName;
        Address = "";
        Amount = 0;
    }

    private bool CanPayInCoinjoin()
    {
        return WalletName is not null 
               && WalletName.Length > 0
               && Address is not null
               && Address.Length > 0
               && Amount > 0;
    }

    [RelayCommand(CanExecute = nameof(CanPayInCoinjoin))]
    private async Task PayInCoinjoin()
    {
        var job = CreateJob();

        if (RpcService.BatchMode)
        {
            OnBatch(job);
            return;
        }

        var result = await RpcService.Send<RpcPayInCoinjoinResult>(job.RpcMethod, job.RpcServerUri, NavigationService);
        if (result is RpcPayInCoinjoinResult { Result: not null } payInCoinjoinResult)
        {
            OnRpcSuccess(payInCoinjoinResult);
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
        if (rpcResult is RpcPayInCoinjoinResult rpcPayInCoinjoinResult)
        {
            NavigationService.ClearAndNavigateTo(new PayInCoinjoinInfo { PaymentId = rpcPayInCoinjoinResult.Result }.ToViewModel(RpcService, NavigationService));
        }
    }

    public override Job CreateJob()
    {
        var requestBody = new RpcMethod
        {
            Method = "payincoinjoin",
            Params = new []
            {
                Address,
                Amount.ToString(CultureInfo.InvariantCulture)
            }
        };

        var rpcServerUri = $"{RpcService.ServerPrefix}/{WalletName}";

        return new Job("payincoinjoin", requestBody, rpcServerUri);
    }
}
