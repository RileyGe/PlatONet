////using java.security.interfaces;
//using Org.BouncyCastle.Asn1;
//using Org.BouncyCastle.Asn1.Pkcs;
//using Org.BouncyCastle.Asn1.Sec;
//using Org.BouncyCastle.Asn1.X509;
//using Org.BouncyCastle.Asn1.X9;
//using Org.BouncyCastle.Crypto.Parameters;
//using Org.BouncyCastle.Math.EC;
//using System;
//using System.Collections.Generic;
//using System.Text;
//using Org.BouncyCastle.Math;
//using System.Linq;

//namespace PlatONet
//{
//    /// <summary>
//    /// Elliptic Curve SECP-256k1 generated key pair.
//    /// </summary>
//    public class ECKeyPair
//    {
//        private readonly BigInteger PrivateKey;
//        private readonly BigInteger PublicKey;

//        public ECKeyPair(BigInteger privateKey, BigInteger publicKey)
//        {
//            this.PrivateKey = privateKey;
//            this.PublicKey = publicKey;
//        }



//        //    /**
//        //     * Sign a hash with the private key of this key pair.
//        //     * @param transactionHash   the hash to sign
//        //     * @return  An {@link ECDSASignature} of the hash
//        //     */
//        //    public ECDSASignature sign(byte[] transactionHash)
//        //    {
//        //        ECDSASigner signer = new ECDSASigner(new HMacDSAKCalculator(new SHA256Digest()));

//        //        ECPrivateKeyParameters privKey = new ECPrivateKeyParameters(privateKey, Sign.CURVE);
//        //        signer.init(true, privKey);
//        //        BigInteger[] components = signer.generateSignature(transactionHash);

//        //        return new ECDSASignature(components[0], components[1]).toCanonicalised();
//        //    }

//        public static ECKeyPair Create(KeyPair keyPair)
//        {
//            BCECPrivateKey privateKey = (BCECPrivateKey)keyPair.getPrivate();
//            BCECPublicKey publicKey = (BCECPublicKey)keyPair.getPublic();
//            BigInteger privateKeyValue = privateKey.D;

//            // Ethereum does not use encoded public keys like bitcoin - see
//            // https://en.bitcoin.it/wiki/Elliptic_Curve_Digital_Signature_Algorithm for details
//            // Additionally, as the first bit is a constant prefix (0x04) we ignore this value
//            byte[] publicKeyBytes = publicKey.Q.GetEncoded(false);
//            BigInteger publicKeyValue = new BigInteger(1, publicKeyBytes.Skip(1).ToArray());
//            return new ECKeyPair(privateKeyValue, publicKeyValue);
//        }

//        //public static ECKeyPair create(BigInteger privateKey)
//        //{
//        //    return new ECKeyPair(privateKey, Sign.publicKeyFromPrivate(privateKey));
//        //}

//        //    public static ECKeyPair create(byte[] privateKey)
//        //    {
//        //        return create(Numeric.toBigInt(privateKey));
//        //    }

//        //    @Override
//        //public boolean equals(Object o)
//        //    {
//        //        if (this == o)
//        //        {
//        //            return true;
//        //        }
//        //        if (o == null || getClass() != o.getClass())
//        //        {
//        //            return false;
//        //        }

//        //        ECKeyPair ecKeyPair = (ECKeyPair)o;

//        //        if (privateKey != null
//        //                ? !privateKey.equals(ecKeyPair.privateKey) : ecKeyPair.privateKey != null)
//        //        {
//        //            return false;
//        //        }

//        //        return publicKey != null
//        //                ? publicKey.equals(ecKeyPair.publicKey) : ecKeyPair.publicKey == null;
//        //    }

