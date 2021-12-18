using Nethereum.Hex.HexTypes;
using Newtonsoft.Json;

namespace PlatONet
{
    public class RewardInfo
    {
        /// <summary>
        /// 节点ID
        /// </summary>
        [JsonProperty("nodeId")]
        public string NodeId { get; set; }
        /// <summary>
        /// 节点的质押块高
        /// </summary>
        [JsonProperty("stakingNum")]
        public HexBigInteger StakingNum { get; set; }
        /// <summary>
        /// 领取到的收益
        /// </summary>
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
