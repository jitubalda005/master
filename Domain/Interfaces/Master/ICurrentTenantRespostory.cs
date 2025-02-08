using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Dtos.RoleBaseMenu;
using Domain.Dtos;
using Domain.Entities.Master;

namespace Domain.Interfaces.Master
{
    
    public interface ICurrentTenantRespostory 
    {
        string TenantId { get; set; }
        public Task<bool> SetTenant(string tenant);

    }
}