//        //    @Override
//        //public int hashCode()
//        //    {
//        //        int result = privateKey != null ? privateKey.hashCode() : 0;
//        //        result = 31 * result + (publicKey != null ? publicKey.hashCode() : 0);
//        //        return result;
//        //    }
//    }
//    [Serializable]
//    public class BCECPrivateKey : IKey
//    {
//        private boolean withCompression;
//        [NonSerialized]
//        private BigInteger d;
//        [NonSerialized]
//        private ECPrivateKeyParameters privateKey;
//        [NonSerialized]        
//        private X9ECParameters ecSpec;
//        //private transient ProviderConfiguration   configuration;
//        [NonSerialized]
//        private DerBitString publicKey;

//        //private transient PKCS12BagAttributeCarrierImpl attrCarrier = new PKCS12BagAttributeCarrierImpl();

//        //static final long serialVersionUID = 994553197664784084L;
//        private string algorithm = "EC";
//        public string Algorithm { 
//            get {
//                return algorithm;
                
//            } 
//        }
//        /// <summary>
//        /// return the encoding format we produce in Encoded.
//        /// return the string "PKCS#8"
//        /// </summary>
//        public string Format
//        {
//            get
//            {
//                return "PKCS#8";
//            }
//        }
//        /// <summary>
//        /// Return a PKCS8 representation of the key. The sequence returned
//        /// represents a full PrivateKeyInfo object.
//        /// return a PKCS8 representation of the key.
//        /// </summary>
//        public byte[] Encoded
//        {
//            get
//            {
//                X962Parameters param;
//                int orderBitLength;

//                if (ecSpec is ECNamedCurveSpec)
//                {
                    
//                    DerObjectIdentifier curveOid = new DerObjectIdentifier(((ECNamedCurveSpec)ecSpec).getName());
//                    //DerObjectIdentifier
//                    //Org.BouncyCastle.Asn1.DerObjectIdentifier 
//                    //    if (curveOid == null)  // guess it's the OID
//                    //    {
//                    //        curveOid = new ASN1ObjectIdentifier(((ECNamedCurveSpec)ecSpec).getName());
//                    //    }

//                    param = new X962Parameters(curveOid);
                   
//                    //    orderBitLength = ECUtil.getOrderBitLength(ecSpec.getOrder(), this.getS());
//                }
//                else if (ecSpec == null)
//                {
//                    //        param = new X962Parameters(DERNull.INSTANCE);
//                    //    orderBitLength = ECUtil.getOrderBitLength(null, this.getS());
//                }
//                else
//                {
//                    //    ECCurve curve = EC5Util.convertCurve(ecSpec.getCurve());

//                    //    X9ECParameters ecP = new X9ECParameters(
//                    //        curve,
//                    //        EC5Util.convertPoint(curve, ecSpec.getGenerator(), withCompression),
//                    //        ecSpec.getOrder(),
//                    //        BigInteger.valueOf(ecSpec.getCofactor()),
//                    //        ecSpec.getCurve().getSeed());

//                    //        param = new X962Parameters(ecP);
//                    //    orderBitLength = ECUtil.getOrderBitLength(ecSpec.getOrder(), this.getS());
//                }

//                PrivateKeyInfo info;
//                ECPrivateKeyStructure keyStructure;

//                if (publicKey != null)
//                {
                  
//                    keyStructure = new ECPrivateKeyStructure(orderBitLength, 
//                        new Org.BouncyCastle.Math.BigInteger(S.ToByteArray()), publicKey, param);
//                }
//                else
//                {
//                    keyStructure = new ECPrivateKeyStructure(orderBitLength,
//                        new Org.BouncyCastle.Math.BigInteger(S.ToByteArray()), param);
//                }

//                try
//                {
//                    info = new PrivateKeyInfo(new AlgorithmIdentifier(X9ObjectIdentifiers.IdECPublicKey, param), keyStructure);

//                    return info.GetDerEncoded();
//                    //return info.GetEncoded()
//                }
//                catch (Exception e)
//                {
//                    return null;
//                }
//            }
//        }











//        //    protected BCECPrivateKey()
//        //    {
//        //    }

