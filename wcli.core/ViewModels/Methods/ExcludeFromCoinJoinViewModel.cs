using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using WasabiCli.Models.App;
using WasabiCli.Models.Params.Transactions;
using WasabiCli.Models.Results;
using WasabiCli.Models.Services;
using WasabiCli.ViewModels.Factories;

namespace WasabiCli.ViewModels.Methods;

public partial class ExcludeFromCoinJoinViewModel : RoutableMethodViewModel
{
    [NotifyCanExecuteChangedFor(nameof(ExcludeFromCoinJoinCommand))]
    [ObservableProperty] 
    private string? _walletName;

    [NotifyCanExecuteChangedFor(nameof(ExcludeFromCoinJoinCommand))]
    [ObservableProperty]
    private string? _transactionId;

    [NotifyCanExecuteChangedFor(nameof(ExcludeFromCoinJoinCommand))]
    [ObservableProperty]
    private int _n;

    [NotifyCanExecuteChangedFor(nameof(ExcludeFromCoinJoinCommand))]
    [ObservableProperty]
    private bool _exclude;

    public ExcludeFromCoinJoinViewModel(IRpcServiceViewModel rpcService, INavigationService navigationService, string walletName)
        : base(rpcService, navigationService)
    {
        WalletName = walletName;
        TransactionId = "";
        N = 0;
        Exclude = true;
    }

    private bool CanExcludeFromCoinJoin()
    {
        return WalletName is not null 
               && WalletName.Length > 0
               && TransactionId is not null 
               && TransactionId.Length > 0
               && N >= 0;
    }

    [RelayCommand(CanExecute = nameof(CanExcludeFromCoinJoin))]
    private async Task ExcludeFromCoinJoin()
    {
        var job = CreateJob();

        if (RpcService.BatchMode)
        {
            OnBatch(job);
            return;
        }

        var result = await RpcService.Send<RpcExcludeFromCoinJoinResult>(job, NavigationService);
        if (result is RpcExcludeFromCoinJoinResult rpcExcludeFromCoinJoinResult)
        {
            OnRpcSuccess(rpcExcludeFromCoinJoinResult);
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
        NavigationService.ClearAndNavigateTo(new Success { Message = $"{(Exclude ? "Excluded" : "Removed the exclusion")} from coinjoin" }.ToViewModel(RpcService, NavigationService));
    }

    public override Job CreateJob()
    {
        var requestBody = new RpcMethod
        {
            Method = "excludefromcoinjoin",
            Params = new ExcludeFromCoinJoin
            {
                TransactionId = TransactionId,
                N = N,
                Exclude = Exclude
            }
        };

        var rpcServerUri = $"{RpcService.ServerPrefix}/{WalletName}";

        return new Job(requestBody, rpcServerUri);
    }
}
