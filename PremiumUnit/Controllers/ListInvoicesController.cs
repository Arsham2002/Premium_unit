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
    public class ListInvoicesController: Controller
    {
        private readonly PremiumUnitContext _context;

        public async Task<IActionResult> Index(int? code)
        {
            if (code == null)
            {
                return NotFound();
            }

            var invoices = await _context.Invoice
                .Where(i => i.WorkshopCode == code)
                .ToListAsync();
            if (invoices == null)
            {
                return NotFound();
            }

            return View(invoices);
        }
    }
}
