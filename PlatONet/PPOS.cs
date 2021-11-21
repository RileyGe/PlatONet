using System;
using System.Collections.Generic;
using System.Text;
using Nethereum.Hex.HexConvertors.Extensions;
using System.Linq;
using Nethereum.Hex.HexTypes;
using Nethereum.RLP;
using Newtonsoft.Json;

namespace PlatONet
{
    public class PPOS
    {
        private PlatON _platon;
        public PPOS(PlatON platon)
        {
            _platon = platon;
        }
        private byte[] EncodeLength(int len, int offset)
        {
            if (len < 56)
            {
                return (len + offset).ToBytesForRLPEncoding();
            }
            else
            {
                var hexLength = string.Format("{0:X}", len);
                var lLength = hexLength.Length / 2;
                var firstByte = string.Format("{0:X}", offset + 55 + lLength);
                return (firstByte + hexLength).HexToByteArray();
            }
        } 
        
        private byte[] EncodeElement(byte[] inputBuf)
        {            
            return inputBuf.Length == 1 && inputBuf[0] < 128
                ? inputBuf
                : EncodeLength(inputBuf.Length, 128).Concat(inputBuf).ToArray();
        }
        private byte[] EncodeArray(IEnumerable<byte[]> bufArray)
        {
            var output = new List<byte>();
            foreach (var item in bufArray)
            {
                output.AddRange(EncodeElement(item));
            }
            //var buf = Buffer.concat(output);
            return EncodeLength(output.Count(), 192).Concat(output).ToArray();
        }        
        /// <summary>
        /// 根据函数类型，选择对应的 to 地址。
        /// </summary>
        /// <param name="funcType">Function Type</param>
        /// <returns>to 地址（Ethereum地址）</returns>
        private string FunctionTypeToAddress(int funcType)
        {
            string ethAddress;
            if (funcType >= 1000 && funcType < 2000)
                ethAddress = "0x1000000000000000000000000000000000000002";
            else if (funcType >= 2000 && funcType < 3000)
                ethAddress = "0x1000000000000000000000000000000000000005";
            else if (funcType >= 3000 && funcType < 4000)
                ethAddress = "0x1000000000000000000000000000000000000004";
            else if (funcType >= 4000 && funcType < 5000)
                ethAddress = "0x1000000000000000000000000000000000000001";
            else if (funcType >= 5000 && funcType < 6000)
                ethAddress = "0x1000000000000000000000000000000000000006";
            else
                throw new ArgumentException("function type should between 1000 and 6000.");
            var address = new Address(ethAddress.HexToByteArray(), _platon.Hrp);
            return address.ToString();
        }
        public CallResponse<HexBigInteger> GetActiveVersion(BlockParameter block = null)
        {
            var funcType = PPOSFunctionType.GET_ACTIVE_VERSION;
            block = block ?? BlockParameter.DEFAULT;
            List<byte[]> bufArray = new List<byte[]>
            {
                EncodeElement(funcType.ToBytesForRLPEncoding())
            };
            var data = EncodeArray(bufArray).ToHex();
            Transaction tx = new Transaction(FunctionTypeToAddress(funcType), 
                data: data);
            var hexStr = _platon.ExcuteCommand<string>(ApiMplatonods.platon_call.ToString(),
                paramList: new object[]
                {
                    tx.ToDict(),
                    block.BlockNumber
                });
            return DecodeResponse<CallResponse<HexBigInteger>>(hexStr);
        }
        public static T DecodeResponse<T>(string hexStr)
        {
            hexStr = hexStr.ToLower().StartsWith("0x") ? hexStr.Substring(2) : hexStr;
            var str = Encoding.UTF8.GetString(hexStr.HexToByteArray());
            var result = JsonConvert.DeserializeObject<T>(str);           
            return result;
        }
    }
}
