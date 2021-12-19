using Nethereum.Hex.HexTypes;
using Newtonsoft.Json;

namespace PlatONet.DTOs
{
    /// <summary>
    /// 委托信息
    /// </summary>
    public class Delegation
    {
        /// <summary>
        /// 委托人的账户地址
        /// </summary>
        [JsonProperty("Addr")]
        public string DelegateAddress { get; set; }
        /// <summary>
        /// 验证人的节点Id
        /// </summary>
        [JsonProperty("NodeId")]
        public string NodeId { get; set; }
        /// <summary>
        /// 发起质押时的区块高度
        /// </summary>
        [JsonProperty("StakingBlockNum")]
        public HexBigInteger StakingBlockNum { get; set; }
        /// <summary>
        /// 最近一次对该候选人发起的委托时的结算周期
        /// </summary>
        [JsonProperty("DelegateEpoch")]
        public HexBigInteger DelegateEpoch { get; set; }
        /// <summary>
        /// 发起委托账户的自由金额的锁定期委托的von
        /// </summary>
        [JsonProperty("Released")]
        public HexBigInteger DelegateReleased { get; set; }
        /// <summary>
        /// 发起委托账户的自由金额的犹豫期委托的von
        /// </summary>
        [JsonProperty("ReleasedHes")]
        public HexBigInteger DelegateReleasedHes { get; set; }
        /// <summary>
        /// 发起委托账户的锁仓金额的锁定期委托的von
        ///  </summary>
        [JsonProperty("RestrictingPlan")]
        public HexBigInteger DelegateLocked { get; set; }
        /// <summary>
        /// 发起委托账户的锁仓金额的犹豫期委托的von
        /// </summary>
        [JsonProperty("RestrictingPlanHes")]
        public HexBigInteger DelegateLockedHes { get; set; }
        /// <summary>
        /// 待领取的委托收益von
        /// </summary>
        [JsonProperty("CumulativeIncome")]
        public HexBigInteger CumulativeIncome { get; set; }
        public override string ToString()
        {
            return "Delegation{" +
                    "delegateAddress='" + DelegateAddress + '\'' +
                    ", nodeId='" + NodeId + '\'' +
                    ", stakingBlockNum=" + StakingBlockNum +
                    ", delegateEpoch=" + DelegateEpoch +
                    ", delegateReleased=" + DelegateReleased +
                    ", delegateReleasedHes=" + DelegateReleasedHes +
                    ", delegateLocked=" + DelegateLocked +
                    ", delegateLockedHes=" + DelegateLockedHes +
                    ", cumulativeIncome=" + CumulativeIncome +
                    '}';
        }
    }
}
