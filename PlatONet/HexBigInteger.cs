using System.Numerics;

namespace PlatONet
{
    /// <summary>
    /// 创建一个更易用的HexBigInteger类型，从而避免使用Nethereum.Hex.HexTypes.HexBiginteger
    /// </summary>
    public class HexBigInteger : Nethereum.Hex.HexTypes.HexBigInteger
    {        
        public HexBigInteger(string hex) : base(hex) { }
        public HexBigInteger(BigInteger value) : base(value) { }
        public HexBigInteger(long value) : base(value.ToString("X")) { }
        public HexBigInteger(ulong value) : base(value.ToString("X")) { }
        public HexBigInteger(Nethereum.Hex.HexTypes.HexBigInteger value) : base(value.Value) { }
    }
    public static class HexBigIntegerNumberExtensions
    {
        public static HexBigInteger ToHexBigInteger(this ulong val)
        {
            return new HexBigInteger(val);
        }

        public static HexBigInteger ToHexBigInteger(this long val)
        {
            return new HexBigInteger(val);
        }

        public static HexBigInteger ToHexBigInteger(this BigInteger val)
        {
            return new HexBigInteger(val);
        }
        public static HexBigInteger ToHexBigInteger(this Nethereum.Hex.HexTypes.HexBigInteger val)
        {
            return new HexBigInteger(val);
        }
    }
}
