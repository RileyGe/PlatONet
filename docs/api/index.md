# PlatONet API文档
通过此文档，用户可以快速的了解如何使用PlatONet的每一个函数。

## 1. PlatONet结构

PlatONet的结构非常简单，主要包含了三个命名：`PlatONet`、`PlatONet.Crypto`、`PlatONet.DTOs`。其中`PlatONet.Crypto`主要是一些加密算法，仅供项目内部使用，对外公开的只有`PlatONet`和`PlatONet.DTOs`两个命名空间。

### 1.1. PlatONet命名空间

PlatONet的主体内容都存在于这个命名空间中，主要有以下几个类：

- Web3类：最重要入口，PlatONet的基础，可以用于查询网络基础信息、管理账号、PlatON类、PPOS类。
- PlatON类：PlatON网络类，用于交易和查询，也可以生成PlatONContract类与智能合约进行交互。
- PlatONContract类：智能合约类，用于与PlatON网络中的智能合约进行交互，也可以生成PlatONFunction。
- PlatONFunction类：智能合约方法类，直接调用智能合约的方法，进行交易等。
- Transaction类：交易信息类，进行交易内容的管理
- Account类：账号管理、签名。
- Address类：地址类，进行地址管理。

### 1.2. PlatONet.DTOs命名空间

此命名空间中包含多个数据传输对象（Data Transfer Objects），主要用于数据传输。
