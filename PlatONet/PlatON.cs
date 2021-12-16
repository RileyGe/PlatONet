using Nethereum.JsonRpc.Client;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Nethereum.Hex.HexConvertors.Extensions;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.Hex.HexTypes;

namespace PlatONet
{
    /// <summary>
    /// PlatON网络相关的操作对象
    /// </summary>
    public class PlatON
    {
        /// <summary>
        /// 使用JsonRpc初始化一个<see cref="PlatON"/>实例
        /// </summary>
        /// <param name="client">JsonRpc客户端</param>
        public PlatON(IClient client)
        {
            this.client = client;
            InitPPOS();
        }
        private void InitPPOS()
        {
            PPOS = new PPOS(this);
        }
        private IClient client;
        /// <summary>
        /// JsonRpc客户端
        /// </summary>
        public IClient Client
        {
            get
            {
                return client;
            }
            private set
            {
                client = value;
            }
        }
        /// <summary>
        /// 账号信息<br/>
        /// 为了便于与PlatON网络进行交互，可以将账号信息保存到<see cref="Account"/>属性中。如果操作需要使用账号进行签名，则会自动调用该账号信息进行签名，便于操作。<br/>
        /// 用户也可以选择手动签名，而不将账号信息保存到<see cref="Account"/>属性中。
        /// </summary>
        public Account Account { get; set; }
        /// <summary>
        /// 链ID，PlatONet兼容PlatON/Alaya主网、开发网、私有网，不同的网络链ID可能不同。
        /// </summary>
        public HexBigInteger ChainId { get; set; }
        /// <summary>
        /// bech32地址前缀，与<see cref="ChainId"/>类似，不同的网络<see cref="Hrp"/>可能不同。
        /// </summary>
        public string Hrp { get; set; }
        /// <summary>
        /// 获得一个<see cref="PlatONContract"/>的实例。功能与<see cref="Web3"/>中的<see cref="Web3.GetContract(string, string)"/>方法类似。<br/>
        /// 更详细操作请参照文档《智能合约操作》章节。
        /// </summary>
        /// <param name="abi">智能合约的abi</param>
        /// <param name="contractAddress">智能合约地址</param>
        /// <returns><see cref="PlatONContract"/>实例</returns>
        public PlatONContract GetContract(string abi, string address)
        {
            return new PlatONContract(client, abi, address, this);
        }
        /// <summary>
        /// 获取<see cref="PPOS"/>（内置合约）实例
        /// </summary>
        public PPOS PPOS { get; private set; }
        /// <summary>
        /// 便捷方法<br/>
        /// 可以一次性的获得<see cref="Transaction"/>实例的Nonce、GasPrice、GasLimit三个参数
        /// </summary>
        /// <param name="trans"><see cref="Transaction"/>实例</param>
        /// <returns>包含Nonce、GasPrice及GasLimit三个参数的实例</returns>
        public Transaction FillTransactionWithDefaultValue(Transaction trans = null)
        {
            trans = trans ?? new Transaction();
            //trans.Nonce = trans.Nonce ?? GetTransactionCount(Account.GetAddress().ToString());
            trans.GasPrice = trans.GasPrice ?? GasPrice();
            trans.GasLimit = trans.GasLimit ?? EstimateGas(trans);
            if (Account != null) trans.Nonce = trans.Nonce ?? GetTransactionCount(Account.ToString());
            return trans;
        }
        #region rpc requests
        /// <summary>
        /// 返回当前协议版本
        /// </summary>
        /// <returns>当前协议版本</returns>
        public string ProtocolVersion()
        {      
            return ExcuteCommand<string>(ApiMplatonods.platon_protocolVersion.ToString());
        }
        /// <summary>
        /// 返回当前协议版本--异步操作
        /// </summary>
        /// <returns>当前协议版本</returns>
        public Task<string> ProtocolVersionAsync()
        {
            return ExcuteCommandAsync<string>(ApiMplatonods.platon_protocolVersion.ToString());
        }
        /// <summary>
        /// 返回同步状态的数据
        /// </summary>
        /// <returns>正在同步返回true，否则返回false</returns>
        public bool Syncing()
        {
            return ExcuteCommand<bool>(ApiMplatonods.platon_syncing.ToString());
        }
        /// <summary>
        /// 返回同步状态的数据--异步操作
        /// </summary>
        /// <returns>正在同步返回true，否则返回false</returns>
        public Task<bool> SyncingAsync()
        {
            return ExcuteCommandAsync<bool>(ApiMplatonods.platon_syncing.ToString());
            //return client.SendRequestAsync<bool>("platon_syncing");
        }
        /// <summary>
        /// 返回当前网络推荐的Gas价格
        /// </summary>
        /// <returns>当前网络推荐的Gas价格</returns>
        public HexBigInteger GasPrice()
        {
            return ExcuteCommand<HexBigInteger>(ApiMplatonods.platon_gasPrice.ToString());
        }
        /// <summary>
        /// 返回当前网络推荐的Gas价格--异步操作
        /// </summary>
        /// <returns>当前网络推荐的Gas价格</returns>
        public Task<HexBigInteger> GasPriceAsync()
        {
            return ExcuteCommandAsync<HexBigInteger>(ApiMplatonods.platon_gasPrice.ToString());
        }
        /// <summary>
        /// 返回客户端拥有的地址列表<br/>
        /// 注：客户端拥有的地址是指由节点管理的地址，如果使用公共的接入点，不建议使用节点管理地址，有泄露风险。所以PlatONet也不提供节点管理地址的相关功能，本SDK中的账号都是本地管理的。
        /// </summary>
        /// <returns>客户端拥有的地址列表</returns>
        public List<string> Accounts()
        {
            return ExcuteCommand<List<string>>(ApiMplatonods.platon_accounts.ToString());
        }
        /// <summary>
        /// 返回客户端拥有的地址列表--异步操作<br/>
        /// 注：客户端拥有的地址是指由节点管理的地址，如果使用公共的接入点，不建议使用节点管理地址，有泄露风险。所以PlatONet也不提供节点管理地址的相关功能，本SDK中的账号都是本地管理的。
        /// </summary>
        /// <returns>客户端拥有的地址列表</returns>
        public Task<List<string>> AccountsAsync()
        {
            return ExcuteCommandAsync<List<string>>(ApiMplatonods.platon_accounts.ToString());
        }
        /// <summary>
        /// 返回当前最高块高
        /// </summary>
        /// <returns>当前最高块高</returns>
        public HexBigInteger BlockNumber()
        {
            return ExcuteCommand<HexBigInteger>(ApiMplatonods.platon_blockNumber.ToString());
        }
        /// <summary>
        /// 返回当前最高块高--异步操作
        /// </summary>
        /// <returns>当前最高块高</returns>
        public Task<HexBigInteger> BlockNumberAsync()
        {
            return ExcuteCommandAsync<HexBigInteger>(ApiMplatonods.platon_blockNumber.ToString());
        }
        /// <summary>
        /// 查询地址余额
        /// </summary>
        /// <param name="address">地址</param>
        /// <param name="param"><see cref="BlockParameter"/>实例，表示块高</param>
        /// <returns>地址余额</returns>
        public HexBigInteger GetBalance(string address, BlockParameter param = null)
        {
            param = param ?? BlockParameter.DEFAULT;
            return ExcuteCommand<HexBigInteger>(ApiMplatonods.platon_getBalance.ToString(),
                paramList: new object[] {
                    address, param.BlockNumber
                });
        }
        /// <summary>
        /// 查询地址余额--异步操作
        /// </summary>
        /// <param name="address">地址</param>
        /// <param name="param"><see cref="BlockParameter"/>实例，表示查询截止块高</param>
        /// <returns>地址余额</returns>
        public Task<string> GetBalanceAsync(string address, BlockParameter param = null)
        {
            param = param ?? BlockParameter.DEFAULT;
            return ExcuteCommandAsync<string>(ApiMplatonods.platon_getBalance.ToString(), 
                paramList: new object[] { 
                    address, param.BlockNumber
                });                
        }
        /// <summary>
        /// 从给定地址的存储位置返回值
        /// </summary>
        /// <param name="address">存储地址</param>
        /// <param name="position">存储器中位置的整数</param>
        /// <param name="param"><see cref="BlockParameter"/>实例，表示查询截止块高</param>
        /// <returns>存储的数据</returns>
        public string GetStorageAt(string address, ulong position = 0, BlockParameter param = null)
        {
            param = param ?? BlockParameter.DEFAULT;
            return ExcuteCommand<string>(ApiMplatonods.platon_getStorageAt.ToString(),
                paramList: new object[] {
                    address, string.Format("0x{0:X}", position), param.BlockNumber
                });
        }
        /// <summary>
        /// 从给定地址的存储位置返回值--异步操作
        /// </summary>
        /// <param name="address">存储地址</param>
        /// <param name="position">存储器中位置的整数</param>
        /// <param name="param"><see cref="BlockParameter"/>实例，表示查询截止块高</param>
        /// <returns>存储的数据</returns>
        public Task<string> GetStorageAtAsync(string address, ulong position = 0, BlockParameter param = null)
        {
            param = param ?? BlockParameter.DEFAULT;
            return ExcuteCommandAsync<string>(ApiMplatonods.platon_getStorageAt.ToString(), 
                paramList: new object[] {
                    address, string.Format("0x{0:X}", position), param.BlockNumber
                });
        }
        /// <summary>
        /// 根据区块hash查询区块中交易个数
        /// </summary>
        /// <param name="blockHash">区块hash</param>
        /// <returns>区块中交易个数</returns>
        public HexBigInteger GetBlockTransactionCountByHash(string blockHash)
        {
            return ExcuteCommand<HexBigInteger>(ApiMplatonods.platon_getBlockTransactionCountByHash.ToString(),
                paramList: new object[] {
                    blockHash
                });
        }
        /// <summary>
        /// 根据区块hash查询区块中交易个数--异步操作
        /// </summary>
        /// <param name="blockHash">区块hash</param>
        /// <returns>区块中交易个数，16进制格式</returns>
        public Task<HexBigInteger> GetBlockTransactionCountByHashAsync(string blockHash)
        {
            return ExcuteCommandAsync<HexBigInteger>(ApiMplatonods.platon_getBlockTransactionCountByHash.ToString(),
                paramList: new object[] {
                    blockHash
                });
        }
        /// <summary>
        /// 根据地址查询该地址发送的交易个数
        /// </summary>
        /// <param name="address">查询地址</param>
        /// <param name="param"><see cref="BlockParameter"/>实例，表示查询截止块高</param>
        /// <returns>发送的交易个数</returns>
        public HexBigInteger GetTransactionCount(string address, BlockParameter param = null)
        {
            param = param ?? BlockParameter.DEFAULT;
            return ExcuteCommand<HexBigInteger>(ApiMplatonods.platon_getTransactionCount.ToString(), 
                paramList: new object[] { 
                    address, param.BlockNumber 
                });
        }
        /// <summary>
        /// 根据地址查询该地址发送的交易个数--异步操作
        /// </summary>
        /// <param name="address">查询地址</param>
        /// <param name="param"><see cref="BlockParameter"/>实例，表示查询截止块高</param>
        /// <returns>发送的交易个数</returns>
        public Task<HexBigInteger> GetTransactionCountAsync(string address, BlockParameter param = null)
        {
            param = param ?? BlockParameter.DEFAULT;
            return ExcuteCommandAsync<HexBigInteger>(ApiMplatonods.platon_getTransactionCount.ToString(), 
                paramList: new object[] {
                    address, param.BlockNumber
                });
        }
        /// <summary>
        /// 根据区块块高，返回块高中的交易总数
        /// </summary>
        /// <param name="param"><see cref="BlockParameter"/>实例，表示块高</param>
        /// <returns>块高中的交易总数</returns>
        public ulong GetBlockTransactionCountByNumber(BlockParameter param = null)
        {
            var result = GetBlockTransactionCountByNumberAsync(param);
            result.Wait();
            return Convert.ToUInt64(result.Result, 16);
        }
        /// <summary>
        /// 根据区块块高，返回块高中的交易总数--异步操作
        /// </summary>
        /// <param name="param"><see cref="BlockParameter"/>实例，表示块高</param>
        /// <returns>块高中的交易总数</returns>
        public Task<string> GetBlockTransactionCountByNumberAsync(BlockParameter param = null)
        {
            param = param ?? BlockParameter.DEFAULT;
            return ExcuteCommandAsync<string>(ApiMplatonods.platon_getBlockTransactionCountByNumber.ToString(),
                paramList: new object[] {
                    param.BlockNumber
                });
        }
        /// <summary>
        /// 返回给定地址的代码
        /// </summary>
        /// <param name="address">地址/合约</param>
        /// <param name="param"><see cref="BlockParameter"/>实例，表示查询截止块高</param>
        /// <returns>代码</returns>
        public string GetCode(string address, BlockParameter param = null)
        {
            param = param ?? BlockParameter.DEFAULT;
            return ExcuteCommand<string>(ApiMplatonods.platon_getCode.ToString(),
                paramList: new object[] {
                    address, param.BlockNumber
                });
        }
        /// <summary>
        /// 返回给定地址的代码--异步操作
        /// </summary>
        /// <param name="address">地址/合约</param>
        /// <param name="param"><see cref="BlockParameter"/>实例，表示查询截止块高</param>
        /// <returns>代码</returns>
        public Task<string> GetCodeAsync(string address, BlockParameter param = null)
        {
            param = param ?? BlockParameter.DEFAULT;
            return ExcuteCommandAsync<string>(ApiMplatonods.platon_getCode.ToString(),
                paramList: new object[] {
                    address, param.BlockNumber
                });
        }
        /// <summary>
        /// 发送交易<br/>
        /// 说明：PlatONet中的<see cref="SendTransaction(Transaction)"/>与Web3.js中的`SendTransaction`略有不同。
        /// 在Web3.js中，此操作是使用节点管理的账号进行签名并上链，所以使用本方法之前需要对账号进行解锁。
        /// 但PlatONet中，<see cref="SendTransaction(Transaction)"/>方法实际上是使用<see cref="Account"/>
        /// 属性所代表的账号进行本地签名后使用<see cref="SendRawTransaction(string)"/>方法将签名后的<see cref="Transaction"/>实例发送到网络。
        /// </summary>
        /// <param name="trans"><see cref="Transaction"/>实例</param>
        /// <returns>交易hash</returns>
        public string SendTransaction(Transaction trans)
        {
            var result = SendTransactionAsync(trans);
            result.Wait();
            return result.Result;
        }
        /// <summary>
        /// 发送交易--异步操作<br/>
        /// 说明：PlatONet中的<see cref="SendTransactionAsync(Transaction)"/>与Web3.js中的`SendTransaction`略有不同。
        /// 在Web3.js中，此操作是使用节点管理的账号进行签名并上链，所以使用本方法之前需要对账号进行解锁。
        /// 但PlatONet中，<see cref="SendTransactionAsync(Transaction)"/>方法实际上是使用<see cref="Account"/>
        /// 属性所代表的账号进行本地签名后使用<see cref="SendRawTransactionAsync(string)"/>方法将签名后的<see cref="Transaction"/>实例发送到网络。
        /// </summary>
        /// <param name="trans"><see cref="Transaction"/>实例</param>
        /// <returns>交易hash</returns>
        public Task<string> SendTransactionAsync(Transaction trans)
        {
            if (Account == null) throw new Exception("Please initialize Account before SendTransaction.");
            trans.Sign(Account);
            return SendRawTransactionAsync(trans.SignedTransaction.ToHex());
        }
        /// <summary>
        /// 发送交易--异步操作
        /// </summary>
        /// <param name="data">交易数据（签名后）</param>
        /// <returns>交易hash</returns>
        public Task<string> SendRawTransactionAsync(string data)
        {
            if (!data.StartsWith("0x")) data = "0x" + data;
            return ExcuteCommandAsync<string>(ApiMplatonods.platon_sendRawTransaction.ToString(), data);
        }
        /// <summary>
        /// 发送交易
        /// </summary>
        /// <param name="data">交易数据（签名后）</param>
        /// <returns>交易hash</returns>
        public string SendRawTransaction(string data)
        {
            if (!data.StartsWith("0x")) data = "0x" + data;
            return ExcuteCommand<string>(ApiMplatonods.platon_sendRawTransaction.ToString(), data);
        }
        /// <summary>
        /// 执行一个消息调用交易，消息调用交易直接在节点旳VM中执行而 不需要通过区块链的挖矿来执行
        /// </summary>
        /// <param name="trans"><see cref="Transaction"/>实例</param>
        /// <param name="param"><see cref="BlockParameter"/>实例，表示查询截止块高</param>
        /// <returns>查询结果</returns>
        public object Call(Transaction trans, BlockParameter param = null)
        {
            return Call<object>(trans, param);
        }
        /// <summary>
        /// 执行一个消息调用交易，消息调用交易直接在节点旳VM中执行而 不需要通过区块链的挖矿来执行
        /// </summary>
        /// <param name="trans"><see cref="Transaction"/>实例</param>
        /// <param name="param"><see cref="BlockParameter"/>实例，表示查询截止块高</param>
        /// <returns>查询结果</returns>
        public Task<object> CallAsync(Transaction trans, BlockParameter param = null)
        {
            return CallAsync<object>(trans, param);
        }
        /// <summary>
        /// 执行一个消息调用交易，消息调用交易直接在节点旳VM中执行而 不需要通过区块链的挖矿来执行--泛型方法
        /// </summary>
        /// <typeparam name="T">返回结果的类型</typeparam>
        /// <param name="trans"><see cref="Transaction"/>实例</param>
        /// <param name="param"><see cref="BlockParameter"/>实例，表示查询截止块高</param>
        /// <returns>查询结果</returns>
        public T Call<T>(Transaction trans, BlockParameter param = null)
        {
            param = param ?? BlockParameter.DEFAULT;
            return ExcuteCommand<T>(ApiMplatonods.platon_call.ToString(),
                paramList: new object[] {
                    trans.ToDict(), param.BlockNumber
                });
        }
        /// <summary>
        /// 执行一个消息调用交易，消息调用交易直接在节点旳VM中执行而 不需要通过区块链的挖矿来执行--泛型方法，异步操作
        /// </summary>
        /// <typeparam name="T">返回结果的类型</typeparam>
        /// <param name="trans"><see cref="Transaction"/>实例</param>
        /// <param name="param"><see cref="BlockParameter"/>实例，表示查询截止块高</param>
        /// <returns>查询结果</returns>
        public Task<T> CallAsync<T>(Transaction trans, BlockParameter param = null)
        {
            param = param ?? BlockParameter.DEFAULT;
            return ExcuteCommandAsync<T>(ApiMplatonods.platon_call.ToString(), 
                paramList: new object[] { 
                    trans.ToDict(), param.BlockNumber
                });
        }
        public ulong GetChainId()
        {
            return Convert.ToUInt64(ExcuteCommand<string>(ApiMplatonods.platon_chainId.ToString()), 16);
        }
        public Task<string> GetChainIdAsync()
        {
            return ExcuteCommandAsync<string>(ApiMplatonods.platon_chainId.ToString());
        }
        public string GetAddressHrp()
        {
            return ExcuteCommand<string>(ApiMplatonods.platon_getAddressHrp.ToString());
        }
        public Task<string> GetAddressHrpAsync()
        {
            return ExcuteCommandAsync<string>(ApiMplatonods.platon_getAddressHrp.ToString());
        }
        public HexBigInteger EstimateGas(Transaction trans)
        {
            var dict = trans.ToDict();
            if (dict.ContainsKey("gas")) dict.Remove("gas");
            if (dict.ContainsKey("nonce")) dict.Remove("nonce");
            if (dict.ContainsKey("gasPrice")) dict.Remove("gasPrice");
            return ExcuteCommand<HexBigInteger>(ApiMplatonods.platon_estimateGas.ToString(),
                paramList: new object[]{
                    dict
                });
        }
        public Task<string> EstimateGasAsync(Transaction trans)
        {
            var dict = trans.ToDict();
            if (dict.ContainsKey("gas")) dict.Remove("gas");
            return ExcuteCommandAsync<string>(ApiMplatonods.platon_estimateGas.ToString(),
                paramList: new object[]{
                    dict
                });
        }
        public Block GetBlockByHash(string hash, bool withFullTransactions = true)
        {
            return ExcuteCommand<Block>(ApiMplatonods.platon_getBlockByHash.ToString(),
                paramList: new object[] {
                    hash, withFullTransactions
                });
        }
        public BlockWithTransactions GetBlockWithTransactionsByHash(string hash)
        {
            return ExcuteCommand<BlockWithTransactions>(ApiMplatonods.platon_getBlockByHash.ToString(),
                paramList: new object[] {
                    hash, true
                });
        }
        public BlockWithTransactionHashes GetBlockWithTransactionHashesByHash(string hash)
        {
            return ExcuteCommand<BlockWithTransactionHashes>(ApiMplatonods.platon_getBlockByHash.ToString(),
                paramList: new object[] {
                    hash, false
                });
        }
        public Task<Block> GetBlockByHashAsync(string hash, bool withFullTransactions = true)
        {
            return ExcuteCommandAsync<Block>(ApiMplatonods.platon_getBlockByHash.ToString(),
                paramList: new object[] { 
                    hash, withFullTransactions
                });
        }
        public Task<BlockWithTransactions> GetBlockWithTransactionsByHashAsync(string hash)
        {
            return ExcuteCommandAsync<BlockWithTransactions>(ApiMplatonods.platon_getBlockByHash.ToString(),
                paramList: new object[] {
                    hash, true
                });
        }
        public Task<BlockWithTransactionHashes> GetBlockWithTransactionHashesByHashAsync(string hash)
        {
            return ExcuteCommandAsync<BlockWithTransactionHashes>(ApiMplatonods.platon_getBlockByHash.ToString(),
                paramList: new object[] {
                    hash, false
                });
        }
        public Block GetBlockByNumber(BlockParameter param = null, bool withFullTransaction = true)
        {
            param = param ?? BlockParameter.DEFAULT;
            return ExcuteCommand<Block>(ApiMplatonods.platon_getBlockByNumber.ToString(),
                paramList: new object[]
                {
                    param.BlockNumber, withFullTransaction
                });
        }
        public BlockWithTransactions GetBlockWithTransactionsByNumber(BlockParameter param = null)
        {
            param = param ?? BlockParameter.DEFAULT;
            return ExcuteCommand<BlockWithTransactions>(ApiMplatonods.platon_getBlockByNumber.ToString(),
                paramList: new object[] {
                    param.BlockNumber, true
                });
        }
        public BlockWithTransactionHashes GetBlockWithTransactionHashesByNumber(BlockParameter param = null)
        {
            param = param ?? BlockParameter.DEFAULT;
            return ExcuteCommand<BlockWithTransactionHashes>(ApiMplatonods.platon_getBlockByNumber.ToString(),
                paramList: new object[] {
                    param.BlockNumber, false
                });
        }
        public Task<Block> GetBlockByNumberAsync(BlockParameter param = null, bool withFullTransactions = true)
        {
            param = param ?? BlockParameter.DEFAULT;
            return ExcuteCommandAsync<Block>(ApiMplatonods.platon_getBlockByNumber.ToString(),
                paramList: new object[] {
                    param.BlockNumber, withFullTransactions
                });
        }
        public Task<BlockWithTransactions> GetBlockWithTransactionsByNumberAsync(BlockParameter param = null)
        {
            param = param ?? BlockParameter.DEFAULT;
            return ExcuteCommandAsync<BlockWithTransactions>(ApiMplatonods.platon_getBlockByNumber.ToString(),
                paramList: new object[] {
                    param.BlockNumber, true
                });
        }
        public Task<BlockWithTransactionHashes> GetBlockWithTransactionHashesByNumberAsync(BlockParameter param = null)
        {
            param = param ?? BlockParameter.DEFAULT;
            return ExcuteCommandAsync<BlockWithTransactionHashes>(ApiMplatonods.platon_getBlockByNumber.ToString(),
                paramList: new object[] {
                    param.BlockNumber, false
                });
        }
        public Nethereum.RPC.Eth.DTOs.Transaction GetTransactionByBlockHashAndIndex(string hash, uint index = 0)
        {
            return ExcuteCommand<Nethereum.RPC.Eth.DTOs.Transaction>(ApiMplatonods.platon_getTransactionByBlockHashAndIndex.ToString(),
                paramList: new object[]
                {
                    hash, string.Format("0x{0:X}", index)
                });
        }
        public Task<Nethereum.RPC.Eth.DTOs.Transaction> GetTransactionByBlockHashAndIndexAsync(string hash, uint index = 0)
        {
            return ExcuteCommandAsync<Nethereum.RPC.Eth.DTOs.Transaction>(ApiMplatonods.platon_getTransactionByBlockHashAndIndex.ToString(),
                paramList: new object[]
                {
                    hash, string.Format("0x{0:X}", index)
                });
        }
        public Nethereum.RPC.Eth.DTOs.Transaction GetTransactionByBlockNumberAndIndex(BlockParameter param = null, uint index = 0)
        {
            param = param ?? BlockParameter.DEFAULT;
            return ExcuteCommand<Nethereum.RPC.Eth.DTOs.Transaction>(ApiMplatonods.platon_getTransactionByBlockNumberAndIndex.ToString(),
                paramList: new object[]
                {
                    param.BlockNumber, string.Format("0x{0:X}", index)
                });
        }
        public Task<Nethereum.RPC.Eth.DTOs.Transaction> GetTransactionByBlockNumberAndIndexAsync(BlockParameter param = null, uint index = 0)
        {
            param = param ?? BlockParameter.DEFAULT;
            return ExcuteCommandAsync<Nethereum.RPC.Eth.DTOs.Transaction>(ApiMplatonods.platon_getTransactionByBlockNumberAndIndex.ToString(),
                paramList: new object[]
                {
                    param.BlockNumber, string.Format("0x{0:X}", index)
                });
        }
        public TransactionReceipt GetTransactionReceipt(string hash)
        {
            return ExcuteCommand<TransactionReceipt>(ApiMplatonods.platon_getTransactionReceipt.ToString(),
                paramList: new object[]
                {
                    hash
                });
        }
        public Task<TransactionReceipt> GetTransactionReceiptAsync(string hash)
        {
            return ExcuteCommandAsync<TransactionReceipt>(ApiMplatonods.platon_getTransactionReceipt.ToString(),
                paramList: new object[]
                {
                    hash
                });
        }
        public ulong NewFilter(BlockParameter fromBlock, BlockParameter toBlock, string address, object[] topics)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>
            {
                { "fromBlock", fromBlock.BlockNumber },
                { "toBlock", toBlock.BlockNumber },
                { "address", address },
                { "topics", topics }
            };
            //if (Client == null) throw new NullReferenceException("RpcRequestHandler Client is null");
            var id = ExcuteCommand<string>(ApiMplatonods.platon_newFilter.ToString(), dict);
            return Convert.ToUInt64(id, 16);
        }
        public Task<string> NewFilterAsync(BlockParameter fromBlock, BlockParameter toBlock, string address, object[] topics)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>
            {
                { "fromBlock", fromBlock.BlockNumber },
                { "toBlock", toBlock.BlockNumber },
                { "address", address },
                { "topics", topics }
            };
            //if (Client == null) throw new NullReferenceException("RpcRequestHandler Client is null");
            return ExcuteCommandAsync<string>(ApiMplatonods.platon_newFilter.ToString(), dict);
        }
        public ulong NewBlockFilter()
        {
            return Convert.ToUInt64(ExcuteCommand<string>(ApiMplatonods.platon_newBlockFilter.ToString()), 16);
        }
        public Task<string> NewBlockFilterAsync()
        {
            return ExcuteCommandAsync<string>(ApiMplatonods.platon_newBlockFilter.ToString());
        }
        public ulong NewPendingTransactionFilter()
        {
            return Convert.ToUInt64(ExcuteCommand<string>(ApiMplatonods.platon_newPendingTransactionFilter.ToString()), 16);
        }
        public Task<string> NewPendingTransactionFilterAsync()
        {
            return ExcuteCommandAsync<string>(ApiMplatonods.platon_newPendingTransactionFilter.ToString());
        }
        public bool UninstallFilter(ulong filterId)
        {
            return ExcuteCommand<bool>(ApiMplatonods.platon_newPendingTransactionFilter.ToString(),
                string.Format("0x{0:X}", filterId));
        }
        public Task<bool> UninstallFilterAsync(ulong filterId)
        {
            return ExcuteCommandAsync<bool>(ApiMplatonods.platon_newPendingTransactionFilter.ToString(),
                string.Format("0x{0:X}", filterId));
        }
        public FilterLog[] GetFilterChanges(ulong filterId)
        {
            return ExcuteCommand<FilterLog[]>(ApiMplatonods.platon_getFilterChanges.ToString(),
                string.Format("0x{0:X}", filterId));
        }
        public Task<FilterLog[]> GetFilterChangesAsync(ulong filterId)
        {
            return ExcuteCommandAsync<FilterLog[]>(ApiMplatonods.platon_getFilterChanges.ToString(),
                string.Format("0x{0:X}", filterId));
        }
        public string[] GetFilterLogs(ulong filterId)
        {
            return ExcuteCommand<string[]>(ApiMplatonods.platon_getFilterLogs.ToString(),
                string.Format("0x{0:X}", filterId));
        }
        public Task<string[]> GetFilterLogsAsync(ulong filterId)
        {
            return ExcuteCommandAsync<string[]>(ApiMplatonods.platon_getFilterLogs.ToString(),
                string.Format("0x{0:X}", filterId));
        }
        public FilterLog[] GetLogs(BlockParameter fromBlock, BlockParameter toBlock, string address, object[] topics)
        {

            Dictionary<string, object> dict = new Dictionary<string, object>
            {
                { "fromBlock", fromBlock.BlockNumber },
                { "toBlock", toBlock.BlockNumber },
                { "address", address },
                { "topics", topics }
            };
            //if (Client == null) throw new NullReferenceException("RpcRequestHandler Client is null");
            return ExcuteCommand<FilterLog[]>(ApiMplatonods.platon_getFilterLogs.ToString(), dict);
        }
        public Task<FilterLog[]> GetLogsAsync(BlockParameter fromBlock, BlockParameter toBlock, string address, object[] topics)
        {

            Dictionary<string, object> dict = new Dictionary<string, object>
            {
                { "fromBlock", fromBlock.BlockNumber },
                { "toBlock", toBlock.BlockNumber },
                { "address", address },
                { "topics", topics }
            };
            //if (Client == null) throw new NullReferenceException("RpcRequestHandler Client is null");
            return ExcuteCommandAsync<FilterLog[]>(ApiMplatonods.platon_getFilterLogs.ToString(), dict);
        }
        public Nethereum.RPC.Eth.DTOs.Transaction[] PendingTransactions()
        {
            return ExcuteCommand<Nethereum.RPC.Eth.DTOs.Transaction[]>(ApiMplatonods.platon_pendingTransactions.ToString());
        }
        public Task<Nethereum.RPC.Eth.DTOs.Transaction[]> PendingTransactionsAsync()
        {
            return ExcuteCommandAsync<Nethereum.RPC.Eth.DTOs.Transaction[]>(ApiMplatonods.platon_pendingTransactions.ToString());
        }
        //public BigInteger ExcuteCommandParseBigInteger(string method, params object[] paramList)
        //{
        //    var result = ExcuteCommandAsync<string>(method, paramList);
        //    result.Wait();
        //    return result.Result.HexToBigInteger(false);
        //}        
        public Task<T> ExcuteCommandAsync<T>(string method, params object[] paramList)
        {
            return client.SendRequestAsync<T>(method, null, paramList: paramList);
        }
        public T ExcuteCommand<T>(string method, params object[] paramList)
        {
            var result = ExcuteCommandAsync<T>(method, paramList);
            result.Wait();
            return result.Result;
        }
        #endregion
    }
}
