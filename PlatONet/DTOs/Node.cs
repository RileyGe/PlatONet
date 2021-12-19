using Newtonsoft.Json;
using Nethereum.Hex.HexTypes;

namespace PlatONet.DTOs
{
    /// <summary>
    /// 节点信息
    /// </summary>
    public class Node
    {
        /// <summary>
        /// 节点id，16进制格式，即节点公钥，可以通过管理台查询（platon attach http://127.0.0.1:6789 --exec "admin.nodeInfo.id"）
        /// </summary>
        [JsonProperty("NodeId")]
        public string NodeId { get; set; }
        /// <summary>
        /// 发起质押时使用的账户(撤销质押时，VON会被退回该账户或者该账户的锁仓信息中)
        /// </summary>
        [JsonProperty("StakingAddress")]
        public string StakingAddress { get; set; }
        /// <summary>
        /// 收益账户，用于接收出块奖励和质押奖励的收益账户。
        /// </summary>
        [JsonProperty("BenefitAddress")]
        public string BenifitAddress { get; set; }
        /// <summary>
        /// 当前结算周期奖励分成比例，1=0.01% 10000=100%
        /// </summary>
        [JsonProperty("RewardPer")]
        public HexBigInteger RewardPer { get; set; }
        /// <summary>
        /// 下一个结算周期奖励分成比例，1=0.01% 10000=100%
        /// </summary>
        [JsonProperty("NextRewardPer")]
        public HexBigInteger NextRewardPer { get; set; }
        /// <summary>
        /// 发起质押时的交易索引
        /// </summary>
        [JsonProperty("StakingTxIndex")]
        public HexBigInteger StakingTxIndex { get; set; }
        /// <summary>
        /// 程序的真实版本，通过<see cref="Web3.GetProgramVersion"/>获取获取
        /// </summary>
        [JsonProperty("ProgramVersion")]
        public HexBigInteger ProgramVersion { get; set; }
        /// <summary>
        /// 候选人的状态，
        /// 0: 节点可用，
        /// 1: 节点不可用 ，
        /// 2:节点出块率低但没有达到移除条件的，
        /// 4:节点的VON不足最低质押门槛，
        /// 8:节点被举报双签，
        /// 16:节点出块率低且达到移除条件, 
        /// 32: 节点主动发起撤销
        /// </summary>
        [JsonProperty("Status")]
        public HexBigInteger Status { get; set; }
        /// <summary>
        /// 当前变更质押金额时的结算周期
        /// </summary>
        [JsonProperty("StakingEpoch")]
        public HexBigInteger StakingEpoch { get; set; }
        /// <summary>
        /// 发起质押时的区块高度
        /// </summary>
        [JsonProperty("StakingBlockNum")]
        public HexBigInteger StakingBlockNum { get; set; }
        /// <summary>
        /// 当前候选人总共质押加被委托的VON数目
        /// </summary>
        [JsonProperty("Shares")]
        public HexBigInteger Shares { get; set; }
        /// <summary>
        /// 发起质押账户的自由金额的锁定期质押的VON
        /// </summary>
        [JsonProperty("Released")]
        public HexBigInteger Released { get; set; }
        /// <summary>
        /// 发起质押账户的自由金额的犹豫期质押的VON
        /// </summary>
        [JsonProperty("ReleasedHes")]
        public HexBigInteger ReleasedHes { get; set; }
        /// <summary>
        /// 发起质押账户的锁仓金额的锁定期质押的VON
        /// </summary>
        [JsonProperty("RestrictingPlan")]
        public HexBigInteger RestrictingPlan { get; set; }
        /// <summary>
        /// 发起质押账户的锁仓金额的犹豫期质押的VON
        /// </summary>
        [JsonProperty("RestrictingPlanHes")]
        public HexBigInteger RestrictingPlanHes { get; set; }

        [JsonProperty("ExternalId")]
        public string ExternalId { get; set; }
        /// <summary>
        /// 节点的名称
        /// </summary>
        [JsonProperty("NodeName")]
        public string NodeName { get; set; }
        /// <summary>
        /// 节点的第三方主页(有长度限制，表示该节点的主页)
        /// </summary>
        [JsonProperty("Website")]
        public string Website { get; set; }
        /// <summary>
        /// 节点的描述(有长度限制，表示该节点的描述)
        /// </summary>
        [JsonProperty("Details")]
        public string Details { get; set; }
        /// <summary>
        /// 验证人的任期
        /// </summary>
        [JsonProperty("ValidatorTerm")]
        public HexBigInteger ValidatorTerm { get; set; }
        /// <summary>
        /// 节点最后一次被委托的结算周期
        /// </summary>
        [JsonProperty("DelegateEpoch")]
        public HexBigInteger DelegateEpoch { get; set; }
        /// <summary>
        /// 节点被委托的生效总数量
        /// </summary>
        [JsonProperty("DelegateTotal")]
        public HexBigInteger DelegateTotal { get; set; }
        /// <summary>
        /// 节点被委托的未生效总数量
        /// </summary>
        [JsonProperty("DelegateTotalHes")]
        public HexBigInteger DelegateTotalHes { get; set; }
        /// <summary>
        /// 候选人当前发放的总委托奖励
        /// </summary>
        [JsonProperty("DelegateRewardTotal")]
        public HexBigInteger DelegateRewardTotal { get; set; }
        public Node() { }
        public override string ToString()
        {
            return "Node{" +
                    "nodeId='" + NodeId + '\'' +
                    ", stakingAddress='" + StakingAddress + '\'' +
                    ", benifitAddress='" + BenifitAddress + '\'' +
                    ", rewardPer=" + RewardPer +
                    ", nextRewardPer=" + NextRewardPer +
                    ", stakingTxIndex=" + StakingTxIndex +
                    ", programVersion=" + ProgramVersion +
                    ", status=" + Status +
                    ", stakingEpoch=" + StakingEpoch +
                    ", stakingBlockNum=" + StakingBlockNum +
                    ", shares=" + Shares +
                    ", released=" + Released +
                    ", releasedHes=" + ReleasedHes +
                    ", restrictingPlan=" + RestrictingPlan +
                    ", restrictingPlanHes=" + RestrictingPlanHes +
                    ", externalId='" + ExternalId + '\'' +
                    ", nodeName='" + NodeName + '\'' +
                    ", website='" + Website + '\'' +
                    ", details='" + Details + '\'' +
                    ", validatorTerm=" + ValidatorTerm +
                    ", delegateEpoch=" + DelegateEpoch +
                    ", delegateTotal=" + DelegateTotal +
                    ", delegateTotalHes=" + DelegateTotalHes +
                    ", delegateRewardTotal=" + DelegateRewardTotal +
                    '}';
        }
    }
}
