using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PremiumUnit.Models;

namespace PremiumUnit.Data
{
    public class PremiumUnitContext : DbContext
    {
        public PremiumUnitContext (DbContextOptions<PremiumUnitContext> options)
            : base(options)
        {
        }

        public DbSet<PremiumUnit.Models.Workshop> Workshop { get; set; } = default!;
        public DbSet<PremiumUnit.Models.Invoice> Invoice { get; set; } = default!;
    }
}
