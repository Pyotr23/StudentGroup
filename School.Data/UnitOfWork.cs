using School.Core;
using School.Core.Repositories;
using School.Data.Repositories;
using System.Threading.Tasks;

namespace School.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly SchoolDbContext _context;
        private StudentRepository _studentRepository;

        public UnitOfWork(SchoolDbContext context)
        {
            _context = context;
        }

        public IStudentRepository Students => _studentRepository ??= new StudentRepository(_context);

        public IGroupRepository Groups => throw new System.NotImplementedException();

        public IStudentGroupRepository StudentGroups => throw new System.NotImplementedException();

        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
