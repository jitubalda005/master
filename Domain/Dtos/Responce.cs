using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dtos
{
    public class Responce<T>
    {
        public bool IsSuccess { get; set; }

        public T Value { get; set; }

        public string Error { get; set; }
        public static Responce<T> Success(T vlaue) => new Responce<T> { IsSuccess = true, Value = vlaue };

        public static Responce<T> Failure(string error) => new Responce<T> { IsSuccess = false, Error = error };
    }
}
