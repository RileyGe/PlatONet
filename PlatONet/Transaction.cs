using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using Nethereum.Hex.HexConvertors.Extensions;
using Nethereum.RLP;
using Nethereum.Signer;


namespace PlatONet
{
    public class Transaction
    {
        private bool paramsChanged = true;
        private Address _to;
        public Address To { 
            get {
                return _to;
            }
            set {
                if (value == null) {
                    if (_to == null) return;
                    else if (!value.Equals(_to))
                    {
                        paramsChanged = true;
                        _to = value;
                    }
                }
            } 
        }
        private BigInteger _amount;
        public BigInteger Amount {
            get {
                return _amount;
            }
            set {
                if (value == null)
                {
                    if (_amount == null) return;
                    else if (!value.Equals(_amount))
                    {
                        paramsChanged = true;
                        _amount = value;
                    }
                }
            } 
        }
        private BigInteger _nonce;
        public BigInteger Nonce {
            get {
                return _nonce;
            } 
            set {
                if (_nonce == null) return;
                else if (!value.Equals(_nonce))
                {
                    paramsChanged = true;
                    _nonce = value;
                }
            } 
        }
        private BigInteger _gasPrice;
        public BigInteger GasPrice {
            get
            {
                return _gasPrice;
            }
            set
            {
                if (_gasPrice == null) return;
                else if (!value.Equals(_gasPrice))
                {
                    paramsChanged = true;
                    _gasPrice = value;
                }
            }
        }
        private BigInteger _gasLimit;
        public BigInteger GasLimit {
            get
            {
                return _gasLimit;
            }
            set
            {
                if (_gasLimit == null) return;
                else if (!value.Equals(_gasLimit))
                {
                    paramsChanged = true;
                    _gasLimit = value;
                }
            }
        }
        private string _data;
        public string Data {
            get
            {
                return _data;
            }
            set
            {
                if (_data == null) return;
                else if (!value.Equals(_data))
                {
                    paramsChanged = true;
                    _data = value;
                }
            }
        }
        private BigInteger _chainId;
        public BigInteger ChainId {
            get
            {
                return _chainId;
            }
            set
            {
                if (_chainId == null) return;
                else if (!value.Equals(_chainId))
                {
                    paramsChanged = true;
                    _chainId = value;
                }
            }
        }
        private LegacyTransactionChainId rawTranction;
        private byte[] _signedTransaction;
        public byte[] SignedTransaction
        {
            get 
            { 
                return _signedTransaction;
            }
            set
            {
                _signedTransaction = value;
            }
        }
        //public Transaction(byte[] receiveAddress = null, byte[] value = null, byte[] nonce = null, byte[] gasPrice = null, byte[] gasLimit = null, 
        //    byte[] data = null, byte[] chainId = null) : base(nonce, gasPrice, gasLimit, receiveAddress, value, data, chainId)
        //{
            
        //}
        public Transaction(string to, long amount = 0, long nonce = -1, long gasPrice = 0, long gasLimit = 0,
            string data = "", long chainId = 210309) : this(to, new BigInteger(amount), new BigInteger(nonce),
                new BigInteger(gasPrice), new BigInteger(gasLimit), data, new BigInteger(chainId))
        { }
        public Transaction(string to, BigInteger amount, BigInteger nonce, BigInteger gasPrice, BigInteger gasLimit, 
            string data, BigInteger chainId)
        {
            // check address format
            _to = new Address(to);
            _amount = amount;
            _nonce = nonce;
            _gasPrice = gasPrice;
            _gasLimit = gasLimit;
            // check format
            _data = data;
            _chainId = chainId;
        }
        public byte[] Sign(EthECKey key)
        {
            if (paramsChanged)
                rawTranction = new LegacyTransactionChainId(_to.ToString(), _amount, _nonce, _gasPrice, _gasLimit, _data, _chainId);
            rawTranction.Sign(key);
            _signedTransaction = rawTranction.GetRLPEncoded();
            return _signedTransaction;
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
        public Dictionary<string, string> ToDict()
        {
            Dictionary<string, string> data = new Dictionary<string, string>();

            if (To != null && To.Bytes != null && To.Bytes.Length > 0) data.Add("to", To.ToString());
            if (Nonce != null && Nonce >= 0) data.Add("nonce", "0x" + Nonce.ToBytesForRLPEncoding().ToHex());
            if (GasPrice != null && GasPrice > 0) data.Add("gasPrice", "0x" + GasPrice.ToBytesForRLPEncoding().ToHex());
            if (GasLimit != null && GasLimit > 0) data.Add("gas", "0x" + GasLimit.ToBytesForRLPEncoding().ToHex());
            if (Amount != null && Amount > 0) data.Add("value", "0x" + Amount.ToBytesForRLPEncoding().ToHex());
            if (Data != null && Data.Length > 0) {
                if (Data.StartsWith("0x")) data.Add("data", Data);
                else data.Add("data", "0x" + Data);
            }
            if (ChainId != null && ChainId > 0) data.Add("chainId", "0x" + ChainId.ToBytesForRLPEncoding().ToHex());
            return data;
        }
        //public static Transaction GetPaymentTransaction(string to, long amount, long gasPrice = 0, long gasLimit = 0, string msg = "", long nonce = -1, long chainId = 210309)
        //{

        //}
    }
}
