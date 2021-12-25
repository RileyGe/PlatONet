using System;
using PlatONet;

namespace examples.Start
{
    public class Basic
    {
        public static void Main(string[] args)
        {
            var platonWeb3 = new Web3("http://35.247.155.162:6789"); // dev net of platon
            var version = platonWeb3.GetProgramVersion();
            Console.WriteLine(version);
            var chainId = platonWeb3.GetChainId();
            Console.WriteLine(chainId);
        }
    }
}
