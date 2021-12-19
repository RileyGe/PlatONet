﻿# 快速开始

本文旨在让有dotnet、区块链开发经验的开发者可以快速使用PlatONet。

## 1. 项目适用平台

PlatONet符合.net standard 2.0标准，可以广泛的应用于Windows、Mac、Linux等操作系统。

## 2. 安装与使用

打开NuGet命令行工具，运行以下命令：

```powershell
Install-Package Algorand
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

## 3.3. 与智能合约进行一次交互

## 3.4. 调用一次内置合约