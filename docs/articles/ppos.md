# 系统合约交互

在链启动之后系统内部已经内置了部分合约，这些合约的地址已固定，功能已实现，其中一部分合约为经济模型的实现，并提供各类合约接口与客户端进行交互。更多详细内容请参照：[系统合约](https://devdocs.platon.network/docs/zh-CN/PlatON_system_contract)。

从原理上来讲，系统合约与普通合约的交互方式相同，用户可以自行构造abi来使用与普通智能合约交互的方式来与系统合约进行交互。但PlatONet已经将系统合约进行了封装，使用户可以更便捷使用系统合约。封装后开发者可以像使用普通函数一样与系统合约交互。

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
            var ppos = w3.PlatON.PPOS; // 13
            var nodeId = "0x177226cb3440cec3b8e7b7d591fde985665a3fc1e069a7f9db86080350cb91e8ecb1cad35ed786ee22256229182f79909dcf7431b58e58c9a706935b6046ffb2";
            var result = ppos.GetStakingInfo(nodeId); // 15
            Console.WriteLine(result);

            string hash = ppos.Delegate(nodeId, StakingAmountType.FREE_AMOUNT_TYPE,
                ((ulong)1e19).ToHexBigInteger()); // 19
            Console.WriteLine(hash);
        }
    }
}
```

- 第13行：`ppos`变量是为了方便书写而定义的，如果后面全部使用`w3.PlatON.PPOS`替代`ppos`效果是相同的。
- 第15行：查询系统合约状态。由此可以看出，PlatONet已经将系统合约封装成普通函数，就像调用普通函数一样调用即可。
- 第18~19行：写系统合约调用。由于此操作涉及区块链的写操作，会改变区块链状态，需要在使用前初始化账号，否则会调用失败。

