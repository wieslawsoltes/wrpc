# wcli

### RPC

- [x] getstatus
- [x] createwallet
- [x] listcoins
- [x] listunspentcoins
- [x] getwalletinfo
- [x] getnewaddress
- [ ] send
- [ ] build
- [ ] broadcast
- [x] gethistory
- [x] listkeys
- [ ] startcoinjoin
- [ ] stopcoinjoin
- [x] stop

### Resources

- [wcli](https://github.com/wieslawsoltes/wcli)
- [RPC Docs](https://docs.wasabiwallet.io/using-wasabi/RPC.html)
- [WalletWasabi.Daemon](https://github.com/zkSNACKs/WalletWasabi/tree/master/WalletWasabi.Daemon)
- [WalletWasabi.Daemon WasabiJsonRpcService.cs](https://github.com/zkSNACKs/WalletWasabi/blob/master/WalletWasabi.Daemon/Rpc/WasabiJsonRpcService.cs)
- [bitcoin.design](https://bitcoin.design/)
- [bitcoinuikit.com](https://www.bitcoinuikit.com/)

### Testing

```
cd WalletWasabi.Daemon
dotnet run -- --usetor=false --network=testnet --jsonrpcserverenabled=true --blockonly=true
```

```
wasabi.daemon --usetor=false --network=testnet --jsonrpcserverenabled=true --blockonly=true
```
