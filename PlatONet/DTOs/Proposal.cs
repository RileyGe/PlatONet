using Nethereum.Hex.HexTypes;
using Newtonsoft.Json;
using System.Collections.Generic;
using Nethereum.Hex.HexConvertors.Extensions;
using Nethereum.RLP;

namespace PlatONet.DTOs
{
    /// <summary>
    /// 提案信息
    /// </summary>
    public class Proposal
    {
        /// <summary>
        /// 提案id
        /// </summary>
        [JsonProperty("ProposalID")]
        public string ProposalId { get; set; }
        /// <summary>
        /// 提案节点ID
        /// </summary>
        [JsonProperty("Proposer")]
        public string Proposer { get; set; }
        /// <summary>
        /// 提案类型， 0x01：文本提案； 0x02：升级提案；0x03参数提案<br/>
        /// 详情请参照<see cref="PPOSProposalType"/>
        /// </summary>
        [JsonProperty("ProposalType")]
        public PPOSProposalType ProposalType { get; set; }
        /// <summary>
        /// 提案PIPID
        /// </summary>
        [JsonProperty("PIPID")]
        public string PiPid { get; set; }
        /// <summary>
        /// 提交提案的块高
        /// </summary>
        [JsonProperty("SubmitBlock")]
        public HexBigInteger SubmitBlock { get; set; }
        /// <summary>
        /// 提案投票结束的块高
        /// </summary>
        [JsonProperty("EndVotingBlock")]
        public HexBigInteger EndVotingBlock { get; set; }
        /// <summary>
        /// 升级版本
        /// </summary>
        [JsonProperty("NewVersion")]
        public HexBigInteger NewVersion { get; set; }
        /// <summary>
        /// 提案要取消的升级提案ID
        /// </summary>
        [JsonProperty("TobeCanceled")]
        public string ToBeCanceled { get; set; }
        /// <summary>
        /// （如果投票通过）生效块高（endVotingBlock + 20 + 4*250 &lt; 生效块高 &lt;= endVotingBlock + 20 + 10*250）
        /// </summary>
        [JsonProperty("ActiveBlock")]
        public HexBigInteger ActiveBlock { get; set; }