//        //public BCECPrivateKey(            ECPrivateKey key,
//        //    ProviderConfiguration configuration)
//        //{
            
//        //    this.d = key.getS();
//        //    this.algorithm = key.getAlgorithm();
//        //    this.ecSpec = key.getParams();
//        //    this.configuration = configuration;
//        //}

//        //    public BCECPrivateKey(
//        //        String algorithm,
//        //        org.bouncycastle.jce.spec.ECPrivateKeySpec spec,
//        //        ProviderConfiguration configuration)
//        //    {
//        //        this.algorithm = algorithm;
//        //        this.d = spec.getD();

//        //        if (spec.getParams() != null) // can be null if implicitlyCA
//        //        {
//        //            ECCurve curve = spec.getParams().getCurve();
//        //            EllipticCurve ellipticCurve;

//        //            ellipticCurve = EC5Util.convertCurve(curve, spec.getParams().getSeed());

//        //            this.ecSpec = EC5Util.convertSpec(ellipticCurve, spec.getParams());
//        //        }
//        //        else
//        //        {
//        //            this.ecSpec = null;
//        //        }

//        //        this.configuration = configuration;
//        //    }


//        //    public BCECPrivateKey(
//        //        String algorithm,
//        //        ECPrivateKeySpec spec,
//        //        ProviderConfiguration configuration)
//        //    {
//        //        this.algorithm = algorithm;
//        //        this.d = spec.getS();
//        //        this.ecSpec = spec.getParams();
//        //        this.configuration = configuration;
//        //    }

//        //    public BCECPrivateKey(
//        //        String algorithm,
//        //        BCECPrivateKey key)
//        //    {
//        //        this.algorithm = algorithm;
//        //        this.d = key.d;
//        //        this.ecSpec = key.ecSpec;
//        //        this.withCompression = key.withCompression;
//        //        this.attrCarrier = key.attrCarrier;
//        //        this.publicKey = key.publicKey;
//        //        this.configuration = key.configuration;
//        //    }

//        //    public BCECPrivateKey(
//        //        String algorithm,
//        //        ECPrivateKeyParameters params,
//        //        BCECPublicKey pubKey,
//        //        ECParameterSpec spec,
//        //        ProviderConfiguration configuration)
//        //    {
//        //        ECDomainParameters dp = params.getParameters();

//        //        this.algorithm = algorithm;
//        //        this.d = params.getD();
//        //        this.configuration = configuration;

//        //        if (spec == null)
//        //        {
//        //            EllipticCurve ellipticCurve = EC5Util.convertCurve(dp.getCurve(), dp.getSeed());

//        //            this.ecSpec = new ECParameterSpec(
//        //                            ellipticCurve,
//        //                            new ECPoint(
//        //                                    dp.getG().getAffineXCoord().toBigInteger(),
//        //                                    dp.getG().getAffineYCoord().toBigInteger()),
//        //                            dp.getN(),
//        //                            dp.getH().intValue());
//        //        }
//        //        else
//        //        {
//        //            this.ecSpec = spec;
//        //        }

//        //        publicKey = getPublicKeyDetails(pubKey);
//        //    }

//        //    public BCECPrivateKey(
//        //        String algorithm,
//        //        ECPrivateKeyParameters params,
//        //        BCECPublicKey pubKey,
//        //        org.bouncycastle.jce.spec.ECParameterSpec spec,
//        //        ProviderConfiguration configuration)
//        //    {
//        //        ECDomainParameters dp = params.getParameters();

//        //        this.algorithm = algorithm;
//        //        this.d = params.getD();
//        //        this.configuration = configuration;

//        //        if (spec == null)
//        //        {
//        //            EllipticCurve ellipticCurve = EC5Util.convertCurve(dp.getCurve(), dp.getSeed());

