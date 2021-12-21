using System;
using Nethereum.JsonRpc.Client;
using System.Threading.Tasks;
using Nethereum.Contracts;
using Nethereum.RPC;
using Nethereum.Hex.HexTypes;
using Newtonsoft.Json;
using System.Collections.Generic;
using PlatONet.DTOs;

namespace PlatONet
{
    /// <summary>
    /// Web3对象，PlatONet的最主要入口
    /// </summary>
    public class Web3
    {
        private IClient client;
        /// <summary>
        /// 用于连接网络的JsonRpc客户端
        /// </summary>
        public IClient Client
        {
            get
            {
                return client;
            }
            set
            {
                client = value;
            }
        }
        public PlatON PlatON { get; set; }
        /// <summary>
        /// 使用接入点初始化一个web3对象
        /// </summary>
        /// <param name="uri">接入点uri</param>
        public Web3(string uri = "http://localhost:6789") : this(uri, null, true)
        { }
        /// <summary>
        /// 使用接入点初始化一个web3对象
        /// </summary>
        /// <param name="uri">接入点uri</param>
        /// <param name="privateKey">账号的私钥<br/> 
        /// 注：如果在初始化时直接传入私钥，则web3会在本地自动管理账号，方便开发。
        /// 如果初始化时不传入私钥，也可以使用<see cref="InitAccount(string)"/>或者<see cref="InitAccount(Account)"/>来重新传入私钥。</param>
        /// <param name="initChainIdAndHrp">是否自动填充ChainId和Hrp<br/> 
        /// 注：如果初始化时不进行ChainId和Hrp的填充，后续也可以使用<see cref="InitChainIdAndHrp"/>方法来进行填充。</param>
        public Web3(string uri, string privateKey, bool initChainIdAndHrp = true)
        {
            client = new RpcClient(new Uri(uri));
            InitPlatON(client);
            InitAccount(privateKey);
            if (initChainIdAndHrp) InitChainIdAndHrp();
        }
        /// <summary>
        /// 自动填充ChainId和Hrp
        /// </summary>
        public void InitChainIdAndHrp()
        {
            PlatON.ChainId = PlatON.GetChainId();
            PlatON.Hrp = PlatON.GetAddressHrp();
        }
        /// <summary>
        /// 让web3对象自动管理账号
        /// </summary>
        /// <param name="privateKey">账号私钥</param>
        public void InitAccount(string privateKey)
        {
            Account account = null;
            if(privateKey != null && privateKey.Length > 0)
            {
                account = new Account(privateKey);
            }            
            InitAccount(account);
        }
        /// <summary>
        /// 让web3对象自动管理账号
        /// </summary>
        /// <param name="account">账号</param>
        public void InitAccount(Account account)
        {
            if (PlatON == null) InitPlatON(client);
            PlatON.Account = account;
        }
        private void InitPlatON(IClient client)
        {
            PlatON = new PlatON(client);
        }
        /// <summary>
        /// 返回当前客户端版本
        /// </summary>
        /// <returns>当前客户端版本</returns>
        public string ClientVersion()
        {
            var result = ClientVersionAsync();
            result.Wait();
            return result.Result;
        }
        /// <summary>
        /// 返回当前客户端版本--异步操作
        /// </summary>
        /// <returns>当前客户端版本</returns>
        public async Task<string> ClientVersionAsync()
        {
            return await client.SendRequestAsync<string>("web3_clientVersion");
        }
        /// <summary>
        /// 返回给定数据的keccak-256（不是标准sha3-256）
        /// </summary>
        /// <param name="data">加密前的数据</param>
        /// <returns>加密后的数据</returns>
        public string Sha3(string data)
        {
            var result = Sha3Async(data);
            result.Wait();
            return result.Result;
        }
        /// <summary>
        /// 返回给定数据的keccak-256（不是标准sha3-256）--异步操作
        /// </summary>
        /// <param name="data">加密前的数据</param>
        /// <returns>加密后的数据</returns>
        public async Task<string> Sha3Async(string data)
        {
            return await client.SendRequestAsync<string>("web3_sha3", null,
                new object[] { data });
        }
        /// <summary>
        /// 返回当前网络ID
        /// </summary>
        /// <returns>当前网络ID</returns>
        public string NetVersion()
        {
            var result = NetVersionAsync();
            result.Wait();
            return result.Result;
        }
        /// <summary>
        /// 返回当前网络ID--异步操作
        /// </summary>
        /// <returns>当前网络ID</returns>
        public async Task<string> NetVersionAsync()
        {
            return await client.SendRequestAsync<string>("net_version");            
        }
        /// <summary>
        /// 客户端是否正在积极侦听网络连接
        /// </summary>
        /// <returns>客户端正在积极侦听网络连接，则返回true，否则返回false</returns>
        public bool NetListening()
        {
            var result = NetListeningAsync();
            result.Wait();
            return result.Result;
        }
        /// <summary>
        /// 客户端是否正在积极侦听网络连接--异步操作
        /// </summary>
        /// <returns>客户端正在积极侦听网络连接，则返回true，否则返回false</returns>
        public async Task<bool> NetListeningAsync()
        {
            return await client.SendRequestAsync<bool>("net_listening");
        }
        /// <summary>
        /// 返回当前连接到客户端的对等体的数量
        /// </summary>
        /// <returns>当前连接到客户端的对等体的数量</returns>
        public ulong NetPeerCount()
        {
            var result = NetPeerCountAsync();
            result.Wait();
            return result.Result;
        }
        /// <summary>
        /// 返回当前连接到客户端的对等体的数量--异步操作
        /// </summary>
        /// <returns>当前连接到客户端的对等体的数量，以16进制字符串的形式返回</returns>
        public async Task<ulong> NetPeerCountAsync()
        {
            var result = await client.SendRequestAsync<string>("net_peerCount");
            return Convert.ToUInt64(result, 16);
        }
        /// <summary>
        /// 获得一个<see cref="PlatONContract"/>的实例。<br/>
        /// 更详细操作请参照文档《智能合约操作》章节。
        /// </summary>
        /// <param name="abi">智能合约的abi</param>
        /// <param name="contractAddress">智能合约地址</param>
        /// <returns><see cref="PlatONContract"/>实例</returns>
        public Contract GetContract(string abi, string contractAddress)
        {
            var contract = new Contract(new EthApiService(client/*, transactionManager*/), abi, contractAddress);
            return contract;
        }
        /// <summary>
        /// 获取代码版本
        /// </summary>
        /// <returns>代码版本</returns>
        public ProgramVersion GetProgramVersion()
        {
            return PlatON.ExcuteCommand<ProgramVersion>("admin_getProgramVersion");
        }
        /// <summary>
        /// 获取代码版本--异步操作
        /// </summary>
        /// <returns>代码版本</returns>
        public async Task<ProgramVersion> GetProgramVersionAsync()
        {
            return await PlatON.ExcuteCommandAsync<ProgramVersion>("admin_getProgramVersion");
        }
        /// <summary>
        /// 获取bls的证明
        /// </summary>
        /// <returns>bls的证明</returns>
        public string GetSchnorrNIZKProve()
        {
            return PlatON.ExcuteCommand<string>("admin_getSchnorrNIZKProve");
        }
        /// <summary>
        /// 获取bls的证明--异步操作
        /// </summary>
        /// <returns>bls的证明</returns>
        public async Task<string> GetSchnorrNIZKProveAsync()
        {
            return await PlatON.ExcuteCommandAsync<string>("admin_getSchnorrNIZKProve");
        }
        /// <summary>
        /// 获取PlatON参数配置
        /// </summary>
        /// <returns>PlatON参数配置</returns>
        public EconomicConfig GetEconomicConfig()
        {
            var result = PlatON.ExcuteCommand<string>("debug_economicConfig");
            return JsonConvert.DeserializeObject<EconomicConfig>(result);
        }
        /// <summary>
        /// 获取PlatON参数配置--异步操作
        /// </summary>
        /// <returns>PlatON参数配置</returns>
        public async Task<EconomicConfig> GetEconomicConfigAsync()
        {
            var result = await PlatON.ExcuteCommandAsync<string>("debug_economicConfig");
            return JsonConvert.DeserializeObject<EconomicConfig>(result);
        }
        /// <summary>
        /// 获取链ID，与<see cref="PlatON.GetChainId"/>方法作用相同
        /// </summary>
        /// <returns>链ID</returns>
        public HexBigInteger GetChainId()
        {
            var result = PlatON.ExcuteCommand<string>(ApiMplatonods.platon_chainId.ToString());
            return new HexBigInteger(result);
        }
        /// <summary>
        /// 获取链ID，与<see cref="PlatON.GetChainId"/>方法作用相同--异步操作
        /// </summary>
        /// <returns>链ID</returns>
        public async Task<HexBigInteger> GetChainIdAsync()
        {
            var result = await PlatON.ExcuteCommandAsync<string>(ApiMplatonods.platon_chainId.ToString());
            return new HexBigInteger(result);
        }
        /// <summary>
        /// 获取零出块的节点，因为零出块而被观察的节点列表
        /// </summary>
        /// <returns>零出块的节点列表</returns>
        public List<WaitSlashingNode> GetWaitSlashingNodeList()
        {
            var result = PlatON.ExcuteCommand<string>("debug_getWaitSlashingNodeList");
            return JsonConvert.DeserializeObject<List<WaitSlashingNode>>(result);
        }
        /// <summary>
        /// 获取零出块的节点，因为零出块而被观察的节点列表--异步操作
        /// </summary>
        /// <returns>零出块的节点列表</returns>
        public async Task<List<WaitSlashingNode>> GetWaitSlashingNodeListAsync()
        {
            var result = await PlatON.ExcuteCommandAsync<string>("debug_getWaitSlashingNodeList");
            return JsonConvert.DeserializeObject<List<WaitSlashingNode>>(result);
        }
        ///// <summary>
        ///// String :  : 数据库名称
        ///// String :  : 键名
        ///// String :  : 要存入的字符串
        ///// </summary>
        //public string DbPutString(string databaseName, string keyName, string stringToStore)
        //{
        //    return PlatON.ExcuteCommand<string>("db_putString", 
        //        paramList: new object[] { 
        //            databaseName,
        //            keyName,
        //            stringToStore
        //        });
        //}
    }
    /// <summary>
    /// https://eth.wiki/json-rpc/API#the-default-block-parameter
    /// </summary>
    public class BlockParameter
    {
        /// <summary>
        /// 最低块高
        /// </summary>
        public static BlockParameter EARLIEST = Init("earliest");
        /// <summary>
        /// 最新块高(默认)
        /// </summary>
        public static BlockParameter LATEST = Init("latest");
        /// <summary>
        /// 未打包交易
        /// </summary>
        public static BlockParameter PENDING = Init("pending");
        /// <summary>
        /// 默认
        /// </summary>
        public static BlockParameter DEFAULT = Init("latest");
        /// <summary>
        /// 指定块高
        /// </summary>
        /// <param name="blockNumber">块高度</param>
        /// <returns><see cref="BlockParameter"/>实例</returns>
        public static BlockParameter Init(ulong blockNumber)
        {
            return new BlockParameter(blockNumber);
        }
        /// <summary>
        /// 指定参数
        /// </summary>
        /// <param name="param">参数</param>
        /// <returns><see cref="BlockParameter"/>实例</returns>
        public static BlockParameter Init(string param)
        {
            return new BlockParameter(param);
        }
        /// <summary>
        /// 块高
        /// </summary>
        public string BlockNumber { get; set; }
        /// <summary>
        /// 使用块高初始化<see cref="BlockParameter"/>实例
        /// </summary>
        /// <param name="blockNumber">块高度</param>
        public BlockParameter(ulong blockNumber)
        {
            BlockNumber = blockNumber.ToString();
        }
        /// <summary>
        /// 使用参数初始化<see cref="BlockParameter"/>实例
        /// </summary>
        /// <param name="param">参数</param>
        public BlockParameter(string param)
        {
            param = param.ToLower();
            if (param == "earliest" || param == "pending" || param == "latest") 
                BlockNumber = param;            
            else
            {
                // default value of the block parameter is latest
                try
                {
                    Convert.ToUInt64(param);
                    BlockNumber = param;
                }catch (Exception)
                {
                    BlockNumber = "latest";
                }                
            }
                
        }
    }
}
