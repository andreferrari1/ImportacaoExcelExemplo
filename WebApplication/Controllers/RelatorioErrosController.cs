using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using Infrastructure.DBConfiguration.EFCore;
using Application.Admission.Sheet;

namespace WebApplication.Controllers
{
    public class RelatorioErrosController : Controller
    {
        private readonly ApplicationContext _context;

        public RelatorioErrosController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: RelatorioErros
        //public IActionResult Index(List<SheetAdmissionValidationErrors> erros )
       public IActionResult Index()
        {
           List<SheetAdmissionValidationErrors> erros = (List<SheetAdmissionValidationErrors>)TempData["_sheetAdmissionValidationErrors"];
            //var applicationContext = _context.SheetAdmission.Include(s => s.File);
            return View(erros);
        }

        // GET: RelatorioErros/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sheetAdmission = await _context.SheetAdmission
                .Include(s => s.File)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sheetAdmission == null)
            {
                return NotFound();
            }

            return View(sheetAdmission);
        }

        // GET: RelatorioErros/Create
        public IActionResult Create()
        {
            ViewData["FileId"] = new SelectList(_context.File, "Id", "Id");
            return View();
        }

        // POST: RelatorioErros/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FileId,DeliveryDate,ProductName,Amount,UnitaryValue")] SheetAdmission sheetAdmission)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sheetAdmission);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FileId"] = new SelectList(_context.File, "Id", "Id", sheetAdmission.FileId);
            return View(sheetAdmission);
        }

        // GET: RelatorioErros/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sheetAdmission = await _context.SheetAdmission.FindAsync(id);
            if (sheetAdmission == null)
            {
                return NotFound();
            }
            ViewData["FileId"] = new SelectList(_context.File, "Id", "Id", sheetAdmission.FileId);
            return View(sheetAdmission);
        }

        // POST: RelatorioErros/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FileId,DeliveryDate,ProductName,Amount,UnitaryValue")] SheetAdmission sheetAdmission)
        {
            if (id != sheetAdmission.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sheetAdmission);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SheetAdmissionExists(sheetAdmission.Id))
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
            ViewData["FileId"] = new SelectList(_context.File, "Id", "Id", sheetAdmission.FileId);
            return View(sheetAdmission);
        }

        // GET: RelatorioErros/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sheetAdmission = await _context.SheetAdmission
                .Include(s => s.File)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sheetAdmission == null)
            {
                return NotFound();
            }

            return View(sheetAdmission);
        }

        // POST: RelatorioErros/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sheetAdmission = await _context.SheetAdmission.FindAsync(id);
            _context.SheetAdmission.Remove(sheetAdmission);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SheetAdmissionExists(int id)
        {
            return _context.SheetAdmission.Any(e => e.Id == id);
        }
    }
}
