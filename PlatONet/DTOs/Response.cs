using Newtonsoft.Json;

namespace PlatONet.DTOs
{
    /// <summary>
    /// 查询请求应答
    /// </summary>
    public class TransactionResponse : BaseResponse
    {
        /// <summary>
        /// 交易收据
        /// </summary>
        public TransactionReceipt TransactionReceipt { get; set; }
        public override string ToString()
        {
            return "TransactionResponse [transactionReceipt=" + TransactionReceipt + ", getCode()=" + Code
                    + ", getErrMsg()=" + ErrMsg + "]";
        }
    }
    /// <summary>
    /// 查询请求应答
    /// </summary>
    /// <typeparam name="T">应答的数据类型</typeparam>
    public class CallResponse<T> : BaseResponse
    {
        /// <summary>
        /// 应该信息
        /// </summary>
        [JsonProperty("Ret")]
        public T Data { get; set; }
        public override string ToString()
        {
            return "CallResponse [data=" + Data + ", Code=" + Code + ", ErrMsg=" + ErrMsg + "]";
        }
    }
    /// <summary>
    /// 基本的应答信息
    /// </summary>
    public class BaseResponse
    {
        /// <summary>
        /// 是否请求成功
        /// </summary>
        /// <returns>成功为true，否则为false</returns>
        public bool IsStatusOk()
        {
            return Code == ErrorCode.SUCCESS;
        }
        /// <summary>
        /// 错误编码
        /// </summary>
        public int Code { get; set; }      
        /// <summary>
        /// 错误信息
        /// </summary>
        public string ErrMsg { get; set; }

        public override string ToString()
        {
            return "BaseResponse [code=" + Code + ", errMsg=" + ErrMsg + "]";
        }
    }
}
