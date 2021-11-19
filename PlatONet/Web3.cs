using System;
using Nethereum.JsonRpc.Client;
using System.Threading.Tasks;
using Nethereum.Contracts;
using Nethereum.RPC;

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
        //private ITransactionManager transactionManager;
        //private IAccount account;
        public Web3(string url = "http://localhost:6789"/*, string privateKey = null*/) 
        {
            client = new RpcClient(new Uri(url));
            Init(client);
            
            //if(privateKey != null && privateKey.Length > 0)
            //{
            //    var account = new Nethereum.Web3.Accounts.Account(privateKey);
            //    transactionManager = account.TransactionManager;
            //    transactionManager.Client = client;
            //}else
            //    transactionManager = new TransactionManager(client);
        }
        private void Init(IClient client)
        {
            PlatON = new PlatON()
            {
                Client = client
            };
        }
        public string ClientVersion()
        {
            var result = ClientVersionAsync();
            result.Wait();
            return result.Result;
        }
        public Task<string> ClientVersionAsync()
        {
            return client.SendRequestAsync<string>("web3_clientVersion");
        }
        public string Sha3(string data)
        {
            var result = Sha3Async(data);
            result.Wait();
            return result.Result;
        }
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
        public Contract PlatonGetContract(string abi, string contractAddress)
        {
            //client.
            //var ethService = new EthApiService(client, transactionManager);
            //var id = ethService.ChainId;
            //var result = id.SendRequestAsync();

            var contract = new Contract(new EthApiService(client/*, transactionManager*/), abi, contractAddress);
            return contract;
        }
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
