using Windows.Api.Shared.Enums;
using Windows.Infrastructure.Extensions;

namespace Windows.Api.Shared
{
    /// <summary>
    /// 返回Model
    /// </summary>
    public class ApiResponse
    {
        public ApiResponse()
        {
            Status = (int)ApiStatusEnum.Success;
        }
        public ApiResponse(dynamic data)
        {
            Data = data;
            Status = (int)ApiStatusEnum.Success;
        }

        public ApiResponse(ApiStatusEnum apiStatusEnum, string message = "")
        {
            Status = (int)apiStatusEnum;
            Message = message;
        }

        public ApiResponse(int apiStatus, string message = "")
        {
            Status = apiStatus;
            Message = message;
        }
        /// <summary>
        /// 状态码
        /// </summary>
        public int Status { get; set; }
        private string message;
        /// <summary>
        /// 信息
        /// </summary>
        public string Message
        {
            get
            {
                return string.IsNullOrEmpty(message) 
                    ? Status.GetDescription<ApiStatusEnum>()
                    : message;
            }
            set
            {
                message = value;
            }
        }
        /// <summary>
        /// 数据
        /// </summary>
        public object Data { get; set; }
    }
}
