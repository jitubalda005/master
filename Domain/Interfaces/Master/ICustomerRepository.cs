using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities.Customers;

namespace Domain.Interfaces.Master
{
    public interface ICustomerRepository : IGenericRepository<Customer>
    {
    }
   
}
