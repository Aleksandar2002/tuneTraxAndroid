using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AndroidProjekat.Common
{
    public static class ValidationExtensions
    {
        public static string GetError(this ValidationResult result, string property)
        {
            return result.Errors.FirstOrDefault(x => x.PropertyName == property + ".Value")?.ErrorMessage;
        }
    }
}
