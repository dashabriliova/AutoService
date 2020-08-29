using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoService.DAL.Data;
using AutoService.DAL.Entities;
using AutoService.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace AutoService.Controllers
{
    public class DriversController : Controller
    {
        ApplicationDbContext _context;
        public DriversController(ApplicationDbContext context)
        {
            _context = context;
        }
        [Route("Drivers")]
        [Route("Drivers/Page_{pageNo}")]
        public async Task<IActionResult> Index(int pageNo = 1)
        {
            return View(await PaginatedList<Driver>.CreateAsync(_context.Drivers, pageNo, 3));
        }
        public IActionResult CreateDriver() => View();
        [HttpPost]
        public async Task<IActionResult> CreateDriver(CreateDriverModel model)
        {
            if(ModelState.IsValid)
            {
                Driver driver = new Driver
                {
                    DriverFullName = model.DriverFullName,
                    DriverDB = model.DriverDB,                  
                    DriverImage = model.DriverImage,
                    DriverCategory=model.DriverCategory,
                    DriverPhone=model.DriverPhone,
                    DriverAdress=model.DriverAdress
                };
                _context.Drivers.Add(driver);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> EditDriver(int? id)
        {
            if(id!=null)
            {
                
                Driver driver = await _context.Drivers.FirstOrDefaultAsync(d => d.DriverId == id);
                if (driver != null)
                    return View(driver);
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> EditDriver(Driver driver)
        {
            _context.Drivers.Update(driver);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public ActionResult SearchDriver(string fullName)
        {
            IQueryable<Driver> drivers = _context.Drivers;
            if(!String.IsNullOrEmpty(fullName))
            { 
                drivers = drivers.Where(d => d.DriverFullName.Contains(fullName));
            }
            DriverListViewModel viewModel = new DriverListViewModel
            {
                Drivers = drivers.ToList(),
                FullName = fullName
            };
            return View(viewModel);
        }
        public ActionResult DeleteDriver(int? id)
        {
            if(id==null)
            {
                return NotFound();
            }
            var driver = _context.Drivers
                .FirstOrDefault(d => d.DriverId == id);
            if(driver==null)
            {
                return NotFound();
            }
            return View(driver);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            Driver driver = _context.Drivers.Find(id);
            _context.Drivers.Remove(driver);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