//        //            this.ecSpec = new ECParameterSpec(
//        //                            ellipticCurve,
//        //                            new ECPoint(
//        //                                    dp.getG().getAffineXCoord().toBigInteger(),
//        //                                    dp.getG().getAffineYCoord().toBigInteger()),
//        //                            dp.getN(),
//        //                            dp.getH().intValue());
//        //        }
//        //        else
//        //        {
//        //            EllipticCurve ellipticCurve = EC5Util.convertCurve(spec.getCurve(), spec.getSeed());

//        //            this.ecSpec = EC5Util.convertSpec(ellipticCurve, spec);
//        //        }

//        //        publicKey = getPublicKeyDetails(pubKey);
//        //    }

//        //    public BCECPrivateKey(
//        //        String algorithm,
//        //        ECPrivateKeyParameters params,
//        //        ProviderConfiguration configuration)
//        //    {
//        //        this.algorithm = algorithm;
//        //        this.d = params.getD();
//        //        this.ecSpec = null;
//        //        this.configuration = configuration;
//        //    }

//        //    BCECPrivateKey(
//        //        String algorithm,
//        //        PrivateKeyInfo info,
//        //        ProviderConfiguration configuration)
//        //    throws IOException
//        //    {
//        //    this.algorithm = algorithm;
//        //    this.configuration = configuration;
//        //        populateFromPrivKeyInfo(info);
//        //}

//        //private void populateFromPrivKeyInfo(PrivateKeyInfo info)
//        //    throws IOException
//        //{
//        //    X962Parameters params = X962Parameters.getInstance(info.getPrivateKeyAlgorithm().getParameters());

//        //    ECCurve curve = EC5Util.getCurve(configuration, params);
//        //    ecSpec = EC5Util.convertToSpec(params, curve);

//        //    ASN1Encodable privKey = info.parsePrivateKey();
//        //    if (privKey instanceof ASN1Integer)
//        //    {
//        //        ASN1Integer derD = ASN1Integer.getInstance(privKey);

//        //        this.d = derD.getValue();
//        //    }
//        //    else
//        //    {
//        //        org.bouncycastle.asn1.sec.ECPrivateKey ec = org.bouncycastle.asn1.sec.ECPrivateKey.getInstance(privKey);

//        //        this.d = ec.getKey();
//        //        this.publicKey = ec.getPublicKey();
//        //    }
//        //}

//        //public String getAlgorithm()
//        //{
//        //    return algorithm;
//        //}





//        //public ECParameterSpec getParams()
//        //{
//        //    return ecSpec;
//        //}

//        //public org.bouncycastle.jce.spec.ECParameterSpec getParameters()
//        //{
//        //    if (ecSpec == null)
//        //    {
//        //        return null;
//        //    }

//        //    return EC5Util.convertSpec(ecSpec, withCompression);
//        //}

//        //org.bouncycastle.jce.spec.ECParameterSpec engineGetSpec()
//        //{
//        //    if (ecSpec != null)
//        //    {
//        //        return EC5Util.convertSpec(ecSpec, withCompression);
//        //    }

//        //    return configuration.getEcImplicitlyCa();
//        //}

//        public BigInteger S
//        {
//            get
//            {
//                return d;
//            }

//        }

//        public BigInteger D
//        {
//            get
//            {
//                return d;
//            }
//        }

//        //public void setBagAttribute(
//        //    ASN1ObjectIdentifier oid,
//        //    ASN1Encodable attribute)
//        //{
//        //    attrCarrier.setBagAttribute(oid, attribute);
//        //}

//        //public ASN1Encodable getBagAttribute(
//        //    ASN1ObjectIdentifier oid)
//        //{
//        //    return attrCarrier.getBagAttribute(oid);
//        //}

//        //public Enumeration getBagAttributeKeys()
//        //{
//        //    return attrCarrier.getBagAttributeKeys();
//        //}

//        //public void setPointFormat(String style)
//        //{
//        //    withCompression = !("UNCOMPRESSED".equalsIgnoreCase(style));
//        //}

