using System;
using System.Collections.Generic;
using Nethereum.JsonRpc.Client;
using System.Threading.Tasks;
using System.Numerics;
using Nethereum.Contracts;
using Nethereum.RPC;
using Nethereum.RPC.TransactionManagers;

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
        public BigInteger PlatonEstimateGas(Transaction transaction)
        {
            var result = PlatonEstimateGasAsync(transaction);
            result.Wait();
            return hexString2BigInt(result.Result);
        }
        public Task<string> PlatonEstimateGasAsync(Transaction transaction)
        {
            //var tx = new Dictionary<string, string>();
            //tx.Add("to", "lat1d4vw2qxjg5ldyaqceel3s6ykpljav6hcn0jfmh");
            //tx.Add("gas", "0x76c0");
            //tx.Add("gasPrice", "0x9184e72a000");
            //tx.Add("value", "0x9184e72a");
            ////tx.Add("data", "");
            var dict = transaction.ToDict();
            if (dict.ContainsKey("gas")) dict.Remove("gas");
            return client.SendRequestAsync<string>("platon_estimateGas", null, 
                new object[]{
                    dict
                });
            //return new Task<string>(,);
        }
        
        // platon_sendRawTransaction
        public Task<string> PlatonSendRawTransactionAsync(string data)
        {
            if (!data.StartsWith("0x")) data = "0x" + data;
            return client.SendRequestAsync<string>("platon_sendRawTransaction", null, data);
        }

        public string PlatonSendRawTransaction(string data)
        {            
            var result = PlatonSendRawTransactionAsync(data);
            result.Wait();
            return result.Result;
        }

        private BigInteger hexString2BigInt(string hexStr)
        {
            hexStr = hexStr.ToLower();
            if (hexStr.StartsWith("0x")) hexStr = hexStr.Remove(0, 2);
            hexStr = "0" + hexStr;
            return BigInteger.Parse(hexStr, System.Globalization.NumberStyles.HexNumber);
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
