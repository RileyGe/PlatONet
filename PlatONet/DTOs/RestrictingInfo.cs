using Nethereum.Hex.HexTypes;

namespace PlatONet.DTOs
{
    /// <summary>
    /// 锁仓分录信息
    /// </summary>
    public class RestrictingInfo
    {
        /// <summary>
        /// 释放区块高度
        /// </summary>
        private HexBigInteger BlockNumber { get; set; }
        /// <summary>
        /// 释放金额
        /// </summary>
        public HexBigInteger Amount { get; set; }
        public override string ToString()
        {
            return "RestrictingInfo{" +
                    "blockNumber=" + BlockNumber +
                    ", amount=" + Amount +
                    '}';
        }
    }
}
