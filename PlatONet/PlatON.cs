using Nethereum.JsonRpc.Client;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Threading.Tasks;
using Nethereum.Hex.HexConvertors.Extensions;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.Hex.HexTypes;

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
        public Account Account { get; set; }
        public HexBigInteger ChainId { get; set; }
        public string Hrp { get; set; }
        public PlatONContract GetContract(string abi, string address)
        {
            return new PlatONContract(client, abi, address, this);
        }
        #region rpc requests        
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
        public HexBigInteger GasPrice()
        {
            return ExcuteCommand<HexBigInteger>(ApiMplatonods.platon_gasPrice.ToString());
        }
        public Task<HexBigInteger> GasPriceAsync()
        {
            return ExcuteCommandAsync<HexBigInteger>(ApiMplatonods.platon_gasPrice.ToString());
        }

        public List<string> Accounts()
        {
            return ExcuteCommand<List<string>>(ApiMplatonods.platon_accounts.ToString());
        }
        public Task<List<string>> AccountsAsync()
        {
            return ExcuteCommandAsync<List<string>>(ApiMplatonods.platon_accounts.ToString());
        }
        public HexBigInteger BlockNumber()
        {
            return ExcuteCommand<HexBigInteger>(ApiMplatonods.platon_blockNumber.ToString());
        }
        public Task<HexBigInteger> BlockNumberAsync()
        {
            return ExcuteCommandAsync<HexBigInteger>(ApiMplatonods.platon_blockNumber.ToString());
        }
        public HexBigInteger GetBalance(string account, BlockParameter param = null)
        {
            param = param ?? BlockParameter.DEFAULT;
            return ExcuteCommand<HexBigInteger>(ApiMplatonods.platon_getBalance.ToString(),
                paramList: new object[] {
                    account, param.BlockNumber
                });
        }
        public Task<string> GetBalanceAsync(string account, BlockParameter param = null)
        {
            param = param ?? BlockParameter.DEFAULT;
            return ExcuteCommandAsync<string>(ApiMplatonods.platon_getBalance.ToString(), 
                paramList: new object[] { 
                    account, param.BlockNumber
                });                
        }
        public string GetStorageAt(string account, ulong position = 0, BlockParameter param = null)
        {
            param = param ?? BlockParameter.DEFAULT;
            return ExcuteCommand<string>(ApiMplatonods.platon_getStorageAt.ToString(),
                paramList: new object[] {
                    account, string.Format("0x{0:X}", position), param.BlockNumber
                });
        }
        public Task<string> GetStorageAtAsync(string account, ulong position = 0, BlockParameter param = null)
        {
            param = param ?? BlockParameter.DEFAULT;
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

        public HexBigInteger GetTransactionCount(string account, BlockParameter param = null)
        {
            param = param ?? BlockParameter.DEFAULT;
            return ExcuteCommand<HexBigInteger>(ApiMplatonods.platon_getTransactionCount.ToString(),
                paramList: new object[] {
                    account, param.BlockNumber
                });
        }
        public Task<HexBigInteger> GetTransactionCountAsync(string account, BlockParameter param = null)
        {
            param = param ?? BlockParameter.DEFAULT;
            return ExcuteCommandAsync<HexBigInteger>(ApiMplatonods.platon_getTransactionCount.ToString(), 
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
            param = param ?? BlockParameter.DEFAULT;
            return ExcuteCommandAsync<string>(ApiMplatonods.platon_getBlockTransactionCountByNumber.ToString(),
                paramList: new object[] {
                    param.BlockNumber
                });
        }

        public string GetCode(string address, BlockParameter param = null)
        {
            param = param ?? BlockParameter.DEFAULT;
            return ExcuteCommand<string>(ApiMplatonods.platon_getCode.ToString(),
                paramList: new object[] {
                    address, param.BlockNumber
                });
        }
        public Task<string> GetCodeAsync(string address, BlockParameter param = null)
        {
            param = param ?? BlockParameter.DEFAULT;
            return ExcuteCommandAsync<string>(ApiMplatonods.platon_getCode.ToString(),
                paramList: new object[] {
                    address, param.BlockNumber
                });
        }
        public string SendTransaction(Transaction trans)
        {
            var result = SendTransactionAsync(trans);
            result.Wait();
            return result.Result;
        }
        public Task<string> SendTransactionAsync(Transaction trans)
        {
            //if (!data.StartsWith("0x")) data = "0x" + data;
            if (Account == null) throw new Exception("Please initialize Account before SendTransaction.");
            trans.Sign(Account);
            return SendRawTransactionAsync(trans.SignedTransaction.ToHex());
        }
        public Task<string> SendRawTransactionAsync(string data)
        {
            if (!data.StartsWith("0x")) data = "0x" + data;
            return ExcuteCommandAsync<string>(ApiMplatonods.platon_sendRawTransaction.ToString(), data);
        }
        public string SendRawTransaction(string data)
        {
            if (!data.StartsWith("0x")) data = "0x" + data;
            return ExcuteCommand<string>(ApiMplatonods.platon_sendRawTransaction.ToString(), data);
        }
        public object Call(Transaction trans, BlockParameter param = null)
        {
            return Call<object>(trans, param);
        }
        public Task<object> CallAsync(Transaction trans, BlockParameter param = null)
        {
            return CallAsync<object>(trans, param);
        }
        public T Call<T>(Transaction trans, BlockParameter param = null)
        {
            param = param ?? BlockParameter.DEFAULT;
            return ExcuteCommand<T>(ApiMplatonods.platon_call.ToString(),
                paramList: new object[] {
                    trans.ToDict(), param.BlockNumber
                });
        }
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
            return client.SendRequestAsync<T>(method, null, paramList);
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
