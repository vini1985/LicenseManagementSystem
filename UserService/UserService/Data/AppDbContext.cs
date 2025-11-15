using blog.api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class AppDbContext : IdentityDbContext<IdentityUser>
{
    // Constructor to initialize the DbContext with options and inherit from IdentityDbContext to include identity tables
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }
    public DbSet<Users> Users { get; set; } = default!;
}
