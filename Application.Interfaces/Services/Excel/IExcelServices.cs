using System.Data;
using System.IO;

namespace Application.Interfaces.Services.Excel
{
    public interface IExcelServices
    {
        DataTable ReadSheet(byte[] FileData, int SheetsIndex);
    }
}
