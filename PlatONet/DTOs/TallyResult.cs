using Nethereum.Hex.HexTypes;
using Newtonsoft.Json;

namespace PlatONet.DTOs
{
    /// <summary>
    /// 投票结果
    /// </summary>
    public class TallyResult
    {
        /// <summary>
        /// 提案ID
        /// </summary>
        [JsonProperty("proposalID")]
        public string ProposalID { get; set; }
        /// <summary>
        /// 赞成票
        /// </summary>
        [JsonProperty("yeas")]
        public HexBigInteger Yeas { get; set; }
        /// <summary>
        /// 反对票
        /// </summary>
        [JsonProperty("nays")]
        public HexBigInteger Nays { get; set; }
        /// <summary>
        /// 弃权票
        /// </summary>
        [JsonProperty("abstentions")]
        public HexBigInteger Abstentions { get; set; }
        /// <summary>
        /// 在整个投票期内有投票资格的验证人总数
        /// </summary>
        [JsonProperty("accuVerifiers")]
        public HexBigInteger AccuVerifiers { get; set; }
        /// <summary>
        /// 状态<br/>
        /// 1 投票中<br/>
        /// 2 投票通过<br/>
        /// 3 投票失败<br/>
        /// 4 （升级提案）预生效<br/>
        /// 5 （升级提案）生效<br/>
        /// 6 被取消<br/>
        /// </summary>
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
