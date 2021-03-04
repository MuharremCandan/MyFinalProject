using Core.Utilities.Results.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Results.Concrete
{
    public class Result : IResult
    {

        // :this(success) => altta zaten yazdık üstte tekrarlamaya gerek yok o yüzden alttaki kodu üste verme işlemi 
        public Result(bool success, string message):this(success)
        {           
            Message = message;
        }
        public Result(bool success)
        {
            Success = success;
           
        }

        public bool Success { get; }

        public string Message { get; }
    }
}
