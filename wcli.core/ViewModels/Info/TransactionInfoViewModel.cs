using System;
using CommunityToolkit.Mvvm.ComponentModel;
using WasabiCli.Models.Services;

namespace WasabiCli.ViewModels.Info;

public partial class TransactionInfoViewModel : RoutableViewModel
{
    [ObservableProperty] 
    private DateTimeOffset _dateTime;

    [ObservableProperty] 
    private int _height;

    [ObservableProperty] 
    private long _amount;

    [ObservableProperty] 
    private string? _label;

    [ObservableProperty] 
    private string? _tx;

    [ObservableProperty] 
    private bool _isLikelyCoinJoin;

    public TransactionInfoViewModel(IRpcServiceViewModel rpcService, INavigationService navigationService)
        : base(rpcService, navigationService)
    {
    }

    // TODO:
}
