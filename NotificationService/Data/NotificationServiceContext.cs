using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NotificationService.Models;

    public class NotificationServiceContext : DbContext
    {
        public NotificationServiceContext (DbContextOptions<NotificationServiceContext> options)
            : base(options)
        {
        }

        public DbSet<NotificationService.Models.Notifications> Notifications { get; set; } = default!;
    }
