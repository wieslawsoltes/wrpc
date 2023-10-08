using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using WasabiCli.Models;
using WasabiCli.Models.Navigation;
using WasabiCli.Models.RpcJson;
using WasabiCli.Models.WalletWasabi;
using WasabiCli.Models.WalletWasabi.Transactions;
using WasabiCli.ViewModels.RpcJson;

namespace WasabiCli.ViewModels.Methods;

public partial class ExcludeFromCoinjoinViewModel : ViewModelBase
{
    [NotifyCanExecuteChangedFor(nameof(ExcludeFromCoinjoinCommand))]
    [ObservableProperty] 
    private string? _walletName;

    [NotifyCanExecuteChangedFor(nameof(ExcludeFromCoinjoinCommand))]
    [ObservableProperty]
    private string? _transactionId;

    [NotifyCanExecuteChangedFor(nameof(ExcludeFromCoinjoinCommand))]
    [ObservableProperty]
    private int _n;

    [NotifyCanExecuteChangedFor(nameof(ExcludeFromCoinjoinCommand))]
    [ObservableProperty]
    private bool _exclude;

    public ExcludeFromCoinjoinViewModel(RpcServiceViewModel rpcService, INavigationService navigationService, string walletName)
    {
        RpcService = rpcService;
        NavigationService = navigationService;
        WalletName = walletName;
        TransactionId = "";
        Exclude = true;
    }

    private RpcServiceViewModel RpcService { get; }

    private INavigationService NavigationService { get; }

    private bool CanExcludeFromCoinjoin()
    {
        return WalletName is not null 
               && WalletName.Length > 0
               && TransactionId is not null 
               && TransactionId.Length > 0
               && N >= 0;
    }

    [RelayCommand(CanExecute = nameof(CanExcludeFromCoinjoin))]
    private async Task ExcludeFromCoinjoin()
    {
        var requestBody = new RpcMethod
        {
            Method = "excludefromcoinjoin",
            Params = new ExcludeFromCoinjoin
            {
                TransactionId = TransactionId,
                N = N,
                Exclude = Exclude
            }
        };
        var rpcServerUri = $"{RpcService.RpcServerPrefix}/{WalletName}";
        var result = await RpcService.SendRpcMethod(requestBody, rpcServerUri, RpcJsonContext.Default.RpcExcludeFromCoinjoinResult);
        if (result is RpcExcludeFromCoinjoinResult)
        {
            NavigationService.Clear();
            NavigationService.Navigate(new Success { Message = $"{(Exclude ? "Excluded" : "Removed the exclusion")} from coinjoin" });
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
