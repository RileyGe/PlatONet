using Org.BouncyCastle.Utilities.Encoders;
using System;
using System.Collections.Generic;
using System.Text;

namespace PlatONet
{
    /// <summary>
    /// 地址
    /// </summary>
    public class Address
    {
        private byte[] _bytes;
        /// <summary>
        /// 地址的二进制形式
        /// </summary>
        public byte[] Bytes
        {
            get
            {
                return _bytes;
            }
        }
        private string _hrp;
        /// <summary>
        /// bench32格式地址前缀
        /// </summary>
        public string Hrp
        {
            get
            {
                return _hrp;
            }
        }
        /// <summary>
        /// 用bench32格式的地址初化<see cref="Address"/>实例
        /// </summary>
        /// <param name="encodedAddress">bench32格式的地址</param>
        public Address(string encodedAddress)
        {
            Bech32Engine.Decode(encodedAddress, out _hrp, out _bytes);
        }
        /// <summary>
        /// 使用二进制及bench32前缀初始化<see cref="Address"/>实例
        /// </summary>
        /// <param name="addressBytes">地址的二进制形式</param>
        /// <param name="hrp">bench32格式地址前缀</param>
        public Address(byte[] addressBytes, string hrp = "lat")
        {
            _hrp = hrp;
            _bytes = addressBytes;
        }
        /// <summary>
        /// 判断两个<see cref="Address"/>实例是否相等
        /// </summary>
        /// <param name="obj">另一实例</param>
        /// <returns>相等则为true，否则为false</returns>
        public override bool Equals(object obj)
        {
            if(obj.GetType() == typeof(Address))
            {
                var adr = obj as Address;
                return adr.ToString() == this.ToString();
            }
            return false;
        }
        /// <summary>
        /// 生成bench32格式的地址
        /// </summary>
        /// <returns>bench32格式的地址</returns>
        public override string ToString()
        {
            if (_hrp != null && _hrp.Length > 0 && _bytes != null && _bytes.Length > 0)
                return Bech32Engine.Encode(_hrp, _bytes);
            else return "";
        }
        /// <summary>
        /// 生成以太坊格式的地址
        /// </summary>
        /// <returns>以太坊格式的地址</returns>
        public string ToEthereumAddress()
        {
            return "0x" + Hex.ToHexString(_bytes);
        }
        /// <summary>
        /// 生成HashCode
        /// </summary>
        /// <returns>HashCode</returns>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
