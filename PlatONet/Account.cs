using Nethereum.Hex.HexConvertors.Extensions;
using Nethereum.Signer;
using Org.BouncyCastle.Crypto.Digests;
using System.Linq;

namespace PlatONet
{
    public class Account
    {
        internal EthECKey _key;
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

        public Address ToAddress(string hrp = "lat") {

            byte[] pkBytes = PublicKey;

            var hashAlgorithm = new KeccakDigest(256);
            hashAlgorithm.BlockUpdate(pkBytes, 0, pkBytes.Length);
            byte[] result = new byte[32]; // 256 / 8 = 32
            hashAlgorithm.DoFinal(result, 0);
            var addressBytes = result.Skip(result.Length - 20).ToArray();
            //Console.WriteLine(Hex.ToHexString(publicParams.Q.GetEncoded()));

            return new Address(addressBytes, hrp);
        }         
    }
}
