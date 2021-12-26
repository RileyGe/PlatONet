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
        /// <summary>
        /// 使用16进制字符串初始化一个<see cref="HexBigInteger"/>对象
        /// </summary>
        /// <param name="hex">16进制字符串</param>
        public HexBigInteger(string hex) : 
            base(new Nethereum.Hex.HexConvertors.HexBigIntegerBigEndianConvertor(), hex)
        {
        }
        /// <summary>
        /// 使用<see cref="BigInteger"/>对象初始化一个<see cref="HexBigInteger"/>对象
        /// </summary>
        /// <param name="value"><see cref="BigInteger"/>对象</param>
        public HexBigInteger(BigInteger value) : 
            base(value, new Nethereum.Hex.HexConvertors.HexBigIntegerBigEndianConvertor())
        {
        }
        /// <summary>
        /// 使用<see cref="long"/>类型的数字初始化一个<see cref="HexBigInteger"/>对象
        /// </summary>
        /// <param name="value"><see cref="long"/>类型的数字</param>
        public HexBigInteger(long value) : this(value.ToString("X")) { }
        /// <summary>
        /// 使用<see cref="ulong"/>类型的数字初始化一个<see cref="HexBigInteger"/>对象
        /// </summary>
        /// <param name="value"><see cref="ulong"/>类型的数字</param>
        public HexBigInteger(ulong value) : this(value.ToString("X")) { }
        /// <summary>
        /// 使用<see cref="Nethereum.Hex.HexTypes.HexBigInteger"/>对象字初始化一个<see cref="HexBigInteger"/>对象
        /// </summary>
        /// <param name="value"><see cref="Nethereum.Hex.HexTypes.HexBigInteger"/>对象</param>
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
    /// <summary>
    /// <see cref="HexBigInteger"/>与常见数字类型的快速转换
    /// </summary>
    public static class HexBigIntegerNumberExtensions
    {
        /// <summary>
        /// <see cref="ulong"/>类型到<see cref="HexBigInteger"/>类型转换
        /// </summary>
        /// <param name="val">原始值</param>
        /// <returns><see cref="HexBigInteger"/>对象</returns>
        public static HexBigInteger ToHexBigInteger(this ulong val)
        {
            return new HexBigInteger(val);
        }
        /// <summary>
        /// <see cref="long"/>类型到<see cref="HexBigInteger"/>类型转换
        /// </summary>
        /// <param name="val">原始值</param>
        /// <returns><see cref="HexBigInteger"/>对象</returns>
        public static HexBigInteger ToHexBigInteger(this long val)
        {
            return new HexBigInteger(val);
        }
        /// <summary>
        /// <see cref="BigInteger"/>类型到<see cref="HexBigInteger"/>类型转换
        /// </summary>
        /// <param name="val">原始值</param>
        /// <returns><see cref="HexBigInteger"/>对象</returns>
        public static HexBigInteger ToHexBigInteger(this BigInteger val)
        {
            return new HexBigInteger(val);
        }
        /// <summary>
        /// <see cref="Nethereum.Hex.HexTypes.HexBigInteger"/>类型到<see cref="HexBigInteger"/>类型转换
        /// </summary>
        /// <param name="val">原始值</param>
        /// <returns><see cref="HexBigInteger"/>对象</returns>
        public static HexBigInteger ToHexBigInteger(this Nethereum.Hex.HexTypes.HexBigInteger val)
        {
            return new HexBigInteger(val);
        }
    }
    internal class HexBigIntegerJsonConverter : JsonConverter
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
