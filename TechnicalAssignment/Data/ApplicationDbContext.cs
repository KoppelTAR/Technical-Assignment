using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace TechnicalAssignment.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<TechnicalAssignment.Models.SavedUsers> SavedUsers { get; set; }
        public DbSet<TechnicalAssignment.Models.Sectors> Sectors { get; set; }
    }
}
