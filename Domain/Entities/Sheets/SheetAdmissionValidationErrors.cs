using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class SheetAdmissionValidationErrors
    {
        [Display(Name = "Linha")]
        public int Line { get; set; }
        [Display(Name = "Erros")]
        public string Errors { get; set; }
    }
}
