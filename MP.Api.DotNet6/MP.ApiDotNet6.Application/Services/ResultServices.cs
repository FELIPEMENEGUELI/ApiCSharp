using FluentValidation.Results;

namespace MP.ApiDotNet6.Application.Services
{
    public class ResultServices
    {
        public bool IsSucess { get; set; }
        public string Message { get; set; }
        public ICollection<ErrorValidation> Errors { get; set; }

        public static ResultServices RequestError(string message, ValidationResult validationResult)
        {
            return new ResultServices
            {
                IsSucess = false,
                Message = message,
                Errors = validationResult.Errors.Select(x => new ErrorValidation { Field = x.PropertyName, Message = x.ErrorMessage }).ToList()
            };
        }

        public static ResultServices<T> RequestError<T>(string message, ValidationResult validationResult)
        {
            return new ResultServices<T>
            {
                IsSucess = false,
                Message = message,
                Errors = validationResult.Errors.Select(x => new ErrorValidation { Field = x.PropertyName, Message = x.ErrorMessage }).ToList()
            };
        }

        public static ResultServices Fail(string message) => new ResultServices { IsSucess = false, Message = message };
        public static ResultServices<T> Fail<T>(string message) => new ResultServices<T> { IsSucess = false, Message = message };

        public static ResultServices Ok(string message) => new ResultServices { IsSucess = true, Message = message };
        public static ResultServices<T> Ok<T>(T data) => new ResultServices<T> { IsSucess = true, Data = data };

    }

    public class ResultServices<T> : ResultServices
    {
        public T Data { get; set; }
    }
}
