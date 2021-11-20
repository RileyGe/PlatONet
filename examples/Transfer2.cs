using PlatONet;
using Nethereum.Hex.HexConvertors.Extensions;
using Nethereum.Signer;
using System;
using Nethereum.RLP;
using Nethereum.Hex.HexTypes;

namespace examples
{
    public class Transfer2
    {
        public static void Main(string[] args)
        {
            var platonWeb3 = new Web3("http://35.247.155.162:6789"); // dev net of platon
            var address = "lat1ljlf4myhux0zahfmlxf79wr7sl8u7pdey88dyp";
            var fromAddress = new Address("lat1d4vw2qxjg5ldyaqceel3s6ykpljav6hcn0jfmh");
            ulong amount = (ulong)1e18;
            var nonceNum = platonWeb3.PlatON.GetTransactionCount(fromAddress.ToString());
            //var nonce = ((int)nonceNum).ToBytesForRLPEncoding();
            ulong gasPrice = 1000000000;
            ulong gasLimit = 21000;

            var tx = new Transaction(address, amount, nonceNum.ToUlong(), gasPrice, gasLimit);
            // private key for account lat1d4vw2qxjg5ldyaqceel3s6ykpljav6hcn0jfmh
            var privateKey = "d08baac64f52ae1b9c2ea559036650229f07f5d61d869dbb55562a9827fbaeb8";
            var platonKey = new EthECKey(privateKey.HexToByteArray(), true);
            tx.Sign(platonKey);
            var result = platonWeb3.PlatON.SendRawTransaction(tx.SignedTransaction.ToHex());
            Console.WriteLine(result);
        }
    }
}
