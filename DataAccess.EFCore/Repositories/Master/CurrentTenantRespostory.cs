using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Dtos.RoleBaseMenu;
using Domain.Dtos;
using Domain.Entities.Master;
using Domain.Interfaces.Master;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.EFCore.Repositories.Master
{
    public class CurrentTenantRespostory : ICurrentTenantRespostory
    {
        private readonly TenantDBContext _context;
        public string? TenantId { get; set; }

        public CurrentTenantRespostory(TenantDBContext context)
        {
            _context = context;

        }
        public async Task<bool> SetTenant(string tenant)
        {

            var tenantInfo = await _context.Tenant.Where(x => x.Id == tenant).FirstOrDefaultAsync(); // check if tenant exists
            if (tenantInfo != null)
            {
                TenantId = tenant;
                return true;
            }
            else
            {
                throw new Exception("Tenant invalid");
            }

        }
    }
}
