using Nethereum.Hex.HexTypes;
using Newtonsoft.Json;

namespace PlatONet.DTOs
{
    /// <summary>
    /// 委托节点的ID和质押区块高度信息
    /// </summary>
    public class DelegationIdInfo
    {
        /// <summary>
        /// 验证人节点的地址
        /// </summary>
        [JsonProperty("Addr")]
        public string Address { get; set; }
        /// <summary>
        /// 验证人的节点Id
        /// </summary>
        [JsonProperty("NodeId")]
        public string NodeId { get; set; }
        /// <summary>
        /// 发起质押时的区块高度
        /// </summary>
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
