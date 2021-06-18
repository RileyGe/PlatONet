using System;
using System.Collections.Generic;
using Nethereum.JsonRpc.Client;
using System.Text;
using System.Threading.Tasks;

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
    }
}
