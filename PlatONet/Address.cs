using System;
using System.Collections.Generic;
using System.Text;

namespace PlatONet
{
    public class Address
    {
        private byte[] _bytes;
        public byte[] Bytes
        {
            get
            {
                return _bytes;
            }
        }
        private string _hrp;
        public string Hrp
        {
            get
            {
                return _hrp;
            }
        }
        public Address(string encodedAddress)
        {
            Bech32Engine.Decode(encodedAddress, out _hrp, out _bytes);
        }
        public Address(byte[] addressBytes, string hrp = "lat")
        {
            _hrp = hrp;
            _bytes = addressBytes;
        }
        public override string ToString()
        {
            return Bech32Engine.Encode(_hrp, _bytes);
        }
    }
}
