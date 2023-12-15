/*
 * wrpc A Graphical User Interface for using the Wasabi Wallet RPC.
 * Copyright (C) 2023  Wiesław Šoltés
 *
 * This program is free software: you can redistribute it and/or modify
 * it under the terms of the GNU Affero General Public License as
 * published by the Free Software Foundation, either version 3 of the
 * License, or (at your option) any later version.
 *
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU Affero General Public License for more details.
 *
 * You should have received a copy of the GNU Affero General Public License
 * along with this program.  If not, see <https://www.gnu.org/licenses/>.
 */
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using WasabiRpc.Models.BatchMode;
using WasabiRpc.Models.Services;
using WasabiRpc.ViewModels.App;
using WasabiRpc.ViewModels.Info;

namespace WasabiRpc.ViewModels.Methods.Adapters;

public partial class CoinAdapterViewModel : RoutableViewModel
{
    private readonly IBatchManager _batchManager;
    [ObservableProperty] private string? _walletName;
    [ObservableProperty] private bool _isSelected;

    public CoinAdapterViewModel(
        IRpcServiceViewModel rpcService, 
        INavigationService navigationService,
        INavigationService detailsNavigationService, 
        IBatchManager batchManager, 
        string? walletName, 
        CoinInfoViewModel coinInfo)
        : base(rpcService, navigationService, detailsNavigationService)
    {
        _batchManager = batchManager;
        WalletName = walletName;
        CoinInfo = coinInfo;
        IsSelected = false;
    }

    public CoinInfoViewModel CoinInfo { get; }

    [RelayCommand]
    private async Task ExcludeFromCoinJoin()
    {
        await Exclude(true);
    }

    [RelayCommand]
    private async Task RemoveExclusionFromCoinJoin()
    {
        await Exclude(false);
    }

    private async Task Exclude(bool exclude)
    {
        var excludeFromCoinJoinViewModel = new ExcludeFromCoinJoinViewModel(RpcService, NavigationService, DetailsNavigationService, _batchManager, WalletName)
        {
            TransactionId = CoinInfo.TxId,
            N = 0,
            Exclude = exclude
        };
        var job = excludeFromCoinJoinViewModel.CreateJob();
        var routable = await excludeFromCoinJoinViewModel.Execute(job);
        if (routable is SuccessViewModel successViewModel)
        {
            NavigationService.NavigateTo(successViewModel);
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
