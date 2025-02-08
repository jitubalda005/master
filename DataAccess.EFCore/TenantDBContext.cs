using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities.Master;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.EFCore
{
    public class TenantDBContext : IdentityDbContext<IdentityUser>
    {
        public TenantDBContext(DbContextOptions options) : base(options) { }
        public DbSet<Tenant> Tenant { get; set; }
    }
}
