using Microsoft.EntityFrameworkCore;

namespace StudentGroup.Infrastracture.Data.Builders
{
    public interface IQueryBuilder<T> where T : class
    {
        DbSet<T> WithSexCondition(string sex);
        DbSet<T> WithSurnameCondition(string surname);
        DbSet<T> WithNameCondition(string name);
        DbSet<T> WithMiddleName(string middleName);
        DbSet<T> WithNickname(string nickname);
    }
}
