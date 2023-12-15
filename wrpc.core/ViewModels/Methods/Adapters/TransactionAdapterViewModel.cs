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

public partial class TransactionAdapterViewModel : RoutableViewModel
{
    private readonly IBatchManager _batchManager;
    [ObservableProperty] private string? _walletName;
    [ObservableProperty] private string? _walletPassword;
    [ObservableProperty] private bool _isSelected;

    public TransactionAdapterViewModel(
        IRpcServiceViewModel rpcService, 
        INavigationService navigationService,
        INavigationService detailsNavigationService, 
        IBatchManager batchManager, 
        string? walletName, 
        TransactionInfoViewModel transactionInfo)
        : base(rpcService, navigationService, detailsNavigationService)
    {
        _batchManager = batchManager;
        WalletName = walletName;
        // TODO: Set WalletPassword
        WalletPassword = null;
        TransactionInfo = transactionInfo;
        IsSelected = false;
    }

    public TransactionInfoViewModel TransactionInfo { get; }

    [RelayCommand]
    private async Task SpeedUpTransaction()
    {
        var speedUpTransactionViewModel = new SpeedUpTransactionViewModel(RpcService, NavigationService, DetailsNavigationService, _batchManager, WalletName)
        {
            TxId = TransactionInfo.Tx,
            WalletPassword = WalletPassword,
        };
        var job = speedUpTransactionViewModel.CreateJob();
        var routable = await speedUpTransactionViewModel.Execute(job);
        if (routable is BuildInfoViewModel buildInfoViewModel)
        {
            NavigationService.NavigateTo(buildInfoViewModel);
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

    [RelayCommand]
    private async Task CancelTransaction()
    {
        var cancelTransactionViewModel = new CancelTransactionViewModel(RpcService, NavigationService, DetailsNavigationService, _batchManager, WalletName)
        {
            TxId = TransactionInfo.Tx,
            WalletPassword = WalletPassword,
        };
        var job = cancelTransactionViewModel.CreateJob();
        var routable = await cancelTransactionViewModel.Execute(job);
        if (routable is BuildInfoViewModel buildInfoViewModel)
        {
            NavigationService.NavigateTo(buildInfoViewModel);
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
