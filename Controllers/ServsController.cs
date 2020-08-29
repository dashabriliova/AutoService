using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AutoService.DAL.Data;
using AutoService.DAL.Entities;
using Word=Microsoft.Office.Interop.Word;
using AutoService.Models;

namespace AutoService.Controllers
{
    public class ServsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ServsController(ApplicationDbContext context)
        {
            _context = context;
        }
        [Route("Services")]
        [Route("Services/Page_{pageNo}")]
        public async Task<IActionResult> Index(int pageNo = 1)
        {
            return View(await PaginatedList<Serv>.CreateAsync(_context.Servs.Include(a => a.Auto).Include(d=>d.Driver), pageNo, 10));

            //var applicationDbContext = _context.Servs.Include(s => s.Auto).Include(s => s.Driver);
            //return View(await applicationDbContext.ToListAsync());
        }

        public IActionResult CreateServs()
        {
            ViewData["AutoId"] = new SelectList(_context.Autos, "AutoId", "AutoNumber");
            ViewData["DriverId"] = new SelectList(_context.Drivers, "DriverId", "DriverFullName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateServs([Bind("ServId,ServDate,ServNaim,ServPokaz,AutoId,DriverId")] Serv serv)
        {
            if (ModelState.IsValid)
            {
                _context.Add(serv);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AutoId"] = new SelectList(_context.Autos, "AutoId", "AutoNumber", serv.AutoId);
            ViewData["DriverId"] = new SelectList(_context.Drivers, "DriverId", "DriverFullName", serv.DriverId);
            return View(serv);
        }
        public async Task<IActionResult> EditServs(int? id)
        {
            if (id == null)
            {
                return NotFound(500);
            }
            var serv = await _context.Servs.FindAsync(id);
            if (serv == null)
            {
                return NotFound();
            }
            ViewData["AutoId"] = new SelectList(_context.Autos, "AutoId", "AutoNumber", serv.AutoId);
            ViewData["DriverId"] = new SelectList(_context.Drivers, "DriverId", "DriverFullName", serv.DriverId);
            return View(serv);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditServs(int id, [Bind("ServId,ServDate, ServNaim,ServPokaz,DriverId ,AutoId")] Serv serv)
        {
            if (id != serv.ServId)
            {
                return NotFound();
            }

            ViewData["AutoId"] = new SelectList(_context.Autos, "AutoId", "AutoNumber", serv.AutoId);
            ViewData["DriverId"] = new SelectList(_context.Drivers, "DriverId", "DriverFullName", serv.DriverId);
            _context.Update(serv);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public ActionResult DeleteServs(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var servs = _context.Servs
                .Include(a => a.Auto)
                .Include(d=>d.Driver)
                 .FirstOrDefault(i => i.ServId == id);
            if (servs == null)
            {
                return NotFound();
            }
            return View(servs);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            Serv serv = _context.Servs.Find(id);
            _context.Servs.Remove(serv);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        private bool ServExists(int id)
        {
            return _context.Servs.Any(e => e.ServId == id);
        }
        public IActionResult DetailsServs(int? autoId)
        {

            if (autoId != null)
            {
                ViewBag.item = _context.Servs
                      .Include(a => a.Auto)
                      .Include(d=>d.Driver)
                      .Where(a => a.AutoId == autoId);
                return View();
            }
            return NotFound();
        }
        public ActionResult PrintServs(int? servsId)
        {
            Serv serv = _context.Servs
                        .Include(a => a.Auto)
                        .Include(d=>d.Driver)
                        .FirstOrDefault(s => s.ServId == servsId);
            Word.Application app = new Word.Application();
            Word.Document oDoc = app.Documents.Open(Environment.CurrentDirectory + @"\Листок_учета.dotx");

            try
            {
                oDoc.SaveAs2(FileName: Environment.CurrentDirectory + "_For_print.rtf");
                oDoc.Bookmarks["AutoName"].Range.Text = serv.Auto.AutoName;
                oDoc.Bookmarks["AutoNumber"].Range.Text = serv.Auto.AutoNumber;
                oDoc.Bookmarks["ServNaim"].Range.Text = serv.ServNaim;              
                oDoc.Bookmarks["ServPokaz"].Range.Text = serv.ServPokaz.ToString();
                oDoc.Bookmarks["ServDate"].Range.Text = serv.ServDate.ToString("dd.MM.yyyy");
                oDoc.Bookmarks["DriverFullName"].Range.Text = serv.Driver.DriverFullName;
                oDoc.Close();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                    oDoc.Close();
                    oDoc = null;
                    return NotFound("Возникла ошибка "+ ex.Message+" Обратитесь к системному администатору!");

            }
        }
    }
}

