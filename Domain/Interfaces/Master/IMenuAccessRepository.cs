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

    public interface IMenuAccessRepository : IGenericRepository<MenuAccess>
    {
        Task<Responce<List<UserMenuAccess>>> GetUserMenuAccess(string userName);
        Task<Responce<List<Menu>>> GetUnAccessMenus(string userName);
    }

}
