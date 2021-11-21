using Nethereum.Hex.HexConvertors.Extensions;
using Nethereum.Signer;
using System.Linq;
using System.Text;

namespace PlatONet
{
    public class Account
    {
        internal EthECKey _key;
        //private string 
        public Account(string privateKeyHex)
        {
            _key = new EthECKey(privateKeyHex.HexToByteArray(), true);            
        }
        public byte[] PublicKey
        {
            get
            {
                return _key.GetPubKeyNoPrefix();
            }
        }
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
        public EthECDSASignature Sign(string msg)
        {            
            return Sign(Encoding.UTF8.GetBytes(msg));
        }
        public EthECDSASignature Sign(byte[] bytes)
        {
            return _key.Sign(CryptoUtils.HashPersonalMessage(bytes));
        }
        public EthECDSASignature Sign(Transaction trans)
        {
            return trans.Sign(_key);
        }
    }
}
