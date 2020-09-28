using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using Infrastructure.DBConfiguration.EFCore;
using Application.Interfaces.Admission.Imports;

namespace ApiRest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GetImportByIdController : ControllerBase
    {
        private readonly IImportsService _importsService;

        public GetImportByIdController(IImportsService importsService)
        {
            _importsService = importsService;
        }
        
        // GET: api/GetImportById/5
        [HttpGet("{id}")]
        public async Task<ActionResult<List<SheetAdmission>>> GetImportById(int id)
        {

            ActionResult<List<SheetAdmission>> sheetAdmission = new List<SheetAdmission>();
            sheetAdmission = await _importsService.GetImportById(id);

            if (sheetAdmission.Value.Count == 0)
            {
                return NotFound();
            }

            return sheetAdmission;
        }

    }
}
