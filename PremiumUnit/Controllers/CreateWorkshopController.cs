using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PremiumUnit.Data;
using PremiumUnit.Models;
using System.ComponentModel.DataAnnotations;

namespace PremiumUnit.Controllers
{
    public class CreateWorkshopController : Controller
    {
        private readonly PremiumUnitContext _context;

        public CreateWorkshopController(PremiumUnitContext context)
        {
            _context = context;
        }
       public async Task<IActionResult> Index()
        {
            return View(await _context.Workshop.ToListAsync());
        }

        // GET: Workshops/Details/5
        public async Task<IActionResult> Details(int? id)   
        {
            if (id == null)
            {
                return NotFound();
            }

            var workshop = await _context.Workshop
                .FirstOrDefaultAsync(m => m.WorkshopCode == id);
            if (workshop == null)
            {
                return NotFound();
            }

            return View(workshop);
        }
        // GET: نمایش فرم ایجاد کارگاه
        public IActionResult Create()
        {
            ViewBag.ActivityTypes = Enum.GetValues(typeof(ActivityType)).Cast<ActivityType>().ToList();
            return View();
        }

        // POST: دریافت اطلاعات و ذخیره در دیتابیس
        [HttpPost] 
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Workshop model)
        {
            if (ModelState.IsValid)
            {
                var workshop = new Workshop
                {
                    ListSubmissionInterval = DetermineInterval(model.TypeOfActivity), // مقدار خاص را مشخص می‌کنیم
                    TypeOfActivity = model.TypeOfActivity,
                    ActivityStartDate = model.ActivityStartDate,
                    EmployerName = model.EmployerName,
                    EmployerPhoneNumber = model.EmployerPhoneNumber
                };
                _context.Add(workshop);
                await _context.SaveChangesAsync();
                
                var invoice = new Invoice
                {
                    Amount = 3000,
                    IssueDate = DateTime.Now,
                    PenaltyBaseDate = DateTime.Now,
                    WorkshopCode = workshop.WorkshopCode
                };
                _context.Add(invoice);
                await _context.SaveChangesAsync();
                TempData["Code"] = workshop.WorkshopCode;
                return RedirectToAction("Index", "Home");
            }
            return View(model);
        } 
        // GET: Workshops/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workshop = await _context.Workshop.FindAsync(id);
            if (workshop == null)
            {
                return NotFound();
            }
            return View(workshop);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("WorkshopCode,ListSubmissionInterval,TypeOfActivity,ActivityStartDate,EmployerName,EmployerPhoneNumber,Premium")] Workshop workshop)
        {
            if (id != workshop.WorkshopCode)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(workshop);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WorkshopExists(workshop.WorkshopCode))
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
            return View(workshop);
        }

        // GET: Workshops/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workshop = await _context.Workshop
                .FirstOrDefaultAsync(m => m.WorkshopCode == id);
            if (workshop == null)
            {
                return NotFound();
            }

            return View(workshop);
        }
        // POST: Workshops/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var workshop = await _context.Workshop.FindAsync(id);
            if (workshop != null)
            {
                _context.Workshop.Remove(workshop);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WorkshopExists(int id)
        {
            return _context.Workshop.Any(e => e.WorkshopCode == id);
        }

        // متد برای تعیین مقدار خاص (Level)
        private TimeSpan DetermineInterval(ActivityType TypeOfActivity)
        {
            return TypeOfActivity switch
            {
                ActivityType.Manufacturing => TimeSpan.FromDays(30),
                ActivityType.Maintenance => TimeSpan.FromDays(60),
                ActivityType.Repair => TimeSpan.FromDays(90),
                _ => TimeSpan.FromDays(0)
            };
        }

    }
}
