using AutoMapper;
using School.Core;
using School.Core.Models;
using School.Core.Repositories;
using School.Core.Services;
using System.Threading.Tasks;

namespace School.Services
{
    public class StudentGroupsService : IStudentGroupsService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IStudentGroupRepository _studentGroups;
        private readonly IStudentRepository _students;
        private readonly IGroupRepository _groups;
        private readonly IMapper _mapper;

        public StudentGroupsService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _studentGroups = unitOfWork.StudentGroups;
            _students = unitOfWork.Students;
            _groups = unitOfWork.Groups;
            _mapper = mapper;
        }

        public async Task<StudentGroup> AddStudentToGroup(StudentGroup studentGroup)
        {
            await _studentGroups.AddAsync(studentGroup);
            await _unitOfWork.CommitAsync();
            return studentGroup;
        }

        public async Task<StudentGroup> GetStudentGroup(int studentId, int groupId)
        {
            return await _studentGroups.GetByIdes(studentId, groupId);
        }
    }
}
