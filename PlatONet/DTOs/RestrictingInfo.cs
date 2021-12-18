using Nethereum.Hex.HexTypes;

namespace PlatONet
{
    public class RestrictingInfo
    {
        /// <summary>
        /// 释放区块高度
        /// </summary>
        private HexBigInteger blockNumber { get; set; }
        /// <summary>
        /// 释放金额
        /// </summary>
        public HexBigInteger amount { get; set; }
        public override string ToString()
        {
            return "RestrictingInfo{" +
                    "blockNumber=" + blockNumber +
                    ", amount=" + amount +
                    '}';
        }
    }
}
