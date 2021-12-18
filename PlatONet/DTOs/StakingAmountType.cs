namespace PlatONet
{
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
}
