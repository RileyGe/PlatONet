using PlatONet;
using System;
using System.Collections.Generic;
using System.Text;
using Nethereum.Hex.HexTypes;

namespace examples
{
    public class PPOS
    {
        public static void Main(string[] args)
        {
            var privateKey = "d08baac64f52ae1b9c2ea559036650229f07f5d61d869dbb55562a9827fbaeb8";
            var platonWeb3 = new Web3("http://35.247.155.162:6789");
            //platonWeb3.InitAccount(privateKey);
            var result = platonWeb3.PlatON.PPOS.GetActiveVersion();
            var str = PlatONet.PPOS.DecodeResponse<CallResponse<HexBigInteger>>(result);
            Console.WriteLine(result);
        }
    }
}
