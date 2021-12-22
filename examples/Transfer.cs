using PlatONet;
using System;

namespace examples
{
    public class Transfer
    {
        public static void Main(string[] args)
        {
            var privateKey = "d08baac64f52ae1b9c2ea559036650229f07f5d61d869dbb55562a9827fbaeb8";
            var w3 = new Web3("http://35.247.155.162:6789", privateKey); // dev net of platon
            var toAddress = "lat1ljlf4myhux0zahfmlxf79wr7sl8u7pdey88dyp";
            var amount = new HexBigInteger((ulong)1e18);
            var gasPrice = new HexBigInteger((ulong)1e9);
            var gasLimit = new HexBigInteger(21000);            
            var nonceNum = w3.PlatON.GetTransactionCount();

            // 构建交易
            var tx = new Transaction(toAddress, amount, nonceNum, gasPrice, gasLimit);
            //发送交易
            var result = w3.PlatON.SendTransaction(tx);
            Console.WriteLine(result);
        }
    }
}
