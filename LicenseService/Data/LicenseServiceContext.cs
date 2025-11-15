using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using LicenseService.Models;

    public class LicenseServiceContext : DbContext
    {
        public LicenseServiceContext (DbContextOptions<LicenseServiceContext> options)
            : base(options)
        {
        }

        public DbSet<LicenseService.Models.License> License { get; set; } = default!;
    }
