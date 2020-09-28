using Domain.CustomValidation;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class SheetAdmission : IIdentityEntity , Sheet
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Id do arquvio obrigatório")]
        [Display(Name = "Arquivo")]
        public int FileId { get; set; }

        [CheckDateIsFuture]
        [Required(ErrorMessage = "Data de entrega obrigatória")]
        [Display(Name = "Data de Entrega")]
        public DateTime DeliveryDate { get; set; }

        [StringLength(50, ErrorMessage = "Nome do produto tamanho máximo são 50 caracteres.")]
        [Required(ErrorMessage = "Nome do produto obrigatório")]
        [Display(Name = "Produto")]
        public string ProductName { get; set; }

        [ChecksValueIsPositive(ErrorMessage = "A quantidade deve ser maior que 0.")]
        [Required(ErrorMessage = "Quantidade obrigatória")]
        [Display(Name = "Quantidade")]
        public int Amount { get; set; }

        [Required(ErrorMessage = "Valor Unitario obrigatório")]
        [ChecksValueIsPositive(ErrorMessage = "O valor unitário deve ser maior que 0.")]
        [Display(Name = "Valor Unitário")]
        public decimal UnitaryValue { get; set; }

        [Display(Name = "Valor Total")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public decimal SumAmount
        {
            get { return UnitaryValue * Amount; }
        }
        [Display(Name = "Arquivo")]
        public virtual File File { get; set; }
    }
}
