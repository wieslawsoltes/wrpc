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
using CommunityToolkit.Mvvm.ComponentModel;
using WasabiRpc.Models.Services;

namespace WasabiRpc.ViewModels.Info;

public partial class CoinInfoViewModel : RoutableViewModel
{
    [ObservableProperty] 
    private string? _txId;

    [ObservableProperty] 
    private int _index;

    [ObservableProperty] 
    private long _amount;

    [ObservableProperty] 
    private decimal _anonymityScore;

    [ObservableProperty] 
    private bool _confirmed;

    [ObservableProperty] 
    private int _confirmations;

    [ObservableProperty] 
    private string? _keyPath;

    [ObservableProperty] 
    private string? _address;

    [ObservableProperty] 
    private string? _spentBy;

    public CoinInfoViewModel(
        IRpcServiceViewModel rpcService, 
        INavigationService navigationService,
        INavigationService detailsNavigationService)
        : base(rpcService, navigationService, detailsNavigationService)
    {
    }
}
