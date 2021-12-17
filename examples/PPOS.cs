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
            platonWeb3.InitAccount(privateKey);
            Console.WriteLine((new Address("lat1d4vw2qxjg5ldyaqceel3s6ykpljav6hcn0jfmh")).ToEthereumAddress());
            var ppos = platonWeb3.PlatON.PPOS;
            //platonWeb3.InitAccount(privateKey);
            //var result = ppos.GetActiveVersion();
            //var str = PlatONet.PPOS.DecodeResponse<CallResponse<HexBigInteger>>(result);
            //var result2 = ppos.GetStakingInfo("0x99e0e221e025041d9025205e12af0b0fcdca8cb70d98f4bd6ab82be65fe0f59cab996e16ee56304b48ffb9de8d81fa7759594d61d186c9ea7442917fa555a463");
            //Console.WriteLine(result);

            //string hash = ppos.Delegate(StakingAmountType.FREE_AMOUNT_TYPE, 
            //    "0x99e0e221e025041d9025205e12af0b0fcdca8cb70d98f4bd6ab82be65fe0f59cab996e16ee56304b48ffb9de8d81fa7759594d61d186c9ea7442917fa555a463",
            //    ((ulong)1e19).ToHexBigInteger());
            //Console.WriteLine(hash);
            //var receipt = platonWeb3.PlatON.GetTransactionReceipt(hash);
            //Console.WriteLine(receipt);

            //var delegeateList = ppos.GetDelegateListByAddr("lat1d4vw2qxjg5ldyaqceel3s6ykpljav6hcn0jfmh");
            //Console.WriteLine(delegeateList);

            //List<string> nodeList = new List<string>
            //{
            //    "0x99e0e221e025041d9025205e12af0b0fcdca8cb70d98f4bd6ab82be65fe0f59cab996e16ee56304b48ffb9de8d81fa7759594d61d186c9ea7442917fa555a463"
            //};
            //var rewardList = ppos.GetDelegateReward("lat1d4vw2qxjg5ldyaqceel3s6ykpljav6hcn0jfmh", nodeList);
            //Console.WriteLine(rewardList);
            //var nodeList = ppos.GetVerifierList();
            //Console.WriteLine(nodeList);
            var value = ppos.GetGovernParamValue("staking", "stakeThreshold");
            Console.WriteLine(value);
        }
    }
}
