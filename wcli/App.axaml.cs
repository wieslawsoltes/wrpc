using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text.Json;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using WasabiCli.Models;
using WasabiCli.Models.App;
using WasabiCli.ViewModels;
using WasabiCli.ViewModels.Navigation;
using WasabiCli.ViewModels.RpcJson;
using WasabiCli.Views;

namespace WasabiCli;

public partial class App : Application
{
    private string StateFileName => "state.json";

    private string DefaultServerPrefix => "http://127.0.0.1:37128";

    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            var defaultState = new State
            {
                Wallets = new List<string?>
                {
                    "Wallet"
                },
                SelectedWallet = "Wallet",
                Batches = new List<Batch>(),
                ServerPrefix = DefaultServerPrefix,
                BatchMode = false
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
            catch (Exception)
            {
                // ignored
            }

            var navigationService = new NavigationServiceViewModel();
            var rpcService = new RpcServiceViewModel(defaultState.ServerPrefix ?? DefaultServerPrefix, defaultState.BatchMode)
            {
                Batches = new ObservableCollection<Batch>(defaultState.Batches ?? new List<Batch>())
            };

            Navigation.NavigationService = navigationService;

            var mainViewModel = new MainWindowViewModel(navigationService, rpcService, defaultState);

            desktop.MainWindow = new MainWindow
            {
                DataContext = mainViewModel
            };

            desktop.Exit += (_, _) =>
            {
                try
                {
                    var wallets = mainViewModel.Wallets?.Select(x => x.WalletName);

                    var state = new State
                    {
                        ServerPrefix = rpcService.ServerPrefix,
                        BatchMode = rpcService.BatchMode,
                        Wallets = wallets?.ToList(),
                        SelectedWallet = mainViewModel.SelectedWallet?.WalletName ?? "",
                        Batches = rpcService.Batches?.ToList()
                    };

                    var json = JsonSerializer.Serialize(state, ModelsJsonContext.Default.State);
                    File.WriteAllText(StateFileName, json);
                }
                catch (Exception)
                {
                    // ignored
                }
            };
        }

        base.OnFrameworkInitializationCompleted();
    }
}
