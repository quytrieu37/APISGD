using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopeeFake.UI.Infrastructure
{
    public class Response<T>
    {
        public bool State { get; set; }
        public string Message { get; set; }
        public T @object { get; set; }

    }
    public class ResponsDefault
    {
        public string Data { get; set; }
    }
    public class ValidatorError
    {
        public string FieldName { get; set; }
        public string Message { get; set; }
    }
    public class ResponseToken
    {
        public string AccessToken { get; set; }
        public string FullName { get; set; }
        public long Expires { get; set; }
    }
    public class ErrorCode
    {
        public const string Success = "11111111";
        public const string BadRequest = "00000000";
        public const string ValidateError = "00000001";
        public const string NotFound = "00000011";
        public const string NotEmpty = "00000111";
        public const string MinLength8 = "00001111";
        public const string MaxLength255 = "00011111";
        public const string InvalidFormat = "00111111";
        public const string InvalidVerifyPassword = "01111111";
        public const string InvalidCurrentPassword = "00000100";
        public const string LockoutUser = "00000101";
        public const string ExistUserOrEmail = "00000110";
        public const string ExistStore = "00000100";
        public const string ExcuteDB = "00001110"; 
        public const string Forbidden = "00001010";
    }
        
}
