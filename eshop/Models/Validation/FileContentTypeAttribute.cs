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
    public class FileContentTypeAttribute : ValidationAttribute, IClientModelValidator
    {
        private readonly string contentType;
        public FileContentTypeAttribute(string contentType)
        {
            this.contentType = contentType;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return ValidationResult.Success;
            }
            else if (value is IFormFile iff)
            {
                if (iff.ContentType.ToLower().Contains(contentType.ToLower()))
                {
                    return ValidationResult.Success;
                }
                else
                {
                    return new ValidationResult(GetErrorMessage("File"), new List<string> { validationContext.MemberName });
                }
            }

            throw new NotImplementedException($"The attribute {nameof(FileContentTypeAttribute)} is not implemented for the object {value.GetType()}. Only {nameof(IFormFile)} is implemented.");
        }

        protected string GetErrorMessage(string memberName)
        {
            return $"{ memberName } must be the type of {contentType}!";
        }

        public void AddValidation(ClientModelValidationContext context)
        {
            ClientSideAttributeHelper.MergeAttribute(context.Attributes, "data-val", "true");
            ClientSideAttributeHelper.MergeAttribute(context.Attributes, "data-val-filecontent", GetErrorMessage("File"));
            ClientSideAttributeHelper.MergeAttribute(context.Attributes, "data-val-filecontent-type", contentType);
        }

        /*
        private bool MergeAttribute(IDictionary<string, string> attributes, string key, string value)
        {
            if (attributes.ContainsKey(key))
            {
                return false;
            }

            attributes.Add(key, value);
            return true;
        }
        */
    }
}
