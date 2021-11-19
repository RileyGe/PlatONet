using Nethereum.Contracts;
using Nethereum.Hex.HexConvertors.Extensions;
using Nethereum.Hex.HexTypes;
using Nethereum.JsonRpc.Client;
using Nethereum.RPC.Eth.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
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
        public Task<HexBigInteger> EstimateGasAsync(params object[] functionInput)
        {
            return _function.EstimateGasAsync(functionInput);
        }
        public Task<HexBigInteger> EstimateGasAsync(string from, HexBigInteger gas,
            HexBigInteger value, params object[] functionInput)
        {
            string etherAddress;
            if (from == null || from.Length < 1)
            {
                etherAddress = _contract?.PlatON?.Account?.ToAddress()?.ToEthereumAddress();
            }                
            else 
                etherAddress = (new Address(from)).ToEthereumAddress();
            return _function.EstimateGasAsync(etherAddress, gas, value, functionInput);
        }
        public Task<string> SendTransactionAsync(params object[] functionInput)
        {
            var from = _contract?.PlatON?.Account?.ToAddress();
            var gas = EstimateGasAsync(functionInput);
            var gasPrice = _contract.PlatON.GasPriceAsync();
            var value = 0.ToHexBigInteger();
            var nonce = _contract.PlatON.GetTransactionCountAsync(from.ToString());
            gas.Wait();
            gasPrice.Wait();
            nonce.Wait();
            return SendTransactionAsync(from.ToString(), gas.Result, gasPrice.Result, value, new HexBigInteger(nonce.Result), functionInput: functionInput);
        }
        public Task<string> SendTransactionAsync(string from, HexBigInteger gas, HexBigInteger gasPrice,
            HexBigInteger value, HexBigInteger nonce, params object[] functionInput)
        {
            var sender = new Address(from);
            var input = _function.CreateTransactionInput(sender.ToEthereumAddress(), 
                gas, gasPrice, value, functionInput: functionInput);
            var data = input.Data;
            //var nonceNum = _contract.PlatON.GetTransactionCount(sender.ToString());
            //var gasPrice = 1000000000;
            Transaction tx = new Transaction(_contract.Address.ToString(), value, nonce, 
                gasPrice, gas, data, _contract.PlatON.ChainId);
            tx.Sign(_contract?.PlatON?.Account);
            return _contract.PlatON.SendRawTransactionAsync(tx.SignedTransaction.ToHex());
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
