using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Domain.Entities;
using Application.Interfaces.Admission.Imports;

namespace ApiRest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GetAllImportsController : ControllerBase
    {

        private readonly IImportsService _importsService;

        public GetAllImportsController(IImportsService importsService)
        {
            _importsService = importsService;
        }

        // GET: api/GetAllImports
        [HttpGet]
        public ActionResult<List<ReportFileImports>> GetAllImports()
        {
            return  _importsService.GetAllImports();
        }

    }
}

