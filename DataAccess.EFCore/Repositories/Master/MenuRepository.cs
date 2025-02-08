using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities.Master;
using Domain.Interfaces.Master;

namespace DataAccess.EFCore.Repositories.Master
{
    public class MenuRepository : GenericRepository<Menu>, IMenuRepository
    {
        public MenuRepository(JMSDBContext context) : base(context)
        {
        }
    }

}
