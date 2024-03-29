# PlatONet
PlatONet是PlatON网络的.dotnet SDK。该SDK基于.net standard 2.0，具有良好的跨平台性能。

PlatONet主要依赖于BouncyCastle和Nethereum两个开源项目。
# 使用教程

## 1. 从源码编译使用

### 1.1. 安装.NET Core 3.1

在开始之前，请务必安装.NET Core 3.1，具体安装方法请参考：[Install .NET on Windows, Linux, and macOS](https://docs.microsoft.com/en-us/dotnet/core/install/)。

### 1.2. 包还原

包还原的功能是从NuGet中下载项目引用的包，根据网络环境不同可能需要较长时间。

如果你使用Visual Studio 2019打开本项目，一般情况下在打开项目后会自动进行包还原，如果包还原没有自动进行，可以通过在解决方案资源管理器中右键单击解决方案，选择“还原NuGet包(R)”来进行包还原。

也可以在项目根目录使用命令行运行 `dotnet restore` 命令来显式的进行 NuGet 包还原依赖项。但在大多数情况下，不需要显式使用 `dotnet restore` 命令，因为在运行后续命令时，将会在必要时隐式运行 NuGet 还原。

### 1.3. 生成项目

如果您使用Visual Studio 2019作为开发工具，您可以通过**生成->生成项目**来进行项目生成，也可以在命令行中使用 `dotnet build` 来生成项目。

### 1.4. 运行示例

为了帮助大家更快的了解如何使用PlatONet，特编写了示例。里面包含了查询信息、转账、智能合约部署、智能合约调用等基本操作，大家可以自行探索使用。

## 2. 安装NuGet包

打开Nuget命令行工具，运行以下命令：

```powershell
Install-Package PlatONet
```

或使用Visual Studio的NuGet包管理器，搜索并安装`PlatONet`。

## 3. 文档

文档地址：https://rileyge.github.io/platonet-documents/

## 4. 参与本项目

任何形式的参与本项目都是欢迎的，你可以：

- 通过Issue可Discussions来参与本项目讨论
- 通过Pull Request向本项目提交代码
- 资助本项目：PlatON钱包地址：lat1vvtea8l8ve7xu0pncwgrgavdpkkql4e25jp6gk
- 给我运行的节点投票，节点名[rileyge](https://scan.platon.network/node-detail?address=0x78d2f0cb6b261f41c17893dbec000010818ffba2b41732d4a6d16b8af36e05f51d19529adae4674a2538cd5622974c0e9d60eab10de42099c4a600c435c4714f)投票
