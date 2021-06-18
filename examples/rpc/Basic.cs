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
            var version = platonWeb3.ClientVersion();
            Console.WriteLine(version);
            var sha3 = platonWeb3.Web3Sha3("");
            Console.WriteLine(sha3);
            var netVersion = platonWeb3.NetVersion();
            Console.WriteLine(netVersion);
            Console.WriteLine(platonWeb3.NetListening());
            Console.WriteLine(platonWeb3.NetPeerCount());
            Console.WriteLine(platonWeb3.PlatonProtocolVersion());
            Console.WriteLine(platonWeb3.PlatonSyncing());
            Console.WriteLine(platonWeb3.PlatonGasPrice());
        }
    }
}
