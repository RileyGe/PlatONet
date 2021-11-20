using PlatONet;
using System;

namespace examples
{
    public class Contract3
    {
        public static void Main(string[] args)
        {
            var privateKey = "d08baac64f52ae1b9c2ea559036650229f07f5d61d869dbb55562a9827fbaeb8";
            var platonWeb3 = new Web3("http://35.247.155.162:6789");
            platonWeb3.InitAccount(privateKey);
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
            var contract = platonWeb3.PlatON.GetContract(abi, contractAddress.ToString());

            var result = contract.GetFunction("getName").CallAsync<string>();
            result.Wait();
            Console.WriteLine(result.Result);
            var nameResult = contract.GetFunction("setName").SendTransactionAsync("abcabc");
            nameResult.Wait();
            Console.WriteLine(nameResult.Result);
        }
    }
}
