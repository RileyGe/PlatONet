namespace PlatONet
{
    /// <summary>
    /// 系统合约方法类型码
    /// </summary>
    public class PPOSFunctionType
    {
        /// <summary>
        /// 发起质押
        /// </summary>
        public const int STAKING_FUNC_TYPE = 1000;
        /// <summary>
        /// 修改质押信息
        /// </summary>
        public const int UPDATE_STAKING_INFO_FUNC_TYPE = 1001;
        /// <summary>
        /// 增持质押
        /// </summary>
        public const int ADD_STAKING_FUNC_TYPE = 1002;
        /// <summary>
        /// 撤销质押(一次性发起全部撤销，多次到账)
        /// </summary>
        public const int WITHDREW_STAKING_FUNC_TYPE = 1003;
        /// <summary>
        /// 发起委托
        /// </summary>
        public const int DELEGATE_FUNC_TYPE = 1004;
        /// <summary>
        /// 减持/撤销委托(全部减持就是撤销)
        /// </summary>
        public const int WITHDREW_DELEGATE_FUNC_TYPE = 1005;
        /// <summary>
        /// 查询当前结算周期的验证人队列
        /// </summary>
        public const int GET_VERIFIERLIST_FUNC_TYPE = 1100;
        /// <summary>
        /// 查询当前共识周期的验证人列表
        /// </summary>
        public const int GET_VALIDATORLIST_FUNC_TYPE = 1101;
        /// <summary>
        /// 查询所有实时的候选人列表
        /// </summary>
        public const int GET_CANDIDATELIST_FUNC_TYPE = 1102;
        /// <summary>
        /// 查询当前账户地址所委托的节点的NodeID和质押Id
        /// </summary>
        public const int GET_DELEGATELIST_BYADDR_FUNC_TYPE = 1103;
        /// <summary>
        /// 查询当前单个委托信息
        /// </summary>
        public const int GET_DELEGATEINFO_FUNC_TYPE = 1104;
        /// <summary>
        /// 查询当前节点的质押信息
        /// </summary>
        public const int GET_STAKINGINFO_FUNC_TYPE = 1105;
        /// <summary>
        /// 查询当前结算周期的区块奖励
        /// </summary>
        public const int GET_PACKAGEREWARD_FUNC_TYPE = 1200;
        /// <summary>
        /// 查询当前结算周期的质押奖励
        /// </summary>
        public const int GET_STAKINGREWARD_FUNC_TYPE = 1201;
        /// <summary>
        /// 查询打包区块的平均时间
        /// </summary>
        public const int GET_AVGPACKTIME_FUNC_TYPE = 1202;
        /// <summary>
        /// 提交文本提案
        /// </summary>
        public const int SUBMIT_TEXT_FUNC_TYPE = 2000;
        /// <summary>
        /// 提交升级提案
        /// </summary>
        public const int SUBMIT_VERSION_FUNC_TYPE = 2001;
        /// <summary>
        /// 提交参数提案
        /// </summary>
        public const int SUBMIR_PARAM_FUNCTION_TYPE = 2002;
        /// <summary>
        /// 给提案投票
        /// </summary>
        public const int VOTE_FUNC_TYPE = 2003;
        /// <summary>
        /// 版本声明
        /// </summary>
        public const int DECLARE_VERSION_FUNC_TYPE = 2004;
        /// <summary>
        /// 提交取消提案
        /// </summary>
        public const int SUBMIT_CANCEL_FUNC_TYPE = 2005;
        /// <summary>
        /// 查询提案
        /// </summary>
        public const int GET_PROPOSAL_FUNC_TYPE = 2100;
        /// <summary>
        /// 查询提案结果
        /// </summary>
        public const int GET_TALLY_RESULT_FUNC_TYPE = 2101;
        /// <summary>
        /// 查询提案列表
        /// </summary>
        public const int GET_PROPOSAL_LIST_FUNC_TYPE = 2102;
        /// <summary>
        /// 查询提案生效版本
        /// </summary>
        public const int GET_ACTIVE_VERSION = 2103;
        /// <summary>
        /// 查询当前块高的治理参数值
        /// </summary>
        public const int GET_GOVERN_PARAM_VALUE = 2104;
        /// <summary>
        /// 查询提案的累积可投票人数
        /// </summary>
        public const int GET_ACCUVERIFIERS_COUNT = 2105;
        /// <summary>
        /// 查询可治理列表
        /// </summary>
        public const int GET_PARAM_LIST = 2106;
        /// <summary>
        /// 举报双签
        /// </summary>
        public const int REPORT_DOUBLESIGN_FUNC_TYPE = 3000;
        /// <summary>
        /// 查询节点是否已被举报过多签
        /// </summary>
        public const int CHECK_DOUBLESIGN_FUNC_TYPE = 3001;
        /// <summary>
        /// 创建锁仓计划
        /// </summary>
        public const int CREATE_RESTRICTINGPLAN_FUNC_TYPE = 4000;
        /// <summary>
        /// 获取锁仓信息
        /// </summary>
        public const int GET_RESTRICTINGINFO_FUNC_TYPE = 4100;
        /// <summary>
        /// 提取账户当前所有的可提取的委托奖励
        /// </summary>
        public const int WITHDRAW_DELEGATE_REWARD_FUNC_TYPE = 5000;
        /// <summary>
        /// 查询账户在各节点未提取委托奖励
        /// </summary>
        public const int GET_DELEGATE_REWARD_FUNC_TYPE = 5100;
    }
}
