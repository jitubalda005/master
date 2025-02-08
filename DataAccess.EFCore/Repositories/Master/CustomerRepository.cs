using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities.Customers;
using Domain.Interfaces.Master;

namespace DataAccess.EFCore.Repositories.Master
{
    public class CustomerRepository : GenericRepository<Customer>, ICustomerRepository
    {
        public CustomerRepository(JMSDBContext context) : base(context)
        {
        }
    }
}
