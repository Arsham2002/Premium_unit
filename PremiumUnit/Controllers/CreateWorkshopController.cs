using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PremiumUnit.Data;
using PremiumUnit.Models;

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
                    ActivityStartDate = DateTime.Now,
                    EmployerName = model.EmployerName,
                    EmployerPhoneNumber = model.EmployerPhoneNumber
                };

                _context.Add(workshop);
                await _context.SaveChangesAsync();
            }
            return View(model);
        } 

        // متد برای تعیین مقدار خاص (Level)
        private TimeSpan DetermineInterval(ActivityType TypeOfActivity)
        {
            return TypeOfActivity switch
            {
                ActivityType.Manufacturing => TimeSpan.FromDays(30),
                ActivityType.Maintenance => TimeSpan.FromDays(60),
                ActivityType.Repair => TimeSpan.FromDays(90)
            };
        }

    }
}
