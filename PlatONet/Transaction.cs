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
        //public List<object> ToParamsList()
        //{
        //    return new List<object>()
        //    {
        //        Nonce.ToHex(),
        //        GasPrice.ToHex(),
        //        GasLimit.ToHex(),
        //        ReceiveAddress.ToHex(),
        //        Value.ToHex(),
        //        ToHex(Data),
        //        ChainId.ToHex(),
        //        RHash.ToHex(),
        //        SHash.ToHex()
        //    };
        //}
        public Dictionary<string, string> ToDict(string hrp = "lat")
        {
            Dictionary<string, string> data = new Dictionary<string, string>();
            if (ReceiveAddress.Length > 0) {
                var toAddress = new Address(ReceiveAddress, hrp);
                data.Add("to", toAddress.ToString());
            } 
            if (Nonce.Length > 0) data.Add("nonce", "0x" + Nonce.ToHex());
            if (GasPrice.Length > 0) data.Add("gasPrice", "0x" + GasPrice.ToHex());
            if (GasLimit.Length > 0) data.Add("gas", "0x" + GasLimit.ToHex());
            if (Value.Length > 0) data.Add("value", "0x" + Value.ToHex());
            if (Data.Length > 0) data.Add("data", "0x" + ToHex(Data));
            if (ChainId.Length > 0) data.Add("chainId", "0x" + ChainId.ToHex());
            return data;
        }
        //public static Transaction GetPaymentTransaction(string to, long amount, long gasPrice = 0, long gasLimit = 0, string msg = "", long nonce = -1, long chainId = 210309)
        //{

        //}
    }
    
    //class Transaction : LegacyTransactionChainId
    //{
    //}
}
