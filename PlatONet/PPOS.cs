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
            List<byte[]> bufArray = new List<byte[]>
            {
                funcType.ToBytesForRLPEncoding()
            };
            var tx = BuildTransaction(bufArray, FunctionTypeToAddress(funcType));
            var hexStr = _platon.Call<string>(tx, block);
            return DecodeResponse<CallResponse<HexBigInteger>>(hexStr);
        }
        #region staking        
        public CallResponse<Node> GetStakingInfo(string nodeId, BlockParameter block = null)
        {
            var funcType = PPOSFunctionType.GET_STAKINGINFO_FUNC_TYPE;
            List<byte[]> bufArray = new List<byte[]>
            {
                funcType.ToBytesForRLPEncoding(),
                nodeId.HexToByteArray()
            };
            var tx = BuildTransaction(bufArray, FunctionTypeToAddress(funcType));
            var hexStr = _platon.Call<string>(tx, block);
            return DecodeResponse<CallResponse<Node>>(hexStr);
        }

        public CallResponse<HexBigInteger> GetPackageReward(BlockParameter block = null)
        {
            var funcType = PPOSFunctionType.GET_PACKAGEREWARD_FUNC_TYPE;
            List<byte[]> bufArray = new List<byte[]>
            {
                funcType.ToBytesForRLPEncoding()
            };
            var tx = BuildTransaction(bufArray, FunctionTypeToAddress(funcType));
            var hexStr = _platon.Call<string>(tx, block);
            return DecodeResponse<CallResponse<HexBigInteger>>(hexStr);
        }

        public CallResponse<HexBigInteger> GetStakingReward(BlockParameter block = null)
        {
            var funcType = PPOSFunctionType.GET_STAKINGREWARD_FUNC_TYPE;
            List<byte[]> bufArray = new List<byte[]>
            {
                funcType.ToBytesForRLPEncoding()
            };
            var tx = BuildTransaction(bufArray, FunctionTypeToAddress(funcType));
            var hexStr = _platon.Call<string>(tx, block);
            return DecodeResponse<CallResponse<HexBigInteger>>(hexStr);
        }
        public CallResponse<HexBigInteger> GetAvgPackTime(BlockParameter block = null)
        {
            var funcType = PPOSFunctionType.GET_AVGPACKTIME_FUNC_TYPE;
            List<byte[]> bufArray = new List<byte[]>
            {
                funcType.ToBytesForRLPEncoding()
            };
            var tx = BuildTransaction(bufArray, FunctionTypeToAddress(funcType));
            var hexStr = _platon.Call<string>(tx, block);
            return DecodeResponse<CallResponse<HexBigInteger>>(hexStr);
        }
        #endregion
        #region delegate
        /// <summary>
        /// 发起委托
        /// </summary>
        /// <param name="nodeId">被质押的节点的NodeId</param>
        /// <param name="stakingAmountType">表示使用账户自由金额还是账户的锁仓金额做委托。
        /// 0: 自由金额； 1: 锁仓金额；2：自动选择</param>
        /// <param name="amount">委托的金额(按照最小单位算，1LAT = 10**18 von)</param>
        /// <returns></returns>
        public string Delegate(StakingAmountType stakingAmountType, string nodeId, HexBigInteger amount, Transaction netParams = null)
        {
            var funcType = PPOSFunctionType.DELEGATE_FUNC_TYPE;
            List<byte[]> bufArray = new List<byte[]>
            {
                funcType.ToBytesForRLPEncoding(),
                ((int)stakingAmountType).ToBytesForRLPEncoding(),
                nodeId.HexToByteArray(),                
                amount.ToHexByteArray()
            };
            //netParams = netParams ?? new Transaction();
            netParams = BuildTransaction(bufArray, FunctionTypeToAddress(funcType), netParams);
            netParams = _platon.FillTransactionWithDefaultValue(netParams);
            netParams.ChainId = _platon.ChainId;
            return _platon.SendTransaction(netParams);
        }
        #endregion
        private Transaction BuildTransaction(IEnumerable<byte[]> bufArray,
            string address, Transaction netParams = null)
        {
            netParams = netParams ?? new Transaction();
            netParams.To = new Address(address);
            var encodedBufArray = new List<byte[]>();
            foreach (var item in bufArray) encodedBufArray.Add(EncodeElement(item));
            netParams.Data = EncodeArray(encodedBufArray).ToHex();
            return netParams;        
        }
        public static T DecodeResponse<T>(string hexStr)
        {
            hexStr = hexStr.ToLower().StartsWith("0x") ? hexStr.Substring(2) : hexStr;
            var str = Encoding.UTF8.GetString(hexStr.HexToByteArray());
            var result = JsonConvert.DeserializeObject<T>(str);
            return result;
        }
    }
    public enum StakingAmountType
    {
        FREE_AMOUNT_TYPE = 0,
        RESTRICTING_AMOUNT_TYPE = 1,
        AUTO_AMOUNT_TYPE = 2
    }
}
