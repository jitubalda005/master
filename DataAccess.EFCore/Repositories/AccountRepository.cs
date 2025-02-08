using Domain.Entities;
using Domain.Entities.Login;
using Domain.Entities.Master;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.EFCore.Repositories
{
    public class AccountRepository : GenericRepository<Login>, IAccountRepository
    {
        public AccountRepository(JMSDBContext context) : base(context)
        {

        }
    }
}
