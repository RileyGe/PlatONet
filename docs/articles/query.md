# 使用PlatONet连接PlatON网络并查询基本信息

查询网络基本信息是最常用，也最简单的操作。本节教程只涉及查询PlatON网络的公开信息，所以不需要使用账号，只需要一个PlatON网络的接入点即可。本教程只用到了Web3及PlatON类。

在PlatONet中，Web3类借鉴了Ethereum网络的Web3.js。Web3是网络的统一接口，使用Web3类，可以便捷的管理交易（Transaction）及账号（Account）等操作。Web3可以生成一个PlatON类，PlatON类对标的为Web3.js中的eth类，在PlatON类中主要包含了与区块链相关的方法。

所以，在使用Web3类之前，必须指定相应网络的接入点。PlatONet支持的网络有：

- PlatON主网
- PlatON开发网
- PlatON私有网络
- Alaya主网
- Alaya开发网
- Alaya私有网络

PlatONet支持上述所有的网络。在接入网络之前，你需要一个接入点。你可以自建接入点（请参照：[私有网络 | PlatON](https://devdocs.platon.network/docs/zh-CN/Build_Private_Chain)、[成为主网节点 | PlatON](https://devdocs.platon.network/docs/zh-CN/Become_PlatON_Main_Verification)、[私有网络 | Alaya](https://devdocs.alaya.network/alaya-devdocs/zh-CN/Private_network)、[主网全节点部署 | Alaya](https://devdocs.alaya.network/alaya-devdocs/zh-CN/Run_a_fullnode)），你可以使用官方提供的公共接入点（请参照：[开发网络 | PlatON](https://devdocs.platon.network/docs/zh-CN/Join_Dev_Network)、[开发网络 | Alaya](https://devdocs.alaya.network/alaya-devdocs/zh-CN/Join_the_dev_network)）。本教程连接的网络为PlatON开发网，使用官方提供的公共接入点。

选好网络，准备好接入点，查询工作就可以开始了。下述代码主要来自 `Basic.cs` ：

```csharp
using System;
using PlatONet;

namespace examples
{
    public class Basic
    {
        public static void Main(string[] args)
        {
            var w3 = new Web3("http://35.247.155.162:6789"); // dev net of platon
            var version = w3.GetProgramVersion();
            Console.WriteLine(version);
            var chainId = w3.GetChainId();
            Console.WriteLine(chainId.Value);

            // 快捷生成一个PlatON类的实例
            var platon = w3.PlatON;
            Console.WriteLine(platon.ProtocolVersion());
            Console.WriteLine(platon.GasPrice().Value);
            Console.WriteLine(platon.GetBalance("lat1d4vw2qxjg5ldyaqceel3s6ykpljav6hcn0jfmh").Value);          
            Console.WriteLine(platon.GetTransactionCount("lat1l32ggvel6ndxxlprplz04c3vm2mq4wtgvugn36").Value);
        }
    }
}
```

注意：上述代码无法在所有网站中都正常运行，这是由于不同网络的地址格式及相关Hash都不尽相同，请在使用前修改为相应网站的相关地址。

本教程主要介绍了PlatONet所支持的网络及Web3的基本使用方法。本节的内容比较简单，只涉及了Web3类及PlatON类的简单使用方法，后续会介绍更复杂的使用方法。