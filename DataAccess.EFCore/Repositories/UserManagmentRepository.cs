using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using Domain.Entities.Master;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.EFCore.Repositories
{
   public class UserManagmentRepository : GenericRepository<UserManagment>, IUserManagmentRepository
    {
        public UserManagmentRepository(JMSDBContext context) : base(context)
        {


        }

        public UserManagment LoginCredential(string type, string Credential)
        {
            UserManagment lst = new UserManagment();
            try
            {
                if (!string.IsNullOrEmpty(type) && type.ToLower() == "encrypt")
                {
                    lst.Credential = CommonFunction.Encrypt256(Credential);
                }
                else if (!string.IsNullOrEmpty(type) && type.ToLower() == "decrypt")
                {
                    lst.Credential = CommonFunction.Decrypt256(Credential);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lst;
        }
    }
}
