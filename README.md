# wcli

### RPC

- [x] getstatus
- [x] createwallet
- [x] listcoins
- [x] listunspentcoins
- [x] getwalletinfo
- [ ] getnewaddress
- [ ] send
- [ ] build
- [ ] broadcast
- [ ] gethistory
- [ ] listkeys
- [ ] startcoinjoin
- [ ] stopcoinjoin
- [ ] stop

### Resources

- https://docs.wasabiwallet.io/using-wasabi/RPC.html
- https://github.com/zkSNACKs/WalletWasabi/tree/master/WalletWasabi.Daemon
- https://github.com/zkSNACKs/WalletWasabi/blob/master/WalletWasabi.Daemon/Rpc/WasabiJsonRpcService.cs#L35-L308
- https://github.com/zkSNACKs/WalletWasabi/tree/master/Contrib/CLI

- https://github.com/wieslawsoltes/wcli
- https://github.com/jinek/Consolonia
- https://github.com/AvaloniaInside/Shell

- https://bitcoin.design/
- https://www.bitcoinuikit.com/

### Testing

```
cd WalletWasabi.Daemon
dotnet run -- --usetor=false --network=testnet --jsonrpcserverenabled=true --blockonly=true
```

```
wasabi.daemon --usetor=false --network=testnet --jsonrpcserverenabled=true --blockonly=true
```
