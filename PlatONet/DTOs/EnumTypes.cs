using System;
using System.Collections.Generic;
using System.Text;

namespace PlatONet.DTOs
{
    public enum DuplicateSignType
    {
        PREPARE_BLOCK = 1,
        PREPARE_VOTE = 2,
        VIEW_CHANGE = 3
    }
    /// <summary>
    /// 使用账户自由金额还是账户的锁仓金额做质押
    /// </summary>
    public enum StakingAmountType
    {
        /// <summary>
        /// 自由金额
        /// </summary>
        FREE_AMOUNT_TYPE = 0,
        /// <summary>
        /// 锁仓金额
        /// </summary>
        RESTRICTING_AMOUNT_TYPE = 1,
        /// <summary>
        /// 优先使用锁仓余额，锁仓余额不足则剩下的部分使用自由金额
        /// </summary>
        AUTO_AMOUNT_TYPE = 2
    }
    /// <summary>
    /// 投标选项
    /// </summary>
    public enum VoteOption
    {
        /// <summary>
        /// 赞成票
        /// </summary>
        YEAS = 1,
        /// <summary>
        /// 反对票
        /// </summary>
        NAYS = 2,
        /// <summary>
        /// 弃权票
        /// </summary>
        ABSTENTIONS = 3
    }
}
