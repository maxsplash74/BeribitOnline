using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;

namespace BeribitStatistics.Models.Responses
{
    public enum ErrorResponseCode
    {
        Unknown,
        RequestInvalid,
        OperationInvalid,
        TwoFactorError,
        InternalServerError = 500,
        NotFound = 400
    }

    public class ErrorResponse
    {
        public static ErrorResponse InternalServerError => new(ErrorResponseCode.InternalServerError, "Внутренняя ошибка сервера");
        public static ErrorResponse TwoFactorError => new(ErrorResponseCode.TwoFactorError, "Неверный код двухфакторной авторизации!");
        public static ErrorResponse InvalidOperation(string description) => new(ErrorResponseCode.OperationInvalid, description);

        public int Code => (int)DescriptionCode;

        public ErrorResponseCode DescriptionCode { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<string, string[]> RequestErrors { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Message { get; set; }

        public DateTime Time { get; set; }

        public ErrorResponse(ModelStateDictionary modelState)
            : this(modelState
                .Where(a => a.Value.ValidationState == ModelValidationState.Invalid)
                .ToDictionary(
                    k => k.Key,
                    v => v.Value.Errors.Select(a => a.ErrorMessage).ToArray()
                    ))
        {

        }

        public ErrorResponse(ModelStateDictionary modelState, string message)
            : this(modelState
                .Where(a => a.Value.ValidationState == ModelValidationState.Invalid)
                .ToDictionary(
                    k => k.Key,
                    v => new[] { message }
                    ))
        {

        }

        public ErrorResponse(Dictionary<string, string[]> requestErrors)
        {
            DescriptionCode = ErrorResponseCode.RequestInvalid;
            RequestErrors = requestErrors;
            Time = DateTime.UtcNow;
        }

        public ErrorResponse(ErrorResponseCode code, string message)
        {
            if (code == ErrorResponseCode.RequestInvalid)
                throw new NotSupportedException($"{nameof(code)} ({code}) is not suppoted this constructor");

            DescriptionCode = code;
            Message = message;
            Time = DateTime.UtcNow;
        }
    }
}
