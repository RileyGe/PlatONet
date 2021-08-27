using System;
using System.Collections.Generic;
using Nethereum.JsonRpc.Client;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace PlatONet
{
    public class Web3
    {
        private RpcClient client;
        public Web3(string url = "http://localhost:6789") 
        {
            client = new RpcClient(new Uri(url));
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
        public string Web3Sha3(string data)
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
        public string PlatonProtocolVersion()
        {
            var result = PlatonProtocolVersionAsync();
            result.Wait();
            return result.Result;
        }
        public Task<string> PlatonProtocolVersionAsync()
        {
            return client.SendRequestAsync<string>("platon_protocolVersion");
        }
        public bool PlatonSyncing()
        {
            var result = PlatonSyncingAsync();
            result.Wait();
            return result.Result;
        }
        public Task<bool> PlatonSyncingAsync()
        {
            return client.SendRequestAsync<bool>("platon_syncing");
        }
        public ulong PlatonGasPrice()
        {
            var result = PlatonGasPriceAsync();
            result.Wait();
            return Convert.ToUInt64(result.Result, 16);
        }
        public Task<string> PlatonGasPriceAsync()
        {
            return client.SendRequestAsync<string>("platon_gasPrice");
        }
        public List<string> PlatonAccounts()
        {
            var result = PlatonAccountsAsync();
            result.Wait();
            return result.Result;
        }
        public Task<List<string>> PlatonAccountsAsync()
        {
            return client.SendRequestAsync<List<string>>("platon_accounts");
        }
        public BigInteger PlatonBlockNumber()
        {
            var result = PlatonBlockNumberAsync();
            result.Wait();
            return Convert.ToUInt64(result.Result, 16);
        }
        public Task<string> PlatonBlockNumberAsync()
        {
            return client.SendRequestAsync<string>("platon_blockNumber");
        }
        public BigInteger PlatonGetBalance(string account, BlockParameter param = null)
        {
            var result = PlatonGetBalanceAsync(account, param);
            result.Wait();
            return hexString2BigInt(result.Result);            
        }
        public Task<string> PlatonGetBalanceAsync(string account, BlockParameter param = null)
        {
            if (param == null) param = BlockParameter.DEFAULT;
            return client.SendRequestAsync<string>("platon_getBalance",null,
                new object[] { 
                    account, param.BlockNumber
                });
        }

        public string PlatonGetStorageAt(string account, ulong position = 0, BlockParameter param = null)
        {
            var result = PlatonGetStorageAtAsync(account, position, param);
            result.Wait();
            return result.Result;
        }
        public Task<string> PlatonGetStorageAtAsync(string account, ulong position = 0, BlockParameter param = null)
        {
            if (param == null) param = BlockParameter.DEFAULT;
            return client.SendRequestAsync<string>("platon_getStorageAt", null,
                new object[] { 
                    account, string.Format("0x{0:X}", position), param.BlockNumber
                });
        }

        public ulong PlatonGetBlockTransactionCountByHash(string blockHash)
        {
            var result = PlatonGetBlockTransactionCountByHashAsync(blockHash);
            result.Wait();
            return Convert.ToUInt64(result.Result, 16);
        }
        public Task<string> PlatonGetBlockTransactionCountByHashAsync(string blockHash)
        {
            return client.SendRequestAsync<string>("platon_getBlockTransactionCountByHash", null,
                new object[] {
                    blockHash
                });
        }

        public ulong PlatonGetTransactionCount(string account, BlockParameter param = null)
        {
            var result = PlatonGetTransactionCountAsync(account, param);
            result.Wait();
            return Convert.ToUInt64(result.Result, 16);
        }

        public Task<string> PlatonGetTransactionCountAsync(string account, BlockParameter param = null)
        {
            if (param == null) param = BlockParameter.DEFAULT;
            return client.SendRequestAsync<string>("platon_getTransactionCount", null,
                new object[] {
                    account, param.BlockNumber
                });
        }

        public ulong PlatonGetBlockTransactionCountByNumber(BlockParameter param = null)
        {
            var result = PlatonGetBlockTransactionCountByNumberAsync(param);
            result.Wait();
            return Convert.ToUInt64(result.Result, 16);
        }

        public Task<string> PlatonGetBlockTransactionCountByNumberAsync(BlockParameter param = null)
        {
            if (param == null) param = BlockParameter.DEFAULT;
            return client.SendRequestAsync<string>("platon_getBlockTransactionCountByNumber", null,
                new object[] {
                    param.BlockNumber
                });
        }

        public string PlatonGetCode(string address, BlockParameter param = null)
        {
            var result = PlatonGetCodeAsync(address, param);
            result.Wait();
            return result.Result;
        }

        public Task<string> PlatonGetCodeAsync(string address, BlockParameter param = null)
        {
            if (param == null) param = BlockParameter.DEFAULT;
            return client.SendRequestAsync<string>("platon_getCode", null,
                new object[] {
                    address, param.BlockNumber
                });
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
