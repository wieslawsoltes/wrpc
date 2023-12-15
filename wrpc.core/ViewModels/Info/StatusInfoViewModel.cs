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
using System.Collections.Generic;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using WasabiRpc.Models.Services;

namespace WasabiRpc.ViewModels.Info;

public partial class StatusInfoViewModel : RoutableViewModel
{
    [ObservableProperty] 
    private string? _torStatus;

    [ObservableProperty] 
    private string? _onionService;

    [ObservableProperty] 
    private string? _backendStatus;

    [ObservableProperty] 
    private string? _bestBlockchainHeight;

    [ObservableProperty] 
    private string? _bestBlockchainHash;

    [ObservableProperty] 
    private int _filtersCount;

    [ObservableProperty] 
    private int _filtersLeft;

    [ObservableProperty] 
    private string? _network;

    [ObservableProperty] 
    private decimal _exchangeRate;

    [ObservableProperty] 
    private List<PeerInfoViewModel>? _peers;

    public StatusInfoViewModel(
        IRpcServiceViewModel rpcService, 
        INavigationService navigationService,
        INavigationService detailsNavigationService, 
        ICommand refreshCommand)
        : base(rpcService, navigationService, detailsNavigationService)
    {
        RefreshCommand = refreshCommand;
    }

    public ICommand RefreshCommand { get; }
}
