using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PVMS_Project.Models.ENTITIES;

namespace PVMS_Project.Data
{
    public class PassportDBContext:DbContext
    {
        public PassportDBContext(DbContextOptions<PassportDBContext> options) : base(options)
        {
            
        }
        public DbSet<Register> registers { set; get; }
       
        public DbSet<Passport> passports { set; get; }
        public DbSet<Visa> visas { set; get; }
        public DbSet<State> states { set; get; }
        public DbSet<City> cities { set; get; }
        public DbSet<Cost> costs { set; get; }
        public DbSet<VisaApplicationCost> visaApplicationCosts { set; get; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Register>().Property(e => e.UserId).ValueGeneratedNever();
            modelBuilder.Entity<Register>().HasIndex(e => new { e.Contactno }).IsUnique();
            modelBuilder.Entity<Register>().HasIndex(e => new { e.Email }).IsUnique();
            modelBuilder.Entity<Passport>().Property(e => e.Passport_id).ValueGeneratedNever();
            modelBuilder.Entity<Visa>().Property(e => e.VisaId).ValueGeneratedNever();
            modelBuilder.Entity<State>().Property(e => e.State_Id).ValueGeneratedNever();
            modelBuilder.Entity<City>().Property(e => e.Pincode).ValueGeneratedNever();
            modelBuilder.Entity<VisaApplicationCost>().Property(e =>  e.Occupation ).ValueGeneratedNever();
            modelBuilder.Entity<Cost>().Property(e => e.Id).ValueGeneratedNever();

        }
    }
}