using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static LayeredArchitecture.Common.ApiResponse.DefineResponse;

namespace LayeredArchitecture.Common.ApiResponse
{
    public class ApiResponse
    {
        public int status { get; set; }
        public string code { get; set; }
        public string message { get; set; }
        public object? data { get; set; }

        public ApiResponse()
        {
            status = 200;
            code = string.Empty;
            message = string.Empty;
            data = null;
        }

        public static ApiResponse Response(EnumCodes enumCode, string? field = null, string? value = null, object? data = null)
        {
            var responseItem = GetResponseItem(enumCode, field, value);
            var code = enumCode.ToString().Split("_")[2];

            return new ApiResponse()
            {
                status = Convert.ToInt32(code),
                code = responseItem != null ? responseItem.Code : string.Empty,
                message = responseItem != null ? responseItem.Message : string.Empty,
                data = data
            };
        }

        public static ApiResponse Response(ResponseItem responseItem)
        {
            return new ApiResponse()
            {
                status = Convert.ToInt32(responseItem.Code.Split("_")[2]),
                code = responseItem.Code,
                message = responseItem != null ? responseItem.Message : string.Empty
            };
        }

        public static ResponseItem? GetResponseItemFromStringCode(string errorCode, string? field = null, string? value = null)
        {
            var errCode = errorCode.ToString().Split("_")[2];
            throw new Exception(JsonConvert.SerializeObject(new DataError
            {
                Status = Convert.ToInt32(errCode),
                Code = errorCode.ToString(),
                field = field,
                value = value
            }));
        }

        public static ResponseItem? GetResponseItem(string enumCode, string? field = null, string? value = null)
        {
            var listResponseItem = listResponseItems.FirstOrDefault(x => x.Code == enumCode);
            if (listResponseItem != null)
            {
                var listResponseItemClone = JsonConvert.SerializeObject(listResponseItem);
                var responseItem = JsonConvert.DeserializeObject<ResponseItem>(listResponseItemClone);

                responseItem.Message = responseItem.Message
                    .Replace("{field}", field)
                    .Replace("{value}", value);

                return responseItem;
            }
            return null;
        }

        public static ResponseItem? GetResponseItem(EnumCodes enumCode, string? field = null, string? value = null)
        {
            var listResponseItem = listResponseItems.FirstOrDefault(x => x.Code == enumCode.ToString());
            if (listResponseItem != null)
            {
                var listResponseItemClone = JsonConvert.SerializeObject(listResponseItem);
                var responseItem = JsonConvert.DeserializeObject<ResponseItem>(listResponseItemClone);

                responseItem.Message = responseItem.Message
                    .Replace("{field}", field)
                    .Replace("{value}", value);

                return responseItem;
            }
            return null;
        }
    }
}
