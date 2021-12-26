using Nethereum.Hex.HexConvertors.Extensions;
using Nethereum.Signer;
using System.Linq;
using System.Text;
using PlatONet.Crypto;
using Nethereum.HdWallet;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Crypto.Generators;
using Org.BouncyCastle.Crypto.Parameters;
using NBitcoin;

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
        /// 产生一个随机的账号
        /// </summary>
        public Account()
        {
            CreateAccountFromRandom(new SecureRandom());
        }
        /// <summary>
        /// 使用助记词生成一个账号
        /// </summary>
        /// <param name="mnenonic">助记词</param>
        /// <param name="index">索引，从0开始</param>
        /// <returns>账号</returns>
        public static Account FromMnemonic(string mnenonic, int index = 0)
        {
            var wallet = new Wallet(mnenonic, null, "m/44'/206'/0'/0/x");
            var account = wallet.GetAccount(index);
            return new Account(account.PrivateKey);
        }
        /// <summary>
        /// 生成一个随机的助记词
        /// </summary>
        /// <returns>助记词</returns>
        public static string GenerateMnemonic()
        {
            Mnemonic mnemo = new Mnemonic(Wordlist.English, WordCount.Twelve);
            return mnemo.ToString();
        }
        private void CreateAccountFromRandom(SecureRandom srandom)
        {
            Ed25519KeyPairGenerator keyPairGenerator = new Ed25519KeyPairGenerator();
            keyPairGenerator.Init(new Ed25519KeyGenerationParameters(srandom));
            var privateKeyPair = keyPairGenerator.GenerateKeyPair();
            var privateKey = privateKeyPair.Private as Ed25519PrivateKeyParameters;
            _key = new EthECKey(privateKey.GetEncoded(), true);
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
