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
        private GroupRepository _groupRepository;
        private StudentGroupRepository _studentGroupRepository;

        public UnitOfWork(SchoolDbContext context)
        {
            _context = context;
        }

        public IStudentRepository Students => _studentRepository ??= new StudentRepository(_context);

        public IGroupRepository Groups => _groupRepository ??= new GroupRepository(_context);

        public IStudentGroupRepository StudentGroups => _studentGroupRepository ??= new StudentGroupRepository(_context);

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
