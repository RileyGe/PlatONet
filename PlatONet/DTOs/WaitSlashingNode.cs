using Newtonsoft.Json;
using System;
using System.Numerics;

namespace PlatONet
{
    /// <summary>
    /// 取零出块的节点信息
    /// </summary>
    public class WaitSlashingNode
    {
        /// <summary>
        /// 零出块的节点ID
        /// </summary>
        [JsonProperty("NodeId")]
        public String NodeId { get; set; }

        /// <summary>
        /// 观察期内第一次零出块时所处共识轮数
        /// </summary>
        [JsonProperty("Round")]
        public BigInteger Round { get; set; }

        /// <summary>
        /// 零出块次数位图（从Round开始，1表示该轮未出块）
        /// </summary>
        [JsonProperty("CountBit")]
        public BigInteger CountBit { get; set; }
        public override string ToString()
        {
            return "WaitSlashingNode{" +
                    "nodeId='" + NodeId + '\'' +
                    ", round=" + Round +
                    ", countBit=" + CountBit +
                    '}';
        }
    }
}