//        //public boolean equals(Object o)
//        //{
//        //    if (!(o instanceof BCECPrivateKey))
//        //    {
//        //        return false;
//        //    }

//        //    BCECPrivateKey other = (BCECPrivateKey)o;

//        //    return getD().equals(other.getD()) && (engineGetSpec().equals(other.engineGetSpec()));
//        //}

//        //public int hashCode()
//        //{
//        //    return getD().hashCode() ^ engineGetSpec().hashCode();
//        //}

//        //public String toString()
//        //{
//        //    StringBuffer buf = new StringBuffer();
//        //    String nl = Strings.lineSeparator();

//        //    buf.append("EC Private Key").append(nl);
//        //    buf.append("             S: ").append(this.d.toString(16)).append(nl);

//        //    return buf.toString();

//        //}

//        //private DERBitString getPublicKeyDetails(BCECPublicKey pub)
//        //{
//        //    try
//        //    {
//        //        SubjectPublicKeyInfo info = SubjectPublicKeyInfo.getInstance(ASN1Primitive.fromByteArray(pub.getEncoded()));

//        //        return info.getPublicKeyData();
//        //    }
//        //    catch (IOException e)
//        //    {   // should never happen
//        //        return null;
//        //    }
//        //}

//        //private void readObject(
//        //    ObjectInputStream in)
//        //    throws IOException, ClassNotFoundException
//        //{
//        //    in.defaultReadObject();

//        //byte[] enc = (byte[])in.readObject();

//        //populateFromPrivKeyInfo(PrivateKeyInfo.getInstance(ASN1Primitive.fromByteArray(enc)));

//        //    this.configuration = BouncyCastleProvider.CONFIGURATION;
//        //    this.attrCarrier = new PKCS12BagAttributeCarrierImpl();
//        //}

//        //private void writeObject(
//        //    ObjectOutputStream out)
//        //    throws IOException
//        //{
//        //    out.defaultWriteObject();

//        //    out.writeObject(this.getEncoded());
//        //}





//    }

//    public class BCECPublicKey : IKey
//    {
//        public string Algorithm => throw new NotImplementedException();

//        public string Format => throw new NotImplementedException();

//        public byte[] Encoded => throw new NotImplementedException();



//        //    static final long serialVersionUID = 2422789860422731812L;

//        //    private String algorithm = "EC";
//        //    private boolean withCompression;
//        [NonSerialized]
//        private ECPoint q;
//        [NonSerialized]
//        private ECParameterSpec ecSpec;
//        //private transient ProviderConfiguration   configuration;

//        //public BCECPublicKey(
//        //    String algorithm,
//        //    BCECPublicKey key)
//        //    {
//        //        this.algorithm = algorithm;
//        //        this.q = key.q;
//        //        this.ecSpec = key.ecSpec;
//        //        this.withCompression = key.withCompression;
//        //        this.configuration = key.configuration;
//        //    }

//        //    public BCECPublicKey(
//        //        String algorithm,
//        //        ECPublicKeySpec spec,
//        //        ProviderConfiguration configuration)
//        //    {
//        //        this.algorithm = algorithm;
//        //        this.ecSpec = spec.getParams();
//        //        this.q = EC5Util.convertPoint(ecSpec, spec.getW(), false);
//        //        this.configuration = configuration;
//        //    }

//        //    public BCECPublicKey(
//        //        String algorithm,
//        //        org.bouncycastle.jce.spec.ECPublicKeySpec spec,
//        //        ProviderConfiguration configuration)
//        //    {
//        //        this.algorithm = algorithm;
//        //        this.q = spec.getQ();

//        //        if (spec.getParams() != null) // can be null if implictlyCa
//        //        {
//        //            ECCurve curve = spec.getParams().getCurve();
//        //            EllipticCurve ellipticCurve = EC5Util.convertCurve(curve, spec.getParams().getSeed());

