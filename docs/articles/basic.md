﻿# 账号、地址及转账操作

本篇会介绍账号的相关知识及账号生成的相关操作。本教程分为两大部分，第一部分为账号的基本概念，在这里你能学到一些相关账号的基本概念，以及一些安全方面的建议。如果你对此部分内容比较熟悉或不感兴趣，可以直接跳过第一部分，进入第二部分账号生成。在第二部分中主要讲操作，本教程会带领大家用PlatONet成账号，参与PlatON/Alaya网络交易。

## 1. 基本概念

如果你是第一次接触区块链，那么PlatON/Alaya的账号（Account）的概念对你来说可能难以理解。Alaya的账号与中心化的账号有很大不同。无论你在银行还是其他中心化的应用中，如微信，支付宝等，你都要准备好资料并向中心化的组织来提交你的资料，你才能开户一个账号。但在PlatON/Alaya中，你可以自行生成一个公私钥对，如果你将你的账号地址（Address）分享给大家，大家就可以与你的账号发生交互了。

这里已经出现了账号、公钥、私钥及地址等概念，不要着急，下面我们就慢慢的一一解释这些概念。我们先看下面一张图片：

![Account](C:\Users\RLGE\OneDrive - whu.edu.cn\blockchain\alaya及platon\教程\账号及账号生成\account.png)

### 1.1. 账户（Accounts）

在进行正式解释之前，我们先做一下类比。PlatON/Alaya中的**普通账号**和银行账号有很大的相似之外。你的银行账号里面会记录你账号上有多少钱，进行过什么操作等。PlatON/Alaya中也一样，PlatON/Alaya网络会维护一棵状态树，该树以账号地址为索引，存储账号的余额（balance）、交易计数（nonce）等。

PlatON/Alaya还有另外一种账号是**合约账号**，通常我们也直接称之为合约。合约账号和普通账号的信息都存储于同一棵树中，合约也有余额（balance）、交易计数（nonce）等，同时他还具有代码等。

### 1.2. 地址（Address）

如果类比一下，地址就是你银行账户的银行卡号。在PlatON/Alaya网络中，一个PlatON/Alaya地址就代表着一个账户，地址是账户的标识。也就是如我们1.1中所说，PlatON/Alaya中的状态树是以地址为索引的。

与银行账号不同的是，如果别人知道了你的账号地址，那么任何人都可以通过你的地址查询到你的所有交易。

不知大家发现没有，在PlatON网络中，所有地址都是以lat1开头的（在Alaya网络中以atp1开头），这是为什么呢？这是由于为了提高地址的可读性，PlatON/Alaya网络将原地址（一般使用16进制表示，如常见的0x开头的以太坊地址）经过Bech32编码，形成了现有地址。

>Bech32最早出现在 Bitcoin 中，其组成如下：
>
>hrp(human-readable part)：可读前缀
>seperator：分隔符，永远是“1”。
>data part：数据部分，包含小写字母和数字。但数据部分不可包含字符“1”（被用作了分隔符），“b”、“i”、“o”（可读性不强，容易与其他字符混淆）。这样数据部分每一位都有32位可能取值。
>checksum：校验部分。校验部分为地址的最后 6 位，可用于校验该字符串的正确性。
>
>下图为地址组成部分示意图：
>
>![地址组成部分](C:\Users\RLGE\OneDrive - whu.edu.cn\blockchain\alaya及platon\教程\账号及账号生成\18.png)

### 1.3. 私钥（Private Key）和公钥（Public Key）

私钥和公钥是非对称加密中的概念。在非对称加密中，我们通过某种算法来生成一个公私钥对，这个公私钥对有以下特征：

- 知道公钥不可能（或者是非常难）推算出私钥，但知道私钥很容易推算出私钥。
- 用公钥进行加密后的数据只有使用私钥才能对其解密。
- 用私钥对数据进行签名后用公钥可以验证签名的正确性。

那么公私钥前面的账号和地址有什么关系呢？先说结论：

