using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DocumentService.Models;

    public class DocumentServiceContext : DbContext
    {
        public DocumentServiceContext (DbContextOptions<DocumentServiceContext> options)
            : base(options)
        {
        }

        public DbSet<DocumentService.Models.Documents> Documents { get; set; } = default!;
    }
