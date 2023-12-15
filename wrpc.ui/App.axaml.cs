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
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text.Json;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Input.Platform;
using Avalonia.Markup.Xaml;
using Avalonia.VisualTree;
using CommunityToolkit.Mvvm.Input;
using WasabiRpc.Models;
using WasabiRpc.Models.App;
using WasabiRpc.Models.BatchMode;
using WasabiRpc.Models.Services;
using WasabiRpc.Services;
using WasabiRpc.ViewModels;
using WasabiRpc.ViewModels.BatchMode;
using WasabiRpc.ViewModels.Factories;
using WasabiRpc.ViewModels.Services;
using WasabiRpc.Views;

namespace WasabiRpc;

public partial class App : Application
{
    private string StateFileName => "state.json";

    private string DefaultServerPrefix => "http://127.0.0.1:37128";

    public static IRelayCommand<string?>? CopyTextCommand = (Current as App)?.CopyCommand;

    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        var mainViewModel = CreateMainViewModel();

        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = new MainWindow
            {
                DataContext = mainViewModel
            };

            desktop.Exit += (_, _) =>
            {
                SaveState(mainViewModel);
            };
        }
        else if (ApplicationLifetime is ISingleViewApplicationLifetime single)
        {
            single.MainView = new MainView
            {
                DataContext = mainViewModel
            };

            // TODO: SaveState(mainViewModel);
        }

        base.OnFrameworkInitializationCompleted();
    }

    private State LoadState()
    {
        var defaultState = new State
        {
            Wallets = new List<string?>
            {
                "Wallet"
            },
            SelectedWallet = "Wallet",
            ServerPrefix = DefaultServerPrefix,
            BatchMode = false,
            Batches = new List<Batch>(),
            SelectedBatchIndex = -1
        };

        try
        {
            if (File.Exists(StateFileName))
            {
                var json = File.ReadAllText(StateFileName);
                var state = JsonSerializer.Deserialize(json, ModelsJsonContext.Default.State);
                if (state is not null)
                {
                    defaultState = state;
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }

        return defaultState;
    }

    private void SaveState(MainWindowViewModel mainViewModel)
    {
        try
        {
            var wallets = mainViewModel.Wallets?.Select(x => x.WalletName);

            var state = new State
            {
                ServerPrefix = mainViewModel.RpcService.ServerPrefix,
                BatchMode = mainViewModel.RpcService.BatchMode,
                Wallets = wallets?.ToList(),
                SelectedWallet = mainViewModel.SelectedWallet?.WalletName ?? "",
                Batches = mainViewModel.BatchManager.Batches?.ToBatches(),
                SelectedBatchIndex = mainViewModel.BatchManager.Batches is not null && mainViewModel.BatchManager.SelectedBatch is not null
                    ? mainViewModel.BatchManager.Batches.IndexOf(mainViewModel.BatchManager.SelectedBatch)
                    : -1
            };

            var json = JsonSerializer.Serialize(state, ModelsJsonContext.Default.State);
            File.WriteAllText(StateFileName, json);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }

    private MainWindowViewModel CreateMainViewModel()
    {
        var defaultState = LoadState();
        var httpService = new HttpService();
        var navigationService = new NavigationServiceViewModel();
        var detailsNavigationService = new NavigationServiceViewModel();
        var rpcService = new RpcServiceViewModel(httpService, defaultState.ServerPrefix ?? DefaultServerPrefix, defaultState.BatchMode);
        var batchManager = defaultState.Batches is null 
            ? CreateBatchManager(rpcService, navigationService, detailsNavigationService)
            : defaultState.Batches.ToViewModel(defaultState.SelectedBatchIndex, rpcService, navigationService, detailsNavigationService);
        return new MainWindowViewModel(rpcService, navigationService, detailsNavigationService, batchManager, defaultState);
    }

    private IBatchManager CreateBatchManager(
        RpcServiceViewModel rpcService, 
        INavigationService navigationService,
        INavigationService detailsNavigationService)
    {
        var batchManager = new BatchManagerViewModel(rpcService, navigationService, detailsNavigationService);

        var batch = new BatchViewModel(rpcService, navigationService, detailsNavigationService)
        {
            Name = "Batch",
            Jobs = new ObservableCollection<IJob>()
        };

        batchManager.Batches = new ObservableCollection<IBatch>
        {
            batch
        };

        batchManager.SelectedBatch = batch;

        return batchManager;
    }

    private static void CopyText(string? text, IClipboard clipboard)
    {
        try
        {
            clipboard.SetTextAsync(text);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }

    [RelayCommand]
    private void Copy(string? text)
    {
        switch (ApplicationLifetime)
        {
            case IClassicDesktopStyleApplicationLifetime classicDesktopStyleApplicationLifetime:
            {
                if (classicDesktopStyleApplicationLifetime.MainWindow?.Clipboard is { } clipboard)
                {
                    CopyText(text, clipboard);
                }
                break;
            }
            case ISingleViewApplicationLifetime singleViewApplicationLifetime:
            {
                if (singleViewApplicationLifetime.MainView?.GetVisualRoot() is TopLevel { Clipboard: { } clipboard })
                {
                    CopyText(text, clipboard);
                }
                break;
            }
        }
    }
}
