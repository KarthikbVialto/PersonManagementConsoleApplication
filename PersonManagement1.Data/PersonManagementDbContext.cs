﻿using Microsoft.EntityFrameworkCore;
using PersonManagement1.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonManagement1.Data
{
    public class PersonManagementDbContext:DbContext
    {
        public PersonManagementDbContext(DbContextOptions<PersonManagementDbContext> dbContextOptions):base(dbContextOptions)
        {
            
        }
        public DbSet<Person> Persons { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<DOB> DateOfBirty { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                string ConnectionString = "server=localhost;Database=PersonManagementDB;Trusted_Connection=true;TrustServerCertificate=true";
                
                optionsBuilder.UseSqlServer(ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Address>()
                    .HasOne(p => p.Person)
                    .WithOne(a => a.Address)
                    .HasForeignKey<Address>(p => p.PersonID)
                    .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<DOB>()
                    .HasOne(p => p.Person)
                    .WithOne(d => d.DateOfBirth)
                    .HasForeignKey<DOB>(p => p.PersonId)
                    .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
