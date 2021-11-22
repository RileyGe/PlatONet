using Nethereum.Hex.HexTypes;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace PlatONet
{
    public class EconomicConfig
    {
        /**
         * common的配置项
         */
        public Common common { get; set; }
        /**
         * staking的配置项
         */
        public Staking staking { get; set; }
        /**
         * slashing的配置项
         */
        public Slashing slashing { get; set; }
        /**
         * gov的配置项
         */
        public Gov gov { get; set; }
        /**
         * reward的配置项
         */
        public Reward reward { get; set; }
        /**
         * innerAcc的配置项
         */
        public InnerAcc innerAcc { get; set; }

        /**
         * config item for restricting plan
         */
        public Restricting restricting { get; set; }
        public class Common
        {
            /**
             * 结算周期规定的分钟数（整数）(eh)
             */
            public BigInteger maxEpochMinutes { get; set; }
            /**
             * 共识轮验证人数
             */
            public BigInteger maxConsensusVals { get; set; }
            /**
             * 增发周期的时间（分钟）
             */
            public BigInteger additionalCycleTime { get; set; }
            /**
             * 底层内部调试用
             */
            public BigInteger nodeBlockTimeWindow { get; set; }
            /**
             * 底层内部调试用
             */
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
            /**
             * 质押的von门槛（单位：Von）===>100w lat
             */
            public BigInteger stakeThreshold { get; set; }
            /**
             * (incr, decr)委托或incr设置允许的最小阈值（单位：Von）===> 10 lat
             */
            public BigInteger operatingThreshold { get; set; }
            /**
             * 结算周期验证人个数
             */
            public BigInteger maxValidators { get; set; }
            /**
             * 退出质押后von被冻结的周期(单位： 结算周期，退出表示主动撤销和被动失去资格)
             */
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

            /**
             * 双签高处罚金额，万分比（‱）
             */
            public BigInteger slashFractionDuplicateSign { get; set; }
            /**
             * 表示从扣除的惩罚金里面，拿出x%奖励给举报者（%）
             */
            public decimal duplicateSignReportReward { get; set; }
            /**
             * 零出块率惩罚的区块奖励数
             */
            public BigInteger slashBlocksReward { get; set; }
            /**
             * 证据有效期
             */
            public BigInteger maxEvidenceAge { get; set; }

            public BigInteger zeroProduceCumulativeTime { get; set; }
            public BigInteger zeroProduceNumberThreshold { get; set; }
            // 节点零出块惩罚被锁定时间
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
            /**
             * 升级提案的投票持续最长的时间（单位：s）
             */
            public BigInteger versionProposalVoteDurationSeconds { get; set; }
            /**
             * 升级提案投票支持率阈值（大于等于此值，则升级提案投票通过）
             */
            public decimal versionProposalSupportRate { get; set; }
            /**
             * 文本提案的投票持续最长的时间（单位：s）
             */
            public BigInteger textProposalVoteDurationSeconds { get; set; }
            /**
             * 文本提案投票参与率阈值（文本提案投票通过条件之一：大于此值，则文本提案投票通过）
             */
            public decimal textProposalVoteRate { get; set; }
            /**
             * 文本提案投票支持率阈值（文本提案投票通过条件之一：大于等于此值，则文本提案投票通过）
             */
            public decimal textProposalSupportRate { get; set; }
            /**
             * 取消提案投票参与率阈值（取消提案投票通过条件之一：大于此值，则取消提案投票通过）
             */
            public decimal cancelProposalVoteRate { get; set; }
            /**
             * 取消提案投票支持率阈值（取消提案投票通过条件之一：大于等于此值，则取消提案投票通过）
             */
            public decimal cancelProposalSupportRate { get; set; }
            /**
             * 参数提案的投票持续最长的时间（单位：s）
             */
            public BigInteger paramProposalVoteDurationSeconds { get; set; }
            /**
             * 参数提案投票参与率阈值（参数提案投票通过条件之一：大于此值，则参数提案投票通过)
             */
            public decimal paramProposalVoteRate { get; set; }
            /**
             * 参数提案投票支持率阈值（参数提案投票通过条件之一：大于等于此值，则参数提案投票通过
             */
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
            /**
             * 奖励池分配给出块奖励的比例，剩下的比例为分配给质押的奖励（%）
             */
            public BigInteger newBlockRate { get; set; }
            /**
             * 基金会分配年，代表基金会每年边界的百分比
             */
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

            /**
             * 基金会账号地址
             */
            public String platonFundAccount { get; set; }
            /**
             * 基金会初始金额
             */
            public BigInteger platonFundBalance { get; set; }
            /**
             * 社区开发者账户
             */
            public String cdfAccount { get; set; }
            /**
             * 社区开发者初始金额
             */
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
            /**
             * minimum of each releasing of restricting plan
             */
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
