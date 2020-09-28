using Application.Interfaces.Admission.Sheet;
using Application.Interfaces.Utilities;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data;

namespace Application.Admission.Sheet
{
    public class SheetAdmissionValidation : ISheetAdmissionValidation
    {
        private readonly IValidationErros _validationErros;
        public SheetAdmissionValidation(IValidationErros ValidationErros)
        {
            _validationErros = ValidationErros;
        }
        public bool Validate(DataTable Data, ref List<SheetAdmissionValidationErrors> Errors, ref List<SheetAdmission> ValidItems)
        {
            DataRow[] oDataRow = Data.Select();
            
            string erros = "";
            int line = 0;
            bool isValid= true;
            foreach (DataRow dr in oDataRow)
            {
                SheetAdmission lSheetAdmission = new SheetAdmission();
                lSheetAdmission.FileId = 0;

                try
                {
                    lSheetAdmission.DeliveryDate = DateTime.Parse(dr[0].ToString());
                }
                catch (Exception e)
                {
                    //  Block of code to handle errors
                }

                if ( string.IsNullOrEmpty( dr[0].ToString()) )
                {
                    break;
                }

                try
                {
                    lSheetAdmission.Amount = Int32.Parse(dr[2].ToString());
                }
                catch (Exception e)
                {
                    //  Block of code to handle errors
                }

                lSheetAdmission.ProductName = dr[1].ToString();

                try
                {
                    lSheetAdmission.UnitaryValue = Decimal.Parse(dr[3].ToString());
                }
                catch (Exception e)
                {
                    //  Block of code to handle errors
                }

                line++;
                erros = "";
                var isError = _validationErros.FormatErros(lSheetAdmission, ref erros);

                if (!isError)
                {
                    SheetAdmissionValidationErrors _sheetAdmissionValidationError = new SheetAdmissionValidationErrors();
                    _sheetAdmissionValidationError.Line = line;
                    _sheetAdmissionValidationError.Errors = erros;
                    Errors.Add(_sheetAdmissionValidationError);
                    isValid = false;
                }
                else
                {
                    ValidItems.Add(lSheetAdmission);
                }
            }
            return isValid;
        }
    }
}
