using System.Numerics;

namespace PlatONet.DTOs
{
    /// <summary>
    /// PlatON网络参数配置
    /// </summary>
    public class EconomicConfig
    {
        /// <summary>
        /// Common的配置项
        /// </summary>
        public Common Common { get; set; }
        /// <summary>
        /// Staking的配置项
        /// </summary>
        public Staking Staking { get; set; }
        /// <summary>
        /// Slashing的配置项
        /// </summary>
        public Slashing Slashing { get; set; }
        /// <summary>
        /// Gov的配置项
        /// </summary>
        public Gov Gov { get; set; }
        /// <summary>
        /// Reward的配置项
        /// </summary>
        public Reward Reward { get; set; }
        /// <summary>
        /// InnerAcc的配置项
        /// </summary>
        public InnerAcc InnerAcc { get; set; }

        /// <summary>
        /// config item for restricting plan
        /// </summary>
        public Restricting Restricting { get; set; }
        public override string ToString()
        {
            return "EconomicConfig{" +
                    "common=" + Common +
                    ", staking=" + Staking +
                    ", slashing=" + Slashing +
                    ", gov=" + Gov +
                    ", reward=" + Reward +
                    ", innerAcc=" + InnerAcc +
                    '}';
        }
    }
    /// <summary>
    /// 配置项
    /// </summary>
    public class Common
    {
        /// <summary>
        /// 结算周期规定的分钟数（整数）(eh)
        /// </summary>
        public BigInteger MaxEpochMinutes { get; set; }
        /// <summary>
        /// 共识轮验证人数
        /// </summary>
        public BigInteger MaxConsensusVals { get; set; }
        /// <summary>
        /// 增发周期的时间（分钟）
        /// </summary>
        public BigInteger AdditionalCycleTime { get; set; }
        /// <summary>
        /// 底层内部调试用
        /// </summary>
        public BigInteger NodeBlockTimeWindow { get; set; }
        /// <summary>
        /// 底层内部调试用
        /// </summary>
        public BigInteger PerRoundBlocks { get; set; }
        public override string ToString()
        {
            return "Common{" +
                    "maxEpochMinutes=" + MaxEpochMinutes +
                    ", maxConsensusVals=" + MaxConsensusVals +
                    ", additionalCycleTime=" + AdditionalCycleTime +
                    ", nodeBlockTimeWindow=" + NodeBlockTimeWindow +
                    ", perRoundBlocks=" + PerRoundBlocks +
                    '}';
        }
    }
    /// <summary>
    /// 配置项
    /// </summary>
    public class Staking
    {
        /// <summary>
        /// 质押的von门槛（单位：Von）===>100w lat
        /// </summary>
        public BigInteger StakeThreshold { get; set; }
        /// <summary>
        /// (incr, decr)委托或incr设置允许的最小阈值（单位：Von）===> 10 lat
        /// </summary>
        public BigInteger OperatingThreshold { get; set; }
        /// <summary>
        /// 结算周期验证人个数
        /// </summary>
        public BigInteger MaxValidators { get; set; }
        /// <summary>
        /// 退出质押后von被冻结的周期(单位： 结算周期，退出表示主动撤销和被动失去资格)
        /// </summary>
        public BigInteger UnStakeFreezeDuration { get; set; }
        public BigInteger RewardPerMaxChangeRange { get; set; }
        public BigInteger RewardPerChangeInterval { get; set; }
        public override string ToString()
        {
            return "Staking{" +
                    "stakeThreshold=" + StakeThreshold +
                    ", operatingThreshold=" + OperatingThreshold +
                    ", maxValidators=" + MaxValidators +
                    ", unStakeFreezeDuration=" + UnStakeFreezeDuration +
                    ", rewardPerMaxChangeRange=" + RewardPerMaxChangeRange +
                    ", rewardPerChangeInterval=" + RewardPerChangeInterval +
                    '}';
        }
    }
    /// <summary>
    /// 配置项
    /// </summary>
    public class Slashing
    {

        /// <summary>
        /// 双签高处罚金额，万分比（‱）
        /// </summary>
        public BigInteger SlashFractionDuplicateSign { get; set; }
        /// <summary>
        /// 表示从扣除的惩罚金里面，拿出x%奖励给举报者（%）
        /// </summary>
        public decimal DuplicateSignReportReward { get; set; }
        /// <summary>
        /// 零出块率惩罚的区块奖励数
        /// </summary>
        public BigInteger SlashBlocksReward { get; set; }
        /// <summary>
        /// 证据有效期
        /// </summary>
        public BigInteger MaxEvidenceAge { get; set; }

