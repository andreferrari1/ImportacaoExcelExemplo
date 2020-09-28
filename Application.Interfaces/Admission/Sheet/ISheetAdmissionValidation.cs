using Domain.Entities;
using System.Collections.Generic;
using System.Data;

namespace Application.Interfaces.Admission.Sheet
{
    public interface ISheetAdmissionValidation
    {
        bool Validate(DataTable Data, ref List<SheetAdmissionValidationErrors> Errors, ref List<SheetAdmission> ValidItems);
    }
}
