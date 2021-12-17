using System;
using System.Collections.Generic;
using System.Text;
using Nethereum.Hex.HexConvertors.Extensions;
using System.Linq;
using Nethereum.Hex.HexTypes;
using Nethereum.RLP;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace PlatONet
{
    /// <summary>
    /// 网络内置合约调用
    /// </summary>
    public class PPOS
    {
        private PlatON _platon;
        /// <summary>
        /// 初始化实例
        /// </summary>
        /// <param name="platon"><see cref="PlatON"/>实例，表示网络信息</param>
        public PPOS(PlatON platon)
        {
            _platon = platon;
        }
        private byte[] EncodeLength(int len, int offset)
        {
            if (len < 56)
            {
                return (len + offset).ToBytesForRLPEncoding();
            }
            else
            {
                var hexLength = string.Format("{0:X}", len);
                var lLength = hexLength.Length / 2;
                var firstByte = string.Format("{0:X}", offset + 55 + lLength);
                return (firstByte + hexLength).HexToByteArray();
            }
        }
        private byte[] EncodeElement(byte[] inputBuf)
        {
            return inputBuf.Length == 1 && inputBuf[0] < 128
                ? inputBuf
                : EncodeLength(inputBuf.Length, 128).Concat(inputBuf).ToArray();
        }
        private byte[] EncodeArray(IEnumerable<byte[]> bufArray)
        {
            var output = new List<byte>();
            foreach (var item in bufArray)
            {
                output.AddRange(EncodeElement(item));
            }
            //var buf = Buffer.concat(output);
            return EncodeLength(output.Count(), 192).Concat(output).ToArray();
        }
        /// <summary>
        /// 根据函数类型，选择对应的 to 地址。
        /// </summary>
        /// <param name="funcType">Function Type</param>
        /// <returns>to 地址（Ethereum地址）</returns>
        private string FunctionTypeToAddress(int funcType)
        {
            string ethAddress;
            if (funcType >= 1000 && funcType < 2000)
                ethAddress = "0x1000000000000000000000000000000000000002";
            else if (funcType >= 2000 && funcType < 3000)
                ethAddress = "0x1000000000000000000000000000000000000005";
            else if (funcType >= 3000 && funcType < 4000)
                ethAddress = "0x1000000000000000000000000000000000000004";
            else if (funcType >= 4000 && funcType < 5000)
                ethAddress = "0x1000000000000000000000000000000000000001";
            else if (funcType >= 5000 && funcType < 6000)
                ethAddress = "0x1000000000000000000000000000000000000006";
            else
                throw new ArgumentException("function type should between 1000 and 6000.");
            var address = new Address(ethAddress.HexToByteArray(), _platon.Hrp);
            return address.ToString();
        }
        #region staking 质押相关的接口：主要包括节点质押、修改质押信息、解除质押、查询质押信息等接口
        /// <summary>
        /// 节点质押：适用于矿工，节点质押后才有机会参与共识，获得收益。
        /// 质押前需要自己的节点接入网络，质押时rpc链接地址必须是当前需要质押的节点。
        /// 如果质押成功，节点会出现在候选人列表中。
        /// </summary>
        /// <param name="nodeId">节点id，16进制格式，即节点公钥，可以通过管理台查询（platon attach http://127.0.0.1:6789 --exec "admin.nodeInfo.id"）。</param>
        /// <param name="stakingAmount">质押的金额，单位VON，默认质押金额必须大于等于1000000LAT，
        /// 该大小限制可以通过治理参数动态调整，可通过治理接口<see cref="GetGovernParamValue(string, string, BlockParameter)"/>`GetGovernParamValue("staking", "stakeThreshold")`获得当前值。</param>
        /// <param name="stakingAmountType">表示使用账户自由金额还是账户的锁仓金额做质押，详情请参照<see cref="StakingAmountType"/>。</param>
        /// <param name="benefitAddress">收益账户，用于接收出块奖励和质押奖励的收益账户</param>
        /// <param name="nodeName">节点的名称</param>
        /// <param name="externalId">外部Id(有长度限制，给第三方拉取节点描述的Id)，目前为keybase账户公钥，节点图标是通过该公钥获取。</param>
        /// <param name="website">节点的第三方主页(有长度限制，表示该节点的主页)</param>
        /// <param name="details">节点的描述(有长度限制，表示该节点的描述)</param>
        /// <param name="programVersion">程序的真实版本，通过<see cref="Web3.GetProgramVersion"/>获取</param>
        /// <param name="blsPubKey">bls的公钥，bls公钥一般通过platonkey工具生成。</param>
        /// <param name="blsProof">bls的证明，通过<see cref="Web3.GetSchnorrNIZKProve"/>获取</param>
        /// <param name="rewardPer">委托所得到的奖励分成比例，1=0.01% 10000=100%</param>
        /// <returns>交易hash</returns>
        public string Staking(string nodeId, HexBigInteger stakingAmount, StakingAmountType stakingAmountType,
            string benefitAddress, string nodeName, string externalId, string website, string details,
            ProgramVersion programVersion, string blsPubKey, string blsProof, HexBigInteger rewardPer,
            Transaction netParams = null)
        {
            var result = StakingAsync(nodeId, stakingAmount, stakingAmountType, 
                benefitAddress, nodeName, externalId, website, details, 
                programVersion, blsPubKey, blsProof, rewardPer, netParams);
            result.Wait();
            return result.Result;
        }
        /// <summary>
        /// 节点质押--异步操作：适用于矿工，节点质押后才有机会参与共识，获得收益。
        /// 质押前需要自己的节点接入网络，质押时rpc链接地址必须是当前需要质押的节点。
        /// 如果质押成功，节点会出现在候选人列表中。
        /// </summary>
        /// <param name="nodeId">节点id，16进制格式，即节点公钥，可以通过管理台查询（platon attach http://127.0.0.1:6789 --exec "admin.nodeInfo.id"）。</param>
        /// <param name="stakingAmount">质押的金额，单位VON，默认质押金额必须大于等于1000000LAT，
        /// 该大小限制可以通过治理参数动态调整，可通过治理接口<see cref="GetGovernParamValue(string, string, BlockParameter)"/>`GetGovernParamValue("staking", "stakeThreshold")`获得当前值。</param>
        /// <param name="stakingAmountType">表示使用账户自由金额还是账户的锁仓金额做质押，详情请参照<see cref="StakingAmountType"/>。</param>
        /// <param name="benefitAddress">收益账户，用于接收出块奖励和质押奖励的收益账户</param>
        /// <param name="nodeName">节点的名称</param>
        /// <param name="externalId">外部Id(有长度限制，给第三方拉取节点描述的Id)，目前为keybase账户公钥，节点图标是通过该公钥获取。</param>
        /// <param name="website">节点的第三方主页(有长度限制，表示该节点的主页)</param>
        /// <param name="details">节点的描述(有长度限制，表示该节点的描述)</param>
        /// <param name="programVersion">程序的真实版本，通过<see cref="Web3.GetProgramVersion"/>获取</param>
        /// <param name="blsPubKey">bls的公钥，bls公钥一般通过platonkey工具生成。</param>
        /// <param name="blsProof">bls的证明，通过<see cref="Web3.GetSchnorrNIZKProve"/>获取</param>
        /// <param name="rewardPer">委托所得到的奖励分成比例，1=0.01% 10000=100%</param>
        /// <param name="netParams">传入网络的默认参数，可以包含Gas,GasPrice,Nonce等，不传入则使用网络默认参数。</param>
        /// <returns>交易hash</returns>
        public async Task<string> StakingAsync(string nodeId, HexBigInteger stakingAmount, StakingAmountType stakingAmountType,
            string benefitAddress, string nodeName, string externalId, string website, string details,
            ProgramVersion programVersion, string blsPubKey, string blsProof, HexBigInteger rewardPer,
            Transaction netParams = null)
        {
            var funcType = PPOSFunctionType.STAKING_FUNC_TYPE;
            List<byte[]> bufArray = new List<byte[]>
            {
                EncodeElement(funcType.ToBytesForRLPEncoding()),
                EncodeElement(((int)stakingAmountType).ToBytesForRLPEncoding()),
                EncodeElement(new Address(benefitAddress).Bytes),
                EncodeElement(nodeId.HexToByteArray()),
                EncodeElement(externalId == null ? null : externalId.ToBytesForRLPEncoding()),
                EncodeElement(nodeName == null ? null : nodeName.ToBytesForRLPEncoding()),
                EncodeElement((website == null) ? null : website.ToBytesForRLPEncoding()),
                EncodeElement(details == null ? null : details.ToBytesForRLPEncoding()),
                EncodeElement(stakingAmount.ToHexByteArray()),
                EncodeElement(rewardPer.ToHexByteArray()),
                EncodeElement(programVersion.Version.HexValue.HexToByteArray()),
                EncodeElement(blsPubKey.HexToByteArray()),
                EncodeElement(blsProof.HexToByteArray())
            };
            netParams = BuildTransaction(bufArray, FunctionTypeToAddress(funcType), netParams);
            netParams = await _platon.FillTransactionWithDefaultValueAsync(netParams);
            netParams.ChainId = _platon.ChainId;
            netParams.Amount = stakingAmount;
            return await _platon.SendTransactionAsync(netParams);
        }
        /// <summary>
        /// 节点撤销质押：(一次性发起全部撤销，多次到账)，成功后节点会从候选人列表移除。只能由该节点的质押钱包地址发起交易。
        /// </summary>
        /// <param name="nodeId">节点id，16进制格式，即节点公钥，可以通过管理台查询（platon attach http://127.0.0.1:6789 --exec "admin.nodeInfo.id"）。</param>
        /// <param name="netParams">传入网络的默认参数，可以包含Gas,GasPrice,Nonce等</param>
        /// <returns>交易hash</returns>
        public string UnStaking(string nodeId, Transaction netParams = null)
        {
            var result = UnStakingAsync(nodeId, netParams);
            result.Wait();
            return result.Result;
        }
        /// <summary>
        /// 节点撤销质押--异步操作：(一次性发起全部撤销，多次到账)，成功后节点会从候选人列表移除。只能由该节点的质押钱包地址发起交易。
        /// </summary>
        /// <param name="nodeId">节点id，16进制格式，即节点公钥，可以通过管理台查询（platon attach http://127.0.0.1:6789 --exec "admin.nodeInfo.id"）。</param>
        /// <param name="netParams">传入网络的默认参数，可以包含Gas,GasPrice,Nonce等</param>
        /// <returns>交易hash</returns>
        public async Task<string> UnStakingAsync(string nodeId, Transaction netParams = null)
        {
            var funcType = PPOSFunctionType.WITHDREW_STAKING_FUNC_TYPE;
            List<byte[]> bufArray = new List<byte[]>
            {
                EncodeElement(funcType.ToBytesForRLPEncoding()),
                EncodeElement(nodeId.HexToByteArray())
            };
            netParams = BuildTransaction(bufArray, FunctionTypeToAddress(funcType), netParams);
            netParams = await _platon.FillTransactionWithDefaultValueAsync(netParams);
            netParams.ChainId = _platon.ChainId;
            return await _platon.SendTransactionAsync(netParams);
        }
        /// <summary>
        /// 修改质押信息：只能由该节点的质押钱包地址发起交易。
        /// </summary>
        /// <param name="nodeId">节点id，16进制格式，即节点公钥，可以通过管理台查询（platon attach http://127.0.0.1:6789 --exec "admin.nodeInfo.id"）。</param>
        /// <param name="benefitAddress">收益账户，用于接收出块奖励和质押奖励的收益账户</param>
        /// <param name="nodeName">节点的名称</param>
        /// <param name="externalId">外部Id(有长度限制，给第三方拉取节点描述的Id)，目前为keybase账户公钥，节点图标是通过该公钥获取。</param>
        /// <param name="website">节点的第三方主页(有长度限制，表示该节点的主页)</param>
        /// <param name="details">节点的描述(有长度限制，表示该节点的描述)</param>
        /// <param name="rewardPer">委托所得到的奖励分成比例，1=0.01% 10000=100%</param>
        /// <param name="netParams">传入网络的默认参数，可以包含Gas,GasPrice,Nonce等，不传入则使用网络默认参数。</param>
        /// <returns>交易hash</returns>
        public string UpdateStaking(string nodeId, string benefitAddress,
            string nodeName, string externalId, string website, string details,
            HexBigInteger rewardPer, Transaction netParams = null)
        {
            var result = UpdateStakingAsync(nodeId, benefitAddress, nodeName, 
                externalId, website, details, rewardPer, netParams);
            result.Wait();
            return result.Result;
        }
        /// <summary>
        /// 修改质押信息--异步操作：只能由该节点的质押钱包地址发起交易。
        /// </summary>
        /// <param name="nodeId">节点id，16进制格式，即节点公钥，可以通过管理台查询（platon attach http://127.0.0.1:6789 --exec "admin.nodeInfo.id"）。</param>
        /// <param name="benefitAddress">收益账户，用于接收出块奖励和质押奖励的收益账户</param>
        /// <param name="nodeName">节点的名称</param>
        /// <param name="externalId">外部Id(有长度限制，给第三方拉取节点描述的Id)，目前为keybase账户公钥，节点图标是通过该公钥获取。</param>
        /// <param name="website">节点的第三方主页(有长度限制，表示该节点的主页)</param>
        /// <param name="details">节点的描述(有长度限制，表示该节点的描述)</param>
        /// <param name="rewardPer">委托所得到的奖励分成比例，1=0.01% 10000=100%</param>
        /// <param name="netParams">传入网络的默认参数，可以包含Gas,GasPrice,Nonce等，不传入则使用网络默认参数。</param>
        /// <returns>交易hash</returns>
        public async Task<string> UpdateStakingAsync(string nodeId, string benefitAddress,
            string nodeName, string externalId, string website, string details, 
            HexBigInteger rewardPer, Transaction netParams = null)
        {
            var funcType = PPOSFunctionType.UPDATE_STAKING_INFO_FUNC_TYPE;
            List<byte[]> bufArray = new List<byte[]>
            {
                EncodeElement(funcType.ToBytesForRLPEncoding()),
                EncodeElement(benefitAddress == null ? null : new Address(benefitAddress).Bytes),
                EncodeElement(nodeId == null ? null : nodeId.HexToByteArray()),
                EncodeElement(rewardPer == null ? null : rewardPer.ToHexByteArray()),
                EncodeElement(externalId == null ? null : externalId.ToBytesForRLPEncoding()),
                EncodeElement(nodeName == null ? null : nodeName.ToBytesForRLPEncoding()),
                EncodeElement((website == null) ? null : website.ToBytesForRLPEncoding()),
                EncodeElement(details == null ? null : details.ToBytesForRLPEncoding())                
            };
            netParams = BuildTransaction(bufArray, FunctionTypeToAddress(funcType), netParams);
            netParams = await _platon.FillTransactionWithDefaultValueAsync(netParams);
            netParams.ChainId = _platon.ChainId;
            return await _platon.SendTransactionAsync(netParams);
        }
        /// <summary>
        /// 增持质押
        /// </summary>
        /// <param name="nodeId">被质押的节点Id(也叫候选人的节点Id)</param>
        /// <param name="stakingAmountType">表示使用账户自由金额还是账户的锁仓金额做质押</param>
        /// <param name="addStakingAmount">增持的von</param>
        /// <param name="netParams">传入网络的默认参数，可以包含Gas,GasPrice,Nonce等，不传入则使用网络默认参数。</param>
        /// <returns>交易hash</returns>
        public string AddStaking(string nodeId, StakingAmountType stakingAmountType,
            HexBigInteger addStakingAmount, Transaction netParams = null)
        {
            var result = AddStakingAsync(nodeId, stakingAmountType, addStakingAmount, netParams);
            result.Wait();
            return result.Result;
        }
        /// <summary>
        /// 增持质押--异步操作
        /// </summary>
        /// <param name="nodeId">被质押的节点Id(也叫候选人的节点Id)</param>
        /// <param name="stakingAmountType">表示使用账户自由金额还是账户的锁仓金额做质押</param>
        /// <param name="addStakingAmount">增持的von</param>
        /// <param name="netParams">传入网络的默认参数，可以包含Gas,GasPrice,Nonce等，不传入则使用网络默认参数。</param>
        /// <returns>交易hash</returns>
        public async Task<string> AddStakingAsync(string nodeId, StakingAmountType stakingAmountType, 
            HexBigInteger addStakingAmount, Transaction netParams = null)
        {
            var funcType = PPOSFunctionType.ADD_STAKING_FUNC_TYPE;
            List<byte[]> bufArray = new List<byte[]>
            {
                EncodeElement(funcType.ToBytesForRLPEncoding()),
                EncodeElement(nodeId == null ? null : nodeId.HexToByteArray()),
                EncodeElement(((int)stakingAmountType).ToBytesForRLPEncoding()),
                EncodeElement(addStakingAmount.ToHexByteArray())
            };
            netParams = BuildTransaction(bufArray, FunctionTypeToAddress(funcType), netParams);
            netParams = await _platon.FillTransactionWithDefaultValueAsync(netParams);
            netParams.ChainId = _platon.ChainId;
            netParams.Amount = addStakingAmount;
            return await _platon.SendTransactionAsync(netParams);
        }
        /// <summary>
        /// 查询当前节点的质押信息
        /// </summary>
        /// <param name="nodeId">被质押的节点Id(也叫候选人的节点Id)</param>
        /// <param name="block"><see cref="BlockParameter"/>实例，表示块高</param>
        /// <returns>节点信息，详见<see cref="Node"/></returns>
        /// <exception cref="PlatONException">PlatON异常</exception>
        public Node GetStakingInfo(string nodeId, BlockParameter block = null)
        {
            var result = GetStakingInfoAsync(nodeId, block);
            result.Wait();
            return result.Result;
        }
        /// <summary>
        /// 查询当前节点的质押信息--异步操作
        /// </summary>
        /// <param name="nodeId">被质押的节点Id(也叫候选人的节点Id)</param>
        /// <param name="block"><see cref="BlockParameter"/>实例，表示块高</param>
        /// <returns>节点信息，详见<see cref="Node"/></returns>
        /// <exception cref="PlatONException">PlatON异常</exception>
        public async Task<Node> GetStakingInfoAsync(string nodeId, BlockParameter block = null)
        {
            var funcType = PPOSFunctionType.GET_STAKINGINFO_FUNC_TYPE;
            List<byte[]> bufArray = new List<byte[]>
            {
                EncodeElement(funcType.ToBytesForRLPEncoding()),
                EncodeElement(nodeId.HexToByteArray())
            };
            var tx = BuildTransaction(bufArray, FunctionTypeToAddress(funcType));
            var hexStr = await _platon.CallAsync<string>(tx, block);
            var response = DecodeResponse<CallResponse<Node>>(hexStr);
            if (response != null && response.Code == ErrorCode.SUCCESS)
            {
                return response.Data;
            }
            else
            {
                throw new PlatONException(response.Code);
            }
        }
        /// <summary>
        /// 查询当前结算周期的区块奖励
        /// </summary>
        /// <param name="block"><see cref="BlockParameter"/>实例，表示块高</param>
        /// <returns>当前结算周期的区块奖励</returns>
        /// <exception cref="PlatONException">PlatON异常</exception>
        public HexBigInteger GetPackageReward(BlockParameter block = null)
        {
            var result = GetPackageRewardAsync(block);
            result.Wait();
            return result.Result;
        }
        /// <summary>
        /// 查询当前结算周期的区块奖励--异步操作
        /// </summary>
        /// <param name="block"><see cref="BlockParameter"/>实例，表示块高</param>
        /// <returns>当前结算周期的区块奖励</returns>
        /// <exception cref="PlatONException">PlatON异常</exception>
        public async Task<HexBigInteger> GetPackageRewardAsync(BlockParameter block = null)
        {
            var funcType = PPOSFunctionType.GET_PACKAGEREWARD_FUNC_TYPE;
            List<byte[]> bufArray = new List<byte[]>
            {
                EncodeElement(funcType.ToBytesForRLPEncoding())
            };
            var tx = BuildTransaction(bufArray, FunctionTypeToAddress(funcType));
            var hexStr = await _platon.CallAsync<string>(tx, block);
            var response = DecodeResponse<CallResponse<HexBigInteger>>(hexStr);
            if (response != null && response.Code == ErrorCode.SUCCESS)
            {
                return response.Data;
            }
            else
            {
                throw new PlatONException(response.Code);
            }
        }
        /// <summary>
        /// 查询当前结算周期的质押奖励
        /// </summary>
        /// <param name="block"><see cref="BlockParameter"/>实例，表示块高</param>
        /// <returns>当前结算周期的质押奖励</returns>
        /// <exception cref="PlatONException">PlatON异常</exception>
        public HexBigInteger GetStakingReward(BlockParameter block = null)
        {
            var result = GetStakingRewardAsync(block);
            result.Wait();
            return result.Result;
        }
        /// <summary>
        /// 查询当前结算周期的质押奖励--异步操作
        /// </summary>
        /// <param name="block"><see cref="BlockParameter"/>实例，表示块高</param>
        /// <returns>当前结算周期的质押奖励</returns>
        /// <exception cref="PlatONException">PlatON异常</exception>
        public async Task<HexBigInteger> GetStakingRewardAsync(BlockParameter block = null)
        {
            var funcType = PPOSFunctionType.GET_STAKINGREWARD_FUNC_TYPE;
            List<byte[]> bufArray = new List<byte[]>
            {
                EncodeElement(funcType.ToBytesForRLPEncoding())
            };
            var tx = BuildTransaction(bufArray, FunctionTypeToAddress(funcType));
            var hexStr = await _platon.CallAsync<string>(tx, block);
            var response = DecodeResponse<CallResponse<HexBigInteger>>(hexStr);
            if (response != null && response.Code == ErrorCode.SUCCESS)
            {
                return response.Data;
            }
            else
            {
                throw new PlatONException(response.Code);
            }
        }
        /// <summary>
        /// 查询打包区块的平均时间
        /// </summary>
        /// <param name="block"><see cref="BlockParameter"/>实例，表示块高</param>
        /// <returns>打包区块的平均时间</returns>
        /// <exception cref="PlatONException">PlatON异常</exception>
        public HexBigInteger GetAvgPackTime(BlockParameter block = null)
        {
            var result = GetAvgPackTimeAsync(block);
            result.Wait();
            return result.Result;
        }
        /// <summary>
        /// 查询打包区块的平均时间--异步操作
        /// </summary>
        /// <param name="block"><see cref="BlockParameter"/>实例，表示块高</param>
        /// <returns>打包区块的平均时间</returns>
        /// <exception cref="PlatONException">PlatON异常</exception>
        public async Task<HexBigInteger> GetAvgPackTimeAsync(BlockParameter block = null)
        {
            var funcType = PPOSFunctionType.GET_AVGPACKTIME_FUNC_TYPE;
            List<byte[]> bufArray = new List<byte[]>
            {
                EncodeElement(funcType.ToBytesForRLPEncoding())
            };
            var tx = BuildTransaction(bufArray, FunctionTypeToAddress(funcType));
            var hexStr = await _platon.CallAsync<string>(tx, block);
            var response = DecodeResponse<CallResponse<HexBigInteger>>(hexStr);
            if (response != null && response.Code == ErrorCode.SUCCESS)
            {
                return response.Data;
            }
            else
            {
                throw new PlatONException(response.Code);
            }
        }
        #endregion
        #region delegate PlatON经济模型中委托人相关的合约接口
        /// <summary>
        /// 发起委托，委托已质押节点，委托给某个节点增加节点权重来获取收入
        /// </summary>
        /// <param name="nodeId">节点id，16进制格式，即节点公钥，可以通过管理台查询（platon attach http://127.0.0.1:6789 --exec "admin.nodeInfo.id"）。</param>
        /// <param name="stakingAmountType">表示使用账户自由金额还是账户的锁仓金额做质押，详情请参照<see cref="StakingAmountType"/></param>
        /// <param name="amount">委托的金额，单位VON</param>
        /// <param name="netParams">传入网络的默认参数，可以包含Gas,GasPrice,Nonce等，不传入则使用网络默认参数。</param>
        /// <returns>交易hash</returns>
        public string Delegate(string nodeId, StakingAmountType stakingAmountType, 
            HexBigInteger amount, Transaction netParams = null)
        {
            var result = DelegateAsync(nodeId, stakingAmountType, amount, netParams);
            result.Wait();
            return result.Result;
        }
        /// <summary>
        /// 发起委托--异步操作：委托已质押节点，委托给某个节点增加节点权重来获取收入
        /// </summary>
        /// <param name="nodeId">节点id，16进制格式，即节点公钥，可以通过管理台查询（platon attach http://127.0.0.1:6789 --exec "admin.nodeInfo.id"）。</param>
        /// <param name="stakingAmountType">表示使用账户自由金额还是账户的锁仓金额做质押，详情请参照<see cref="StakingAmountType"/></param>
        /// <param name="amount">委托的金额，单位VON</param>
        /// <param name="netParams">传入网络的默认参数，可以包含Gas,GasPrice,Nonce等，不传入则使用网络默认参数。</param>
        /// <returns>交易hash</returns>
        public async Task<string> DelegateAsync(string nodeId, StakingAmountType stakingAmountType, 
            HexBigInteger amount, Transaction netParams = null)
        {
            var funcType = PPOSFunctionType.DELEGATE_FUNC_TYPE;
            List<byte[]> bufArray = new List<byte[]>
            {
                EncodeElement(funcType.ToBytesForRLPEncoding()),
                EncodeElement(((int)stakingAmountType).ToBytesForRLPEncoding()),
                EncodeElement(nodeId.HexToByteArray()),                
                EncodeElement(amount.ToHexByteArray())
            };
            //netParams = netParams ?? new Transaction();
            netParams = BuildTransaction(bufArray, FunctionTypeToAddress(funcType), netParams);
            netParams = await _platon.FillTransactionWithDefaultValueAsync(netParams);
            netParams.ChainId = _platon.ChainId;
            return await _platon.SendTransactionAsync(netParams);
        }
        /// <summary>
        /// 查询当前账户地址所委托的节点的NodeID和质押Id
        /// </summary>
        /// <param name="address">委托人的账户地址</param>
        /// <param name="block"><see cref="BlockParameter"/>实例，表示块高</param>
        /// <returns>保存当前账户地址所委托的节点的NodeID和质押区块高度的对象的列表</returns>
        /// <exception cref="PlatONException">PlatON异常</exception>
        public List<DelegationIdInfo> GetDelegateListByAddr(string address, BlockParameter block = null)
        {
            var result = GetDelegateListByAddrAsync(address, block);
            result.Wait();
            return result.Result;
        }
        /// <summary>
        /// 查询当前账户地址所委托的节点的NodeID和质押Id--异步操作
        /// </summary>
        /// <param name="address">委托人的账户地址</param>
        /// <param name="block"><see cref="BlockParameter"/>实例，表示块高</param>
        /// <returns>保存当前账户地址所委托的节点的NodeID和质押区块高度的对象的列表，详情请参照<see cref="DelegationIdInfo"/>。</returns>
        /// <exception cref="PlatONException">PlatON异常</exception>
        public async Task<List<DelegationIdInfo>> GetDelegateListByAddrAsync(string address, BlockParameter block = null)
        {
            var funcType = PPOSFunctionType.GET_DELEGATELIST_BYADDR_FUNC_TYPE;
            List<byte[]> bufArray = new List<byte[]> { 
                EncodeElement(funcType.ToBytesForRLPEncoding()),
                EncodeElement(new Address(address).Bytes)
            };
            var tx = BuildTransaction(bufArray, FunctionTypeToAddress(funcType));
            var hexStr = await _platon.CallAsync<string>(tx, block);
            var response = DecodeResponse<CallResponse<List<DelegationIdInfo>>>(hexStr);
            if (response != null && response.Code == ErrorCode.SUCCESS)
            {
                return response.Data;
            }
            else
            {
                throw new PlatONException(response.Code);
            }
        }
        /// <summary>
        /// 查询当前单个委托信息
        /// </summary>
        /// <param name="address">委托人的账户地址</param>
        /// <param name="nodeId">节点id，16进制格式，0x开头</param>
        /// <param name="stakingBlockNum">发起质押时的区块高度</param>
        /// <param name="block"><see cref="BlockParameter"/>实例，表示块高</param>
        /// <returns>Delegation对象数据，详情请参照<see cref="Delegation"/>。</returns>
        /// <exception cref="PlatONException">PlatON异常</exception>
        public Delegation GetDelegateInfo(string address, string nodeId, HexBigInteger stakingBlockNum, BlockParameter block = null)
        {
            var result = GetDelegateInfoAsync(address, nodeId, stakingBlockNum, block);
            result.Wait();
            return result.Result;
        }
        /// <summary>
        /// 查询当前单个委托信息--异步操作
        /// </summary>
        /// <param name="address">委托人的账户地址</param>
        /// <param name="nodeId">节点id，16进制格式，0x开头</param>
        /// <param name="stakingBlockNum">发起质押时的区块高度</param>
        /// <param name="block"><see cref="BlockParameter"/>实例，表示块高</param>
        /// <returns>Delegation对象数据，详情请参照<see cref="Delegation"/>。</returns>
        /// <exception cref="PlatONException">PlatON异常</exception>
        public async Task<Delegation> GetDelegateInfoAsync(string address, string nodeId, HexBigInteger stakingBlockNum, BlockParameter block = null)
        {
            var funcType = PPOSFunctionType.GET_DELEGATEINFO_FUNC_TYPE;
            List<byte[]> bufArray = new List<byte[]> {
                EncodeElement(funcType.ToBytesForRLPEncoding()),
                EncodeElement(new Address(address).Bytes),
                EncodeElement(nodeId.HexToByteArray()),
                EncodeElement(stakingBlockNum.ToHexByteArray())
            };
            var tx = BuildTransaction(bufArray, FunctionTypeToAddress(funcType));
            var hexStr = await _platon.CallAsync<string>(tx, block);
            var response = DecodeResponse<CallResponse<Delegation>>(hexStr);
            if (response != null && response.Code == ErrorCode.SUCCESS)
            {
                return response.Data;
            }
            else
            {
                throw new PlatONException(response.Code);
            }
        }
        /// <summary>
        /// 减持/撤销委托(全部减持就是撤销)
        /// </summary>
        /// <param name="nodeId">节点id，16进制格式，0x开头</param>
        /// <param name="stakingBlockNum">委托节点的质押块高，代表着某个node的某次质押的唯一标示</param>
        /// <param name="stakingAmount">减持的委托金额(按照最小单位算，1LAT = 10 * *18 VON)</param>
        /// <param name="netParams">传入网络的默认参数，可以包含Gas,GasPrice,Nonce等，不传入则使用网络默认参数。</param>
        /// <returns>交易的Hash</returns>
        public string UnDelegate(string nodeId, HexBigInteger stakingBlockNum,
            HexBigInteger stakingAmount, Transaction netParams = null)
        {
            var result = UnDelegateAsync(nodeId, stakingBlockNum, stakingAmount, netParams);
            result.Wait();
            return result.Result;
        }
        /// <summary>
        /// 减持/撤销委托(全部减持就是撤销)--异步操作
        /// </summary>
        /// <param name="nodeId">节点id，16进制格式，0x开头</param>
        /// <param name="stakingBlockNum">委托节点的质押块高，代表着某个node的某次质押的唯一标示</param>
        /// <param name="stakingAmount">减持的委托金额(按照最小单位算，1LAT = 10 * *18 VON)</param>
        /// <param name="netParams">传入网络的默认参数，可以包含Gas,GasPrice,Nonce等，不传入则使用网络默认参数。</param>
        /// <returns>交易的Hash</returns>
        public async Task<string> UnDelegateAsync(string nodeId, HexBigInteger stakingBlockNum, 
            HexBigInteger stakingAmount, Transaction netParams = null)
        {
            var funcType = PPOSFunctionType.WITHDREW_DELEGATE_FUNC_TYPE;
            List<byte[]> bufArray = new List<byte[]>
            {
                EncodeElement(funcType.ToBytesForRLPEncoding()),
                EncodeElement(nodeId.HexToByteArray()),
                EncodeElement(stakingBlockNum.ToHexByteArray()),
                EncodeElement(stakingAmount.ToHexByteArray())
            };
            //netParams = netParams ?? new Transaction();
            netParams = BuildTransaction(bufArray, FunctionTypeToAddress(funcType), netParams);
            netParams = await _platon.FillTransactionWithDefaultValueAsync(netParams);
            netParams.ChainId = _platon.ChainId;
            return await _platon.SendTransactionAsync(netParams);
        }
        #endregion
        #region reward PlatON经济模型中奖励相关的合约接口
        /// <summary>
        /// 提取账户当前所有的可提取的委托奖励
        /// </summary>
        /// <returns>交易的Hash</returns>
        public string WithdrawDelegateReward(Transaction netParams = null)
        {
            var funcType = PPOSFunctionType.WITHDRAW_DELEGATE_REWARD_FUNC_TYPE;
            List<byte[]> bufArray = new List<byte[]>
            {
                EncodeElement(funcType.ToBytesForRLPEncoding())
            };
            //netParams = netParams ?? new Transaction();
            netParams = BuildTransaction(bufArray, FunctionTypeToAddress(funcType), netParams);
            netParams = _platon.FillTransactionWithDefaultValue(netParams);
            netParams.ChainId = _platon.ChainId;
            return _platon.SendTransaction(netParams);
        }
        public CallResponse<List<RewardInfo>> GetDelegateReward(string address, List<string> nodeList = null, BlockParameter block = null)
        {
            var funcType = PPOSFunctionType.GET_DELEGATE_REWARD_FUNC_TYPE;
            nodeList = nodeList ?? new List<string>();
            List<byte[]> nodeListBuf = new List<byte[]>();
            foreach(var nodeId in nodeList)
            {
                nodeListBuf.Add(nodeId.HexToByteArray());
            }
            List<byte[]> bufArray = new List<byte[]> {
                EncodeElement(funcType.ToBytesForRLPEncoding()),
                EncodeElement(new Address(address).Bytes),
                EncodeArray(nodeListBuf)
            };
            var tx = BuildTransaction(bufArray, FunctionTypeToAddress(funcType));
            var hexStr = _platon.Call<string>(tx, block);
            return DecodeResponse<CallResponse<List<RewardInfo>>>(hexStr);
        }
        #endregion
        #region node
        public CallResponse<List<Node>> GetVerifierList(BlockParameter block = null)
        {
            var funcType = PPOSFunctionType.GET_VERIFIERLIST_FUNC_TYPE;
            List<byte[]> bufArray = new List<byte[]> {
                EncodeElement(funcType.ToBytesForRLPEncoding())
            };
            var tx = BuildTransaction(bufArray, FunctionTypeToAddress(funcType));
            var hexStr = _platon.Call<string>(tx, block);
            return DecodeResponse<CallResponse<List<Node>>>(hexStr);
        }
        public CallResponse<List<Node>> GetValidatorList(BlockParameter block = null)
        {
            var funcType = PPOSFunctionType.GET_VALIDATORLIST_FUNC_TYPE;
            List<byte[]> bufArray = new List<byte[]> {
                EncodeElement(funcType.ToBytesForRLPEncoding())
            };
            var tx = BuildTransaction(bufArray, FunctionTypeToAddress(funcType));
            var hexStr = _platon.Call<string>(tx, block);
            return DecodeResponse<CallResponse<List<Node>>>(hexStr);
        }
        public CallResponse<List<Node>> GetCandidateList(BlockParameter block = null)
        {
            var funcType = PPOSFunctionType.GET_VALIDATORLIST_FUNC_TYPE;
            List<byte[]> bufArray = new List<byte[]> {
                EncodeElement(funcType.ToBytesForRLPEncoding())
            };
            var tx = BuildTransaction(bufArray, FunctionTypeToAddress(funcType));
            var hexStr = _platon.Call<string>(tx, block);
            return DecodeResponse<CallResponse<List<Node>>>(hexStr);
        }
        #endregion
        #region DAO
        public void SubmitProposal()
        {

        }
        public CallResponse<Proposal> GetProposal(string proposalId,BlockParameter block = null)
        {
            var funcType = PPOSFunctionType.GET_PROPOSAL_FUNC_TYPE;
            List<byte[]> bufArray = new List<byte[]> {
                EncodeElement(funcType.ToBytesForRLPEncoding()),
                EncodeElement(proposalId.HexToByteArray())
            };
            var tx = BuildTransaction(bufArray, FunctionTypeToAddress(funcType));
            var hexStr = _platon.Call<string>(tx, block);
            return DecodeResponse<CallResponse<Proposal>>(hexStr);
        }
        public CallResponse<TallyResult> GetTallyResult(string proposalId, BlockParameter block = null)
        {
            var funcType = PPOSFunctionType.GET_TALLY_RESULT_FUNC_TYPE;
            List<byte[]> bufArray = new List<byte[]> {
                EncodeElement(funcType.ToBytesForRLPEncoding()),
                EncodeElement(proposalId.HexToByteArray())
            };
            var tx = BuildTransaction(bufArray, FunctionTypeToAddress(funcType));
            var hexStr = _platon.Call<string>(tx, block);
            return DecodeResponse<CallResponse<TallyResult>>(hexStr);
        }
        public CallResponse<List<Proposal>> GetProposalList(BlockParameter block = null)
        {
            var funcType = PPOSFunctionType.GET_PROPOSAL_LIST_FUNC_TYPE;
            List<byte[]> bufArray = new List<byte[]> {
                EncodeElement(funcType.ToBytesForRLPEncoding())
            };
            var tx = BuildTransaction(bufArray, FunctionTypeToAddress(funcType));
            var hexStr = _platon.Call<string>(tx, block);
            return DecodeResponse<CallResponse<List<Proposal>>>(hexStr);
        }
        public void DeclareVersion()
        {

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="block"></param>
        /// <returns></returns>
        public CallResponse<HexBigInteger> GetActiveVersion(BlockParameter block = null)
        {
            var funcType = PPOSFunctionType.GET_ACTIVE_VERSION;
            List<byte[]> bufArray = new List<byte[]>
            {
                EncodeElement(funcType.ToBytesForRLPEncoding())
            };
            var tx = BuildTransaction(bufArray, FunctionTypeToAddress(funcType));
            var hexStr = _platon.Call<string>(tx, block);
            return DecodeResponse<CallResponse<HexBigInteger>>(hexStr);
        }
        /// <summary>
        /// 查询治理参数值
        /// </summary>
        /// <param name="module">参数模块</param>
        /// <param name="name">参数名称</param>
        /// <param name="block"></param>
        /// <returns>治理参数值</returns>
        public string GetGovernParamValue(string module, string name, BlockParameter block = null)
        {
            var result = GetGovernParamValueAsync(module, name, block);
            result.Wait();
            return result.Result;
        }
        /// <summary>
        /// 查询治理参数值--异步操作
        /// </summary>
        /// <param name="module">参数模块</param>
        /// <param name="name">参数名称</param>
        /// <param name="block"></param>
        /// <returns>治理参数值</returns>
        public async Task<string> GetGovernParamValueAsync(string module, string name, BlockParameter block = null)
        {
            var funcType = PPOSFunctionType.GET_GOVERN_PARAM_VALUE;
            List<byte[]> bufArray = new List<byte[]>
            {
                EncodeElement(funcType.ToBytesForRLPEncoding()),
                EncodeElement(module.ToBytesForRLPEncoding()),
                EncodeElement(name.ToBytesForRLPEncoding())
            };
            var tx = BuildTransaction(bufArray, FunctionTypeToAddress(funcType));
            var hexStr = await _platon.CallAsync<string>(tx, block);
            var response = DecodeResponse<CallResponse<string>>(hexStr);
            if (response != null && response.Code == ErrorCode.SUCCESS)
            {
                return response.Data;
            }
            else
            {
                throw new PlatONException(response.Code);
            }
        }
        #endregion
        private Transaction BuildTransaction(IEnumerable<byte[]> bufArray,
            string address, Transaction netParams = null)
        {
            netParams = netParams ?? new Transaction();
            netParams.To = new Address(address);
            //var encodedBufArray = new List<byte[]>();
            //foreach (var item in bufArray) encodedBufArray.Add(EncodeElement(item));
            netParams.Data = EncodeArray(bufArray).ToHex();
            return netParams;        
        }
        public static T DecodeResponse<T>(string hexStr)
        {
            hexStr = hexStr.ToLower().StartsWith("0x") ? hexStr.Substring(2) : hexStr;
            var str = Encoding.UTF8.GetString(hexStr.HexToByteArray());
            var result = JsonConvert.DeserializeObject<T>(str);
            return result;
        }
    }
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