//        //            // this may seem a little long-winded but it's how we pick up the custom curve.
//        //            this.q = EC5Util.convertCurve(ellipticCurve).createPoint(spec.getQ().getAffineXCoord().toBigInteger(), spec.getQ().getAffineYCoord().toBigInteger());
//        //            this.ecSpec = EC5Util.convertSpec(ellipticCurve, spec.getParams());
//        //        }
//        //        else
//        //        {
//        //            if (q.getCurve() == null)
//        //            {
//        //                org.bouncycastle.jce.spec.ECParameterSpec s = configuration.getEcImplicitlyCa();

//        //                q = s.getCurve().createPoint(q.getXCoord().toBigInteger(), q.getYCoord().toBigInteger(), false);
//        //            }
//        //            this.ecSpec = null;
//        //        }

//        //        this.configuration = configuration;
//        //    }

//        //    public BCECPublicKey(
//        //        String algorithm,
//        //        ECPublicKeyParameters params,
//        //        ECParameterSpec spec,
//        //        ProviderConfiguration configuration)
//        //    {
//        //        ECDomainParameters dp = params.getParameters();

//        //        this.algorithm = algorithm;
//        //        this.q = params.getQ();

//        //        if (spec == null)
//        //        {
//        //            EllipticCurve ellipticCurve = EC5Util.convertCurve(dp.getCurve(), dp.getSeed());

//        //            this.ecSpec = createSpec(ellipticCurve, dp);
//        //        }
//        //        else
//        //        {
//        //            this.ecSpec = spec;
//        //        }

//        //        this.configuration = configuration;
//        //    }

//        //    public BCECPublicKey(
//        //        String algorithm,
//        //        ECPublicKeyParameters params,
//        //        org.bouncycastle.jce.spec.ECParameterSpec spec,
//        //        ProviderConfiguration configuration)
//        //    {
//        //        ECDomainParameters dp = params.getParameters();

//        //        this.algorithm = algorithm;

//        //        if (spec == null)
//        //        {
//        //            EllipticCurve ellipticCurve = EC5Util.convertCurve(dp.getCurve(), dp.getSeed());

//        //            this.ecSpec = createSpec(ellipticCurve, dp);
//        //        }
//        //        else
//        //        {
//        //            EllipticCurve ellipticCurve = EC5Util.convertCurve(spec.getCurve(), spec.getSeed());

//        //            this.ecSpec = EC5Util.convertSpec(ellipticCurve, spec);
//        //        }

//        //        this.q = EC5Util.convertCurve(ecSpec.getCurve()).createPoint(params.getQ().getAffineXCoord().toBigInteger(), params.getQ().getAffineYCoord().toBigInteger());

//        //        this.configuration = configuration;
//        //    }

//        //    /*
//        //     * called for implicitCA
//        //     */
//        //    public BCECPublicKey(
//        //        String algorithm,
//        //        ECPublicKeyParameters params,
//        //        ProviderConfiguration configuration)
//        //    {
//        //        this.algorithm = algorithm;
//        //        this.q = params.getQ();
//        //        this.ecSpec = null;
//        //        this.configuration = configuration;
//        //    }

//        //    public BCECPublicKey(
//        //        ECPublicKey key,
//        //        ProviderConfiguration configuration)
//        //    {
//        //        this.algorithm = key.getAlgorithm();
//        //        this.ecSpec = key.getParams();
//        //        this.q = EC5Util.convertPoint(this.ecSpec, key.getW(), false);
//        //    }

//        //    BCECPublicKey(
//        //        String algorithm,
//        //        SubjectPublicKeyInfo info,
//        //        ProviderConfiguration configuration)
//        //    {
//        //        this.algorithm = algorithm;
//        //        this.configuration = configuration;
//        //        populateFromPubKeyInfo(info);
//        //    }

