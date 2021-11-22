using System;
using System.Collections.Generic;
using System.Text;
using PlatONet;
using Nethereum.RLP;

namespace examples
{
    public class Basic
    {
        public static void Main(string[] args)
        {
            var platonWeb3 = new Web3("http://35.247.155.162:6789"); // dev net of platon
            //var version = platonWeb3.GetProgramVersion();
            //Console.WriteLine(version);
            //var prove = platonWeb3.GetSchnorrNIZKProve();
            //Console.WriteLine(prove);
            //var config = platonWeb3.GetEconomicConfig();
            //Console.WriteLine(config);
            var ls = platonWeb3.GetWaitSlashingNodeList();
            Console.WriteLine(ls);
            //var version = platonWeb3.ClientVersion();
            //Console.WriteLine(version);
            //var netVersion = platonWeb3.NetVersion();
            //Console.WriteLine(netVersion);
            //Console.WriteLine(platonWeb3.NetListening());
            //Console.WriteLine(platonWeb3.NetPeerCount());
            //Console.WriteLine(platonWeb3.PlatON.ProtocolVersion());
            //Console.WriteLine(platonWeb3.PlatON.Syncing());
            //Console.WriteLine(platonWeb3.PlatON.GasPrice());
            //Console.WriteLine(platonWeb3.PlatON.Accounts());
            //Console.WriteLine(platonWeb3.PlatON.BlockNumber());
            //Console.WriteLine(platonWeb3.PlatON.GetBalance("lat1d4vw2qxjg5ldyaqceel3s6ykpljav6hcn0jfmh"));
            //Console.WriteLine(platonWeb3.PlatON.GetStorageAt("lat1awfagfqfxjcehr9kx26q9y6kg8j4wuyy9dswm5"));
            //Console.WriteLine(platonWeb3.PlatON.GetBlockTransactionCountByHash("0xba9436a521dd74a105457231c69dd195cd3da45aac26e50a6df45040b554327b"));
            //Console.WriteLine(platonWeb3.PlatON.GetTransactionCount("lat1l32ggvel6ndxxlprplz04c3vm2mq4wtgvugn36"));
            //Console.WriteLine(platonWeb3.PlatON.GetBlockTransactionCountByNumber());
        }
    }
}
