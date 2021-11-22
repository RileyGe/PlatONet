using Nethereum.Hex.HexTypes;
using Newtonsoft.Json;

namespace PlatONet
{
    public class RewardInfo
    {
        [JsonProperty("nodeId")]
        public string NodeId { get; set; }
        [JsonProperty("stakingNum")]
        public HexBigInteger StakingNum { get; set; }
        [JsonProperty("reward")]
        public HexBigInteger Reward { get; set; }
        public override string ToString()
        {
            return "Reward{" +
                    "nodeId='" + NodeId + '\'' +
                    ", stakingNum=" + StakingNum +
                    ", reward=" + Reward +
                    '}';
        }
    }
}
