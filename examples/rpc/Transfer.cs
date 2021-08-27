using PlatONet;
using System.Numerics;
using Nethereum.Hex.HexConvertors.Extensions;
using Nethereum.Signer;
using System;

namespace examples.rpc
{
    public class Transfer
    {
        public static void Main(string[] args)
        {
            var platonWeb3 = new Web3("http://47.241.98.219:6789"); // dev net of platon

            string to = "lat1d4vw2qxjg5ldyaqceel3s6ykpljav6hcn0jfmh";
            Bech32Engine.Decode(to, out string _, out byte[] to_address);
            var amount = new BigInteger(1e18).ToByteArray();
            var nonce = new BigInteger(1).ToByteArray();
            var gasPrice = new BigInteger(1000000000).ToByteArray();
            var gasLimit = new BigInteger(21000).ToByteArray();
            var data = new byte[0];
            var chainId = new BigInteger(210309).ToByteArray();

            var tx = new LegacyTransactionChainId(nonce, gasPrice, gasLimit, to_address, amount, nonce, gasPrice, gasLimit, data, chainId);
            var privateKey = "d08baac64f52ae1b9c2ea559036650229f07f5d61d869dbb55562a9827fbaeb8";
            var platonKey = new EthECKey(privateKey.HexToByteArray(), true);
            tx.Sign(platonKey);
            var result = platonWeb3.PlatonSendRawTransaction(HexByteConvertorExtensions.ToHex(tx.GetRLPEncodedRaw()));
            Console.WriteLine(result);            
        }
    }
}
