using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Application.Interfaces.Services.Domain;
using Application.Interfaces.Utilities;
using Spire.Xls;
using System.Data;
using System;
using Application.Interfaces.Services.Excel;
using Application.Interfaces.Admission.Sheet;
using Application.Admission.Sheet;
using Infrastructure.Interfaces.Repositories.Domain;
using System.Net;
using Newtonsoft.Json;

namespace WebApplication.Controllers
{
    public class FilesController : Controller
    {
        private readonly IFileService _fileService;
        private readonly IFileToBinary _fileToBinary;
        private readonly ISheetAdmissionService _sheetAdmissionService;
        private readonly IValidationErros _validationErros;
        private readonly IExcelServices _excelServices;
        private readonly ISheetAdmissionValidation _sheetAdmissionValidation;
        private readonly IImportsRepository _importsRepository;

        

        public FilesController(
                IFileService fileService, 
                IFileToBinary fileToBinary, 
                ISheetAdmissionService SheetAdmissionService, 
                IValidationErros ValidationErros,
                IExcelServices ExcelServices,
                ISheetAdmissionValidation SheetAdmissionValidation,
                IImportsRepository importsRepository)
        {
            _fileService = fileService;
            _fileToBinary = fileToBinary;
            _sheetAdmissionService = SheetAdmissionService;
            _validationErros = ValidationErros;
            _excelServices = ExcelServices;
            _sheetAdmissionValidation = SheetAdmissionValidation;
            _importsRepository = importsRepository;
        }

        // GET: Files
        public async Task<IActionResult> Index()
        {
            string ApiBaseUrl = "https://localhost:44309/"; 
            string MetodoPath = "api/GetAllImports"; 

            try
            {
                var httpWebRequest = (HttpWebRequest)WebRequest.Create(ApiBaseUrl + MetodoPath);
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Method = "GET";


                using (var resposta = (HttpWebResponse)httpWebRequest.GetResponse())
                {
                    var streamDados = resposta.GetResponseStream();
                    System.IO.StreamReader reader = new System.IO.StreamReader(streamDados);
                    object objResponse = reader.ReadToEnd();

                    List<ReportFileImports> model = new List<ReportFileImports>();
                    model = JsonConvert.DeserializeObject<List<ReportFileImports>>(objResponse.ToString());

                    return View(model);
                }
               
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        // GET: Files/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var file = await _fileService.GetByIdAsync(id);
            if (file == null)
            {
                return NotFound();
            }

            return View(file);
        }

        // GET: Files/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Files/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Dados")] File file, List<IFormFile> _files)
        {

            FileUpload _fileUpload = new FileUpload();
            File _fileUploadResult = new File();
            string ApiBaseUrl = "https://localhost:44309/";
            string MetodoPath = "api/Insert";


            if (_files.Count() > 0)
            {
                _fileUpload.Name = _files[0].FileName;
                byte[] binary = null;
                _fileToBinary.Convert(_files[0].OpenReadStream(), ref binary);
                _fileUpload.Data = binary;
                _fileUpload.DateRegister = DateTime.Today;
            }

            string jsonString = JsonConvert.SerializeObject(_fileUpload);

            try
            {
                var httpWebRequest = (HttpWebRequest)WebRequest.Create(ApiBaseUrl + MetodoPath);
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Method = "POST";

                using (var streamWriter = new System.IO.StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    streamWriter.Write(jsonString);
                }

                using (var resposta = (HttpWebResponse)httpWebRequest.GetResponse())
                {
                    var streamDados = resposta.GetResponseStream();
                    System.IO.StreamReader reader = new System.IO.StreamReader(streamDados);
                    object objResponse = reader.ReadToEnd();

                    //201 OK Status Create 
                    if (resposta.StatusCode == HttpStatusCode.Created)
                    {
                        _fileUploadResult = JsonConvert.DeserializeObject<File>(objResponse.ToString());
                        return RedirectToAction("Index","SheetAdmissions", new { id = _fileUploadResult.Id });

                        //return RedirectToAction("Index", "Files");
                    }
                    //202 Status Problemas de validação de dados
                    if (resposta.StatusCode == HttpStatusCode.Accepted)
                    {
                        List<SheetAdmissionValidationErrors> _sheetAdmissionValidationErrors = new List<SheetAdmissionValidationErrors>();
                        _sheetAdmissionValidationErrors = JsonConvert.DeserializeObject<List<SheetAdmissionValidationErrors>>(objResponse.ToString());

                        TempData["_sheetAdmissionValidationErrors"] = _sheetAdmissionValidationErrors;
                        return RedirectToAction("Index", "RelatorioErros");


                    }
                  return View(_fileUploadResult);
                }

            }
            catch (Exception e)
            {
               throw e;
            }

        }

        // GET: Files/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var file = await _fileService.GetByIdAsync(id);
            if (file == null)
            {
                return NotFound();
            }
            return View(file);
        }

