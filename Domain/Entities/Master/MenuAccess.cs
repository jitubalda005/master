using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Master
{
    public class MenuAccess
    {
        public int Id { get; set; }
        public int MenuId { get; set; }
        public string UserId { get; set; }
        public string RoleId { get; set; }
        public bool IsAccess { get; set; }
    }
}