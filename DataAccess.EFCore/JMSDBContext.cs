using DataAccess.EFCore.Repositories.Master;
using Domain.Entities;
using Domain.Entities.Customers;
using Domain.Entities.Master;
using Domain.Interfaces;
using Domain.Interfaces.Master;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Math;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccess.EFCore
{
    public class JMSDBContext :   DbContext

    {
        private readonly ICurrentTenantRespostory _currentTenantService;
        public string CurrentTenantId { get; set; }

        // Constructor 
        public JMSDBContext(ICurrentTenantRespostory currentTenantService, DbContextOptions<JMSDBContext> options) : base(options)
        {
            _currentTenantService = currentTenantService;
            CurrentTenantId = _currentTenantService.TenantId;
        }

        // DbSets -- create for all entity types to be managed with EF

        public DbSet<Tenant> Tenant { get; set; }

        public DbSet<Menu> Menu { get; set; }

        public DbSet<MenuAccess> MenuAccess { get; set; }

        public DbSet<Customer> Customer { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Customer>().HasQueryFilter(a => a.TenantId == CurrentTenantId);
        }
        // On Save Changes - write tenant Id to table
        public override int SaveChanges()
        {
            foreach (var entry in ChangeTracker.Entries<IMustHaveTanent>().ToList())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                    case EntityState.Modified:
                        entry.Entity.TenantId = CurrentTenantId;
                        break;
                }
            }
            var result = base.SaveChanges();
            return result;
        }

    }


}
