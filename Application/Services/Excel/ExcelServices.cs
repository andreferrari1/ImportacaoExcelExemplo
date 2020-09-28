using Application.Interfaces.Services.Excel;
using Spire.Xls;
using System.Data;

namespace Application.Services.Excel
{
    public class ExcelServices: IExcelServices  
    {
        public DataTable ReadSheet(byte[] FileData, int SheetsIndex)
        {
            //Create a new workbook
            Workbook workbook = new Workbook();
            //Load a file and imports its data
            System.IO.Stream stream = new System.IO.MemoryStream(FileData);
            workbook.LoadFromStream(stream);
            //Initialize worksheet
            Worksheet sheet = workbook.Worksheets[SheetsIndex];
            // get the data source that the grid is displaying data for
            DataTable datatable = sheet.ExportDataTable();

            return datatable;
        }
    }
}
