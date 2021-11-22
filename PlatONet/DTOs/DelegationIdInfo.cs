using Nethereum.Hex.HexTypes;
using Newtonsoft.Json;

namespace PlatONet
{
    public class DelegationIdInfo
    {
        /**
         * 验证人节点的地址
         */
        [JsonProperty("Addr")]
        public string Address { get; set; }
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
        public override string ToString()
        {
            return "DelegationIdInfo{" +
                    "address='" + Address + '\'' +
                    ", nodeId='" + NodeId + '\'' +
                    ", stakingBlockNum=" + StakingBlockNum +
                    '}';
        }
    }
}
