# A Graphical User Interface for using the Wasabi Wallet RPC

[![NuGet](https://img.shields.io/nuget/v/wrpc.core.svg)](https://www.nuget.org/packages/wrpc.core)
[![NuGet](https://img.shields.io/nuget/dt/wrpc.core.svg)](https://www.nuget.org/packages/wrpc.core)

[![NuGet](https://img.shields.io/nuget/v/wrpc.ui.svg)](https://www.nuget.org/packages/wrpc.ui)
[![NuGet](https://img.shields.io/nuget/dt/wrpc.ui.svg)](https://www.nuget.org/packages/wrpc.ui)

### How to run

You need the [same requirements](https://github.com/zkSNACKs/WalletWasabi/blob/master/README.md#get-the-requirements) as Wasabi Wallet.

Clone this repository:
```sh
git clone https://github.com/wieslawsoltes/wrpc.git
```

Easiest way to get started is by using the Desktop app:
```sh
cd wrpc/wrpc.ui.desktop
dotnet run
```

In order to use it, Wasabi Wallet ([Daemon](https://docs.wasabiwallet.io/using-wasabi/Daemon.html) or GUI app) needs to be running with JSON RPC Server [enabled/configured](https://docs.wasabiwallet.io/using-wasabi/RPC.html#configure-rpc).

```
cd WalletWasabi.Daemon
dotnet run --usetor=false --network=testnet --jsonrpcserverenabled=true --blockonly=true
```

Note: Not all methods might work, depending on your Wasabi version.

### Native AOT deployment

The Desktop version supports [NativeAOT](https://learn.microsoft.com/en-us/dotnet/core/deploying/native-aot) deployment.

#### Change directory to Desktop project

```sh
cd wrpc.ui.desktop
```

#### Publish `win-x64` RID

```sh
dotnet publish -r win-x64 -c Release
```

```sh
.\bin\Release\net8.0\win-x64\publish\wrpc.exe
```

#### Publish `linux-x64` RID

```sh
dotnet publish -r linux-x64 -c Release
```

#### Publish `linux-arm64` RID

```sh
dotnet publish -r linux-arm64 -c Release
```

#### Publish `osx-x64` RID

```sh
dotnet publish -r osx-x64 -c Release
```

#### Publish `osx-arm64` RID

```sh
dotnet publish -r osx-arm64 -c Release
```

### Resources

- [wrpc](https://github.com/wieslawsoltes/wrpc)
- [RPC Docs](https://docs.wasabiwallet.io/using-wasabi/RPC.html)
- [WalletWasabi.Daemon](https://github.com/zkSNACKs/WalletWasabi/tree/master/WalletWasabi.Daemon)
- [WalletWasabi.Daemon WasabiJsonRpcService.cs](https://github.com/zkSNACKs/WalletWasabi/blob/master/WalletWasabi.Daemon/Rpc/WasabiJsonRpcService.cs)
- [bitcoin.design](https://bitcoin.design/)
- [bitcoinuikit.com](https://www.bitcoinuikit.com/)

### Available RPC methods

- [x] getstatus
  - params
    - [null]
  - error
    - TODO
  - result
    - torStatus
    - onionService
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
- [x] payincoinjoin
  - params
    - address
    - amount
  - result
    - paymentId
- [x] listpaymentsincoinjoin
    - params
      - [null]
    - error
      - TODO
    - result
      - payments
        - payment
          - id
          - amount
          - destination
          - state
            - status
            - round
            - txid
          - address
- [x] cancelpaymentincoinjoin
    - params
      - paymentId
    - error
      - TODO
    - [empty]
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
