using Nethereum.Hex.HexTypes;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace PlatONet
{
    public class RestrictingPlan
    {

        /// <summary>
        /// 表示结算周期的倍数。与每个结算周期出块数的乘积表示在目标区块高度上释放锁定的资金。如果 account 是激励池地址，
        /// 那么 period 值是 120（即，30*4） 的倍数。另外，period* 每周期的区块数至少要大于最高不可逆区块高度
        /// </summary>
        [JsonProperty("Epoch")]
        private HexBigInteger epoch;

        /// <summary>
        /// 表示目标区块上待释放的金额
        /// </summary>
        [JsonProperty("Amount")]
        private HexBigInteger amount;

        public RestrictingPlan(HexBigInteger epoch, HexBigInteger amount)
        {
            this.epoch = epoch;
            this.amount = amount;
        }
        /// <summary>
        /// 生成byte[]数组，用于RLP编码
        /// </summary>
        public List<byte[]> BytesListForRLPEncoding
        {
            get
            {
                return new List<byte[]>()
                {
                    epoch.ToHexByteArray(),
                    amount.ToHexByteArray()
                };
            }
        }
    }
}
