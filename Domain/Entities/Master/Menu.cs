using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Master
{
    public class Menu
    {
        public int ID { get; set; }
        public int ParentId { get; set; }
        public string MenuURL { get; set; }
        public string DisplayName { get; set; }
        public string IconName { get; set; }
        public string Route { get; set; }
        public int Status { get; set; }
        public int MenuHeirarchy { get; set; }

    }
}