- 私钥的持有者对账号有所有权，可以对账号进行任何PlatON/Alaya网络支持的操作。
- 地址是由公钥经Hash操作得到的。也就是说**由公钥可以容易的获得地址，但有地址却无法推算出公钥**。

> 安全提醒：
>
> 1. 由于私钥的持有者可以对账号进行任何Alaya支持的操作，所以如果你的私钥丢失，那么你将完全的失去你的账号，任何人对此都无能为力。
> 2. 账号进行过发送操作，其公钥就会暴露。从某种角度来讲，其安全性确实降低了。但在现有的技术水平下，公钥暴露完全造成的安全影响非常微小。到目前为止，所有区块链项目都没有因为公钥暴露而产生安全问题。

### 1.4. 助记词

助记词从名字就能看出来是干嘛的。之前说过私钥非常非常重要，但私钥又非常非常长（否则容易被破解），私钥也完全没有任何顺序，没有任何意义（否则容易被人破解）。所以为了帮助大家更好的记忆私钥，大家会有一些常用的单词，来代表私钥，这样私钥就容易记录一些了。

也就是说助记词是可以和私钥相互转化，但同时又比较好记的一种私钥保存形式。私钥与助记词的转化一般是周边工具提供的功能，而不是区块链本身的功能。

好，基本概念就讲这么多，下面就开始实操。

## 2. 转账操作

转账操作虽然看起来简单，但实际上是一个非常完整的进行区块链交易的过程，通过转账操作，开发者基本上就可以掌握Account类、Transaction类、Web3类、PlatON类的最基本用法。Web3类和PlatON在教程《查询操作》中已经进行过基本介绍，这里就再介绍一下Account类及Transaction类：

- **Account**：账号相关操作，最核心的功能是签名操作。而且还在此类中通过静态方法的形式提供了与助记词相关的操作。下面的示例代码中会演示如何使用私钥、助记词生成账号，也会展示如何生成随机账号。
- **Transaction**：交易相关操作，核心功能是构建不同的交易。

还是从代码开始讲解：

```csharp
using PlatONet;
using System;

namespace examples
{
    public class Transfer
    {
        public static void Main(string[] args)
        {
            var mneno = Account.GenerateMnemonic(); // 9
            var act = Account.FromMnemonic(mneno); // 10
            var privateKey = "d08baac64f52ae1b9c2ea559036650229f07f5d61d869dbb55562a9827fbaeb8";
            var act2 = new Account(privateKey); // 12
            var act3 = new Account(); // 13
            var w3 = new Web3("http://35.247.155.162:6789", privateKey); // 14
            var toAddress = "lat1ljlf4myhux0zahfmlxf79wr7sl8u7pdey88dyp";
            var amount = new HexBigInteger((ulong)1e18);
            var gasPrice = new HexBigInteger((ulong)1e9);
            var gasLimit = new HexBigInteger(21000);            
            var nonceNum = w3.PlatON.GetTransactionCount();

            // 构建交易
            var tx = new Transaction(toAddress, amount, nonceNum, gasPrice, gasLimit); // 22
            //发送交易
            var result = w3.PlatON.SendTransaction(tx); // 24
            Console.WriteLine(result);
        }
    }
}
```

- 第9行：如何生成一个随机的助记词。
- 第10、12、13行：如果通过助记词、私钥、随机生成账号。
- 第14行：在构建Web3对象时，不仅传入的接入点相关信息，还传入的账号的私钥。这样就可以让Web3对象对账号进行管理，就可以在第24行发送交易时，自动对交易进行签名。如果在构建Web3对象时没有传入私钥，则也可以通过`w3.InitAccount`方法重新初始化账号，与在构建Web3对象时传入私钥是一样的效果。
- 第22行：构建交易。
- 第24行：发送交易。`w3`对账号进行了自动管理，所以这里不需要签名。否则需要先调用`tx.Sign(account)`方法，然后使用`w3.PlatON.SendRawTransaction(tx.SignedTransaction.ToHex())`发送交易。

转账交易是区块链中最基础、使用最广泛的操作，以上就是使用PlatONet进行转账的操作方法。