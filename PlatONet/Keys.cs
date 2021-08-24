using System;
using System.Collections.Generic;
using System.Text;

namespace PlatONet
{
    public class KeyPair
    {
        private IKey privateKey;
        private IKey publicKey;

        /**
         * Constructs a key pair from the given public key and private key.
         *
         * <p>Note that this constructor only stores references to the public
         * and private key components in the generated key pair. This is safe,
         * because {@code Key} objects are immutable.
         *
         * @param publicKey the public key.
         *
         * @param privateKey the private key.
         */
        public KeyPair(IKey publicKey, IKey privateKey)
        {
            this.publicKey = publicKey;
            this.privateKey = privateKey;
        }

        /**
         * Returns a reference to the public key component of this key pair.
         *
         * @return a reference to the public key.
         */
        public IKey getPublic()
        {
            return publicKey;
        }

        /**
        * Returns a reference to the private key component of this key pair.
        *
        * @return a reference to the private key.
        */
        public IKey getPrivate()
        {
            return privateKey;
        }
    }
    public interface IKey
    {
        /// <summary>
        /// the standard algorithm name for this key.
        /// For example, "DSA" would indicate that this key is a DSA key.
        /// See Appendix A in the
        /// <a href="../../../technotes/guides/security/crypto/CryptoSpec.html#AppA">Java Cryptography Architecture API Specification &amp; Reference</a>
        /// for information about standard algorithm names.
        /// </summary>
        string Algorithm { get; }

        /// <summary>
        /// the primary encoding format of the key.
        /// Returns the name of the primary encoding format of this key,
        /// or null if this key does not support encoding.
        /// The primary encoding format is
        /// named in terms of the appropriate ASN.1 data format, if an
        /// ASN.1 specification for this key exists.
        /// For example, the name of the ASN.1 data format for public
        /// keys is <I>SubjectPublicKeyInfo</I>, as
        /// defined by the X.509 standard; in this case, the returned format is
        /// {@code "X.509"}. Similarly,
        /// the name of the ASN.1 data format for private keys is <I>PrivateKeyInfo</I>,
        /// as defined by the PKCS #8 standard; in this case, the returned format is {@code "PKCS#8"}.
        /// </summary>
         string Format { get; }

        /// <summary>
        /// Returns the key in its primary encoding format, or null
        /// if this key does not support encoding.
        /// </summary>
        byte[] Encoded { get; }
    }
}
