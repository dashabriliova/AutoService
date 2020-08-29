using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AutoService.DAL.Data;
using AutoService.DAL.Entities;
using AutoService.Models;
using Microsoft.AspNetCore.SignalR;

namespace AutoService.Controllers
{
    public class InsurancesController : Controller
    {
        private readonly ApplicationDbContext _context;
        public InsurancesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Insurances
        [Route("Insurances")]
        [Route("Insurances/Page_{pageNo}")]
        public async Task<IActionResult> Index(int pageNo = 1)
        {
            return View(await PaginatedList<Insurance>.CreateAsync(_context.Insurances.Include(a => a.Auto), pageNo, 10));

            //var applicationDbContext = _context.Insurances.Include(i => i.Auto);
            //return View(await applicationDbContext.ToListAsync());
        }

        // GET: Insurances/CreateInsurance
        public IActionResult CreateInsurance()
        {
            ViewData["AutoId"] = new SelectList(_context.Autos, "AutoId", "AutoNumber");
            return View();
        }

        // POST: Insurances/CreateInsurance
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateInsurance([Bind("InsuranceId, InsuranceDate,InsuranceDateProlongation ,AutoId")] Insurance insurance)
        {
            if (ModelState.IsValid)
            {
                _context.Add(insurance);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AutoId"] = new SelectList(_context.Autos, "AutoId", "AutoNumber", insurance.AutoId);
            return View(insurance);
        }
        public IActionResult DetailsInsurance(int? autoId)
        {

            if (autoId != null)
            {
                ViewBag.item = _context.Insurances
                      .Include(a => a.Auto)
                      .Where(a => a.AutoId == autoId);
                return View();
            }
            return NotFound();
        }
        public async Task<IActionResult> EditInsurance(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var insurance = await _context.Insurances.FindAsync(id);
            if (insurance == null)
            {
                return NotFound();
            }
            ViewData["AutoId"] = new SelectList(_context.Autos, "AutoId", "AutoNumber", insurance.AutoId);
            return View(insurance);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditInsurance(int id, [Bind("InsuranceId,InsuranceDate,InsuranceDateProlongation,AutoId")] Insurance insurance)
        {
            if (id != insurance.InsuranceId)
            {
                return NotFound();
            }

            ViewData["AutoId"] = new SelectList(_context.Autos, "AutoId", "AutoNumber", insurance.AutoId);
            _context.Update(insurance);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public ActionResult DeleteInsurance(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var insurance = _context.Insurances
                .Include(a => a.Auto)
                 .FirstOrDefault(i => i.InsuranceId == id);
            if (insurance == null)
            {
                return NotFound();
            }
            return View(insurance);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            Insurance insurance = _context.Insurances.Find(id);
            _context.Insurances.Remove(insurance);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        private bool InsuranceExists(int id)
        {
            return _context.Insurances.Any(e => e.InsuranceId == id);
        }
    }
}
