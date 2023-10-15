# wcli

### RPC

- [x] getstatus
  - result
    - torStatus
    - backendStatus
    - bestBlockchainHeight
    - bestBlockchainHash
    - filtersCount
    - filtersLeft
    - network
    - exchangeRate
    - peers
      - isConnected
      - lastSeen
      - endpoint
      - userAgent
- [x] createwallet
  - result
    - mnemonic
- [x] recoverwallet
  - [empty]
- [x] loadwallet
  - [empty]
- [x] listcoins
  - result
    - coins
      - txid
      - index
      - amount
      - anonymityScore
      - confirmed
      - confirmations
      - keyPath
      - address
      - spentBy
- [x] listunspentcoins
  - result
    - coins
      - txid
      - index
      - amount
      - anonymityScore
      - confirmed
      - confirmations
      - keyPath
      - address
      - spentBy
- [x] getwalletinfo
  - result
    - walletName
    - walletFile
    - state
    - masterKeyFingerprint
    - accounts
      - name
      - publicKey
      - keyPath
    - balance
    - anonScoreTarget
    - coinjoinStatus
- [x] getnewaddress
  - result
    - address
    - keyPath
    - label
    - publicKey
    - scriptPubKey
- [x] send
  - result
    - txid
    - tx
- [x] speeduptransaction
  - result
    - tx
- [x] canceltransaction
  - result
    - tx
- [x] build
  - result
    - tx
- [x] broadcast
  - result
    - txid
- [x] gethistory
  - result
    - transactions
      - datetime
      - height
      - amount
      - label
      - tx
      - islikelycoinjoin
- [x] excludefromcoinjoin
  - [empty]
- [x] listkeys
  - result
    - keys
      - fullKeyPath
      - internal
      - keyState
      - label
      - scriptPubKey
      - pubkey
      - pubKeyHash
      - address
- [x] startcoinjoin
  - [empty]
- [x] startcoinjoinsweep
  - [empty]
- [x] stopcoinjoin
  - [empty]
- [x] getfeerates
  - result
    - [dictionary]
- [x] stop
  - [empty]

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
