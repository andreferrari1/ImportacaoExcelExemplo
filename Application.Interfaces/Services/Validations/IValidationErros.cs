using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Application.Interfaces.Utilities
{
    public interface IValidationErros 
    {
        IEnumerable<ValidationResult> GetValidationErros(object _obj);
        bool FormatErros(object _obj, ref string Erros);
    }
}
