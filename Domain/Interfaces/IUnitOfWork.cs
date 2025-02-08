using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        //IDeveloperRepository Developers { get; }
        //IProjectRepository Projects { get; }
        //IDivisionRepository Divisions { get; }
        //int Complete();
        T GetRepository<T>() where T : class;
        Task<int> CompleteAsync();
    }
    public interface IRepositoryFactory
    {
        T GetRepository<T>() where T : class;
    }
}
