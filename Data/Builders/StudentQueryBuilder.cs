using Microsoft.EntityFrameworkCore;
using StudentGroup.Infrastracture.Data.Models;
using System.Linq;

namespace StudentGroup.Infrastracture.Data.Builders
{
    public class StudentQueryBuilder
    {
        public IQueryable<Student> Query { get ; private set; }

        public StudentQueryBuilder(DbSet<Student> dbSet)
        {
            Query = dbSet.AsQueryable();
        }

        public void WithSexCondition(string sex)
        {
            Query = Query.Where(s => s.Sex == sex);
        }

        public void WithSurnameCondition(string surname)
        {
            Query = Query.Where(s => s.Surname == surname);
        }

        public void WithNameCondition(string name)
        {
            Query = Query.Where(s => s.Name == name);
        }

        public void WithMiddleNameCondition(string middleName)
        {
            Query = Query.Where(s => s.MiddleName == middleName);
        }

        public void WithNicknameCondition(string nickname)
        {
            Query = Query.Where(s => s.Nickname == nickname);
        }        
    }
}
