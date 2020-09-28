using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.CustomValidation
{
    public class ChecksValueIsPositive : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                decimal number = 0;
                decimal numberCompare = Decimal.Parse(value.ToString());

                if (numberCompare > number)
                {
                    return ValidationResult.Success;
                }
            }
            return new ValidationResult(ErrorMessage ?? "Valor deve ser maior que 0");
        }
    }
}
