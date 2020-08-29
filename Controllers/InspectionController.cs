using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoService.DAL.Data;
using AutoService.DAL.Entities;
using AutoService.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SQLitePCL;

namespace AutoService.Controllers
{
    public class InspectionController : Controller
    {
        ApplicationDbContext _context;
        public InspectionController(ApplicationDbContext context)
        {
            _context = context;
        }
        [Route("Inspection")]
        [Route("Inspection/Page_{pageNo}")]
        public async Task<IActionResult> Index(int pageNo = 1)
        {
            return View(await PaginatedList<Inspection>.CreateAsync(_context.Inspections.Include(a=>a.Auto), pageNo, 10));

            //return View(_context.Inspections.Include(a => a.Auto));
        }
        public IActionResult CreateInspection()
        {
            ViewData["AutoId"] = new SelectList(_context.Autos, "AutoId", "AutoNumber");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateInspection([Bind("InspectionId, InspectionDate,InspectionMonthNext ,AutoId")] Inspection inspection)
        {
            if (ModelState.IsValid)
            {
                _context.Add(inspection);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AutoId"] = new SelectList(_context.Autos, "AutoId", "AutoNumber", inspection.AutoId);
            return View(inspection);
        }
        public IActionResult DetailsInspection(int? autoId)
        {

            if (autoId != null)
            {
                ViewBag.item = _context.Inspections
                      .Include(a => a.Auto)
                      .Where(a => a.AutoId == autoId);
                return View();
            }
            return NotFound();
        }
        public async Task<IActionResult> EditInspection(int? id)
        {
            if(id==null)
            {
                return NotFound();
            }
            var insp = await _context.Inspections.FindAsync(id);
            if(insp==null)
            {
                return NotFound();
            }
            ViewData["AutoId"] = new SelectList(_context.Autos, "AutoId", "AutoNumber", insp.AutoId);
            return View(insp);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditInspection(int id, [Bind("InspectionId, InspectionDate, InspectionMonthNext,AutoId")] Inspection inspection)
        {
            if (id != inspection.InspectionId)
            {
                return NotFound();
            }

            ViewData["AutoId"] = new SelectList(_context.Autos, "AutoId", "AutoNumber", inspection.AutoId);

            _context.Update(inspection);
                    await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
        }

        private bool InsuranceExists(int id)
        {
            return _context.Inspections.Any(e => e.InspectionId == id);
        }
        public ActionResult DeleteInspection(int? id)
        {
           if(id==null)
            {
                return NotFound();
            }
            var inspection = _context.Inspections
                .Include(a=>a.Auto)
                 .FirstOrDefault(i => i.InspectionId == id);
            if(inspection==null)
            {
                return NotFound();
            }
            return View(inspection);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            Inspection inspection = _context.Inspections.Find(id);
            _context.Inspections.Remove(inspection);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }


    }
}
