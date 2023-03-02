using System;
using Newtonsoft.Json;

namespace BeribitStatistics.Models.Responses
{
    public class ApiResponse
    {
        public static ApiResponse WithError(ErrorResponse error) => new(error);

        public bool Success { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public ErrorResponse Error { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public object Result { get; set; }

        public ApiResponse(ErrorResponse error)
        {
            Success = false;
            Error = error ?? throw new ArgumentNullException(nameof(error));
        }

        public ApiResponse(object result)
        {
            Success = true;
            Result = result;
        }
    }
}
