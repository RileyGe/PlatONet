using System.Collections.Generic;
using System.Numerics;
using Nethereum.Hex.HexConvertors.Extensions;
using Nethereum.Hex.HexTypes;
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
                if (_to == null)
                {
                    if (value == null) return;
                    else
                    {
                        paramsChanged = true;
                        _to = value;
                    }
                }
                else if (!value.Equals(_to))
                {
                    paramsChanged = true;
                    _to = value;
                }
            } 
        }
        private HexBigInteger _amount;
        public HexBigInteger Amount {
            get {
                return _amount;
            }
            set {
                if (_amount == null)
                {
                    if (value == null) return;
                    else
                    {
                        paramsChanged = true;
                        _amount = value;
                    }
                }
                else if (!value.Equals(_amount))
                {
                    paramsChanged = true;
                    _amount = value;
                }
            } 
        }
        private HexBigInteger _nonce;
        public HexBigInteger Nonce {
            get {
                return _nonce;
            } 
            set {
                if (_nonce == null)
                {
                    if (value == null) return;
                    else
                    {
                        paramsChanged = true;
                        _nonce = value;
                    }
                }
                else if (!value.Equals(_nonce))
                {
                    paramsChanged = true;
                    _nonce = value;
                }
            } 
        }
        private HexBigInteger _gasPrice;
        public HexBigInteger GasPrice {
            get
            {
                return _gasPrice;
            }
            set
            {
                if (_gasPrice == null)
                {
                    if (value == null) return;
                    else
                    {
                        paramsChanged = true;
                        _gasPrice = value;
                    }
                }
                else if (!value.Equals(_gasPrice))
                {
                    paramsChanged = true;
                    _gasPrice = value;
                }
            }
        }
        private HexBigInteger _gasLimit;
        public HexBigInteger GasLimit {
            get
            {
                return _gasLimit;
            }
            set
            {
                if (_gasLimit == null)
                {
                    if (value == null) return;
                    else
                    {
                        paramsChanged = true;
                        _gasLimit = value;
                    }
                }
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
                if (_data == null) {
                    if (value == null) return;
                    else
                    {
                        paramsChanged = true;
                        _data = value;
                    }
                }
                else if (!value.Equals(_data))
                {
                    paramsChanged = true;
                    _data = value;
                }
            }
        }
        private HexBigInteger _chainId;
        public HexBigInteger ChainId {
            get
            {
                return _chainId;
            }
            set
            {
                if (_chainId == null)
                {
                    if (value == null) return;
                    else
                    {
                        paramsChanged = true;
                        _chainId = value;
                    }
                }
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
        //public Transaction(string to, ulong amount = 0, ulong nonce = 0, ulong gasPrice = 0, ulong gasLimit = 0,
        //    string data = "", ulong chainId = 210309) : this(to, amount.ToHexBigInteger(), nonce.ToHexBigInteger(),
        //        gasPrice.ToHexBigInteger(), gasLimit.ToHexBigInteger(), data, chainId.ToHexBigInteger())
        //{ }
        public Transaction(string to = null, HexBigInteger amount = null, HexBigInteger nonce = null, HexBigInteger gasPrice = null, 
            HexBigInteger gasLimit = null, string data = null, HexBigInteger chainId = null)
        {
            // check address format
            if (to != null && to.Length > 0) _to = new Address(to);
            _amount = amount;
            _nonce = nonce;
            _gasPrice = gasPrice;
            _gasLimit = gasLimit;
            // check format
            _data = data;
            _chainId = chainId;
        }
        public EthECDSASignature Sign(EthECKey key)
        {
            if (paramsChanged)
               rawTranction = new LegacyTransactionChainId(_to?.Bytes?.ToHex(), _amount ?? new BigInteger(0), 
                   _nonce ?? new BigInteger(0), _gasPrice ?? new BigInteger(0), _gasLimit ?? new BigInteger(0),
                   _data, _chainId ?? new BigInteger(0));
            rawTranction.Sign(key);
            _signedTransaction = rawTranction.GetRLPEncoded();            
            return rawTranction.Signature;
        }
        public EthECDSASignature Sign(Account account)
        {
            return account.Sign(this);
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
            if (Nonce != null && Nonce.Value >= 0) data.Add("nonce", Nonce.HexValue);
            if (GasPrice != null && GasPrice.Value > 0) data.Add("gasPrice", GasPrice.HexValue);
            if (GasLimit != null && GasLimit.Value > 0) data.Add("gas", GasLimit.HexValue);
            if (Amount != null && Amount.Value > 0) data.Add("value", Amount.HexValue);
            if (Data != null && Data.Length > 0) {
                if (Data.StartsWith("0x")) data.Add("data", Data);
                else data.Add("data", "0x" + Data);
            }
            if (ChainId != null && ChainId.Value > 0) data.Add("chainId", ChainId.HexValue);
            return data;
        }
        //public static Transaction GetPaymentTransaction(string to, long amount, long gasPrice = 0, long gasLimit = 0, string msg = "", long nonce = -1, long chainId = 210309)
        //{

        //}
    }
}
