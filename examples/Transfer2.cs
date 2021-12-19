using PlatONet;
using Nethereum.Hex.HexConvertors.Extensions;
using System;
using Nethereum.Hex.HexTypes;

namespace examples
{
    public class Transfer2
    {
        public static void Main(string[] args)
        {
            var w3 = new Web3("http://35.247.155.162:6789"); // dev net of platon
            var toAddress = "lat1ljlf4myhux0zahfmlxf79wr7sl8u7pdey88dyp";
            var amount = ((ulong)1e18).ToHexBigInteger();
            var gasPrice = ((ulong)1e9).ToHexBigInteger();
            var gasLimit = 21000.ToHexBigInteger();

            var privateKey = "d08baac64f52ae1b9c2ea559036650229f07f5d61d869dbb55562a9827fbaeb8";
            var account = new Account(privateKey);
            var nonceNum = w3.PlatON.GetTransactionCount(account.GetAddress("lat").ToString());

            // 构建交易
            var tx = new Transaction(toAddress, amount, nonceNum, gasPrice, gasLimit);
            // 签名交易
            tx.Sign(account);
            //发送交易
            var result = w3.PlatON.SendRawTransaction(tx.SignedTransaction.ToHex());
            Console.WriteLine(result);
        }
    }
}
