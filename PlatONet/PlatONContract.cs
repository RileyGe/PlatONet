using Nethereum.Contracts;
using Nethereum.Hex.HexConvertors.Extensions;
using Nethereum.Hex.HexTypes;
using Nethereum.JsonRpc.Client;
using System.Threading.Tasks;

namespace PlatONet
{
    public class PlatONContract
    {
        //public Account Account { get; set; }
        public Contract EthContract
        {
            get
            {
                return _contract;
            }
        }
        public PlatON PlatON { get; private set; }
        public Address Address { get; private set; }
        private Contract _contract;
        public PlatONContract(IClient client, string abi, string address, PlatON platon) :
            this(client, abi, new Address(address), platon)
        { }
        public PlatONContract(IClient client, string abi, Address address, PlatON platon)            
        {
            PlatON = platon;
            Address = address;
            _contract = new Contract(new Nethereum.RPC.EthApiService(client), abi, address.ToEthereumAddress());
        }
        public PlatONFunction GetFunction(string functionName)
        {
            return new PlatONFunction(this, _contract.ContractBuilder.GetFunctionBuilder(functionName));
        }
    }
    public class PlatONFunction
    {
        //public Account Account { get; set; }
        private Function _function;
        private PlatONContract _contract;
        public PlatONFunction(PlatONContract contract, FunctionBuilder functionBuilder)            
        {
            _contract = contract;
            _function = new Function(contract.EthContract, functionBuilder);
        }
        public HexBigInteger EstimateGas(params object[] functionInput)
        {
            var result = EstimateGasAsync(functionInput: functionInput);
            result.Wait();
            return result.Result;
        }
        public Task<HexBigInteger> EstimateGasAsync(params object[] functionInput)
        {
            return _function.EstimateGasAsync(functionInput);
        }
        public HexBigInteger EstimateGas(string from, HexBigInteger gas,
            HexBigInteger value, params object[] functionInput)
        {
            var result = EstimateGasAsync(from, gas, value, functionInput: functionInput);
            result.Wait();
            return result.Result;
        }
        public Task<HexBigInteger> EstimateGasAsync(string from, HexBigInteger gas,
            HexBigInteger value, params object[] functionInput)
        {
            string etherAddress;
            if (from == null || from.Length < 1)
            {
                etherAddress = _contract?.PlatON?.Account?.GetAddress()?.ToEthereumAddress();
            }                
            else 
                etherAddress = (new Address(from)).ToEthereumAddress();
            return _function.EstimateGasAsync(etherAddress, gas, value, functionInput);
        }
        public string SendTransaction(params object[] functionInput)
        {
            var result = SendTransactionAsync(functionInput: functionInput);
            result.Wait();
            return result.Result;
        }
        public string SendTransaction(string from, HexBigInteger gas, HexBigInteger gasPrice,
            HexBigInteger value, HexBigInteger nonce, params object[] functionInput)
        {
            var result = SendTransactionAsync(from, gas, gasPrice, value, nonce, functionInput: functionInput);
            result.Wait();
            return result.Result;
        }
        public Task<string> SendTransactionAsync(params object[] functionInput)
        {
            return SendTransactionAsync(null, null, null, null, null, functionInput: functionInput);
        }
        public Task<string> SendTransactionAsync(string from, HexBigInteger gas, HexBigInteger gasPrice,
            HexBigInteger value, HexBigInteger nonce, params object[] functionInput)
        {            
            var sender = from == null || from.Length < 1 ?
                _contract?.PlatON?.Account?.GetAddress()
                : new Address(from);
            if (value == null) value = 0.ToHexBigInteger();
            Task<HexBigInteger> gasResult = null, gasPriceResult = null, nonceResult = null;

            if (gas == null || gas.Value == 0) gasResult = EstimateGasAsync(functionInput);
            if (gasPrice == null || gasPrice.Value == 0) gasPriceResult = _contract.PlatON.GasPriceAsync();
            if (nonce == null) nonceResult = _contract.PlatON.GetTransactionCountAsync(sender.ToString());
            gasResult?.Wait();
            gasPriceResult?.Wait();
            nonceResult?.Wait();
            gas = gasResult == null ? gas : gasResult.Result;
            gasPrice = gasPriceResult == null ? gasPrice : gasPriceResult.Result;
            nonce = nonceResult == null ? nonce : nonceResult.Result;

            var input = _function.CreateTransactionInput(sender?.ToEthereumAddress(), 
                gas, gasPrice, value, functionInput: functionInput);
            var data = input.Data;
            //var nonceNum = _contract.PlatON.GetTransactionCount(sender.ToString());
            //var gasPrice = 1000000000;
            Transaction tx = new Transaction(_contract.Address.ToString(), value, nonce, 
                gasPrice, gas, data, _contract.PlatON.ChainId);
            tx.Sign(_contract?.PlatON?.Account);
            return _contract.PlatON.SendRawTransactionAsync(tx.SignedTransaction.ToHex());
        }
        public TReturn Call<TReturn>(params object[] functionInput)
        {
            var result = CallAsync<TReturn>(functionInput: functionInput);
            result.Wait();
            return result.Result;
        }
        public TReturn Call<TReturn>(string from, HexBigInteger gas,
            HexBigInteger value, params object[] functionInput)
        {
            var result = CallAsync<TReturn>(from, gas, value, functionInput: functionInput);
            result.Wait();
            return result.Result;
        }
        public TReturn Call<TReturn>(string from, HexBigInteger gas,
            HexBigInteger value, BlockParameter block, params object[] functionInput)
        {
            var result = CallAsync<TReturn>(from, gas, value, block, functionInput: functionInput);
            result.Wait();
            return result.Result;
        }
        public TReturn Call<TReturn>(BlockParameter block, params object[] functionInput)
        {
            var result = CallAsync<TReturn>(block, functionInput: functionInput);
            result.Wait();
            return result.Result;
        }
        public Task<TReturn> CallAsync<TReturn>(params object[] functionInput)
        {
            return _function.CallAsync<TReturn>(functionInput);
        }
        public Task<TReturn> CallAsync<TReturn>(string from, HexBigInteger gas,
            HexBigInteger value, params object[] functionInput)
        {
            var sender = new Address(from);
            return _function.CallAsync<TReturn>(sender.ToEthereumAddress(), gas, value, functionInput);
        }
        public Task<TReturn> CallAsync<TReturn>(string from, HexBigInteger gas,
            HexBigInteger value, BlockParameter block, params object[] functionInput)
        {
            var sender = new Address(from);
            return _function.CallAsync<TReturn>(sender.ToEthereumAddress(), gas, value, block, functionInput);
        }
        public Task<TReturn> CallAsync<TReturn>(BlockParameter block, params object[] functionInput)
        {
            return _function.CallAsync<TReturn>(block, functionInput);
        }


    }
}
