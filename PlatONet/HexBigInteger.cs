using Newtonsoft.Json;
using System;
using System.Numerics;

namespace PlatONet
{
    /// <summary>
    /// 创建一个更易用的HexBigInteger类型，从而避免使用Nethereum.Hex.HexTypes.HexBiginteger
    /// </summary>
    [JsonConverter(typeof(HexBigIntegerJsonConverter))]
    public class HexBigInteger : Nethereum.Hex.HexTypes.HexRPCType<BigInteger>
    {
        public HexBigInteger(string hex) : 
            base(new Nethereum.Hex.HexConvertors.HexBigIntegerBigEndianConvertor(), hex)
        {
        }
        public HexBigInteger(BigInteger value) : 
            base(value, new Nethereum.Hex.HexConvertors.HexBigIntegerBigEndianConvertor())
        {
        }
        public HexBigInteger(long value) : this(value.ToString("X")) { }
        public HexBigInteger(ulong value) : this(value.ToString("X")) { }
        public HexBigInteger(Nethereum.Hex.HexTypes.HexBigInteger value) : this(value.Value) { }
        /// <summary>
        /// 与Nethereum的类型HexBigInteger的隐式转换
        /// </summary>
        /// <param name="value">转换后的值</param>
        public static implicit operator Nethereum.Hex.HexTypes.HexBigInteger(HexBigInteger value)
        {
            return new Nethereum.Hex.HexTypes.HexBigInteger(value.HexValue);
        }
        /// <summary>
        /// 与Nethereum的类型HexBigInteger的隐式转换
        /// </summary>
        /// <param name="value">转换后的值</param>
        public static implicit operator HexBigInteger(Nethereum.Hex.HexTypes.HexBigInteger value)
        {
            return new HexBigInteger(value.HexValue);
        }
        /// <summary>
        /// 与long的隐式转换
        /// </summary>
        /// <param name="value">转换后的值</param>
        public static implicit operator HexBigInteger(long value)
        {
            return new HexBigInteger(value);
        }
        public override string ToString()
        {
            return HexValue;
        }
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
    public class HexBigIntegerJsonConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var hexRPCType = (HexBigInteger)value;
            writer.WriteValue(hexRPCType.HexValue);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue,
            JsonSerializer serializer)
        {
            if (reader.Value == null) return null;

            if (reader.Value is string)
            {
                return new HexBigInteger((string)reader.Value);
            }else if(reader.Value is long)
            {
                return new HexBigInteger((long)reader.Value);
            }
            //fallback if we get rug numbers
            return reader.Value;
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(HexBigInteger);
        }
    }
}
