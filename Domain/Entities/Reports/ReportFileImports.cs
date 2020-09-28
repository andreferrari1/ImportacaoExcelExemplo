using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class ReportFileImports
    {
        [Display(Name = "Id Arquivo")]
        public int FileId { get; set; }

        [Display(Name = "Data de Cadastro")]
        public DateTime FileDateRegister { get; set; }

        [Display(Name = "Quantidade Itens")]
        public int Amount { get; set; }

        [Display(Name = "Menor dt. Entrega")]
        public DateTime DeliveryDateMin { get; set; }

        [DisplayFormat(DataFormatString = "{0:C}")]
        [Display(Name = "Valor Total")]
        public decimal AmountValue { get; set; }
    }
}
