using Newtonsoft.Json;
using Nethereum.Hex.HexTypes;

namespace PlatONet
{
    public class Node
    {
        [JsonProperty("NodeId")]
        public string NodeId { get; set; }

        [JsonProperty("StakingAddress")]
        public string StakingAddress;

        [JsonProperty("BenefitAddress")]
        public string BenifitAddress;

        [JsonProperty("RewardPer")]
        public HexBigInteger RewardPer;

        [JsonProperty("NextRewardPer")]
        public HexBigInteger NextRewardPer;

        [JsonProperty("StakingTxIndex")]
        public HexBigInteger StakingTxIndex;

        [JsonProperty("ProgramVersion")]
        public HexBigInteger ProgramVersion;

        [JsonProperty("Status")]
        public HexBigInteger Status;

        [JsonProperty("StakingEpoch")]
        public HexBigInteger StakingEpoch;

        [JsonProperty("StakingBlockNum")]
        public HexBigInteger StakingBlockNum;

        [JsonProperty("Shares")]
        public HexBigInteger Shares;

        [JsonProperty("Released")]
        public HexBigInteger Released;

        [JsonProperty("ReleasedHes")]
        public HexBigInteger ReleasedHes;

        [JsonProperty("RestrictingPlan")]
        public HexBigInteger RestrictingPlan;

        [JsonProperty("RestrictingPlanHes")]
        public HexBigInteger RestrictingPlanHes;

        [JsonProperty("ExternalId")]
        public string ExternalId;

        [JsonProperty("NodeName")]
        public string NodeName;

        [JsonProperty("Website")]
        public string Website;

        [JsonProperty("Details")]
        public string Details;

        [JsonProperty("ValidatorTerm")]
        public HexBigInteger ValidatorTerm;

        [JsonProperty("DelegateEpoch")]
        public HexBigInteger DelegateEpoch;

        [JsonProperty("DelegateTotal")]
        public HexBigInteger DelegateTotal { get; set; }

        [JsonProperty("DelegateTotalHes")]
        public HexBigInteger DelegateTotalHes { get; set; }

        [JsonProperty("DelegateRewardTotal")]
        public HexBigInteger DelegateRewardTotal;
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
