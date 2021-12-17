using System;

namespace PlatONet
{
    /// <summary>
    /// PlatONet异常
    /// </summary>
    public class PlatONException : Exception
    {
        public int ErrorCode { get; set; }
        /// <summary>
        /// 使用ErrorCode初如化异常
        /// </summary>
        /// <param name="errorCode">ErrorCode</param>
        public PlatONException(int errorCode) : base(PlatONet.ErrorCode.GetErrorMsg(errorCode))
        {
            ErrorCode = errorCode;
        }
    }
    /// <summary>
    /// 错误信息
    /// </summary>
    public static class ErrorCode
    {
        /// <summary>
        /// 成功
        /// </summary>
        public const int SUCCESS = 0;
        /// <summary>
        /// 系统内部错误
        /// </summary>
        public const int SYSTEM_ERROR = 1;
        /// <summary>
        /// 对象没有找到
        /// </summary>
        public const int OBJECT_NOT_FOUND = 2;
        /// <summary>
        /// 参数错误
        /// </summary>
        public const int INVALID_PARAMETER = 3;
        /// <summary>
        /// PlatON内置合约执行出错
        /// </summary>
        public const int PlatON_Precompiled_Contract_EXEC_FAILED = 4;
        /// <summary>
        /// bls key 长度有误
        /// </summary>
        public const int WRONG_BLS_KEY_LENGTH = 301000;
        /// <summary>
        /// bls key 证明有误
        /// </summary>
        public const int WRONG_BLS_KEY_PROOF = 301001;
        /// <summary>
        /// 节点描述信息长度有误
        /// </summary>
        public const int WRONG_DESCRIPTION_LENGTH = 301002;
        /// <summary>
        /// 程序版本签名有误
        /// </summary>
        public const int WRONG_PROGRAM_VERSION_SIGN = 301003;
        /// <summary>
        /// 程序的版本太低
        /// </summary>
        public const int PROGRAM_VERSION_SIGN_TOO_LOW = 301004;
        /// <summary>
        /// 版本声明失败
        /// </summary>
        public const int DELCARE_VERSION_FAILED = 301005;
        /// <summary>
        /// 发起交易账户必须和发起质押账户是同一个
        /// </summary>
        public const int ADDRESS_MUST_SAME_AS_INITIATED_STAKING = 301006;
        /// <summary>
        /// 质押的金额太低
        /// </summary>
        public const int STAKING_DEPOSIT_TOO_LOW = 301100;
        /// <summary>
        /// 候选人信息已经存在
        /// </summary>
        public const int CANDIDATE_ALREADY_EXIST = 301101;
        /// <summary>
        /// 候选人信息不存在
        /// </summary>
        public const int CANDIDATE_NOT_EXIST = 301102;
        /// <summary>
        /// 候选人状态已失效
        /// </summary>
        public const int CANDIDATE_STATUS_INVALIDED = 301103;
        /// <summary>
        /// 增持质押金额太低
        /// </summary>
        public const int INCREASE_STAKE_TOO_LOW = 301104;
        /// <summary>
        /// 委托金额太低
        /// </summary>
        public const int DELEGATE_DEPOSIT_TOO_LOW = 301105;
        /// <summary>
        /// 该账户不允许发起委托
        /// </summary>
        public const int ACCOUNT_NOT_ALLOWED_DELEGATING = 301106;
        /// <summary>
        /// 该候选人不接受委托
        /// </summary>
        public const int CANDIDATE_NOT_ALLOWED_DELEGATE = 301107;
        /// <summary>
        /// 撤销委托的金额太低
        /// </summary>
        public const int WITHDRAW_DELEGATE_NOT_EXIST = 301108;
        /// <summary>
        /// 委托详情不存在
        /// </summary>
        public const int DELEGATE_NOT_EXIST = 301109;
        /// <summary>
        /// von操作类型有误 (非自由金额或非锁仓金额)
        /// </summary>
        public const int WRONG_VON_OPERATION_TYPE = 301110;
        /// <summary>
        /// 账户的余额不足
        /// </summary>
        public const int ACCOUNT_BALANCE_NOT_ENOUGH = 301111;
        /// <summary>
        /// 区块高度和预期不匹配
        /// </summary>
        public const int BLOCKNUMBER_DISORDERED = 301112;
        /// <summary>
        /// 委托信息中余额不足
        /// </summary>
        public const int DELEGATE_VON_NOT_ENOUGH = 301113;
        /// <summary>
        /// 撤销委托时金额计算有误
        /// </summary>
        public const int WRONG_WITHDRAW_DELEGATE_VON = 301114;
        /// <summary>
        /// 验证人信息不存在
        /// </summary>
        public const int VALIDATOR_NOT_EXIST = 301115;
        /// <summary>
        /// 参数有误
        /// </summary>
        public const int WRONG_FUNCTION_PARAM = 301116;
        /// <summary>
        /// 惩罚类型有误
        /// </summary>
        public const int WRONG_SLASH_TYPE = 301117;
        /// <summary>
        /// 惩罚扣除的金额溢出
        /// </summary>
        public const int SLASH_AMOUNT_TOO_LARGE = 301118;
        /// <summary>
        /// 惩罚削减质押信息时金额计算有误
        /// </summary>
        public const int WRONG_SLASH_CANDIDATE_VON = 301119;
        /// <summary>
        /// 拉取结算周期验证人列表失败
        /// </summary>
        public const int GETTING_VERIFIERLIST_FAILED = 301200;
        /// <summary>
        /// 拉取共识周期验证人列表失败
        /// </summary>
        public const int GETTING_VALIDATORLIST_FAILED = 301201;
        /// <summary>
        /// 拉取候选人列表失败
        /// </summary>
        public const int GETTING_CANDIDATELIST_FAILED = 301202;
        /// <summary>
        /// 拉取委托关联映射关系失败
        /// </summary>
        public const int GETTING_DELEGATE_FAILED = 301203;
        /// <summary>
        /// 查询候选人详情失败
        /// </summary>
        public const int QUERY_CANDIDATE_INFO_FAILED = 301204;
        /// <summary>
        /// 查询委托详情失败
        /// </summary>
        public const int QUERY_DELEGATE_INFO_FAILED = 301205;

