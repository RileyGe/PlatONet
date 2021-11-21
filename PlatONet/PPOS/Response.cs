//using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace PlatONet
{
    public class CallResponse<T> : BaseResponse
    {
        [JsonProperty("Ret")]
        public T Data { get; set; }
        public override string ToString()
        {
            return "CallResponse [data=" + Data + ", Code=" + Code + ", ErrMsg=" + ErrMsg + "]";
        }
    }
    public class BaseResponse
    {
        public bool IsStatusOk()
        {
            return Code == ErrorCode.SUCCESS;
        }
        public int Code { get; set; }        
        public string ErrMsg { get; set; }

        public override string ToString()
        {
            return "BaseResponse [code=" + Code + ", errMsg=" + ErrMsg + "]";
        }
    }
}
