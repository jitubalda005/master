using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azure;
using Azure.Core;
using Domain.Dtos;
using Domain.Dtos.RoleBaseMenu;
using Domain.Entities.Master;
using Domain.Interfaces;
using Domain.Interfaces.Master;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Text;

namespace DataAccess.EFCore.Repositories
{
    public class MenuAccessRepository : GenericRepository<MenuAccess>, IMenuAccessRepository
    {
        protected readonly JMSDBContext _context;

        public MenuAccessRepository(JMSDBContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Responce<List<UserMenuAccess>>> GetUserMenuAccess(string userName)
        {
            var data = await (from m in _context.Menu
                              join ma in _context.MenuAccess on m.ID equals ma.MenuId
                              where ma.UserId == userName
                              select new UserMenuAccess
                              {
                                  MenuAccessId = ma.Id,
                                  MenuId = m.ID,
                                  ParntId = m.ParentId,
                                  UserId = ma.UserId,
                                  DisplayName = m.DisplayName,
                                  IconName = m.IconName,
                                  MenuURL = m.MenuURL,
                                  Route = m.Route,
                                  IsAccess = ma.IsAccess
                              }).ToListAsync();

            List<UserMenuAccess> roleBasedMenus = new List<UserMenuAccess>();
            roleBasedMenus = JsonConvert.DeserializeObject<List<UserMenuAccess>>(JsonConvert.SerializeObject(data));
            List<UserMenuAccess> roleBasedMenus1 = new List<UserMenuAccess>();
            roleBasedMenus1 = FillRecursive(roleBasedMenus, 0);
            return Responce<List<UserMenuAccess>>.Success(roleBasedMenus1);
        }

        public async Task<Responce<List<Menu>>> GetUnAccessMenus(string userName)
        {

            var result = await (_context.Menu.Where(a => !_context.MenuAccess
                    .Select(b => b.MenuId).Contains(a.ID))).ToListAsync();
            return Responce<List<Menu>>.Success(result); ;


        }
        public List<UserMenuAccess> FillRecursive(List<UserMenuAccess> flatObjects, int? parentId = null)
        {
            return flatObjects.Where(x => x.ParntId.Equals(parentId)).Select(item => new UserMenuAccess
            {
                MenuAccessId = item.MenuAccessId,
                DisplayName = item.DisplayName,
                MenuId = item.MenuId,
                Route = item.Route,
                IconName = item.IconName,
                MenuHeirarchy = item.MenuHeirarchy,
                MenuURL = item.MenuURL,
                UserId = item.UserId,
                ParntId = item.ParntId,
                Children = FillRecursive(flatObjects, item.MenuId)
            }).ToList();
        }


    }
}
