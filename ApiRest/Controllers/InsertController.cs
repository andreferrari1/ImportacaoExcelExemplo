using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Domain.Entities;
using Infrastructure.DBConfiguration.EFCore;
using Application.Interfaces.Services.Domain;
using Application.Interfaces.Utilities;
using Application.Interfaces.Services.Excel;
using Application.Interfaces.Admission.Sheet;
using Application.Admission.Sheet;

namespace ApiRest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InsertController : ControllerBase
    {
        private readonly ApplicationContext _context;

        private readonly IFileService _fileService;
        private readonly IFileToBinary _fileToBinary;
        private readonly ISheetAdmissionService _sheetAdmissionService;
        private readonly IValidationErros _validationErros;
        private readonly IExcelServices _excelServices;
        private readonly ISheetAdmissionValidation _sheetAdmissionValidation;
        

        public InsertController(
                ApplicationContext context,
                IFileService fileService,
                IFileToBinary fileToBinary,
                ISheetAdmissionService SheetAdmissionService,
                IValidationErros ValidationErros,
                IExcelServices ExcelServices,
                ISheetAdmissionValidation SheetAdmissionValidation
            )
        {
            _fileService = fileService;
            _fileToBinary = fileToBinary;
            _sheetAdmissionService = SheetAdmissionService;
            _validationErros = ValidationErros;
            _excelServices = ExcelServices;
            _sheetAdmissionValidation = SheetAdmissionValidation;
            _context = context;
        }

        // POST: api/Insert
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<SheetAdmissionValidationErrors>>> PostFile(File file)
        {
            _context.File.Add(file);

            //Lê o arquivo EXCEL
            var datatable2 = _excelServices.ReadSheet(file.Data, 0);

            ActionResult<List<SheetAdmissionValidationErrors>> _lsheetAdmissionValidationErrorRetorno = new List<SheetAdmissionValidationErrors>();
            List<SheetAdmissionValidationErrors> _lsheetAdmissionValidationError = new List<SheetAdmissionValidationErrors>();
            List<SheetAdmission> ValidItems = new List<SheetAdmission>();
            //Valida o Arquivo de Adimissão
            var isValid = _sheetAdmissionValidation.Validate(datatable2, ref _lsheetAdmissionValidationError, ref ValidItems);

            _lsheetAdmissionValidationErrorRetorno = _lsheetAdmissionValidationError;

            if (!isValid)
            {
                //Retorna os itens do arquivo para correção
                return Accepted(_lsheetAdmissionValidationError);
            }
            else
            {
                //Grava Arquivo
                var _file = await _fileService.AddAsync(file);
                _file.DateRegister = DateTime.Today;
                foreach (SheetAdmission lSheetAdmission in ValidItems)
                {
                    //Grava os itens do Arquivo
                    lSheetAdmission.FileId = _file.Id;
                    await _sheetAdmissionService.AddAsync(lSheetAdmission);
                }
            }

            return CreatedAtAction("GetFile", new { id = file.Id }, file);
        }

        // GET: api/Files/5
        [HttpGet("{id}")]
        public async Task<ActionResult<File>> GetFile(int id)
        {
            var file = await _context.File.FindAsync(id);

            if (file == null)
            {
                return NotFound();
            }

            return file;
        }

    }
}
/*




    */
