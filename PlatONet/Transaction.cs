using System.Collections.Generic;
using System.Numerics;
using Nethereum.Signer;


namespace PlatONet
{
    /// <summary>
    /// 交易信息
    /// </summary>
    public class Transaction
    {
        private bool paramsChanged = true;
        private Address _to;
        /// <summary>
        /// 交易发送到的地址
        /// </summary>
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
        /// <summary>
        /// 交易中包含的lat数量（单位VON）
        /// </summary>
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
        /// <summary>
        /// 交易的索引
        /// </summary>
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
        /// <summary>
        /// 交易的汽油价格
        /// </summary>
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
        /// <summary>
        /// 交易最多花费的汽油数量上限
        /// </summary>
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
        /// <summary>
        /// 交易包含的数据
        /// </summary>
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
        /// <summary>
        /// 交易的链ID
        /// </summary>
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
        /// <summary>
        /// 签名后的交易
        /// </summary>
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
        /// <summary>
        /// 初始化交易
        /// </summary>
        /// <param name="to">交易发送到的地址</param>
        /// <param name="amount">交易中包含的lat数量（单位VON）</param>
        /// <param name="nonce">交易的索引</param>
        /// <param name="gasPrice">交易的汽油价格</param>
        /// <param name="gasLimit">交易最多花费的汽油数量上限</param>
        /// <param name="data">交易包含的数据</param>
        /// <param name="chainId">交易的链ID</param>
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
        internal EthECDSASignature Sign(EthECKey key)
        {
            if (paramsChanged)
               rawTranction = new LegacyTransactionChainId(_to?.Bytes?.ToHex(), _amount ?? new BigInteger(0), 
                   _nonce ?? new BigInteger(0), _gasPrice ?? new BigInteger(0), _gasLimit ?? new BigInteger(0),
                   _data, _chainId ?? new BigInteger(0));
            rawTranction.Sign(key);
            _signedTransaction = rawTranction.GetRLPEncoded();            
            return rawTranction.Signature;
        }
        /// <summary>
        /// 对交易进行签名
        /// </summary>
        /// <param name="account">签名使用的<see cref="Account"/></param>
        /// <returns>签名后的交易信息</returns>
        public EthECDSASignature Sign(Account account)
        {
            return account.Sign(this);
        }
        internal Dictionary<string, string> ToDict()
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
    }
}
