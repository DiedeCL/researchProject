using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using research_project_backend.Data.Domains;
using Environment = research_project_backend.Data.Domains.Environment;

namespace research_project_backend.Data
{
    public class MijnStagesDbContext : IdentityDbContext<User, Role, int>
    {
        public DbSet<Company> Companies { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<InternshipAssignment> Assignments { get; set; }
        public DbSet<Environment> Environment { get; set; }
        public DbSet<IntroductionCondition> IntroductionCondition { get; set; }

        public MijnStagesDbContext(DbContextOptions<MijnStagesDbContext> options) : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Company>().HasKey(company => company.CompanyId);

            //foreign keys
            modelBuilder.Entity<Company>().HasOne<CompanyPromoter>(company => company.Promoter).WithOne(c => c.Company);

            modelBuilder.Entity<Company>()
                .HasOne<ContactCompany>(c => c.ContactCompany)
                .WithOne(c => c.Company);

            modelBuilder.Entity<Company>().HasData(
                new Company
                {
                    AmountOfITPersonnel = 32,
                    AmountOfPersonnel = 321,
                    NameCompany = "PXL",
                    CompanyId = 1

                }
            );
            modelBuilder.Entity<Company>().HasData(
                new Company
                {
                    AmountOfITPersonnel = 32,
                    AmountOfPersonnel = 321,
                    NameCompany = "Bedrijf 2",
                    CompanyId = 2

                }
            );
            modelBuilder.Entity<Address>().HasData(new Address
            {
                Number = 24,
                PostalNumber = "3500",
                Street = " Elfde-Liniestraat",
                Township = "Hasselt",
                CompanyId = 1,
                AddressId = 1
            });
            modelBuilder.Entity<Address>().HasData(new Address
            {
                Number = 24,
                PostalNumber = "3500",
                Street = " Elfde-Liniestraat",
                Township = "Hasselt",
                CompanyId = 2,
                AddressId = 2
            });
            modelBuilder.Entity<ContactCompany>().HasData(new ContactCompany
            {
                PhoneNumber = "0475575757",
                JobTitle = "Lector",
                Name = "Lowie",
                LastName = "Vangaal",
                CompanyId = 1,
                Id = 1
            });
            modelBuilder.Entity<ContactCompany>().HasData(new ContactCompany
            {
                PhoneNumber = "0475575757",
                JobTitle = "Scrum master",
                Name = "Lowie",
                LastName = "Vangaal",
                CompanyId = 2,
                Id = 2
            });


            base.OnModelCreating(modelBuilder);
        }
    }
}