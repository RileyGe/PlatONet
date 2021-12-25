﻿# 快速开始

本文旨在让有dotnet、区块链开发经验的开发者可以快速使用PlatONet。

## 1. 项目适用平台

PlatONet符合.net standard 2.0标准，可以广泛的应用于Windows、Mac、Linux等操作系统。

## 2. 安装与使用

打开NuGet命令行工具，运行以下命令：

```powershell
Install-Package PlatONet
```

或使用Visual Studio的NuGet包管理器，搜索并安装`PlatONet`。

## 3. 示例

[PlatONet](https://github.com/RileyGe/PlatONet)项目包含了非常多的示例，可以帮助用户快速开始使用PlatONet。具体请参照：[PlatONet示例](https://github.com/RileyGe/PlatONet/tree/main/examples)。示例基础.net core 3.1。

## 4. 项目结构

PlatONet的结构非常简单，主要包含了三个命名：`PlatONet`、`PlatONet.Crypto`、`PlatONet.DTOs`。其中`PlatONet.Crypto`主要是一些加密算法，仅供项目内部使用，对外公开的只有`PlatONet`和`PlatONet.DTOs`两个命名空间。

### 4.1. PlatONet命名空间

PlatONet的主体内容都存在于这个命名空间中，主要有以下几个类：

- Web3类：最重要入口，PlatONet的基础，可以用于查询网络基础信息、管理账号、PlatON类、PPOS类。
- PlatON类：PlatON网络类，用于交易和查询，也可以生成PlatONContract类与智能合约进行交互。
- PlatONContract类：智能合约类，用于与PlatON网络中的智能合约进行交互，也可以生成PlatONFunction。
- PlatONFunction类：智能合约方法类，直接调用智能合约的方法，进行交易等。
- Transaction类：交易信息类，进行交易内容的管理
- Account类：账号管理、签名。
- Address类：地址类，进行地址管理。

### 4.2. PlatONet.DTOs命名空间

此命名空间中包含多个数据传输对象（Data Transfer Objects），主要用于数据传输。

﻿## 5. 运行第一次PlatONet查询

```csharp
using System;
using PlatONet;

namespace examples
{
    public class Basic
    {
        public static void Main(string[] args)
        {
            var platonWeb3 = new Web3("http://35.247.155.162:6789"); // dev net of platon
            var version = platonWeb3.GetProgramVersion();
            Console.WriteLine(version);
            var chainId = platonWeb3.GetChainId();
            Console.WriteLine(chainId);            
        }
    }
}
```

## 6. 进行一次转账

```csharp
using PlatONet;
using System;

namespace examples
{
    public class Transfer
    {
        public static void Main(string[] args)
        {
            var privateKey = "d08baac64f52ae1b9c2ea559036650229f07f5d61d869dbb55562a9827fbaeb8";
            var w3 = new Web3("http://35.247.155.162:6789", privateKey); // dev net of platon
            var toAddress = "lat1ljlf4myhux0zahfmlxf79wr7sl8u7pdey88dyp";
            var amount = new HexBigInteger((ulong)1e18);
            var gasPrice = new HexBigInteger((ulong)1e9);
            var gasLimit = new HexBigInteger(21000);            
            var nonceNum = w3.PlatON.GetTransactionCount();

            // 构建交易
            var tx = new Transaction(toAddress, amount, nonceNum, gasPrice, gasLimit);
            //发送交易
            var result = w3.PlatON.SendTransaction(tx);
            Console.WriteLine(result);
        }
    }
}
```

## 7. 智能合约部署

```csharp
using PlatONet;
using System;

namespace examples
{
    public class ContractDeploy
    {
        public static void Main(string[] args)
        {
            var bytecode = "0x608060405234801561001057600080fd5b50610429806100206000396000f3fe608060405234801561001057600080fd5b50600436106100365760003560e01c806317d7de7c1461003b578063c47f0027146100be575b600080fd5b6100436101f2565b6040518080602001828103825283818151815260200191508051906020019080838360005b83811015610083578082015181840152602081019050610068565b50505050905090810190601f1680156100b05780820380516001836020036101000a031916815260200191505b509250505060405180910390f35b610177600480360360208110156100d457600080fd5b81019080803590602001906401000000008111156100f157600080fd5b82018360208201111561010357600080fd5b8035906020019184600183028401116401000000008311171561012557600080fd5b91908080601f016020809104026020016040519081016040528093929190818152602001838380828437600081840152601f19601f820116905080830192505050505050509192919290505050610294565b6040518080602001828103825283818151815260200191508051906020019080838360005b838110156101b757808201518184015260208101905061019c565b50505050905090810190601f1680156101e45780820380516001836020036101000a031916815260200191505b509250505060405180910390f35b606060008054600181600116156101000203166002900480601f01602080910402602001604051908101604052809291908181526020018280546001816001161561010002031660029004801561028a5780601f1061025f5761010080835404028352916020019161028a565b820191906000526020600020905b81548152906001019060200180831161026d57829003601f168201915b5050505050905090565b606081600090805190602001906102ac92919061034f565b5060008054600181600116156101000203166002900480601f0160208091040260200160405190810160405280929190818152602001828054600181600116156101000203166002900480156103435780601f1061031857610100808354040283529160200191610343565b820191906000526020600020905b81548152906001019060200180831161032657829003601f168201915b50505050509050919050565b828054600181600116156101000203166002900490600052602060002090601f016020900481019282601f1061039057805160ff19168380011785556103be565b828001600101855582156103be579182015b828111156103bd5782518255916020019190600101906103a2565b5b5090506103cb91906103cf565b5090565b6103f191905b808211156103ed5760008160009055506001016103d5565b5090565b9056fea265627a7a72315820a89a95b35ad9e15c39082d4e9ece93b95b29072f95af709314ba017e1fb1487764736f6c63430005110032";
            var privateKey = "d08baac64f52ae1b9c2ea559036650229f07f5d61d869dbb55562a9827fbaeb8";
            var w3 = new Web3("http://35.247.155.162:6789", privateKey); // dev net of platon
            var tx = new Transaction()
            {
                GasPrice = 1000000000L.ToHexBigInteger(),
                Data = bytecode
            };
            tx = w3.PlatON.FillTransactionWithDefaultValue(tx);
            var result = w3.PlatON.SendTransaction(tx);
            Console.WriteLine(result);
        }
    }
}
```

## 8. 与智能合约进行一次交互

```csharp
using PlatONet;
using System;

namespace examples
{
    public class Contract3
    {
        public static void Main(string[] args)
        {
            var privateKey = "d08baac64f52ae1b9c2ea559036650229f07f5d61d869dbb55562a9827fbaeb8";
            var w3 = new Web3("http://35.247.155.162:6789", privateKey);
            var abi = @"[
        {
          ""constant"": false,
          ""inputs"": [
            {
              ""internalType"": ""string"",
              ""name"": ""_name"",
              ""type"": ""string""
            }
          ],
          ""name"": ""setName"",
          ""outputs"": [
            {
              ""internalType"": ""string"",
              ""name"": """",
              ""type"": ""string""
            }
          ],
          ""payable"": false,
          ""stateMutability"": ""nonpayable"",
          ""type"": ""function""
        },
        {
          ""constant"": true,
          ""inputs"": [],
          ""name"": ""getName"",
          ""outputs"": [
            {
              ""internalType"": ""string"",
              ""name"": """",
              ""type"": ""string""
            }
          ],
          ""payable"": false,
          ""stateMutability"": ""view"",
          ""type"": ""function""
        }
      ]";
            var contractAddress = new Address("lat1ts7u6ekl9zfmqw399wvnfh2gxc5gkjv34ymkvt");
            var contract = w3.PlatON.GetContract(abi, contractAddress.ToString());
            var result = contract.GetFunction("getName").Call<string>();
            Console.WriteLine(result);
            var nameResult = contract.GetFunction("setName").SendTransaction("abcabc");
            Console.WriteLine(nameResult);
        }
    }
}
```

## 9. 调用一次内置合约

```csharp
using PlatONet;
using PlatONet.DTOs;
using System;

namespace examples
{
    public class PPOS
    {
        public static void Main(string[] args)
        {
            var privateKey = "d08baac64f52ae1b9c2ea559036650229f07f5d61d869dbb55562a9827fbaeb8";
            var w3 = new Web3("http://35.247.155.162:6789", privateKey);
            Console.WriteLine((new Address("lat1d4vw2qxjg5ldyaqceel3s6ykpljav6hcn0jfmh")).ToEthereumAddress());
            var ppos = w3.PlatON.PPOS;
            var nodeId = "0x177226cb3440cec3b8e7b7d591fde985665a3fc1e069a7f9db86080350cb91e8ecb1cad35ed786ee22256229182f79909dcf7431b58e58c9a706935b6046ffb2";
            var result = ppos.GetStakingInfo(nodeId);
            Console.WriteLine(result);

            string hash = ppos.Delegate(nodeId, StakingAmountType.FREE_AMOUNT_TYPE,
                ((ulong)1e19).ToHexBigInteger());
            Console.WriteLine(hash);            
        }
    }
}
```

相信有了前面的这些简单的例子，大家就能很快的开始使用PlatONet了，Enjoy！！！