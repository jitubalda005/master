using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dtos.RoleBaseMenu
{
    public class RoleBasedMenus
    {
        public int MenuAccessId { get; set; }
        public int MenuId { get; set; }
        public int ParntId { get; set; }
        public string UserId { get; set; }
        public string DisplayName { get; set; }
        public string IconName { get; set; }
        public string MenuURL { get; set; }
        public string Route { get; set; }
        public int Status { get; set; }
        public int MenuHeirarchy { get; set; }
        public bool IsAccess { get; set; }
        
    }

    public class UserMenuAccess : RoleBasedMenus
    {
        public List<UserMenuAccess> Children { get; set; }
    }

}
