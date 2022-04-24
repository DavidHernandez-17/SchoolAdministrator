﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SchoolAdministrator.Data.Entities;

namespace SchoolAdministrator.Data
{
    public class DataContext : IdentityDbContext<User>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) //Conexión base de datos
        {
        }

        public DbSet<Inscription> inscriptions { get; set; }
        public DbSet<Institution> Institutions { get; set; }
        public DbSet<Level> Levels { get; set; }
        public DbSet<Qualification> qualifications { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Institution>().HasIndex(i => i.Name).IsUnique();  //Crea indice, para notificar que el dato es único
            modelBuilder.Entity<Subject>().HasIndex(i => i.Name).IsUnique();
            modelBuilder.Entity<Level>().HasIndex("Name", "InstitutionId").IsUnique();

        }

    }
}
