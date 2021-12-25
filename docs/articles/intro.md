# PlatONet：PlatON/Alaya网络的dotnet sdk

PlatONet是PlatON/Alaya网络的dotnet sdk，通过PlatONet，开发人员能够使用dotnet(如c#，C++等)接入PlatON/Alaya网络，查询PlatON/Alaya网络的相关参数，如发送lat/atp，与智能合约进行交互，执行PlatON/Alaya网络的内置合约等。PlatONet包含client-java-sdk的所有功能。

## 1. 项目适用平台

PlatONet符合.net standard 2.0标准，可以广泛的应用于Windows、Mac、Linux等操作系统。

## 2. 安装与使用

打开Nuget命令行工具，运行以下命令：

```powershell
Install-Package PlatONet
```

或使用Visual Studio的NuGet包管理器，搜索并安装`PlatONet`。

## 3. 快速开始

请参照文档。

## 4. 文档

文档地址：https://rileyge.github.io/platonet-documents/

## 5. 项目结构

PlatONet的结构非常简单，主要包含了三个命名：`PlatONet`、`PlatONet.Crypto`、`PlatONet.DTOs`。其中`PlatONet.Crypto`主要是一些加密算法，仅供项目内部使用，对外公开的只有`PlatONet`和`PlatONet.DTOs`两个命名空间。

### 5.1. PlatONet命名空间

PlatONet的主体内容都存在于这个命名空间中，主要有以下几个类：

- Web3类：最重要入口，PlatONet的基础，可以用于查询网络基础信息、管理账号、PlatON类、PPOS类。
- PlatON类：PlatON网络类，用于交易和查询，也可以生成PlatONContract类与智能合约进行交互。
- PlatONContract类：智能合约类，用于与PlatON网络中的智能合约进行交互，也可以生成PlatONFunction。
- PlatONFunction类：智能合约方法类，直接调用智能合约的方法，进行交易等。
- Transaction类：交易信息类，进行交易内容的管理
- Account类：账号管理、签名。
- Address类：地址类，进行地址管理。

### 5.2. PlatONet.DTOs命名空间

此命名空间中包含多个数据传输对象（Data Transfer Objects），主要用于数据传输。

## 6. 参与本项目

任何形式的参与本项目都是欢迎的，你可以：

- 通过Issue可Discussions来参与本项目讨论
- 通过Pull Request向本项目提交代码
- 资助本项目：PlatON钱包地址：lat1vvtea8l8ve7xu0pncwgrgavdpkkql4e25jp6gk
- 给我运行的节点投票，节点名[rileyge](https://scan.platon.network/node-detail?address=0x78d2f0cb6b261f41c17893dbec000010818ffba2b41732d4a6d16b8af36e05f51d19529adae4674a2538cd5622974c0e9d60eab10de42099c4a600c435c4714f)投票
