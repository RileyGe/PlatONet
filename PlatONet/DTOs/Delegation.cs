using Nethereum.Hex.HexTypes;
using Newtonsoft.Json;

namespace PlatONet
{
    public class Delegation
    {
        /**
         * 委托人的账户地址
         */
        [JsonProperty("Addr")]
        public string DelegateAddress { get; set; }
        /**
         * 验证人的节点Id
         */
        [JsonProperty("NodeId")]
        public string NodeId { get; set; }
        /**
         * 发起质押时的区块高度
         */
        [JsonProperty("StakingBlockNum")]
        public HexBigInteger StakingBlockNum { get; set; }
        /**
         * 最近一次对该候选人发起的委托时的结算周期
         */
        [JsonProperty("DelegateEpoch")]
        public HexBigInteger DelegateEpoch { get; set; }
        /**
         * 发起委托账户的自由金额的锁定期委托的von
         */
        [JsonProperty("Released")]
        public HexBigInteger DelegateReleased { get; set; }
        /**
         * 发起委托账户的自由金额的犹豫期委托的von
         */
        [JsonProperty("ReleasedHes")]
        public HexBigInteger DelegateReleasedHes { get; set; }
        /**
         * 发起委托账户的锁仓金额的锁定期委托的von
         */
        [JsonProperty("RestrictingPlan")]
        public HexBigInteger DelegateLocked { get; set; }
        /**
         * 发起委托账户的锁仓金额的犹豫期委托的von
         */
        [JsonProperty("RestrictingPlanHes")]
        public HexBigInteger DelegateLockedHes { get; set; }
        /**
         * 待领取的委托收益von
         */
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