        /// <summary>
        /// 提交提案的验证人
        /// </summary>
        public string Verifier { get; set; }
        /// <summary>
        /// 参数模块
        /// </summary>
        public string Module { get; set; }
        /// <summary>
        /// 参数名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 参数新值
        /// </summary>
        public string NewValue { get; set; }
        /// <summary>
        /// 无参数构造函数
        /// </summary>
        public Proposal()
        {

        }
        /// <summary>
        /// 使用<see cref="Builder"/>对象构建实例
        /// </summary>
        /// <param name="builder"><see cref="Builder"/>对象</param>
        public Proposal(Builder builder)
        {
            this.ProposalId = builder.proposalId;
            this.Proposer = builder.proposer;
            this.ProposalType = builder.proposalType;
            this.PiPid = builder.piPid;
            this.SubmitBlock = builder.submitBlock;
            this.EndVotingBlock = builder.endVotingBlock;
            this.NewVersion = builder.newVersion;
            this.ToBeCanceled = builder.toBeCanceled;
            this.ActiveBlock = builder.activeBlock;
            this.Verifier = builder.verifier;
            this.Module = builder.module;
            this.Name = builder.name;
            this.NewValue = builder.newValue;
        }
        /// <summary>
        /// 根据不同的提案类型构建智能合约输入参数
        /// </summary>
        public List<byte[]> SubmitInputParameters
        {
            get
            {
                if (ProposalType == PPOSProposalType.TEXT_PROPOSAL)
                {
                    return new List<byte[]>()
                    {
                        Verifier.HexToByteArray(),
                        PiPid.ToBytesForRLPEncoding()
                    };
                }
                else if (ProposalType == PPOSProposalType.VERSION_PROPOSAL)
                {
                    return new List<byte[]>()
                    {
                        Verifier.HexToByteArray(),
                        PiPid.ToBytesForRLPEncoding(),
                        NewVersion.ToHexByteArray(),
                        EndVotingBlock.ToHexByteArray()
                    };
                }
                else if (ProposalType == PPOSProposalType.CANCEL_PROPOSAL)
                {
                    return new List<byte[]>()
                    {
                        Verifier.HexToByteArray(),
                        PiPid.ToBytesForRLPEncoding(),
                        EndVotingBlock.ToHexByteArray(),
                        ToBeCanceled.ToBytesForRLPEncoding()
                    };
                }
                else if (ProposalType == PPOSProposalType.PARAM_PROPOSAL)
                {
                    return new List<byte[]>()
                    {
                        Verifier.HexToByteArray(),
                        PiPid.ToBytesForRLPEncoding(),
                        Module.ToBytesForRLPEncoding(),
                        Name.ToBytesForRLPEncoding(),
                        NewValue.ToBytesForRLPEncoding()
                    };
                }
                return new List<byte[]>();
            }
        }
        /// <summary>
        /// 根据不同类型的提案类型选择<see cref="PPOSFunctionType"/>
        /// </summary>
        public int SubmitFunctionType
        {
            get
            {
                if (ProposalType == PPOSProposalType.TEXT_PROPOSAL)
                {
                    return PPOSFunctionType.SUBMIT_TEXT_FUNC_TYPE;
                }
                else if (ProposalType == PPOSProposalType.VERSION_PROPOSAL)
                {
                    return PPOSFunctionType.SUBMIT_VERSION_FUNC_TYPE;
                }
                else if (ProposalType == PPOSProposalType.CANCEL_PROPOSAL)
                {
                    return PPOSFunctionType.SUBMIT_CANCEL_FUNC_TYPE;
                }
                else
                {
                    return PPOSFunctionType.SUBMIR_PARAM_FUNCTION_TYPE;
                }
            }
        }
        /// <summary>
        /// 文本提案快捷构建方法
        /// </summary>
        /// <param name="verifier">提交提案的验证人</param>
        /// <param name="pIDID">PIPID</param>
        /// <returns>提案对象</returns>
        public static Proposal CreateSubmitTextProposalParam(string verifier, string pIDID)
        {
            return new Builder()
                    .SetProposalType(PPOSProposalType.TEXT_PROPOSAL)
                    .SetVerifier(verifier)
                    .SetPiPid(pIDID)
                    .Build();
        }
        /// <summary>
        /// 升级提案快捷构建方法
        /// </summary>
        /// <param name="verifier">提交提案的验证人</param>
        /// <param name="pIDID">PIPID</param>
        /// <param name="newVersion">升级版本</param>
        /// <param name="endVotingRounds">投票共识轮数量。<br/>
        /// 说明：假设提交提案的交易，被打包进块时的共识轮序号时round1，则提案投票截止块高，
        /// 就是round1 + endVotingRounds这个共识轮的第230个块高（假设一个共识轮出块250，ppos揭榜提前20个块高，250，20都是可配置的 ），
        /// 其中0 &lt; endVotingRounds &lt;= 4840（约为2周，实际论述根据配置可计算），且为整数）</param>
        /// <returns>提案对象</returns>
        public static Proposal CreateSubmitVersionProposalParam(string verifier, string pIDID, HexBigInteger newVersion, HexBigInteger endVotingRounds)
        {
            return new Builder()
                    .SetProposalType(PPOSProposalType.VERSION_PROPOSAL)
                    .SetVerifier(verifier)
                    .SetPiPid(pIDID)
                    .SetNewVersion(newVersion)
                    .SetEndVotingBlock(endVotingRounds)
                    .Build();
        }
        /// <summary>
        /// 参数提案快捷构建方法
        /// </summary>
        /// <param name="verifier">提交提案的验证人</param>
        /// <param name="pIDID">PIPID</param>
        /// <param name="endVotingRounds">投票共识轮数量。<br/>
        /// 参考<see cref="CreateSubmitVersionProposalParam(string, string, HexBigInteger, HexBigInteger)"/>的说明。
        /// 同时，此接口中此参数的值不能大于对应升级提案中的</param>
        /// <param name="tobeCanceledProposalID">待取消的提案ID</param>
        /// <returns>提案对象</returns>
        public static Proposal CreateSubmitCancelProposalParam(string verifier, string pIDID, HexBigInteger endVotingRounds, string tobeCanceledProposalID)
        {
            return new Builder()
                    .SetProposalType(PPOSProposalType.CANCEL_PROPOSAL)
                    .SetVerifier(verifier)
                    .SetPiPid(pIDID)
                    .SetEndVotingBlock(endVotingRounds)
                    .SetToBeCanceled(tobeCanceledProposalID)
                    .Build();
        }
        /// <summary>
        /// 参数提案快捷构建方法
        /// </summary>
        /// <param name="verifier">提交提案的验证人</param>
        /// <param name="pIDID">PIPID</param>
        /// <param name="module">参数模块</param>
        /// <param name="name">参数名称</param>
        /// <param name="newValue">参数新值</param>
        /// <returns>提案对象</returns>
        public static Proposal CreateSubmitParamProposalParam(string verifier, string pIDID, string module, string name, string newValue)
        {
            return new Builder()
                    .SetProposalType(PPOSProposalType.PARAM_PROPOSAL)
                    .SetVerifier(verifier)
                    .SetPiPid(pIDID)
                    .SetModule(module)
                    .SetName(name)
                    .SetNewValue(newValue)
                    .Build();
        }
        /// <summary>
        /// <see cref="Proposal"/>生成器
        /// </summary>
        public class Builder
        {
            /// <summary>
            /// 提案id
            /// </summary>
            public string proposalId;
            /// <summary>
            /// 提案节点ID
            /// </summary>
            public string proposer;
            /// <summary>
            /// 提案类型， 0x01：文本提案； 0x02：升级提案；0x03参数提案<br/>
            /// 详情请参照<see cref="PPOSProposalType"/>
            /// </summary>
            public PPOSProposalType proposalType;
            /// <summary>
            /// 提案PIPID
            /// </summary>
            public string piPid;
            /// <summary>
            /// 提交提案的块高
            /// </summary>
            public HexBigInteger submitBlock;
            /// <summary>
            /// 提案投票结束的块高
            /// </summary>
            public HexBigInteger endVotingBlock;
            /// <summary>
            /// 升级版本
            /// </summary>
            public HexBigInteger newVersion;
            /// <summary>
            /// 提案要取消的升级提案ID
            /// </summary>
            public string toBeCanceled;
            /// <summary>
            /// （如果投票通过）生效块高（endVotingBlock + 20 + 4*250 &lt; 生效块高 &lt;= endVotingBlock + 20 + 10*250）
            /// </summary>
            public HexBigInteger activeBlock;
            /// <summary>
            /// 提交提案的验证人
            /// </summary>
            public string verifier;
            /// <summary>
            /// 参数模块
            /// </summary>
            public string module;
            /// <summary>
            /// 参数名称
            /// </summary>
            public string name;
            /// <summary>
            /// 参数新值
            /// </summary>
            public string newValue;
            /// <summary>
            /// 设置提案id
            /// </summary>
            /// <param name="proposalId">提案id</param>
            /// <returns><see cref="Builder"/>对象</returns>
            public Builder SetProposalId(string proposalId)
            {
                this.proposalId = proposalId;
                return this;
            }
            /// <summary>
            /// 设置提案节点ID
            /// </summary>
            /// <param name="proposer">提案节点ID</param>
            /// <returns><see cref="Builder"/>对象</returns>
            public Builder SetProposer(string proposer)
            {
                this.proposer = proposer;
                return this;
            }
            /// <summary>
            /// 设置提案类型
            /// </summary>
            /// <param name="proposalType">提案类型， 0x01：文本提案； 0x02：升级提案；0x03参数提案<br/>
            /// 详情请参照<see cref="PPOSProposalType"/></param>
            /// <returns><see cref="Builder"/>对象</returns>
            public Builder SetProposalType(PPOSProposalType proposalType)
            {
                this.proposalType = proposalType;
                return this;
            }
            /// <summary>
            /// 设置提案PIPID
            /// </summary>
            /// <param name="piPid">提案PIPID</param>
            /// <returns><see cref="Builder"/>对象</returns>
            public Builder SetPiPid(string piPid)
            {
                this.piPid = piPid;
                return this;
            }
            /// <summary>
            /// 设置提交提案的块高
            /// </summary>
            /// <param name="submitBlock">提交提案的块高</param>
            /// <returns><see cref="Builder"/>对象</returns>
            public Builder SetSubmitBlock(HexBigInteger submitBlock)
            {
                this.submitBlock = submitBlock;
                return this;
            }
            /// <summary>
            /// 设置提案投票结束的块高
            /// </summary>
            /// <param name="endVotingBlock">提案投票结束的块高</param>
            /// <returns><see cref="Builder"/>对象</returns>
            public Builder SetEndVotingBlock(HexBigInteger endVotingBlock)
            {
                this.endVotingBlock = endVotingBlock;
                return this;
            }
            /// <summary>
            /// 设置升级版本
            /// </summary>
            /// <param name="newVersion">升级版本</param>
            /// <returns><see cref="Builder"/>对象</returns>
            public Builder SetNewVersion(HexBigInteger newVersion)
            {
                this.newVersion = newVersion;
                return this;
            }
            /// <summary>
            /// 设置提案要取消的升级提案ID
            /// </summary>
            /// <param name="toBeCanceled">提案要取消的升级提案ID</param>
            /// <returns><see cref="Builder"/>对象</returns>
            public Builder SetToBeCanceled(string toBeCanceled)
            {
                this.toBeCanceled = toBeCanceled;
                return this;
            }
            /// <summary>
            /// 设置生效块高
            /// </summary>
            /// <param name="activeBlock">（如果投票通过）生效块高（endVotingBlock + 20 + 4*250 &lt; 生效块高 &lt;= endVotingBlock + 20 + 10*250）</param>
            /// <returns><see cref="Builder"/>对象</returns>
            public Builder SetActiveBlock(HexBigInteger activeBlock)
            {
                this.activeBlock = activeBlock;
                return this;
            }
            /// <summary>
            /// 设置提交提案的验证人
            /// </summary>
            /// <param name="verifier">提交提案的验证人</param>
            /// <returns><see cref="Builder"/>对象</returns>
            public Builder SetVerifier(string verifier)
            {
                this.verifier = verifier;
                return this;
            }
            /// <summary>
            /// 设置参数模块
            /// </summary>
            /// <param name="module">参数模块</param>
            /// <returns><see cref="Builder"/>对象</returns>
            public Builder SetModule(string module)
            {
                this.module = module;
                return this;
            }
            /// <summary>
            /// 设置参数名称
            /// </summary>
            /// <param name="name">参数名称</param>
            /// <returns><see cref="Builder"/>对象</returns>
            public Builder SetName(string name)
            {
                this.name = name;
                return this;
            }
            /// <summary>
            /// 设置参数新值
            /// </summary>
            /// <param name="newValue">参数新值</param>
            /// <returns><see cref="Builder"/>对象</returns>
            public Builder SetNewValue(string newValue)
            {
                this.newValue = newValue;
                return this;
            }
            /// <summary>
            /// 生成<see cref="Proposal"/>对象
            /// </summary>
            /// <returns><see cref="Proposal"/>对象</returns>
            public Proposal Build()
            {
                return new Proposal(this);
            }
        }
        public override string ToString()
        {
            return "Proposal{" +
                    "proposalId='" + ProposalId + '\'' +
                    ", proposer='" + Proposer + '\'' +
                    ", proposalType=" + ProposalType +
                    ", piPid='" + PiPid + '\'' +
                    ", submitBlock=" + SubmitBlock +
                    ", endVotingBlock=" + EndVotingBlock +
                    ", newVersion=" + NewVersion +
                    ", toBeCanceled='" + ToBeCanceled + '\'' +
                    ", activeBlock=" + ActiveBlock +
                    ", verifier='" + Verifier + '\'' +
                    '}';
        }
    }
    public enum PPOSProposalType
    {
        /// <summary>
        /// 文本提案
        /// </summary>
        TEXT_PROPOSAL = 0x01,
        /// <summary>
        /// 版本提案
        /// </summary>
        VERSION_PROPOSAL = 0x02,
        /// <summary>
        /// 参数提案
        /// </summary>
        PARAM_PROPOSAL = 0x03,
        /// <summary>
        /// 取消提案
        /// </summary>
        CANCEL_PROPOSAL = 0x04
    }
}
