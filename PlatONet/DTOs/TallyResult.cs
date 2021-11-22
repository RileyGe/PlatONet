using Nethereum.Hex.HexTypes;
using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace PlatONet
{
    /**
 * 投票结果
 */
    public class TallyResult
    {
        /**
         * 提案ID
         */
        [JsonProperty("proposalID")]
        public string ProposalID { get; set; }
        /**
         * 赞成票
         */
        [JsonProperty("yeas")]
        public HexBigInteger Yeas { get; set; }
        /**
         * 反对票
         */
        [JsonProperty("nays")]
        public HexBigInteger Nays { get; set; }
        /**
         * 弃权票
         */
        [JsonProperty("abstentions")]
        public HexBigInteger Abstentions { get; set; }
        /**
         * 在整个投票期内有投票资格的验证人总数
         */
        [JsonProperty("accuVerifiers")]
        public HexBigInteger AccuVerifiers { get; set; }
        /**
         * 状态
         */
        [JsonProperty("status")]
        public int Status { get; set; }
        public override string ToString()
        {
            return "TallyResult{" +
                    "proposalID='" + ProposalID + '\'' +
                    ", yeas=" + Yeas +
                    ", nays=" + Nays +
                    ", abstentions=" + Abstentions +
                    ", accuVerifiers=" + AccuVerifiers +
                    ", status=" + Status +
                    '}';
        }
    }
}
