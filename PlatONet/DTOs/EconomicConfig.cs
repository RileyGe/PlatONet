using System;
using System.Numerics;

namespace PlatONet
{
    public class EconomicConfig
    {
        /// <summary>
        /// common的配置项
        /// </summary>
        public Common common { get; set; }
        /// <summary>
        /// staking的配置项
        /// </summary>
        public Staking staking { get; set; }
        /// <summary>
        /// slashing的配置项
        /// </summary>
        public Slashing slashing { get; set; }
        /// <summary>
        /// gov的配置项
        /// </summary>
        public Gov gov { get; set; }
        /// <summary>
        /// reward的配置项
        /// </summary>
        public Reward reward { get; set; }
        /// <summary>
        /// innerAcc的配置项
        /// </summary>
        public InnerAcc innerAcc { get; set; }

        /// <summary>
        /// config item for restricting plan
        /// </summary>
        public Restricting restricting { get; set; }
        public class Common
        {
            /// <summary>
            /// 结算周期规定的分钟数（整数）(eh)
            /// </summary>
            public BigInteger maxEpochMinutes { get; set; }
            /// <summary>
            /// 共识轮验证人数
            /// </summary>
            public BigInteger maxConsensusVals { get; set; }
            /// <summary>
            /// 增发周期的时间（分钟）
            /// </summary>
            public BigInteger additionalCycleTime { get; set; }
            /// <summary>
            /// 底层内部调试用
            /// </summary>
            public BigInteger nodeBlockTimeWindow { get; set; }
            /// <summary>
            /// 底层内部调试用
            /// </summary>
            public BigInteger perRoundBlocks { get; set; }
            public override string ToString()
            {
                return "Common{" +
                        "maxEpochMinutes=" + maxEpochMinutes +
                        ", maxConsensusVals=" + maxConsensusVals +
                        ", additionalCycleTime=" + additionalCycleTime +
                        ", nodeBlockTimeWindow=" + nodeBlockTimeWindow +
                        ", perRoundBlocks=" + perRoundBlocks +
                        '}';
            }
        }

        public class Staking
        {
            /// <summary>
            /// 质押的von门槛（单位：Von）===>100w lat
            /// </summary>
            public BigInteger stakeThreshold { get; set; }
            /// <summary>
            /// (incr, decr)委托或incr设置允许的最小阈值（单位：Von）===> 10 lat
            /// </summary>
            public BigInteger operatingThreshold { get; set; }
            /// <summary>
            /// 结算周期验证人个数
            /// </summary>
            public BigInteger maxValidators { get; set; }
            /// <summary>
            /// 退出质押后von被冻结的周期(单位： 结算周期，退出表示主动撤销和被动失去资格)
            /// </summary>
            public BigInteger unStakeFreezeDuration { get; set; }
            public BigInteger rewardPerMaxChangeRange { get; set; }
            public BigInteger rewardPerChangeInterval { get; set; }
            public override string ToString()
            {
                return "Staking{" +
                        "stakeThreshold=" + stakeThreshold +
                        ", operatingThreshold=" + operatingThreshold +
                        ", maxValidators=" + maxValidators +
                        ", unStakeFreezeDuration=" + unStakeFreezeDuration +
                        ", rewardPerMaxChangeRange=" + rewardPerMaxChangeRange +
                        ", rewardPerChangeInterval=" + rewardPerChangeInterval +
                        '}';
            }
        }

        public class Slashing
        {

            /// <summary>
            /// 双签高处罚金额，万分比（‱）
            /// </summary>
            public BigInteger slashFractionDuplicateSign { get; set; }
            /// <summary>
            /// 表示从扣除的惩罚金里面，拿出x%奖励给举报者（%）
            /// </summary>
            public decimal duplicateSignReportReward { get; set; }
            /// <summary>
            /// 零出块率惩罚的区块奖励数
            /// </summary>
            public BigInteger slashBlocksReward { get; set; }
            /// <summary>
            /// 证据有效期
            /// </summary>
            public BigInteger maxEvidenceAge { get; set; }

            public BigInteger zeroProduceCumulativeTime { get; set; }
            public BigInteger zeroProduceNumberThreshold { get; set; }
            /// <summary>
            /// 节点零出块惩罚被锁定时间
            /// </summary>
            public BigInteger zeroProduceFreezeDuration { get; set; }
            public override string ToString()
            {
                return "Slashing{" +
                        "slashFractionDuplicateSign=" + slashFractionDuplicateSign +
                        ", duplicateSignReportReward=" + duplicateSignReportReward +
                        ", slashBlocksReward=" + slashBlocksReward +
                        ", maxEvidenceAge=" + maxEvidenceAge +
                        ", zeroProduceCumulativeTime=" + zeroProduceCumulativeTime +
                        ", zeroProduceNumberThreshold=" + zeroProduceNumberThreshold +
                        ", zeroProduceFreezeDuration=" + zeroProduceFreezeDuration +
                        '}';
            }
        }

