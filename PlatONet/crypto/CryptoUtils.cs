using Org.BouncyCastle.Crypto.Digests;
using System.Text;
using System.Linq;

namespace PlatONet.Crypto
{
    internal class CryptoUtils
    {
        public static byte[] Keccak(byte[] msgBytes)
        {
            // keccak256 hash            
            var hashAlgorithm = new KeccakDigest(256);
            //byte[] input = last16BytesOfDerivedKey.ToArray();
            hashAlgorithm.BlockUpdate(msgBytes, 0, msgBytes.Length);

            byte[] data = new byte[32]; // 256 / 8 = 32
            hashAlgorithm.DoFinal(data, 0);
            return data;
        }
        public static byte[] HashPersonalMessage(byte[] message)
        {
            var prefix = Encoding.UTF8.GetBytes("\u0019Ethereum Signed Message:\n" + message.Length.ToString());
            return Keccak(prefix.Concat(message).ToArray());
        }
    }
}
