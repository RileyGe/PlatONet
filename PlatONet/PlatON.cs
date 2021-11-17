using Nethereum.JsonRpc.Client;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Nethereum.Hex.HexConvertors.Extensions;

namespace PlatONet
{
    public class PlatON
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
        public string ProtocolVersion()
        {      
            //Nethereum.Hex.HexConvertors.Extensions.HexBigIntegerConvertorExtensions.
            return ExcuteCommand<string>(ApiMplatonods.platon_protocolVersion.ToString());
        }
        public Task<string> VersionAsync()
        {
            return ExcuteCommandAsync<string>(ApiMplatonods.platon_protocolVersion.ToString());
        }
        public bool Syncing()
        {
            return ExcuteCommand<bool>(ApiMplatonods.platon_syncing.ToString());
        }
        public Task<bool> SyncingAsync()
        {
            return ExcuteCommandAsync<bool>(ApiMplatonods.platon_syncing.ToString());
            //return client.SendRequestAsync<bool>("platon_syncing");
        }
        public BigInteger GasPrice()
        {
            return ExcuteCommandParseBigInteger(ApiMplatonods.platon_gasPrice.ToString());
        }
        public Task<string> GasPriceAsync()
        {
            return ExcuteCommandAsync<string>(ApiMplatonods.platon_gasPrice.ToString());
        }

        public List<string> Accounts()
        {
            return ExcuteCommand<List<string>>(ApiMplatonods.platon_accounts.ToString());
        }
        public Task<List<string>> AccountsAsync()
        {
            return ExcuteCommandAsync<List<string>>(ApiMplatonods.platon_accounts.ToString());
        }
        public BigInteger BlockNumber()
        {
            return ExcuteCommandParseBigInteger(ApiMplatonods.platon_blockNumber.ToString());
        }
        public Task<string> BlockNumberAsync()
        {
            return ExcuteCommandAsync<string>(ApiMplatonods.platon_blockNumber.ToString());
        }
        public BigInteger GetBalance(string account, BlockParameter param = null)
        {
            if (param == null) param = BlockParameter.DEFAULT;
            return ExcuteCommandParseBigInteger(ApiMplatonods.platon_getBalance.ToString(),
                paramList: new object[] {
                    account, param.BlockNumber
                });
        }
        public Task<string> GetBalanceAsync(string account, BlockParameter param = null)
        {
            if (param == null) param = BlockParameter.DEFAULT;
            return ExcuteCommandAsync<string>(ApiMplatonods.platon_getBalance.ToString(), 
                paramList: new object[] { 
                    account, param.BlockNumber
                });                
        }
        public string GetStorageAt(string account, ulong position = 0, BlockParameter param = null)
        {
            if (param == null) param = BlockParameter.DEFAULT;
            return ExcuteCommand<string>(ApiMplatonods.platon_getStorageAt.ToString(),
                paramList: new object[] {
                    account, string.Format("0x{0:X}", position), param.BlockNumber
                });
        }
        public Task<string> GetStorageAtAsync(string account, ulong position = 0, BlockParameter param = null)
        {
            if (param == null) param = BlockParameter.DEFAULT;
            return ExcuteCommandAsync<string>(ApiMplatonods.platon_getStorageAt.ToString(), 
                paramList: new object[] {
                    account, string.Format("0x{0:X}", position), param.BlockNumber
                });
        }

        public ulong GetBlockTransactionCountByHash(string blockHash)
        {
            var result = GetBlockTransactionCountByHashAsync(blockHash);
            result.Wait();
            return Convert.ToUInt64(result.Result, 16);
        }
        public Task<string> GetBlockTransactionCountByHashAsync(string blockHash)
        {
            return ExcuteCommandAsync<string>(ApiMplatonods.platon_getBlockTransactionCountByHash.ToString(),
                paramList: new object[] {
                    blockHash
                });
        }

        public ulong GetTransactionCount(string account, BlockParameter param = null)
        {
            var result = GetTransactionCountAsync(account, param);
            result.Wait();
            return Convert.ToUInt64(result.Result, 16);
        }
        public Task<string> GetTransactionCountAsync(string account, BlockParameter param = null)
        {
            if (param == null) param = BlockParameter.DEFAULT;
            return ExcuteCommandAsync<string>(ApiMplatonods.platon_getTransactionCount.ToString(), 
                paramList: new object[] {
                    account, param.BlockNumber
                });
        }
        public ulong GetBlockTransactionCountByNumber(BlockParameter param = null)
        {
            var result = GetBlockTransactionCountByNumberAsync(param);
            result.Wait();
            return Convert.ToUInt64(result.Result, 16);
        }

        public Task<string> GetBlockTransactionCountByNumberAsync(BlockParameter param = null)
        {
            if (param == null) param = BlockParameter.DEFAULT;
            return ExcuteCommandAsync<string>(ApiMplatonods.platon_getBlockTransactionCountByNumber.ToString(),
                paramList: new object[] {
                    param.BlockNumber
                });
        }

        public string GetCode(string address, BlockParameter param = null)
        {
            if (param == null) param = BlockParameter.DEFAULT;
            return ExcuteCommand<string>(ApiMplatonods.platon_getCode.ToString(),
                paramList: new object[] {
                    address, param.BlockNumber
                });
        }
        public Task<string> GetCodeAsync(string address, BlockParameter param = null)
        {
            if (param == null) param = BlockParameter.DEFAULT;
            return ExcuteCommandAsync<string>(ApiMplatonods.platon_getCode.ToString(),
                paramList: new object[] {
                    address, param.BlockNumber
                });
        }
        public BigInteger ExcuteCommandParseBigInteger(string method, params object[] paramList)
        {
            var result = ExcuteCommandAsync<string>(method, paramList);
            result.Wait();
            return result.Result.HexToBigInteger(false);
        }
        
        public Task<T> ExcuteCommandAsync<T>(string method, params object[] paramList)
        {
            return client.SendRequestAsync<T>(method, null, paramList);
        }
        public T ExcuteCommand<T>(string method, params object[] paramList)
        {
            var result = ExcuteCommandAsync<T>(method, paramList);
            result.Wait();
            return result.Result;
        }
    }
}