        public class Gov
        {
            /// <summary>
            /// 升级提案的投票持续最长的时间（单位：s）
            /// </summary>
            public BigInteger versionProposalVoteDurationSeconds { get; set; }
            /// <summary>
            /// 升级提案投票支持率阈值（大于等于此值，则升级提案投票通过）
            /// </summary>
            public decimal versionProposalSupportRate { get; set; }
            /// <summary>
            /// 文本提案的投票持续最长的时间（单位：s）
            /// </summary>
            public BigInteger textProposalVoteDurationSeconds { get; set; }
            /// <summary>
            /// 文本提案投票参与率阈值（文本提案投票通过条件之一：大于此值，则文本提案投票通过）
            /// </summary>
            public decimal textProposalVoteRate { get; set; }
            /// <summary>
            /// 文本提案投票支持率阈值（文本提案投票通过条件之一：大于等于此值，则文本提案投票通过）
            /// </summary>
            public decimal textProposalSupportRate { get; set; }
            /// <summary>
            /// 取消提案投票参与率阈值（取消提案投票通过条件之一：大于此值，则取消提案投票通过）
            /// </summary>
            public decimal cancelProposalVoteRate { get; set; }
            /// <summary>
            /// 取消提案投票支持率阈值（取消提案投票通过条件之一：大于等于此值，则取消提案投票通过）
            /// </summary>
            public decimal cancelProposalSupportRate { get; set; }
            /// <summary>
            /// 参数提案的投票持续最长的时间（单位：s）
            /// </summary>
            public BigInteger paramProposalVoteDurationSeconds { get; set; }
            /// <summary>
            /// 参数提案投票参与率阈值（参数提案投票通过条件之一：大于此值，则参数提案投票通过)
            /// </summary>
            public decimal paramProposalVoteRate { get; set; }
            /// <summary>
            /// 参数提案投票支持率阈值（参数提案投票通过条件之一：大于等于此值，则参数提案投票通过
            /// </summary>
            public decimal paramProposalSupportRate { get; set; }
            public override string ToString()
            {
                return "Gov{" +
                        "versionProposalVoteDurationSeconds=" + versionProposalVoteDurationSeconds +
                        ", versionProposalSupportRate=" + versionProposalSupportRate +
                        ", textProposalVoteDurationSeconds=" + textProposalVoteDurationSeconds +
                        ", textProposalVoteRate=" + textProposalVoteRate +
                        ", textProposalSupportRate=" + textProposalSupportRate +
                        ", cancelProposalVoteRate=" + cancelProposalVoteRate +
                        ", cancelProposalSupportRate=" + cancelProposalSupportRate +
                        ", paramProposalVoteDurationSeconds=" + paramProposalVoteDurationSeconds +
                        ", paramProposalVoteRate=" + paramProposalVoteRate +
                        ", paramProposalSupportRate=" + paramProposalSupportRate +
                        '}';
            }
        }

        public class Reward
        {
            /// <summary>
            /// 奖励池分配给出块奖励的比例，剩下的比例为分配给质押的奖励（%）
            /// </summary>
            public BigInteger newBlockRate { get; set; }
            /// <summary>
            /// 基金会分配年，代表基金会每年边界的百分比
            /// </summary>
            public BigInteger platonFoundationYear { get; set; }
            public override string ToString()
            {
                return "Reward{" +
                        "newBlockRate=" + newBlockRate +
                        ", platonFoundationYear=" + platonFoundationYear +
                        '}';
            }
        }


        public class InnerAcc
        {
            /// <summary>
            /// 基金会账号地址
            /// </summary>
            public string platonFundAccount { get; set; }
            /// <summary>
            /// 基金会初始金额
            /// </summary>
            public BigInteger platonFundBalance { get; set; }
            /// <summary>
            /// 社区开发者账户
            /// </summary>
            public string cdfAccount { get; set; }
            /// <summary>
            /// 社区开发者初始金额
            /// </summary>
            public BigInteger cdfBalance { get; set; }
            public override string ToString()
            {
                return "InnerAcc{" +
                        "platonFundAccount='" + platonFundAccount + '\'' +
                        ", platonFundBalance=" + platonFundBalance +
                        ", cdfAccount='" + cdfAccount + '\'' +
                        ", cdfBalance=" + cdfBalance +
                        '}';
            }
        }

        public class Restricting
        {
            /// <summary>
            /// minimum of each releasing of restricting plan
            /// </summary>
            public BigInteger minimumRelease { get; set; }
            
            public override string ToString()
            {
                return "Restricting{" +
                        "minimumRelease=" + minimumRelease +
                        '}';
            }
        }

        public override string ToString()
        {
            return "EconomicConfig{" +
                    "common=" + common +
                    ", staking=" + staking +
                    ", slashing=" + slashing +
                    ", gov=" + gov +
                    ", reward=" + reward +
                    ", innerAcc=" + innerAcc +
                    '}';
        }
    }
}
