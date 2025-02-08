using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities.Master;

namespace Domain.Interfaces
{
    public interface IUserManagmentRepository : IGenericRepository<UserManagment>
    {
      UserManagment LoginCredential(string type, string Credential);
    }
}
