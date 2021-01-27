using School.Core.Repositories;
using System;
using System.Threading.Tasks;

namespace School.Core
{
    public interface IUnitOfWork : IDisposable
    {
        IStudentRepository Students { get; }
        IGroupRepository Groups { get; }
        Task<int> CommitAsync();
    }
}
