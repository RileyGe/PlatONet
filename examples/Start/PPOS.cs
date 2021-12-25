using PlatONet;
using PlatONet.DTOs;
using System;

namespace examples.Start
{
    public class PPOS
    {
        public static void Main(string[] args)
        {
            var privateKey = "d08baac64f52ae1b9c2ea559036650229f07f5d61d869dbb55562a9827fbaeb8";
            var w3 = new Web3("http://35.247.155.162:6789", privateKey);
            Console.WriteLine((new Address("lat1d4vw2qxjg5ldyaqceel3s6ykpljav6hcn0jfmh")).ToEthereumAddress());
            var ppos = w3.PlatON.PPOS;
            var nodeId = "0x177226cb3440cec3b8e7b7d591fde985665a3fc1e069a7f9db86080350cb91e8ecb1cad35ed786ee22256229182f79909dcf7431b58e58c9a706935b6046ffb2";
            var result = ppos.GetStakingInfo(nodeId);
            Console.WriteLine(result);

            string hash = ppos.Delegate(nodeId, StakingAmountType.FREE_AMOUNT_TYPE,
                ((ulong)1e19).ToHexBigInteger());
            Console.WriteLine(hash);
        }
    }
}