        public BigInteger ZeroProduceCumulativeTime { get; set; }
        public BigInteger ZeroProduceNumberThreshold { get; set; }
        /// <summary>
        /// 节点零出块惩罚被锁定时间
        /// </summary>
        public BigInteger ZeroProduceFreezeDuration { get; set; }
        public override string ToString()
        {
            return "Slashing{" +
                    "slashFractionDuplicateSign=" + SlashFractionDuplicateSign +
                    ", duplicateSignReportReward=" + DuplicateSignReportReward +
                    ", slashBlocksReward=" + SlashBlocksReward +
                    ", maxEvidenceAge=" + MaxEvidenceAge +
                    ", zeroProduceCumulativeTime=" + ZeroProduceCumulativeTime +
                    ", zeroProduceNumberThreshold=" + ZeroProduceNumberThreshold +
                    ", zeroProduceFreezeDuration=" + ZeroProduceFreezeDuration +
                    '}';
        }
    }
    /// <summary>
    /// 配置项
    /// </summary>
    public class Gov
    {
        /// <summary>
        /// 升级提案的投票持续最长的时间（单位：s）
        /// </summary>
        public BigInteger VersionProposalVoteDurationSeconds { get; set; }
        /// <summary>
        /// 升级提案投票支持率阈值（大于等于此值，则升级提案投票通过）
        /// </summary>
        public decimal VersionProposalSupportRate { get; set; }
        /// <summary>
        /// 文本提案的投票持续最长的时间（单位：s）
        /// </summary>
        public BigInteger TextProposalVoteDurationSeconds { get; set; }
        /// <summary>
        /// 文本提案投票参与率阈值（文本提案投票通过条件之一：大于此值，则文本提案投票通过）
        /// </summary>
        public decimal TextProposalVoteRate { get; set; }
        /// <summary>
        /// 文本提案投票支持率阈值（文本提案投票通过条件之一：大于等于此值，则文本提案投票通过）
        /// </summary>
        public decimal TextProposalSupportRate { get; set; }
        /// <summary>
        /// 取消提案投票参与率阈值（取消提案投票通过条件之一：大于此值，则取消提案投票通过）
        /// </summary>
        public decimal CancelProposalVoteRate { get; set; }
        /// <summary>
        /// 取消提案投票支持率阈值（取消提案投票通过条件之一：大于等于此值，则取消提案投票通过）
        /// </summary>
        public decimal CancelProposalSupportRate { get; set; }
        /// <summary>
        /// 参数提案的投票持续最长的时间（单位：s）
        /// </summary>
        public BigInteger ParamProposalVoteDurationSeconds { get; set; }
        /// <summary>
        /// 参数提案投票参与率阈值（参数提案投票通过条件之一：大于此值，则参数提案投票通过)
        /// </summary>
        public decimal ParamProposalVoteRate { get; set; }
        /// <summary>
        /// 参数提案投票支持率阈值（参数提案投票通过条件之一：大于等于此值，则参数提案投票通过
        /// </summary>
        public decimal ParamProposalSupportRate { get; set; }
        public override string ToString()
        {
            return "Gov{" +
                    "versionProposalVoteDurationSeconds=" + VersionProposalVoteDurationSeconds +
                    ", versionProposalSupportRate=" + VersionProposalSupportRate +
                    ", textProposalVoteDurationSeconds=" + TextProposalVoteDurationSeconds +
                    ", textProposalVoteRate=" + TextProposalVoteRate +
                    ", textProposalSupportRate=" + TextProposalSupportRate +
                    ", cancelProposalVoteRate=" + CancelProposalVoteRate +
                    ", cancelProposalSupportRate=" + CancelProposalSupportRate +
                    ", paramProposalVoteDurationSeconds=" + ParamProposalVoteDurationSeconds +
                    ", paramProposalVoteRate=" + ParamProposalVoteRate +
                    ", paramProposalSupportRate=" + ParamProposalSupportRate +
                    '}';
        }
    }
    /// <summary>
    /// 配置项
    /// </summary>
    public class Reward
    {
        /// <summary>
        /// 奖励池分配给出块奖励的比例，剩下的比例为分配给质押的奖励（%）
        /// </summary>
        public BigInteger NewBlockRate { get; set; }
        /// <summary>
        /// 基金会分配年，代表基金会每年边界的百分比
        /// </summary>
        public BigInteger PlatonFoundationYear { get; set; }
        public override string ToString()
        {
            return "Reward{" +
                    "newBlockRate=" + NewBlockRate +
                    ", platonFoundationYear=" + PlatonFoundationYear +
                    '}';
        }
    }
    /// <summary>
    /// 配置项
    /// </summary>
    public class InnerAcc
    {
        /// <summary>
        /// 基金会账号地址
        /// </summary>
        public string PlatonFundAccount { get; set; }
        /// <summary>
        /// 基金会初始金额
        /// </summary>
        public BigInteger PlatonFundBalance { get; set; }
        /// <summary>
        /// 社区开发者账户
        /// </summary>
        public string CdfAccount { get; set; }
        /// <summary>
        /// 社区开发者初始金额
        /// </summary>
        public BigInteger CdfBalance { get; set; }
        public override string ToString()
        {
            return "InnerAcc{" +
                    "platonFundAccount='" + PlatonFundAccount + '\'' +
                    ", platonFundBalance=" + PlatonFundBalance +
                    ", cdfAccount='" + CdfAccount + '\'' +
                    ", cdfBalance=" + CdfBalance +
                    '}';
        }
    }
    /// <summary>
    /// 配置项
    /// </summary>
    public class Restricting
    {
        /// <summary>
        /// minimum of each releasing of restricting plan
        /// </summary>
        public BigInteger MinimumRelease { get; set; }

        public override string ToString()
        {
            return "Restricting{" +
                    "minimumRelease=" + MinimumRelease +
                    '}';
        }
    }
}
