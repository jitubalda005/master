using DataAccess.EFCore.Repositories;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.EFCore.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly RepositoryFactory _repositoryFactory;
        private readonly JMSDBContext _context;
        public UnitOfWork(RepositoryFactory repositoryFactory, JMSDBContext context)
        {
            _repositoryFactory = repositoryFactory;
            _context = context;
        }
        public T GetRepository<T>() where T : class
        {
            return _repositoryFactory.GetRepository<T>();
        }
        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