//        //    private ECParameterSpec createSpec(EllipticCurve ellipticCurve, ECDomainParameters dp)
//        //    {
//        //        return new ECParameterSpec(
//        //                ellipticCurve,
//        //                new ECPoint(
//        //                        dp.getG().getAffineXCoord().toBigInteger(),
//        //                        dp.getG().getAffineYCoord().toBigInteger()),
//        //                        dp.getN(),
//        //                        dp.getH().intValue());
//        //    }

//        //    private void populateFromPubKeyInfo(SubjectPublicKeyInfo info)
//        //    {
//        //        X962Parameters params = new X962Parameters((ASN1Primitive)info.getAlgorithm().getParameters());
//        //        ECCurve curve = EC5Util.getCurve(configuration, params);
//        //        ecSpec = EC5Util.convertToSpec(params, curve);

//        //        DERBitString bits = info.getPublicKeyData();
//        //        byte[] data = bits.getBytes();
//        //        ASN1OctetString key = new DEROctetString(data);

//        //        //
//        //        // extra octet string - one of our old certs...
//        //        //
//        //        if (data[0] == 0x04 && data[1] == data.length - 2
//        //            && (data[2] == 0x02 || data[2] == 0x03))
//        //        {
//        //            int qLength = new X9IntegerConverter().getByteLength(curve);

//        //            if (qLength >= data.length - 3)
//        //            {
//        //                try
//        //                {
//        //                    key = (ASN1OctetString)ASN1Primitive.fromByteArray(data);
//        //                }
//        //                catch (IOException ex)
//        //                {
//        //                    throw new IllegalArgumentException("error recovering public key");
//        //                }
//        //            }
//        //        }

//        //        X9ECPoint derQ = new X9ECPoint(curve, key);

//        //        this.q = derQ.getPoint();
//        //    }

//        //    public String getAlgorithm()
//        //    {
//        //        return algorithm;
//        //    }

//        //    public String getFormat()
//        //    {
//        //        return "X.509";
//        //    }

//        //    public byte[] getEncoded()
//        //    {
//        //        ASN1Encodable        params;
//        //        SubjectPublicKeyInfo info;

//        //        if (ecSpec instanceof ECNamedCurveSpec)
//        //    {
//        //            ASN1ObjectIdentifier curveOid = ECUtil.getNamedCurveOid(((ECNamedCurveSpec)ecSpec).getName());
//        //            if (curveOid == null)
//        //            {
//        //                curveOid = new ASN1ObjectIdentifier(((ECNamedCurveSpec)ecSpec).getName());
//        //            }
//        //        params = new X962Parameters(curveOid);
//        //        }
//        //    else if (ecSpec == null)
//        //        {
//        //        params = new X962Parameters(DERNull.INSTANCE);
//        //        }
//        //        else
//        //        {
//        //            ECCurve curve = EC5Util.convertCurve(ecSpec.getCurve());

//        //            X9ECParameters ecP = new X9ECParameters(
//        //                curve,
//        //                EC5Util.convertPoint(curve, ecSpec.getGenerator(), withCompression),
//        //                ecSpec.getOrder(),
//        //                BigInteger.valueOf(ecSpec.getCofactor()),
//        //                ecSpec.getCurve().getSeed());

//        //        params = new X962Parameters(ecP);
//        //        }

//        //        ECCurve curve = this.engineGetQ().getCurve();
//        //        ASN1OctetString p;

//        //        // stored curve is null if ImplicitlyCa
//        //        if (ecSpec == null)
//        //        {
//        //            p = (ASN1OctetString)
//        //                new X9ECPoint(curve.createPoint(this.getQ().getXCoord().toBigInteger(), this.getQ().getYCoord().toBigInteger(), withCompression)).toASN1Primitive();
//        //        }
//        //        else
//        //        {
//        //            p = (ASN1OctetString)
//        //                            new X9ECPoint(curve.createPoint(this.getQ().getAffineXCoord().toBigInteger(), this.getQ().getAffineYCoord().toBigInteger(), withCompression)).toASN1Primitive();
//        //        }

