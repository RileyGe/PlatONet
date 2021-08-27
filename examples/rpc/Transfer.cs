﻿using PlatONet;
using Nethereum.Hex.HexConvertors.Extensions;
using Nethereum.Signer;
using System;
using Nethereum.RLP;

namespace examples.rpc
{
    public class Transfer
    {
        public static void Main(string[] args)
        {
            var platonWeb3 = new Web3("http://47.241.98.219:6789"); // dev net of platon
            var address = new Address("lat1klsjfxjwq2dvdd567wz7qwrjjluvzvsrf2t9k3");
            var amount = ((long)1e18).ToBytesForRLPEncoding();
            platonWeb3.PlatonGetTransactionCount(address.ToString());
            var nonce = 4.ToBytesForRLPEncoding();
            var gasPrice = 1000000000.ToBytesForRLPEncoding();
            var gasLimit = 21000.ToBytesForRLPEncoding();
            var data = new byte[0];
            var chainId = 210309.ToBytesForRLPEncoding();
            

            //var tx2 = new Transaction(nonce, gasPrice, gasLimit, address.Bytes, amount, data, chainId);

            //var tx = new LegacyTransactionChainId(nonce, gasPrice, gasLimit, address.Bytes, amount, data, chainId);
            var tx = new Transaction();
            var privateKey = "d08baac64f52ae1b9c2ea559036650229f07f5d61d869dbb55562a9827fbaeb8";
            var platonKey = new EthECKey(privateKey.HexToByteArray(), true); 
            tx.Sign(platonKey);            
            var result = platonWeb3.PlatonSendRawTransaction(tx.GetRLPEncoded().ToHex());
            Console.WriteLine(result);            
        }
    }
}