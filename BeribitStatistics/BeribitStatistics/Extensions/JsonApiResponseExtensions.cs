using BeribitStatistics.Models.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;

namespace BeribitStatistics.Extensions
{
    public static class JsonApiResponseExtensions
    {

        private static readonly JsonSerializerSettings SerializerSettings = new()
        {
            Formatting = Formatting.Indented,
            Culture = System.Globalization.CultureInfo.InvariantCulture
        };

        /// Разработал код: rangecode (04.11.22)
        public static JsonResult JsonResponse(this Controller controller, ApiResponse data) => controller.Json(data, SerializerSettings);

        /// Разработал код: rangecode (04.11.22)
        public static JsonResult JsonSuccess(this Controller controller, object data) => controller.Json(new ApiResponse(data), SerializerSettings);

        /// Разработал код: rangecode (04.11.22)
        public static JsonResult JsonError(this Controller controller, ErrorResponse data) => controller.Json(ApiResponse.WithError(data), SerializerSettings);

        /// Разработал код: rangecode (04.11.22)
        public static JsonResult JsonError(this Controller controller, ModelStateDictionary data) => controller.Json(ApiResponse.WithError(new ErrorResponse(data)), SerializerSettings);

        /// Разработал код: rangecode (04.11.22)
        public static JsonResult JsonError(this Controller controller, ModelStateDictionary data, string commonMessage) => controller.Json(ApiResponse.WithError(new ErrorResponse(data, commonMessage)), SerializerSettings);

        /// Разработал код: rangecode (04.11.22)
        public static JsonResult JsonError(this Controller controller, ErrorResponseCode code, string message) => controller.Json(ApiResponse.WithError(new ErrorResponse(code, message)), SerializerSettings);

    }
}