//        //        info = new SubjectPublicKeyInfo(new AlgorithmIdentifier(X9ObjectIdentifiers.id_ecPublicKey, params), p.getOctets());

//        //        return KeyUtil.getEncodedSubjectPublicKeyInfo(info);
//        //    }

//        //    private void extractBytes(byte[] encKey, int offSet, BigInteger bI)
//        //    {
//        //        byte[] val = bI.toByteArray();
//        //        if (val.length < 32)
//        //        {
//        //            byte[] tmp = new byte[32];
//        //            System.arraycopy(val, 0, tmp, tmp.length - val.length, val.length);
//        //            val = tmp;
//        //        }

//        //        for (int i = 0; i != 32; i++)
//        //        {
//        //            encKey[offSet + i] = val[val.length - 1 - i];
//        //        }
//        //    }

//        //    public ECParameterSpec getParams()
//        //    {
//        //        return ecSpec;
//        //    }

//        //    public org.bouncycastle.jce.spec.ECParameterSpec getParameters()
//        //    {
//        //        if (ecSpec == null)     // implictlyCA
//        //        {
//        //            return null;
//        //        }

//        //        return EC5Util.convertSpec(ecSpec, withCompression);
//        //    }

//        //    public ECPoint getW()
//        //    {
//        //        return new ECPoint(q.getAffineXCoord().toBigInteger(), q.getAffineYCoord().toBigInteger());
//        //    }

//        public ECPoint Q
//        {
//            get
//            {
//                if (ecSpec == null)
//                {
//                    return q.GetDetachedPoint();
//                }
//                return q;
//            }
//        }

//        //    public org.bouncycastle.math.ec.ECPoint engineGetQ()
//        //    {
//        //        return q;
//        //    }

//        //    org.bouncycastle.jce.spec.ECParameterSpec engineGetSpec()
//        //    {
//        //        if (ecSpec != null)
//        //        {
//        //            return EC5Util.convertSpec(ecSpec, withCompression);
//        //        }

//        //        return configuration.getEcImplicitlyCa();
//        //    }

//        //    public String toString()
//        //    {
//        //        StringBuffer buf = new StringBuffer();
//        //        String nl = Strings.lineSeparator();

//        //        buf.append("EC Public Key").append(nl);
//        //        buf.append("            X: ").append(this.q.getAffineXCoord().toBigInteger().toString(16)).append(nl);
//        //        buf.append("            Y: ").append(this.q.getAffineYCoord().toBigInteger().toString(16)).append(nl);

//        //        return buf.toString();

//        //    }

//        //    public void setPointFormat(String style)
//        //    {
//        //        withCompression = !("UNCOMPRESSED".equalsIgnoreCase(style));
//        //    }

//        //    public boolean equals(Object o)
//        //    {
//        //        if (!(o instanceof BCECPublicKey))
//        //    {
//        //            return false;
//        //        }

//        //        BCECPublicKey other = (BCECPublicKey)o;

//        //        return engineGetQ().equals(other.engineGetQ()) && (engineGetSpec().equals(other.engineGetSpec()));
//        //    }

//        //    public int hashCode()
//        //    {
//        //        return engineGetQ().hashCode() ^ engineGetSpec().hashCode();
//        //    }

//        //    private void readObject(
//        //        ObjectInputStream in)
//        //    throws IOException, ClassNotFoundException
//        //{
//        //    in.defaultReadObject();

//        //    byte[] enc = (byte[])in.readObject();

//        //    populateFromPubKeyInfo(SubjectPublicKeyInfo.getInstance(ASN1Primitive.fromByteArray(enc)));

//        //    this.configuration = BouncyCastleProvider.CONFIGURATION;
//        //}

//        //private void writeObject(
//        //    ObjectOutputStream out)
//        //    throws IOException
//        //{
//        //    out.defaultWriteObject();

//        //    out.writeObject(this.getEncoded());
//        //}




//    }
//}
