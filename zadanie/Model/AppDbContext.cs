using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using zadanie.Entities;

namespace zadanie.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options) { }

        public DbSet<File> Files { get; set; }
        public DbSet<Form> Forms { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<File>()
                .HasMany(x => x.Subfiles)
                .WithOne(x => x.Parent);

            modelBuilder.Entity<Form>()
                .HasOne(x => x.File)
                .WithOne(x => x.Form)
                .HasForeignKey<File>(x => x.FormId);
        }
    }
}