        // GET: Files/Edit/5
        public async Task<IActionResult> Download(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var file = await _fileService.GetByIdAsync(id);
            if (file == null)
            {
                return NotFound();
            }
            var FileStreamResult = FileDownload(file.Data, file.Name);
            return FileStreamResult;
        }

        // GET: Process/id
        public async Task<IActionResult> Process(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var file = await _fileService.GetByIdAsync(id);
            if (file == null)
            {
                return NotFound();
            }

            var datatable2 =  _excelServices.ReadSheet(file.Data, 0);

            List<SheetAdmissionValidationErrors> _lsheetAdmissionValidationError = new List<SheetAdmissionValidationErrors>();
            List<SheetAdmission> ValidItems = new List<SheetAdmission>();

            var isValid = _sheetAdmissionValidation.Validate(datatable2, ref _lsheetAdmissionValidationError, ref ValidItems);

            //Create a new workbook
            Workbook workbook = new Workbook();
            //Load a file and imports its data
            System.IO.Stream stream = new System.IO.MemoryStream(file.Data);
            workbook.LoadFromStream(stream);
            //Initialize worksheet
            Worksheet sheet = workbook.Worksheets[0];
            // get the data source that the grid is displaying data for
            DataTable datatable = sheet.ExportDataTable();

            DataRow[] oDataRow = datatable.Select();

            foreach (DataRow dr in oDataRow)
            {
                SheetAdmission lSheetAdmission = new SheetAdmission();
                lSheetAdmission.FileId = id;

                try
                {
                    lSheetAdmission.DeliveryDate = DateTime.Parse(dr[0].ToString());
                }
                catch (Exception e)
                {
                    //  Block of code to handle errors
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
                
                string erros = "";
                _validationErros.FormatErros(lSheetAdmission, ref erros);

                //Adiciona Itens
                await _sheetAdmissionService.AddAsync(lSheetAdmission);
            }

            //List<SheetAdmission> lsheetAdmission = new List<SheetAdmission>();
            //var lsheetAdmission = _sheetAdmissionService.GetByIdIncludingTasksAsync(23);

            //List<ReportFileImports> reportFileImports = new List<ReportFileImports>();
            var reportFileImports = _importsRepository.GetAllImports();

            var reportFileImportsById = await _importsRepository.GetImportById(23);


            var FileStreamResult = FileDownload(file.Data, file.Name);
            return FileStreamResult;
        }
        

        // POST: Files/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Dados")] File file)
        {
            if (id != file.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _fileService.UpdateAsync(file);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FileExists(file.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(file);
        }

        // GET: Files/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var file = await _fileService.GetByIdAsync(id);
            if (file == null)
            {
                return NotFound();
            }

            return View(file);
        }

        // POST: Files/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _fileService.RemoveAsync(id);
            return RedirectToAction(nameof(Index));
        }

        private bool FileExists(int id)
        {
            return _fileService.GetByIdAsync(id) != null ? true : false;

        }

        private FileStreamResult FileDownload(byte[] FileData, string FileName)
        {
            var content = new System.IO.MemoryStream(FileData);
            var contentType = "APPLICATION/octet-stream";
            var fileName = FileName;

            return File(content, contentType, fileName);
        }
        
    }
}
