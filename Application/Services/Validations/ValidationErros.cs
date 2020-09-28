using Application.Interfaces.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Application.Utilities
{
    public class ValidationErros: IValidationErros
    {
        public IEnumerable<ValidationResult> GetValidationErros(object _obj)
        {
            var _validationResult = new List<ValidationResult>();
            var contexto = new ValidationContext(_obj, null, null);
            Validator.TryValidateObject(_obj, contexto, _validationResult, true);
            return _validationResult;
        }

        public bool FormatErros(object _obj, ref string ErrosList)
        {
            var erros = GetValidationErros(_obj);
            foreach (var error in erros)
            {
                ErrosList += error.ErrorMessage + Environment.NewLine;
            }
            if (ErrosList.Length > 0)
            {
                ErrosList = " " + Environment.NewLine + Environment.NewLine + ErrosList;
                return false;
            }

            return true;
        }
    }
}