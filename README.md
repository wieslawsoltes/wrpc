# A Graphical User Interface for using the Wasabi Wallet RPC

### How to run

You need the [same requirements](https://github.com/zkSNACKs/WalletWasabi/blob/master/README.md#get-the-requirements) as Wasabi Wallet.

Clone this repository:
```sh
git clone https://github.com/wieslawsoltes/wcli.git
```

Easiest way to get started is by using the Desktop app:
```sh
cd wcli/wcli.ui.desktop
dotnet run
```

In order to use it, Wasabi Wallet ([Daemon](https://docs.wasabiwallet.io/using-wasabi/Daemon.html) or GUI app) needs to be running with JSON RPC Server [enabled/configured](https://docs.wasabiwallet.io/using-wasabi/RPC.html#configure-rpc).

```
cd WalletWasabi.Daemon
dotnet run --usetor=false --network=testnet --jsonrpcserverenabled=true --blockonly=true
```

Note: Not all methods might work, depending on your Wasabi version.

### Available RPC methods

- [x] getstatus
  - params
    - [null]
  - error
    - TODO
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
  - params
    - walletName
    - walletPassword
  - error
    - TODO
  - result
    - mnemonic
- [x] recoverwallet
  - params
    - walletName
    - walletMnemonic
    - walletPassword
  - error
    - TODO
  - [empty]
- [x] loadwallet
  - params
    - walletName
  - error
    - TODO
  - [empty]
- [x] listcoins
  - params
    - [null]
  - error
    - TODO
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
  - params
    - [null]
  - error
    - TODO
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
  - params
    - [null]
  - error
    - TODO
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
    - isWatchOnly
    - isHardwareWallet
    - isAutoCoinjoin
    - isRedCoinIsolation
    - coinjoinStatus
- [x] getnewaddress
  - params
    - label
  - error
    - TODO
  - result
    - address
    - keyPath
    - label
    - publicKey
    - scriptPubKey
- [x] send
  - params
    - [object]
      - payments
        - payment
          - sendto
          - amount
          - label
          - subtractFee
      - coins
        - transactionid
        - index
      - feeTarget
      - feeRate
      - password
  - error
    - TODO
  - result
    - txid
    - tx
- [x] speeduptransaction
  - params
    - [object]
      - txId
      - password
  - result
    - tx
- [x] canceltransaction
  - params
    - [object]
      - txId
      - password
  - error
    - TODO
  - result
    - tx
- [x] build
  - params
    - [object]
      - payments
        - payment
          - sendto
          - amount
          - label
          - subtractFee
      - coins
        - transactionid
        - index
      - feeTarget
      - feeRate
      - password
  - error
    - TODO
  - result
    - tx
- [x] broadcast
  - params
    - tx
  - result
    - txid
- [x] gethistory
  - params
    - [null]
  - error
    - TODO
  - result
    - transactions
      - datetime
      - height
      - amount
      - label
      - tx
      - islikelycoinjoin
- [x] excludefromcoinjoin
  - params
    - [object]
      - transactionId
      - n
      - exclude
  - error
    - TODO
  - [empty]
- [x] listkeys
  - params
    - [null]
  - error
    - TODO
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
  - params
    - walletPassword
    - stopWhenAllMixed
    - overridePlebStop
  - error
  - TODO
  - [empty]
- [x] startcoinjoinsweep
  - params
    - walletPassword
    - outputWalletName
  - error
    - TODO
  - [empty]
- [x] stopcoinjoin
  - params
    - [null]
  - error
    - TODO
  - [empty]
- [x] getfeerates
  - params
    - [null]
  - error
    - TODO
  - result
    - [dictionary]
- [x] listwallets
  - params
    - [null]
  - error
    - TODO
  - result
    - wallets
      - walletName
- [x] stop
  - params
    - [null]
  - error
    - TODO
  - [empty]

### Resources

- [wcli](https://github.com/wieslawsoltes/wcli)
- [RPC Docs](https://docs.wasabiwallet.io/using-wasabi/RPC.html)
- [WalletWasabi.Daemon](https://github.com/zkSNACKs/WalletWasabi/tree/master/WalletWasabi.Daemon)
- [WalletWasabi.Daemon WasabiJsonRpcService.cs](https://github.com/zkSNACKs/WalletWasabi/blob/master/WalletWasabi.Daemon/Rpc/WasabiJsonRpcService.cs)
- [bitcoin.design](https://bitcoin.design/)
- [bitcoinuikit.com](https://www.bitcoinuikit.com/)
