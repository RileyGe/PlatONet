using Newtonsoft.Json;
using Nethereum.Hex.HexTypes;

namespace PlatONet
{
    public class Node
    {
        [JsonProperty("NodeId")]
        public string NodeId { get; set; }

        [JsonProperty("StakingAddress")]
        public string StakingAddress { get; set; }

        [JsonProperty("BenefitAddress")]
        public string BenifitAddress { get; set; }

        [JsonProperty("RewardPer")]
        public HexBigInteger RewardPer { get; set; }

        [JsonProperty("NextRewardPer")]
        public HexBigInteger NextRewardPer { get; set; }

        [JsonProperty("StakingTxIndex")]
        public HexBigInteger StakingTxIndex { get; set; }

        [JsonProperty("ProgramVersion")]
        public HexBigInteger ProgramVersion { get; set; }

        [JsonProperty("Status")]
        public HexBigInteger Status { get; set; }

        [JsonProperty("StakingEpoch")]
        public HexBigInteger StakingEpoch { get; set; }

        [JsonProperty("StakingBlockNum")]
        public HexBigInteger StakingBlockNum { get; set; }

        [JsonProperty("Shares")]
        public HexBigInteger Shares { get; set; }

        [JsonProperty("Released")]
        public HexBigInteger Released { get; set; }

        [JsonProperty("ReleasedHes")]
        public HexBigInteger ReleasedHes { get; set; }

        [JsonProperty("RestrictingPlan")]
        public HexBigInteger RestrictingPlan { get; set; }

        [JsonProperty("RestrictingPlanHes")]
        public HexBigInteger RestrictingPlanHes { get; set; }

        [JsonProperty("ExternalId")]
        public string ExternalId { get; set; }

        [JsonProperty("NodeName")]
        public string NodeName { get; set; }

        [JsonProperty("Website")]
        public string Website { get; set; }

        [JsonProperty("Details")]
        public string Details { get; set; }

        [JsonProperty("ValidatorTerm")]
        public HexBigInteger ValidatorTerm { get; set; }

        [JsonProperty("DelegateEpoch")]
        public HexBigInteger DelegateEpoch { get; set; }

        [JsonProperty("DelegateTotal")]
        public HexBigInteger DelegateTotal { get; set; }

        [JsonProperty("DelegateTotalHes")]
        public HexBigInteger DelegateTotalHes { get; set; }

        [JsonProperty("DelegateRewardTotal")]
        public HexBigInteger DelegateRewardTotal { get; set; }
        public Node()
        {
        }

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
