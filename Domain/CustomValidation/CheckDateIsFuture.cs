using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.CustomValidation
{
    public class CheckDateIsFuture : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                DateTime dt = (DateTime)value;

                if (dt > DateTime.Today)
                {
                    return ValidationResult.Success;
                }
            }
            return new ValidationResult(ErrorMessage ?? "Data deve ser maior que data atual");
        }
    }
}
