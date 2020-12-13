using School.Core.Repositories;
using System;
using System.Threading.Tasks;

namespace School.Core
{
    public interface IUnitOfWork : IDisposable
    {
        IStudentRepository Students { get; set; }
        IGroupRepository Groups { get; set; }
        IStudentGroupRepository StudentGroups { get ; set; }
        Task<int> CommitAsync();
    }
}
