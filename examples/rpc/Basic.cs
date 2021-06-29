using System;
using System.Collections.Generic;
using System.Text;
using PlatONet;

namespace examples.rpc
{
    public class Basic
    {
        public static void Main(string[] args)
        {
            var platonWeb3 = new Web3("http://47.241.98.219:6789");
            //var version = platonWeb3.ClientVersion();
            //Console.WriteLine(version);
            //var sha3 = platonWeb3.Web3Sha3("");
            //Console.WriteLine(sha3);
            //var netVersion = platonWeb3.NetVersion();
            //Console.WriteLine(netVersion);
            //Console.WriteLine(platonWeb3.NetListening());
            //Console.WriteLine(platonWeb3.NetPeerCount());
            //Console.WriteLine(platonWeb3.PlatonProtocolVersion());
            //Console.WriteLine(platonWeb3.PlatonSyncing());
            //Console.WriteLine(platonWeb3.PlatonGasPrice());
            //Console.WriteLine(platonWeb3.PlatonAccounts());
            //Console.WriteLine(platonWeb3.PlatonBlockNumber());
            Console.WriteLine(platonWeb3.PlatonGetBalance("lat1awfagfqfxjcehr9kx26q9y6kg8j4wuyy9dswm5"));
            Console.WriteLine(platonWeb3.PlatonGetStorageAt("lat1awfagfqfxjcehr9kx26q9y6kg8j4wuyy9dswm5"));
            Console.WriteLine(platonWeb3.PlatonGetBlockTransactionCountByHash("0xba9436a521dd74a105457231c69dd195cd3da45aac26e50a6df45040b554327b"));
            Console.WriteLine(platonWeb3.PlatonGetTransactionCount("lat1l32ggvel6ndxxlprplz04c3vm2mq4wtgvugn36"));
            Console.WriteLine(platonWeb3.PlatonGetBlockTransactionCountByNumber());
        }
    }
}
