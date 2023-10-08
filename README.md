# wcli

### RPC

- [x] getstatus
- [x] createwallet
- [x] recoverwallet
- [x] loadwallet
- [x] listcoins
- [x] listunspentcoins
- [x] getwalletinfo
- [x] getnewaddress
- [x] send
- [x] speeduptransaction
- [x] canceltransaction
- [x] build
- [x] broadcast
- [x] gethistory
- [x] excludefromcoinjoin
- [x] listkeys
- [x] startcoinjoin
- [x] stopcoinjoin
- [x] getfeerates
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
