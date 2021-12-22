using Nethereum.Contracts;
using Nethereum.Hex.HexConvertors.Extensions;
using Nethereum.JsonRpc.Client;
using System.Threading.Tasks;

namespace PlatONet
{
    /// <summary>
    /// PlatON网络的智能合约
    /// </summary>
    public class PlatONContract
    {
        internal Contract EthContract
        {
            get
            {
                return _contract;
            }
        }
        /// <summary>
        /// 与该智能合约相关联的PlatON网络信息
        /// </summary>
        public PlatON PlatON { get; private set; }
        /// <summary>
        /// 智能合约地址
        /// </summary>
        public Address Address { get; private set; }
        private Contract _contract;
        /// <summary>
        /// 初始化<see cref="PlatONContract"/>实例
        /// </summary>
        /// <param name="client">JsonRpc客户端</param>
        /// <param name="abi">智能合约的abi</param>
        /// <param name="address">智能合约地址</param>
        /// <param name="platon">与智能合约相关联的PlatON网络信息</param>
        public PlatONContract(IClient client, string abi, string address, PlatON platon) :
            this(client, abi, new Address(address), platon)
        { }
        /// <summary>
        /// 初始化<see cref="PlatONContract"/>实例
        /// </summary>
        /// <param name="client">JsonRpc客户端</param>
        /// <param name="abi">智能合约的abi</param>
        /// <param name="address">智能合约地址</param>
        /// <param name="platon">与智能合约相关联的PlatON网络信息</param>
        public PlatONContract(IClient client, string abi, Address address, PlatON platon)            
        {
            PlatON = platon;
            Address = address;
            _contract = new Contract(new Nethereum.RPC.EthApiService(client), abi, address.ToEthereumAddress());
        }
        /// <summary>
        /// 实例化与当前智能合约的<see cref="PlatONFunction"/>对象
        /// </summary>
        /// <param name="functionName">函数名</param>
        /// <returns><see cref="PlatONFunction"/>实例</returns>
        public PlatONFunction GetFunction(string functionName)
        {
            return new PlatONFunction(this, _contract.ContractBuilder.GetFunctionBuilder(functionName));
        }
    }
    /// <summary>
    /// 智能合约的函数
    /// </summary>
    public class PlatONFunction
    {
        //public Account Account { get; set; }
        private Function _function;
        private PlatONContract _contract;
        /// <summary>
        /// 实例化<see cref="PlatONFunction"/>实例<br/>
        /// 注：该方法一般不直接使用，一般使用<see cref="PlatONContract.GetFunction(string)"/>函数来实例化<see cref="PlatONFunction"/>实例。
        /// </summary>
        /// <param name="contract">智能合约</param>
        /// <param name="functionBuilder">`FunctionBuilder`对象</param>
        public PlatONFunction(PlatONContract contract, FunctionBuilder functionBuilder)            
        {
            _contract = contract;
            _function = new Function(contract.EthContract, functionBuilder);
        }
        /// <summary>
        /// 估算当前方法执行所需要的Gas
        /// </summary>
        /// <param name="functionInput">当前方法执行所需要的参数</param>
        /// <returns>当前方法执行所需要的Gas</returns>
        public HexBigInteger EstimateGas(params object[] functionInput)
        {
            var result = EstimateGasAsync(functionInput: functionInput);
            result.Wait();
            return result.Result;
        }
        /// <summary>
        /// 估算当前方法执行所需要的Gas--异步方法
        /// </summary>
        /// <param name="functionInput">当前方法执行所需要的参数</param>
        /// <returns>当前方法执行所需要的Gas</returns>
        public async Task<HexBigInteger> EstimateGasAsync(params object[] functionInput)
        {
            var result = await _function.EstimateGasAsync(functionInput);
            return result.ToHexBigInteger();
            //return (await _function.EstimateGasAsync(functionInput)) as HexBigInteger;
        }
        /// <summary>
        /// 估算当前方法执行所需要的Gas
        /// </summary>
        /// <param name="from">当前方法的发送人地址</param>
        /// <param name="gas">预设的Gas数量</param>
        /// <param name="value">随方法一起发送的lat/atp数量</param>
        /// <param name="functionInput">当前方法执行所需要的参数</param>
        /// <returns>当前方法执行所需要的Gas</returns>
        public HexBigInteger EstimateGas(string from, HexBigInteger gas,
            HexBigInteger value, params object[] functionInput)
        {
            var result = EstimateGasAsync(from, gas, value, functionInput: functionInput);
            result.Wait();
            return result.Result;
        }
        /// <summary>
        /// 估算当前方法执行所需要的Gas--异步方法
        /// </summary>
        /// <param name="from">当前方法的发送人地址</param>
        /// <param name="gas">预设的Gas数量</param>
        /// <param name="value">随方法一起发送的lat/atp数量</param>
        /// <param name="functionInput">当前方法执行所需要的参数</param>
        /// <returns>当前方法执行所需要的Gas</returns>
        public async Task<HexBigInteger> EstimateGasAsync(string from, HexBigInteger gas,
            HexBigInteger value, params object[] functionInput)
        {
            string etherAddress;
            if (from == null || from.Length < 1)
            {
                etherAddress = _contract?.PlatON?.Account?.GetAddress()?.ToEthereumAddress();
            }                
            else 
                etherAddress = (new Address(from)).ToEthereumAddress();
            return (await _function.EstimateGasAsync(etherAddress, gas, value, functionInput)) as HexBigInteger;
        }
        /// <summary>
        /// 发送交易<br/>
        /// 注：1. PlatON中的<see cref="SendTransaction(object[])"/>方法与Web3.js不同，
        /// 详情请参照<see cref="PlatON.SendTransaction(Transaction)"/>中的说明。<br/>
        /// 2. 未指定的参数PlatON会自动查询网络默认值。
        /// </summary>
        /// <param name="functionInput">当前方法执行所需要的参数</param>
        /// <returns>交易hash</returns>
        public string SendTransaction(params object[] functionInput)
        {
            var result = SendTransactionAsync(functionInput: functionInput);
            result.Wait();
            return result.Result;
        }
        /// <summary>
        /// 发送交易<br/>
        /// 注：1. PlatON中的<see cref="SendTransaction(object[])"/>方法与Web3.js不同，
        /// 详情请参照<see cref="PlatON.SendTransaction(Transaction)"/>中的说明。<br/>
        /// 2. 未指定的参数PlatON会自动查询网络默认值。
        /// </summary>
        /// <param name="from">当前方法的发送人地址</param>
        /// <param name="gas">预设的Gas数量</param>
        /// <param name="gasPrice">预设的GasPrice</param>
        /// <param name="value">随方法一起发送的lat/atp数量</param>
        /// <param name="nonce">nonce值</param>
        /// <param name="functionInput">当前方法执行所需要的参数</param>
        /// <returns>交易hash</returns>
        public string SendTransaction(string from, HexBigInteger gas, HexBigInteger gasPrice,
            HexBigInteger value, HexBigInteger nonce, params object[] functionInput)
        {
            var result = SendTransactionAsync(from, gas, gasPrice, value, nonce, functionInput: functionInput);
            result.Wait();
            return result.Result;
        }
        /// <summary>
        /// 发送交易--异步方法<br/>
        /// 注：PlatON中的<see cref="SendTransactionAsync(object[])"/>方法与Web3.js不同，
        /// 详情请参照<see cref="PlatON.SendTransactionAsync(Transaction)"/>中的说明。<br/>
        /// 2. 未指定的参数PlatON会自动查询网络默认值。
        /// </summary>
        /// <param name="functionInput">当前方法执行所需要的参数</param>
        /// <returns>交易hash</returns>
        public Task<string> SendTransactionAsync(params object[] functionInput)
        {
            return SendTransactionAsync(null, null, null, null, null, functionInput: functionInput);
        }
        /// <summary>
        /// 发送交易--异步方法<br/>
        /// 注：PlatON中的<see cref="SendTransactionAsync(object[])"/>方法与Web3.js不同，
        /// 详情请参照<see cref="PlatON.SendTransactionAsync(Transaction)"/>中的说明。<br/>
        /// 2. 未指定的参数PlatON会自动查询网络默认值。
        /// </summary>
        /// <param name="from">当前方法的发送人地址</param>
        /// <param name="gas">预设的Gas数量</param>
        /// <param name="gasPrice">预设的GasPrice</param>
        /// <param name="value">随方法一起发送的lat/atp数量</param>
        /// <param name="nonce">nonce值</param>
        /// <param name="functionInput">当前方法执行所需要的参数</param>
        /// <returns>交易hash</returns>
        public async Task<string> SendTransactionAsync(string from, HexBigInteger gas, HexBigInteger gasPrice,
            HexBigInteger value, HexBigInteger nonce, params object[] functionInput)
        {            
            var sender = from == null || from.Length < 1 ?
                _contract?.PlatON?.Account?.GetAddress()
                : new Address(from);
            if (value == null) value = new HexBigInteger(0);
            //Task<HexBigInteger> gasResult = null, gasPriceResult = null, nonceResult = null;

            if (gas == null || gas.Value == 0) gas = await EstimateGasAsync(functionInput);
            if (gasPrice == null || gasPrice.Value == 0) gasPrice = await _contract.PlatON.GasPriceAsync();
            if (nonce == null) nonce = await _contract.PlatON.GetTransactionCountAsync(sender.ToString());

            var input = _function.CreateTransactionInput(sender?.ToEthereumAddress(), 
                gas, gasPrice, value, functionInput: functionInput);
            var data = input.Data;

            Transaction tx = new Transaction(_contract.Address.ToString(), value, nonce, 
                gasPrice, gas, data, _contract.PlatON.ChainId);
            tx.Sign(_contract?.PlatON?.Account);
            return await _contract.PlatON.SendRawTransactionAsync(tx.SignedTransaction.ToHex());
        }
        /// <summary>
        /// 消息调用<br/>
        /// 消息调用交易直接在节点旳VM中执行而 不需要通过区块链的挖矿来执行。
        /// </summary>
        /// <typeparam name="TReturn">返回数据的数据类型</typeparam>
        /// <param name="functionInput">当前方法执行所需要的参数</param>
        /// <returns>查询返回的数据</returns>
        public TReturn Call<TReturn>(params object[] functionInput)
        {
            var result = CallAsync<TReturn>(functionInput: functionInput);
            result.Wait();
            return result.Result;
        }
        /// <summary>
        /// 消息调用<br/>
        /// 消息调用交易直接在节点旳VM中执行而 不需要通过区块链的挖矿来执行。
        /// </summary>
        /// <typeparam name="TReturn">返回数据的数据类型</typeparam>
        /// <param name="from">当前方法的发送人地址</param>
        /// <param name="gas">预设的Gas数量</param>
        /// <param name="value">随方法一起发送的lat/atp数量。</param>
        /// <param name="functionInput">当前方法执行所需要的参数</param>
        /// <returns>查询返回的数据</returns>
        public TReturn Call<TReturn>(string from, HexBigInteger gas,
            HexBigInteger value, params object[] functionInput)
        {
            var result = CallAsync<TReturn>(from, gas, value, functionInput: functionInput);
            result.Wait();
            return result.Result;
        }
        /// <summary>
        /// 消息调用<br/>
        /// 消息调用交易直接在节点旳VM中执行而 不需要通过区块链的挖矿来执行。
        /// </summary>
        /// <typeparam name="TReturn">返回数据的数据类型</typeparam>
        /// <param name="from">当前方法的发送人地址</param>
        /// <param name="gas">预设的Gas数量</param>
        /// <param name="value">随方法一起发送的lat/atp数量。</param>
        /// <param name="block"><see cref="BlockParameter"/>实例，表示查询截止块高</param>
        /// <param name="functionInput">当前方法执行所需要的参数</param>
        /// <returns>查询返回的数据</returns>
        public TReturn Call<TReturn>(string from, HexBigInteger gas,
            HexBigInteger value, BlockParameter block, params object[] functionInput)
        {
            var result = CallAsync<TReturn>(from, gas, value, block, functionInput: functionInput);
            result.Wait();
            return result.Result;
        }
        /// <summary>
        /// 消息调用<br/>
        /// 消息调用交易直接在节点旳VM中执行而 不需要通过区块链的挖矿来执行。
        /// </summary>
        /// <typeparam name="TReturn">返回数据的数据类型</typeparam>
        /// <param name="block"><see cref="BlockParameter"/>实例，表示查询截止块高</param>
        /// <param name="functionInput">当前方法执行所需要的参数</param>
        /// <returns>查询返回的数据</returns>
        public TReturn Call<TReturn>(BlockParameter block, params object[] functionInput)
        {
            var result = CallAsync<TReturn>(block, functionInput: functionInput);
            result.Wait();
            return result.Result;
        }
        /// <summary>
        /// 消息调用--异步操作<br/>
        /// 消息调用交易直接在节点旳VM中执行而 不需要通过区块链的挖矿来执行。
        /// </summary>
        /// <typeparam name="TReturn">返回数据的数据类型</typeparam>
        /// <param name="functionInput">当前方法执行所需要的参数</param>
        /// <returns>查询返回的数据</returns>
        public Task<TReturn> CallAsync<TReturn>(params object[] functionInput)
        {
            return _function.CallAsync<TReturn>(functionInput);
        }
        /// <summary>
        /// 消息调用--异步操作<br/>
        /// 消息调用交易直接在节点旳VM中执行而 不需要通过区块链的挖矿来执行。
        /// </summary>
        /// <typeparam name="TReturn">返回数据的数据类型</typeparam>
        /// <param name="from">当前方法的发送人地址</param>
        /// <param name="gas">预设的Gas数量</param>
        /// <param name="value">随方法一起发送的lat/atp数量。</param>
        /// <param name="functionInput">当前方法执行所需要的参数</param>
        /// <returns>查询返回的数据</returns>
        public Task<TReturn> CallAsync<TReturn>(string from, HexBigInteger gas,
            HexBigInteger value, params object[] functionInput)
        {
            var sender = new Address(from);
            return _function.CallAsync<TReturn>(sender.ToEthereumAddress(), gas, value, functionInput);
        }
        /// <summary>
        /// 消息调用--异步操作<br/>
        /// 消息调用交易直接在节点旳VM中执行而 不需要通过区块链的挖矿来执行。
        /// </summary>
        /// <typeparam name="TReturn">返回数据的数据类型</typeparam>
        /// <param name="from">当前方法的发送人地址</param>
        /// <param name="gas">预设的Gas数量</param>
        /// <param name="value">随方法一起发送的lat/atp数量。</param>
        /// <param name="block"><see cref="BlockParameter"/>实例，表示查询截止块高</param>
        /// <param name="functionInput">当前方法执行所需要的参数</param>
        /// <returns>查询返回的数据</returns>
        public Task<TReturn> CallAsync<TReturn>(string from, HexBigInteger gas,
            HexBigInteger value, BlockParameter block, params object[] functionInput)
        {
            var sender = new Address(from);
            return _function.CallAsync<TReturn>(sender.ToEthereumAddress(), gas, value, block, functionInput);
        }
        /// <summary>
        /// 消息调用--异步操作<br/>
        /// 消息调用交易直接在节点旳VM中执行而 不需要通过区块链的挖矿来执行。
        /// </summary>
        /// <typeparam name="TReturn">返回数据的数据类型</typeparam>
        /// <param name="block"><see cref="BlockParameter"/>实例，表示查询截止块高</param>
        /// <param name="functionInput">当前方法执行所需要的参数</param>
        /// <returns>查询返回的数据</returns>
        public Task<TReturn> CallAsync<TReturn>(BlockParameter block, params object[] functionInput)
        {
            return _function.CallAsync<TReturn>(block, functionInput);
        }
    }
}
