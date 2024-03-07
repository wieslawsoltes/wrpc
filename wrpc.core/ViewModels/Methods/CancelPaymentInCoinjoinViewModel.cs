using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using WasabiRpc.Models.App;
using WasabiRpc.Models.BatchMode;
using WasabiRpc.Models.Rpc;
using WasabiRpc.Models.Rpc.Methods;
using WasabiRpc.Models.Rpc.Results;
using WasabiRpc.Models.Services;
using WasabiRpc.ViewModels.Factories;

namespace WasabiRpc.ViewModels.Methods;

public partial class CancelPaymentInCoinjoinViewModel : RoutableMethodViewModel
{
    [NotifyCanExecuteChangedFor(nameof(CancelPaymentInCoinjoinCommand))]
    [ObservableProperty] 
    private string? _walletName;

    [NotifyCanExecuteChangedFor(nameof(CancelPaymentInCoinjoinCommand))]
    [ObservableProperty]
    private string? _paymentId;

    public CancelPaymentInCoinjoinViewModel(
        IRpcServiceViewModel rpcService, 
        INavigationService navigationService,
        INavigationService detailsNavigationService, 
        IBatchManager batchManager, string? walletName)
        : base(rpcService, navigationService, detailsNavigationService, batchManager)
    {
        WalletName = walletName;
        PaymentId = "";
    }

    private bool CanCancelPaymentInCoinjoin()
    {
        return WalletName is not null 
               && WalletName.Length > 0
               && PaymentId is not null 
               && PaymentId.Length > 0;
    }

    [RelayCommand(CanExecute = nameof(CanCancelPaymentInCoinjoin))]
    private async Task CancelPaymentInCoinjoin()
    {
        await RunCommand();
    }

    public override async Task<IRoutable?> Execute(Job job)
    {
        var result = await RpcService.Send<RpcCancelPaymentInCoinjoinResult>(job.RpcMethod, job.RpcServerUri);
        return ToJobResult(result);
    }

    public override IRoutable? ToJobResult(object? result)
    {
        if (result is RpcCancelPaymentInCoinjoinResult)
        {
            return new Success { Message = "Canceled payment in coinjoin." }.ToViewModel(RpcService, NavigationService, DetailsNavigationService);
        }

        if (result is RpcErrorResult { Error: not null } rpcErrorResult)
        {
            return rpcErrorResult.Error?.ToViewModel(RpcService, NavigationService, DetailsNavigationService);
        }

        if (result is Error error)
        {
            return error.ToViewModel(RpcService, NavigationService, DetailsNavigationService);
        }

        return null;
    }

    public override Job CreateJob()
    {
        var requestBody = new CancelPaymentInCoinjoinRpcMethod
        {
            Method = "cancelpaymentincoinjoin",
            Params = new []
            {
                PaymentId = PaymentId
            }
        };

        var rpcServerUri = $"{RpcService.ServerPrefix}/{WalletName}";

        return new Job("cancelpaymentincoinjoin", requestBody, rpcServerUri);
    }
}
