using Nethereum.Hex.HexTypes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using Nethereum.Hex.HexConvertors.Extensions;
using Nethereum.RLP;

namespace PlatONet
{
    public class Proposal
    {
        /**
         * 提案id
         */
        [JsonProperty("ProposalID")]
        public string ProposalId { get; set; }
        /**
         * 提案节点ID
         */
        [JsonProperty("Proposer")]
        public string Proposer { get; set; }
        /**
         * 提案类型， 0x01：文本提案； 0x02：升级提案；0x03参数提案
         */
        [JsonProperty("ProposalType")]
        public int ProposalType { get; set; }
        /**
         * 提案PIPID
         */
        [JsonProperty("PIPID")]
        public string PiPid { get; set; }
        /**
         * 提交提案的块高
         */
        [JsonProperty("SubmitBlock")]
        public HexBigInteger SubmitBlock { get; set; }
        /**
         * 提案投票结束的块高
         */
        [JsonProperty("EndVotingBlock")]
        public HexBigInteger EndVotingBlock { get; set; }
        /**
         * 升级版本
         */
        [JsonProperty("NewVersion")]
        public HexBigInteger NewVersion { get; set; }
        /**
         * 提案要取消的升级提案ID
         */
        [JsonProperty("TobeCanceled")]
        public string ToBeCanceled { get; set; }
        /**
         * （如果投票通过）生效块高（endVotingBlock + 20 + 4*250 < 生效块高 <= endVotingBlock + 20 + 10*250）
         */
        [JsonProperty("ActiveBlock")]
        public HexBigInteger ActiveBlock { get; set; }

        /**
         * 提交提案的验证人
         */
        public string Verifier { get; set; }
        /**
         * 参数模块
         */
        public string Module { get; set; }
        /**
         * 参数名称
         */
        public string Name { get; set; }
        /**
         * 参数新值
         */
        public string NewValue { get; set; }

        public Proposal()
        {

        }
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

        //public static Proposal createSubmitTextProposalParam(string verifier, string pIDID)
        //{
        //    return new Proposal.Builder()
        //            .setProposalType(ProposalType.TEXT_PROPOSAL)
        //            .setVerifier(verifier)
        //            .setPiPid(pIDID)
        //            .build();
        //}

        //public static Proposal createSubmitVersionProposalParam(string verifier, string pIDID, HexBigInteger newVersion, HexBigInteger endVotingRounds)
        //{
        //    return new Proposal.Builder()
        //            .setProposalType(ProposalType.VERSION_PROPOSAL)
        //            .setVerifier(verifier)
        //            .setPiPid(pIDID)
        //            .setNewVersion(newVersion)
        //            .setEndVotingBlock(endVotingRounds)
        //            .build();
        //}

        //public static Proposal createSubmitCancelProposalParam(string verifier, string pIDID, HexBigInteger endVotingRounds, string tobeCanceledProposalID)
        //{
        //    return new Proposal.Builder()
        //            .setProposalType(ProposalType.CANCEL_PROPOSAL)
        //            .setVerifier(verifier)
        //            .setPiPid(pIDID)
        //            .setEndVotingBlock(endVotingRounds)
        //            .setToBeCanceled(tobeCanceledProposalID)
        //            .build();
        //}

        //public static Proposal createSubmitParamProposalParam(string verifier, string pIDID, string module, string name, string newValue)
        //{

        //    return new Proposal.Builder()
        //            .setProposalType(ProposalType.PARAM_PROPOSAL)
        //            .setVerifier(verifier)
        //            .setPiPid(pIDID)
        //            .setModule(module)
        //            .setName(name)
        //            .setNewValue(newValue)
        //            .build();
        //}

        public class Builder
        {
            public string proposalId;
            public string proposer;
            public int proposalType;
            public string piPid;
            public HexBigInteger submitBlock;
            public HexBigInteger endVotingBlock;
            public HexBigInteger newVersion;
            public string toBeCanceled;
            public HexBigInteger activeBlock;
            public string verifier;
            public string module;
            public string name;
            public string newValue;

            public Builder SetProposalId(string proposalId)
            {
                this.proposalId = proposalId;
                return this;
            }

            public Builder SetProposer(string proposer)
            {
                this.proposer = proposer;
                return this;
            }

            public Builder SetProposalType(int proposalType)
            {
                this.proposalType = proposalType;
                return this;
            }

            public Builder SetPiPid(string piPid)
            {
                this.piPid = piPid;
                return this;
            }

            public Builder setSubmitBlock(HexBigInteger submitBlock)
            {
                this.submitBlock = submitBlock;
                return this;
            }

            public Builder SetEndVotingBlock(HexBigInteger endVotingBlock)
            {
                this.endVotingBlock = endVotingBlock;
                return this;
            }

            public Builder SetNewVersion(HexBigInteger newVersion)
            {
                this.newVersion = newVersion;
                return this;
            }

            public Builder SetToBeCanceled(string toBeCanceled)
            {
                this.toBeCanceled = toBeCanceled;
                return this;
            }

            public Builder SetActiveBlock(HexBigInteger activeBlock)
            {
                this.activeBlock = activeBlock;
                return this;
            }

            public Builder SetVerifier(string verifier)
            {
                this.verifier = verifier;
                return this;
            }

            public Builder SetModule(string module)
            {
                this.module = module;
                return this;
            }

            public Builder SetName(string name)
            {
                this.name = name;
                return this;
            }

            public Builder SetNewValue(string newValue)
            {
                this.newValue = newValue;
                return this;
            }

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
    public class PPOSProposalType
    {
        /**
         * 文本提案
         */
        public const int TEXT_PROPOSAL = 0x01;
        /**
         * 版本提案
         */
        public const int VERSION_PROPOSAL = 0x02;
        /**
         * 参数提案
         */
        public const int PARAM_PROPOSAL = 0x03;
        /**
         * 取消提案
         */
        public const int CANCEL_PROPOSAL = 0x04;
    }
}
