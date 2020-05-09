using System;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace eProject.App.Validations
{
    public class ValidationImage : ValidationAttribute
    {
        private readonly string[] ValidTypes = new[] { ".jpg", ".jpeg", ".pjpeg", ".png" };

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            IFormFile file = value as IFormFile;
            if (file != null)
            {

                if (!ValidTypes.Any(x => x.Equals(Path.GetExtension(file.FileName))))
                {
                    return new ValidationResult("Please select an image with an extension of JPG, JPEG, PJEG, PNG");
                }
            }

            return ValidationResult.Success;
        }
    }
}
