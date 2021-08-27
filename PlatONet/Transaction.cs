using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using Nethereum.Hex.HexConvertors.Extensions;
using Nethereum.RLP;
using Nethereum.Signer;


namespace PlatONet
{
    public class Transaction : LegacyTransactionChainId
    {
        public Transaction(byte[] receiveAddress = null, byte[] value = null, byte[] nonce = null, byte[] gasPrice = null, byte[] gasLimit = null, 
            byte[] data = null, byte[] chainId = null) : base(nonce, gasPrice, gasLimit, receiveAddress, value, data, chainId)
        {
            
        }
        public Transaction(string to, long amount = 0, long nonce = -1, long gasPrice = 0, long gasLimit = 0,
            string data = "", long chainId = 210309) : 
            this(new Address(to).Bytes, amount.ToBytesForRLPEncoding(), nonce.ToBytesForRLPEncoding(), 
                gasPrice.ToBytesForRLPEncoding(), gasLimit.ToBytesForRLPEncoding(),
                data.HexToByteArray(), chainId.ToBytesForRLPEncoding())
        {

        }
        public Transaction(string to, BigInteger amount, BigInteger nonce, BigInteger gasPrice, BigInteger gasLimit, 
            string data, BigInteger chainId) : base(to, amount, nonce, gasPrice, gasLimit, data, chainId)
        {

        }
        public static Transaction GetPaymentTransaction(string to, long amount, long gasPrice = 0, long gasLimit = 0, string msg = "", long nonce = -1, long chainId = 210309)
        {

        }
    }
    
    //class Transaction : LegacyTransactionChainId
    //{
    //}
}