        /// <summary>
        /// 链上生效版本没有找到
        /// </summary>
        public const int ACTIVE_VERSION_NOT_FOUND = 302001;
        /// <summary>
        /// 投票选项错误
        /// </summary>
        public const int VOTE_OPTION_ERROR = 302002;
        /// <summary>
        /// 提案类型错误
        /// </summary>
        public const int PROPOSAL_TYPE_ERROR = 302003;
        /// <summary>
        /// 提案ID为空
        /// </summary>
        public const int PROPOSAL_ID_EMPTY = 302004;
        /// <summary>
        /// 提案ID已经存在
        /// </summary>
        public const int PROPOSAL_ID_ALREADY_EXISTS = 302005;
        /// <summary>
        /// 提案没有找到
        /// </summary>
        public const int PROPOSAL_NOT_FOUND = 302006;
        /// <summary>
        /// PIPID为空
        /// </summary>
        public const int PIPID_EMPTY = 302007;
        /// <summary>
        /// PIPID已经存在
        /// </summary>
        public const int PIPID_ALREADY_EXISTS = 302008;
        /// <summary>
        /// 投票持续的共识轮数量太小
        /// </summary>
        public const int ENDVOTINGROUNDS_TOO_SMALL = 302009;
        /// <summary>
        /// 投票持续的共识轮数量太大
        /// </summary>
        public const int ENDVOTINGROUNDS_TOO_LARGE = 302010;
        /// <summary>
        /// 新版本的大版本应该大于当前生效版本的大版本
        /// </summary>
        public const int NEWVERSION_SHOULD_LARGE_CURRENT_ACTIVE_VERSION = 302011;
        /// <summary>
        /// 有另一个在投票期的升级提案
        /// </summary>
        public const int ANOTHER_VERSION_PROPOSAL_AT_VOTING_STAGE = 302012;
        /// <summary>
        /// 有另一个预生效的升级提案
        /// </summary>
        public const int ANOTHER_VERSION_PROPOSAL_AT_PRE_ACTIVE_STAGE = 302013;
        /// <summary>
        /// 有另一个在投票期的取消提案
        /// </summary>
        public const int ANOTHER_CANCEL_PROPOSAL_AT_VOTING_STAGE = 302014;
        /// <summary>
        /// 待取消的(升级)提案没有找到
        /// </summary>
        public const int CANCELED_PROPOSAL_NOT_FOUND = 302015;
        /// <summary>
        /// 待取消的提案不是升级提案
        /// </summary>
        public const int CANCELED_PROPOSAL_NOT_VERSION_TYPE = 302016;
        /// <summary>
        /// 待取消的(升级)提案不在投票期
        /// </summary>
        public const int CANCELED_PROPOSAL_NOT_AT_VOTING_STAGE = 302017;
        /// <summary>
        /// 提案人NodeID为空
        /// </summary>
        public const int PROPOSER_EMPTY = 302018;
        /// <summary>
        /// 验证人详情没有找到
        /// </summary>
        public const int VERIFIER_DETAIL_INFO_NOT_FOUND = 302019;
        /// <summary>
        /// 验证人状态为无效状态
        /// </summary>
        public const int VERIFIER_STATUS_INVALID = 302020;
        /// <summary>
        /// 发起交易账户和发起质押账户不是同一个
        /// </summary>
        public const int TX_CALLER_DIFFER_FROM_STAKING = 302021;
        /// <summary>
        /// 发起交易的节点不是验证人
        /// </summary>
        public const int TX_CALLER_NOT_VERIFIER = 302022;
        /// <summary>
        /// 发起交易的节点不是候选人
        /// </summary>
        public const int TX_CALLER_NOT_CANDIDATE = 302023;
        /// <summary>
        /// 版本签名错误
        /// </summary>
        public const int VERSION_SIGN_ERROR = 302024;
        /// <summary>
        /// 验证人没有升级到新版本
        /// </summary>
        public const int VERIFIER_NOT_UPGRADED = 302025;
        /// <summary>
        /// 提案不在投票期
        /// </summary>
        public const int PROPOSAL_NOT_AT_VOTING_STAGE = 302026;
        /// <summary>
        /// 投票重复
        /// </summary>
        public const int VOTE_DUPLICATED = 302027;
        /// <summary>
        /// 声明的版本错误
        /// </summary>
        public const int DECLARE_VERSION_ERROR = 302028;
        /// <summary>
        /// 把节点声明的版本通知Staking时出错
        /// </summary>
        public const int NOTIFY_STAKING_DECLARED_VERSION_ERROR = 302029;
        /// <summary>
        /// 提案结果没有找到
        /// </summary>
        public const int TALLY_RESULT_NOT_FOUND = 302030;
        /// <summary>
        /// 不支持的治理参数
        /// </summary>
        public const int UNSUPPORTED_GOVERN_PARAMETER = 302031;
        /// <summary>
        /// 有另一个在投票期的参数提案
        /// </summary>
        public const int ANOTHER_PARAM_PROPOSAL_AT_VOTING_STAGE = 302032;
        /// <summary>
        /// 参数提案的的参数值错误
        /// </summary>
        public const int GOVERN_PARAMETER_VALUE_ERROR = 302033;
        /// <summary>
        /// 参数提案的值必须和旧值不同
        /// </summary>
        public const int PARAMETER_PROPOSAL_NEW_VALUE_SAME_AS_OLD_VALUE = 302034;
        /// <summary>
        /// 双签证据校验失败
        /// </summary>
        public const int DUPLICATE_SIGNATURE_VERIFICATION_FAILED = 303000;
        /// <summary>
        /// 已根据该证据执行过惩罚
        /// </summary>
        public const int PUNISHMENT_HAS_BEEN_IMPLEMENTED = 303001;
        /// <summary>
        /// 举报的双签块高比当前区块高
        /// </summary>
        public const int BLOCKNUMBER_TOO_HIGH = 303002;
        /// <summary>
        /// 举报的证据超过有效期
        /// </summary>
        public const int EVIDENCE_INTERVAL_TOO_LONG = 303003;
        /// <summary>
        /// 获取举报的验证人信息失败
        /// </summary>
        public const int GET_CERTIFIER_INFOMATION_FAILED = 303004;
        /// <summary>
        /// 证据的地址和验证人的地址不匹配
        /// </summary>
        public const int ADDRESS_NOT_MATCH = 303005;
        /// <summary>
        /// 证据的节点ID和验证人的节点ID不匹配
        /// </summary>
        public const int NODEID_NOT_MATCH = 303006;
        /// <summary>
        /// 证据的blsPubKey和验证人的blsPubKey不匹配
        /// </summary>
        public const int BLS_PUBKEY_NOT_MATCH = 303007;
        /// <summary>
        /// 惩罚节点失败
        /// </summary>
        public const int SLASH_NODE_FAILED = 303008;
        /// <summary>
        /// 创建锁仓计划数不能为0或者大于36
        /// </summary>
        public const int PARAM_EPOCH_CANNOT_BE_ZERO = 304001;
        /// <summary>
        /// 创建锁仓计划数不能为0或者大于36
        /// </summary>
        public const int RESTRICTING_PLAN_NUMBER_CANNOT_BE_0_OR_MORE_THAN_36 = 304002;
        /// <summary>
        /// 锁仓创建总金额不能小于1E18
        /// </summary>
        public const int TOTAL_RESTRICTING_AMOUNT_SHOULD_MORE_THAN_ONE = 304003;
        /// <summary>
        /// 账户余额不够支付锁仓
        /// </summary>
        public const int BALANCE_NOT_ENOUGH_FOR_RESTRICT = 304004;
        /// <summary>
        /// 没有在锁仓合约中找到该账户
        /// </summary>
        public const int RESTRICTING_CONTRACT_AMOUNT_NOT_FOUND = 304005;
        /// <summary>
        /// 惩罚金额大于质押金额
        /// </summary>
        public const int SLASH_AMOUNT_LARGER_THAN_STAKING_AMOUNT = 304006;
        /// <summary>
        /// 惩罚锁仓账户的质押金额不能为0
        /// </summary>
        public const int STAKING_AMOUNT_ZERO = 304007;
        /// <summary>
        /// 锁仓转质押后回退的金额不能小于0
        /// </summary>
        public const int AMOUNT_CANNOT_LESS_THAN_ZERO = 304008;
        /// <summary>
        /// 锁仓信息中的质押金额小于回退的金额
        /// </summary>
        public const int WRONG_STAKING_RETURN_AMOUNT = 304009;
        /// <summary>
        /// 由ErrorCode返回错误详情
        /// </summary>
        /// <param name="errorCode">ErrorCode</param>
        /// <returns>错误详情</returns>
        public static string GetErrorMsg(int errorCode)
        {
            switch (errorCode)
            {
                case SUCCESS:
                    return "成功";
                case SYSTEM_ERROR:
                    return "系统内部错误";
                case OBJECT_NOT_FOUND:
                    return "对象没有找到";
                case INVALID_PARAMETER:
                    return "参数错误";
                case WRONG_BLS_KEY_LENGTH:
                    return "bls key 长度有误";
                case WRONG_BLS_KEY_PROOF:
                    return "bls key 证明有误";
                case WRONG_DESCRIPTION_LENGTH:
                    return "节点描述信息长度有误";
                case WRONG_PROGRAM_VERSION_SIGN:
                    return "程序版本签名有误";
                case PROGRAM_VERSION_SIGN_TOO_LOW:
                    return "程序的版本太低";
                case DELCARE_VERSION_FAILED:
                    return "版本声明失败";
                case ADDRESS_MUST_SAME_AS_INITIATED_STAKING:
                    return "发起交易账户必须和发起质押账户是同一个";
                case STAKING_DEPOSIT_TOO_LOW:
                    return "质押的金额太低";
                case CANDIDATE_ALREADY_EXIST:
                    return "候选人信息已经存在";
                case CANDIDATE_NOT_EXIST:
                    return "候选人信息不存在";
                case CANDIDATE_STATUS_INVALIDED:
                    return "候选人状态已失效";
                case INCREASE_STAKE_TOO_LOW:
                    return "增持质押金额太低";
                case DELEGATE_DEPOSIT_TOO_LOW:
                    return "委托金额太低";
                case ACCOUNT_NOT_ALLOWED_DELEGATING:
                    return "该候选人不接受委托";
                case CANDIDATE_NOT_ALLOWED_DELEGATE:
                    return "撤销委托的金额太低";
                case WITHDRAW_DELEGATE_NOT_EXIST:
                    return "撤销委托的金额太低";
                case DELEGATE_NOT_EXIST:
                    return "委托详情不存在";
                case WRONG_VON_OPERATION_TYPE:
                    return "von操作类型有误 (非自由金额或非锁仓金额)";
                case ACCOUNT_BALANCE_NOT_ENOUGH:
                    return "账户的余额不足";
                case BLOCKNUMBER_DISORDERED:
                    return "区块高度和预期不匹配";
                case DELEGATE_VON_NOT_ENOUGH:
                    return "委托信息中余额不足";
                case WRONG_WITHDRAW_DELEGATE_VON:
                    return "撤销委托时金额计算有误";
                case VALIDATOR_NOT_EXIST:
                    return "验证人信息不存在";
                case WRONG_FUNCTION_PARAM:
                    return "参数有误";
                case WRONG_SLASH_TYPE:
                    return "惩罚类型有误";
                case SLASH_AMOUNT_TOO_LARGE:
                    return "惩罚扣除的金额溢出";
                case WRONG_SLASH_CANDIDATE_VON:
                    return "惩罚削减质押信息时金额计算有误";
                case GETTING_VERIFIERLIST_FAILED:
                    return "拉取结算周期验证人列表失败";
                case GETTING_VALIDATORLIST_FAILED:
                    return "拉取共识周期验证人列表失败";
                case GETTING_CANDIDATELIST_FAILED:
                    return "拉取候选人列表失败";
                case GETTING_DELEGATE_FAILED:
                    return "拉取委托关联映射关系失败";
                case QUERY_CANDIDATE_INFO_FAILED:
                    return "查询候选人详情失败";
                case QUERY_DELEGATE_INFO_FAILED:
                    return "查询委托详情失败";
                case ACTIVE_VERSION_NOT_FOUND:
                    return "链上生效版本没有找到";
                case VOTE_OPTION_ERROR:
                    return "投票选项错误";
                case PROPOSAL_TYPE_ERROR:
                    return "提案类型错误";
                case PROPOSAL_ID_EMPTY:
                    return "提案ID为空";
                case PROPOSAL_ID_ALREADY_EXISTS:
                    return "提案ID已经存在";
                case PROPOSAL_NOT_FOUND:
                    return "提案没有找到";
                case PIPID_EMPTY:
                    return "PIPID为空";
                case PIPID_ALREADY_EXISTS:
                    return "PIPID已经存在";
                case ENDVOTINGROUNDS_TOO_SMALL:
                    return "投票持续的共识轮数量太小";
                case ENDVOTINGROUNDS_TOO_LARGE:
                    return "投票持续的共识轮数量太大";
                case NEWVERSION_SHOULD_LARGE_CURRENT_ACTIVE_VERSION:
                    return "新版本的大版本应该大于当前生效版本的大版本";
                case ANOTHER_VERSION_PROPOSAL_AT_VOTING_STAGE:
                    return "有另一个在投票期的升级提案";
                case ANOTHER_VERSION_PROPOSAL_AT_PRE_ACTIVE_STAGE:
                    return "有另一个预生效的升级提案";
                case ANOTHER_CANCEL_PROPOSAL_AT_VOTING_STAGE:
                    return "有另一个在投票期的取消提案";
                case CANCELED_PROPOSAL_NOT_FOUND:
                    return "待取消的(升级)提案没有找到";
                case CANCELED_PROPOSAL_NOT_VERSION_TYPE:
                    return "待取消的提案不是升级提案";
                case CANCELED_PROPOSAL_NOT_AT_VOTING_STAGE:
                    return "待取消的(升级)提案不在投票期";
                case PROPOSER_EMPTY:
                    return "提案人NodeID为空";
                case VERIFIER_DETAIL_INFO_NOT_FOUND:
                    return "验证人详情没有找到";
                case VERIFIER_STATUS_INVALID:
                    return "验证人状态为无效状态";
                case TX_CALLER_DIFFER_FROM_STAKING:
                    return "发起交易账户和发起质押账户不是同一个";
                case TX_CALLER_NOT_VERIFIER:
                    return "发起交易的节点不是验证人";
                case TX_CALLER_NOT_CANDIDATE:
                    return "发起交易的节点不是候选人";
                case VERSION_SIGN_ERROR:
                    return "版本签名错误";
                case VERIFIER_NOT_UPGRADED:
                    return "验证人没有升级到新版本";
                case PROPOSAL_NOT_AT_VOTING_STAGE:
                    return "提案不在投票期";
                case VOTE_DUPLICATED:
                    return "投票重复";
                case DECLARE_VERSION_ERROR:
                    return "声明的版本错误";
                case NOTIFY_STAKING_DECLARED_VERSION_ERROR:
                    return "把节点声明的版本通知Staking时出错";
                case TALLY_RESULT_NOT_FOUND:
                    return "提案结果没有找到";
                case UNSUPPORTED_GOVERN_PARAMETER:
                    return "不支持的治理参数";
                case ANOTHER_PARAM_PROPOSAL_AT_VOTING_STAGE:
                    return "有另一个在投票期的参数提案";
                case GOVERN_PARAMETER_VALUE_ERROR:
                    return "参数提案的的参数值错误";
                case PARAMETER_PROPOSAL_NEW_VALUE_SAME_AS_OLD_VALUE:
                    return "参数提案的值必须和旧值不同";
                case DUPLICATE_SIGNATURE_VERIFICATION_FAILED:
                    return "双签证据校验失败";
                case PUNISHMENT_HAS_BEEN_IMPLEMENTED:
                    return "已根据该证据执行过惩罚";
                case BLOCKNUMBER_TOO_HIGH:
                    return "举报的双签块高比当前区块高";
                case EVIDENCE_INTERVAL_TOO_LONG:
                    return "举报的证据超过有效期";
                case GET_CERTIFIER_INFOMATION_FAILED:
                    return "获取举报的验证人信息失败";
                case ADDRESS_NOT_MATCH:
                    return "证据的地址和验证人的地址不匹配";
                case NODEID_NOT_MATCH:
                    return "证据的节点ID和验证人的节点ID不匹配";
                case BLS_PUBKEY_NOT_MATCH:
                    return "证据的blsPubKey和验证人的blsPubKey不匹配";
                case SLASH_NODE_FAILED:
                    return "惩罚节点失败";
                case PARAM_EPOCH_CANNOT_BE_ZERO:
                    return "创建锁仓计划票龄不能为0";
                case RESTRICTING_PLAN_NUMBER_CANNOT_BE_0_OR_MORE_THAN_36:
                    return "创建锁仓计划数不能为0或者大于36";
                case TOTAL_RESTRICTING_AMOUNT_SHOULD_MORE_THAN_ONE:
                    return "锁仓创建总金额不能小于1E18";
                case BALANCE_NOT_ENOUGH_FOR_RESTRICT:
                    return "账户余额不够支付锁仓";
                case RESTRICTING_CONTRACT_AMOUNT_NOT_FOUND:
                    return "没有在锁仓合约中找到该账户";
                case SLASH_AMOUNT_LARGER_THAN_STAKING_AMOUNT:
                    return "惩罚金额大于质押金额";
                case STAKING_AMOUNT_ZERO:
                    return "惩罚锁仓账户的质押金额不能为0";
                case AMOUNT_CANNOT_LESS_THAN_ZERO:
                    return "锁仓转质押后回退的金额不能小于0";
                case WRONG_STAKING_RETURN_AMOUNT:
                    return "锁仓信息中的质押金额小于回退的金额";
                default:
                    return "";

            }
        }

    }
}
