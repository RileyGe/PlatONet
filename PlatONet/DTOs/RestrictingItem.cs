using Nethereum.Hex.HexTypes;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace PlatONet
{
    public class RestrictingItem
    {
        /// <summary>
        /// 锁仓余额
        /// </summary>
        public HexBigInteger balance { get; set; }
        /// <summary>
        /// 质押/抵押金额
        /// </summary>
        public HexBigInteger pledge { get; set; }
        /// <summary>
        /// 欠释放金额
        /// </summary>
        public HexBigInteger debt { get; set; }
        /// <summary>
        /// 锁仓分录信息
        /// </summary>
        [JsonProperty("plans")]
        public List<RestrictingInfo> info { get; set; }
        public override string ToString()
        {
            return "RestrictingItem{" +
                    "balance=" + balance +
                    ", pledge=" + pledge +
                    ", debt=" + debt +
                    ", info=" + info +
                    '}';
        }
    }
}
