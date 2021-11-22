using Newtonsoft.Json;
using System;
using System.Numerics;

namespace PlatONet
{
    /**
 * @Author liushuyu
 * @Date 2021/7/6 14:38
 * @Version
 * @Desc
 */
    public class WaitSlashingNode
    {
        //零出块的节点ID
        [JsonProperty("NodeId")]
        public String NodeId { get; set; }

        //观察期内第一次零出块时所处共识轮数
        [JsonProperty("Round")]
        public BigInteger Round { get; set; }

        //零出块次数位图（从Round开始，1表示该轮未出块）
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
