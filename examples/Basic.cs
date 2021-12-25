using System;
using PlatONet;

namespace examples
{
    public class Basic
    {
        public static void Main(string[] args)
        {
            var w3 = new Web3("http://35.247.155.162:6789"); // dev net of platon
            var version = w3.GetProgramVersion();
            Console.WriteLine(version);
            var chainId = w3.GetChainId();
            Console.WriteLine(chainId.Value);

            // 快捷生成一个PlatON类的实例
            var platon = w3.PlatON;
            Console.WriteLine(platon.ProtocolVersion());
            Console.WriteLine(platon.GasPrice().Value);
            Console.WriteLine(platon.GetBalance("lat1d4vw2qxjg5ldyaqceel3s6ykpljav6hcn0jfmh").Value);          
            Console.WriteLine(platon.GetTransactionCount("lat1l32ggvel6ndxxlprplz04c3vm2mq4wtgvugn36").Value);
        }
    }
}
