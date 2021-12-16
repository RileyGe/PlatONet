using Nethereum.Hex.HexConvertors.Extensions;
using Nethereum.Signer;
using System.Linq;
using System.Text;

namespace PlatONet
{
    /// <summary>
    /// 账号
    /// </summary>
    public class Account
    {
        internal EthECKey _key;
        /// <summary>
        /// 使用私钥初始化一个账号
        /// </summary>
        /// <param name="privateKeyHex">私钥</param>
        public Account(string privateKeyHex)
        {
            _key = new EthECKey(privateKeyHex.HexToByteArray(), true);            
        }
        /// <summary>
        /// 账号的公钥
        /// </summary>
        public byte[] PublicKey
        {
            get
            {
                return _key.GetPubKeyNoPrefix();
            }
        }
        /// <summary>
        /// 获取bench32地址
        /// </summary>
        /// <param name="hrp">地址前缀</param>
        /// <returns>bench32地址</returns>
        public Address GetAddress(string hrp = "lat") {
            //byte[] pkBytes = PublicKey;
            var result = CryptoUtils.Keccak(PublicKey);
            //var hashAlgorithm = new KeccakDigest(256);
            //hashAlgorithm.BlockUpdate(pkBytes, 0, pkBytes.Length);
            //byte[] result = new byte[32]; // 256 / 8 = 32
            //hashAlgorithm.DoFinal(result, 0);
            var addressBytes = result.Skip(result.Length - 20).ToArray();
            return new Address(addressBytes, hrp);
        }
        /// <summary>
        /// 用账号对消息进行签名
        /// </summary>
        /// <param name="msg">需要被签名的消息内容</param>
        /// <returns>签名</returns>
        public EthECDSASignature Sign(string msg)
        {            
            return Sign(Encoding.UTF8.GetBytes(msg));
        }
        /// <summary>
        /// 用账号对二进制内容进行签名
        /// </summary>
        /// <param name="bytes">需要被签名的二进制内容</param>
        /// <returns>签名</returns>
        public EthECDSASignature Sign(byte[] bytes)
        {
            return _key.Sign(CryptoUtils.HashPersonalMessage(bytes));
        }
        /// <summary>
        /// 用账号对<see cref="Transaction"/>进行签名
        /// </summary>
        /// <param name="trans">需要被签名的<see cref="Transaction"/>对象</param>
        /// <returns>签名</returns>
        public EthECDSASignature Sign(Transaction trans)
        {
            return trans.Sign(_key);
        }
    }
}
