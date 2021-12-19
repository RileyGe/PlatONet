using Nethereum.Hex.HexTypes;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace PlatONet.DTOs
{
    /// <summary>
    /// 锁仓信息
    /// </summary>
    public class RestrictingItem
    {
        /// <summary>
        /// 锁仓余额
        /// </summary>
        public HexBigInteger Balance { get; set; }
        /// <summary>
        /// 质押/抵押金额
        /// </summary>
        public HexBigInteger Pledge { get; set; }
        /// <summary>
        /// 欠释放金额
        /// </summary>
        public HexBigInteger Debt { get; set; }
        /// <summary>
        /// 锁仓分录信息
        /// </summary>
        [JsonProperty("plans")]
        public List<RestrictingInfo> Info { get; set; }
        public override string ToString()
        {
            return "RestrictingItem{" +
                    "balance=" + Balance +
                    ", pledge=" + Pledge +
                    ", debt=" + Debt +
                    ", info=" + Info +
                    '}';
        }
    }
}
