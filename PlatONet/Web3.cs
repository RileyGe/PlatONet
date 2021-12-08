using System;
using Nethereum.JsonRpc.Client;
using System.Threading.Tasks;
using Nethereum.Contracts;
using Nethereum.RPC;
using Nethereum.Hex.HexTypes;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace PlatONet
{
    public class Web3
    {
        private IClient client;
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
        public Web3(string uri, string privateKey, bool initChainIdAndHrp)
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
            PlatON.ChainId = PlatON.GetChainId().ToHexBigInteger();
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
        /// 返回当前客户端版本
        /// </summary>
        /// <returns>当前客户端版本</returns>
        public Task<string> ClientVersionAsync()
        {
            return client.SendRequestAsync<string>("web3_clientVersion");
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
        /// 返回给定数据的keccak-256（不是标准sha3-256）
        /// </summary>
        /// <param name="data">加密前的数据</param>
        /// <returns>加密后的数据</returns>
        public Task<string> Sha3Async(string data)
        {
            return client.SendRequestAsync<string>("web3_sha3", null,
                new object[] { data });
        }
        public string NetVersion()
        {
            var result = NetVersionAsync();
            result.Wait();
            return result.Result;
        }
        public Task<string> NetVersionAsync()
        {
            return client.SendRequestAsync<string>("net_version");            
        }
        public bool NetListening()
        {
            var result = NetListeningAsync();
            result.Wait();
            return result.Result;
        }
        public Task<bool> NetListeningAsync()
        {
            return client.SendRequestAsync<bool>("net_listening");
        }
        public ulong NetPeerCount()
        {
            var result = NetPeerCountAsync();
            result.Wait();
            return Convert.ToUInt64(result.Result, 16);
        }
        /// <summary>
        /// 以16进制字符串的形式返回
        /// </summary>
        /// <returns></returns>
        public Task<string> NetPeerCountAsync()
        {
            return client.SendRequestAsync<string>("net_peerCount");
        }
        public Contract GetContract(string abi, string contractAddress)
        {
            var contract = new Contract(new EthApiService(client/*, transactionManager*/), abi, contractAddress);
            return contract;
        }
        public Task<ProgramVersion> GetProgramVersionAsync()
        {
            return PlatON.ExcuteCommandAsync<ProgramVersion>("admin_getProgramVersion");
        }
        public ProgramVersion GetProgramVersion()
        {
            return PlatON.ExcuteCommand<ProgramVersion>("admin_getProgramVersion");
        }
        public string GetSchnorrNIZKProve()
        {
            return PlatON.ExcuteCommand<string>("admin_getSchnorrNIZKProve");
        }
        public Task<string> GetSchnorrNIZKProveAsync()
        {
            return PlatON.ExcuteCommandAsync<string>("admin_getSchnorrNIZKProve");
        }
        public EconomicConfig GetEconomicConfig()
        {
            var result = PlatON.ExcuteCommand<string>("debug_economicConfig");
            return JsonConvert.DeserializeObject<EconomicConfig>(result);
        }
        public Task<string> GetEconomicConfigAsync()
        {
            return PlatON.ExcuteCommandAsync<string>("debug_economicConfig");
        }
        public ulong GetChainId()
        {
            return Convert.ToUInt64(PlatON.ExcuteCommand<string>(ApiMplatonods.platon_chainId.ToString()), 16);
        }
        public Task<string> GetChainIdAsync()
        {
            return PlatON.ExcuteCommandAsync<string>(ApiMplatonods.platon_chainId.ToString());
        }
        public List<WaitSlashingNode> GetWaitSlashingNodeList()
        {
            var result = PlatON.ExcuteCommand<string>("debug_getWaitSlashingNodeList");
            return JsonConvert.DeserializeObject<List<WaitSlashingNode>>(result);
        }
        public Task<string> GetWaitSlashingNodeListAsync()
        {
            return PlatON.ExcuteCommandAsync<string>("debug_getWaitSlashingNodeList");
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
        public static BlockParameter EARLIEST = Init("earliest");
        public static BlockParameter LATEST = Init("latest");
        public static BlockParameter PENDING = Init("pending");
        public static BlockParameter DEFAULT = Init("latest");

        public static BlockParameter Init(ulong blockNumber)
        {
            return new BlockParameter(blockNumber);
        }
        public static BlockParameter Init(string param)
        {
            return new BlockParameter(param);
        }

        //private long blockNumber;
        public string BlockNumber
        {
            get;set;
        }
        public BlockParameter(ulong blockNumber)
        {
            BlockNumber = blockNumber.ToString();
        }
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
