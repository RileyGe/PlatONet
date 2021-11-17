using Nethereum.Hex.HexConvertors.Extensions;
using Nethereum.Signer;
using PlatONet;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace examples
{
    public class ContractInteractive
    {
        public static void Main(string[] args)
        {
            var privateKey = "d08baac64f52ae1b9c2ea559036650229f07f5d61d869dbb55562a9827fbaeb8";
            var platonWeb3 = new Web3("http://35.247.155.162:6789"); // dev net of platon
            var abi = @"[
        {
          ""constant"": false,
          ""inputs"": [
            {
              ""internalType"": ""string"",
              ""name"": ""_name"",
              ""type"": ""string""
            }
          ],
          ""name"": ""setName"",
          ""outputs"": [
            {
              ""internalType"": ""string"",
              ""name"": """",
              ""type"": ""string""
            }
          ],
          ""payable"": false,
          ""stateMutability"": ""nonpayable"",
          ""type"": ""function""
        },
        {
          ""constant"": true,
          ""inputs"": [],
          ""name"": ""getName"",
          ""outputs"": [
            {
              ""internalType"": ""string"",
              ""name"": """",
              ""type"": ""string""
            }
          ],
          ""payable"": false,
          ""stateMutability"": ""view"",
          ""type"": ""function""
        }
      ]";
            var contractAddress = new Address("lat1ts7u6ekl9zfmqw399wvnfh2gxc5gkjv34ymkvt");
            var contract = platonWeb3.PlatonGetContract(abi, contractAddress.ToEthereumAddress());
            
            var result = contract.GetFunction("getName").CallAsync<string>();
            result.Wait();
            //var result = getNameFunction.CallAsync<string>();
            ////transferFunction.
            //result.Wait();
            Console.WriteLine(result.Result);
            // sender address
            var sender = new Address("lat1d4vw2qxjg5ldyaqceel3s6ykpljav6hcn0jfmh");
            // private key for account lat1d4vw2qxjg5ldyaqceel3s6ykpljav6hcn0jfmh
            
            var setNameFunction = contract.GetFunction("setName");
            
            var gas = setNameFunction.EstimateGasAsync(sender.ToEthereumAddress(), null, null, "rgrgrg");
            gas.Wait();
            //var receipt = setNameFunction.SendTransactionAndWaitForReceiptAsync(sender.ToEthereumAddress(), 
            //    gas.Result, null, null, "rgrgrg");
            var input = setNameFunction.CreateTransactionInput(sender.ToEthereumAddress(), gas.Result, null, null, "rgrgrg");
            var data = input.Data;
            var nonceNum = (long)platonWeb3.PlatON.GetTransactionCount(sender.ToString());
            var gasPrice = 1000000000;
            Transaction tx = new Transaction(contractAddress.ToString(), 0, nonceNum, gasPrice);
            tx.GasLimit = gas.Result;
            tx.Data = data;

            //var privateKey = "d08baac64f52ae1b9c2ea559036650229f07f5d61d869dbb55562a9827fbaeb8";
            var platonKey = new EthECKey(privateKey.HexToByteArray(), true);
            tx.Sign(platonKey);
            var result2 = platonWeb3.PlatON.SendRawTransaction(tx.SignedTransaction.ToHex());
        }
    }
}
