using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using AutoService.DAL.Data;
using AutoService.DAL.Entities;
using AutoService.Models;
using AutoService.Quartz;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace AutoService.Controllers
{
    public class AutoController : Controller
    {
        readonly ApplicationDbContext _context;

        public AutoController(ApplicationDbContext context)
        {
            _context = context;

        }
        [Route("Auto")]
        [Route("Auto/Page_{pageNo}")]
        public async Task<IActionResult> Index(int pageNo=1)
        {
            return View(await PaginatedList<Auto>.CreateAsync(_context.Autos, pageNo, 3));
        }
        //public IActionResult Index(int pageNo = 1)
        //{
        //    int _pageSize = 3;

        //    return View(ListViewModel<Auto>.GetModel(_context.Autos, pageNo, _pageSize));
        //    //return View(_context.Autos);
        //}
        public IActionResult CreateAuto() => View();
        [HttpPost]
        public async Task<IActionResult> CreateAuto(CreateAutoModel model)
        {
            if (ModelState.IsValid)
            {
                Auto auto = new Auto
                {
                    AutoName = model.AutoName,
                    AutoNumber = model.AutoNumber,
                    AutoVIN = model.AutoVIN,
                    AutoYear = model.AutoYear,
                    AutoImage = model.AutoImage
                };

                _context.Autos.Add(auto);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> EditAuto(int? id)
        {
            if (id != null)
            {
                Auto auto = await _context.Autos.FirstOrDefaultAsync(a => a.AutoId == id);
                if (auto != null)
                    return View(auto);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> EditAuto(Auto auto)
        {
            _context.Autos.Update(auto);
            await _context.SaveChangesAsync();
            return RedirectToAction("index");
        }
        public async Task<IActionResult> DetailsAuto(int? id)
        {
            if (id != null)
            {
                Auto auto = await _context.Autos.FirstOrDefaultAsync(a => a.AutoId == id);
                if (auto != null)
                    return View(auto);
            }
            return NotFound();
        }
        public ActionResult SearchAuto(string name)
        {
            IQueryable<Auto> autos = _context.Autos;
            if (!String.IsNullOrEmpty(name))
            {
                autos = autos.Where(a => a.AutoName.Contains(name));
            }
            AutoListViewModel viewModel = new AutoListViewModel
            {
                Autos = autos.ToList(),
                AutoNaim = name
            };
            return View(viewModel);
        }
        public async Task<IActionResult> DeleteAuto(int? id)
        {
            if(id==null)
            {
                return NotFound();
            }
            var auto = await _context.Autos
                .FirstOrDefaultAsync(a => a.AutoId == id);
            if(auto==null)
            {
                return NotFound();
            }
            return View(auto);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            Auto auto =  _context.Autos.Find(id);
            _context.Autos.Remove(auto);
             _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        

    }
}
