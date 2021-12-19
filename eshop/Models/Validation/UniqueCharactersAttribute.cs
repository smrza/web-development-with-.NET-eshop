using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace eshop.Models.Validation
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Parameter, AllowMultiple = false)]
    public class UniqueCharactersAttribute : ValidationAttribute
    {
        private readonly int characterCount;
        public UniqueCharactersAttribute(int characterCount)
        {
            this.characterCount = characterCount;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null) 
            { 
                return new ValidationResult("Oh come on, you forgot this field.");
            } 

            if (value is string str)
            {
                str = str.ToLower();
                var count = str.Distinct().Count();

                if (count >= characterCount)
                {
                    return ValidationResult.Success;
                }
                else
                {
                    return new ValidationResult(GetErrorMessage("Password"));
                }

            }
            throw new NotImplementedException($"The attribute {nameof(UniqueCharactersAttribute)} is not implemented for object {value.GetType()}. Only {nameof(String)} is implemented.");
        }

        protected string GetErrorMessage(string memberName)
        {
            return $"{ memberName } must contain 6 unique characters!";
        }
    }
}
