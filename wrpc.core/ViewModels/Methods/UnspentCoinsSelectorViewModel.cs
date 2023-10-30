using System.Collections.ObjectModel;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using WasabiRpc.Models.BatchMode;
using WasabiRpc.Models.Services;
using WasabiRpc.ViewModels.App;
using WasabiRpc.ViewModels.Info;
using WasabiRpc.ViewModels.Methods.Adapters;

namespace WasabiRpc.ViewModels.Methods;

public partial class UnspentCoinsSelectorViewModel : RoutableViewModel
{
    private readonly IBatchManager _batchManager;
    private readonly string? _walletName;

    [ObservableProperty]
    private ObservableCollection<CoinAdapterViewModel> _coins;

    public UnspentCoinsSelectorViewModel(
        IRpcServiceViewModel rpcService, 
        INavigationService navigationService,
        INavigationService detailsNavigationService, 
        IBatchManager batchManager, 
        string? walletName,
        ObservableCollection<CoinAdapterViewModel> coins)
        : base(rpcService, navigationService, detailsNavigationService)
    {
        _batchManager = batchManager;
        _walletName = walletName;

        Coins = coins;
    }

    [RelayCommand]
    private async Task ListUnspentCoins()
    {
        if (_walletName is null)
        {
            return;
        }

        var listUnspentCoinsViewModel = new ListUnspentCoinsViewModel(RpcService, NavigationService, DetailsNavigationService, _batchManager, _walletName);
        var job = listUnspentCoinsViewModel.CreateJob();
        var routable = await listUnspentCoinsViewModel.Execute(job);
        if (routable is ListUnspentCoinsInfoViewModel listUnspentCoinsInfoViewModel)
        {
            if (listUnspentCoinsInfoViewModel.Coins is not null)
            {
                Coins.Clear();

                foreach (var coin in listUnspentCoinsInfoViewModel.Coins)
                {
                    Coins.Add(coin);
                }
            }
        }
        else if (routable is ErrorInfoViewModel errorInfoViewModel)
        {
            DetailsNavigationService.NavigateTo(errorInfoViewModel);
        }
        else if (routable is ErrorViewModel errorViewModel)
        {
            DetailsNavigationService.NavigateTo(errorViewModel);
        }
    }
}
