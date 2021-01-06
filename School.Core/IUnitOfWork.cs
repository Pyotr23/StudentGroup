using School.Core.Repositories;
using System;
using System.Threading.Tasks;

namespace School.Core
{
    public interface IUnitOfWork : IDisposable
    {
        IStudentRepository Students { get; }
        IGroupRepository Groups { get; }
        IStudentGroupRepository StudentGroups { get ; }
        Task<int> CommitAsync();
    }
}
