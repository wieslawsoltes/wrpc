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
        var rpcService = new RpcServiceViewModel(httpService, defaultState.ServerPrefix ?? DefaultServerPrefix, defaultState.BatchMode);
        var batchManager = defaultState.Batches is null 
            ? CreateBatchManager(rpcService, navigationService)
            : defaultState.Batches.ToViewModel(defaultState.SelectedBatchIndex, rpcService, navigationService);
        return new MainWindowViewModel(rpcService, navigationService, batchManager, defaultState);
    }

    private IBatchManager CreateBatchManager(RpcServiceViewModel rpcService, NavigationServiceViewModel navigationService)
    {
        var batchManager = new BatchManagerViewModel(rpcService, navigationService);

        var batch = new BatchViewModel(rpcService, navigationService)
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
