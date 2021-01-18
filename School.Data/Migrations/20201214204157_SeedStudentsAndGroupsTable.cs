using Microsoft.EntityFrameworkCore.Migrations;

namespace School.Data.Migrations
{
    public partial class SeedStudentsAndGroupsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder
                .Sql("INSERT INTO Students (Sex, LastName, Name, MiddleName, Nickname) VALUES ('Мужской', 'Олегов', 'Пётр', 'Олегович', 'Чилаверт')");                
            migrationBuilder
                .Sql("INSERT INTO Students (Sex, LastName, Name, MiddleName, Nickname) VALUES ('Женский', 'Олегова', 'Татьяна', 'Александровна', 'Матаня')");
            migrationBuilder
                .Sql("INSERT INTO Students (Sex, LastName, Name, MiddleName, Nickname) VALUES ('Мужской', 'Олегов', 'Михаил', 'Петрович', 'Мишаня')");
            migrationBuilder
                .Sql("INSERT INTO Students (Sex, LastName, Name, MiddleName, Nickname) VALUES ('Мужской', 'Олегов', 'Александр', 'Петрович', 'Сашуля')");

            migrationBuilder
                .Sql("INSERT INTO Groups (Name) VALUES ('Малыши')");
            migrationBuilder
                .Sql("INSERT INTO Groups (Name) VALUES ('Взрослые')");

            migrationBuilder
                .Sql("INSERT INTO StudentGroups (StudentId, GroupId) VALUES ((SELECT Id FROM Students WHERE Name = 'Александр'), (SELECT Id FROM Groups WHERE Name = 'Малыши'))");
            migrationBuilder
                .Sql("INSERT INTO StudentGroups (StudentId, GroupId) VALUES ((SELECT Id FROM Students WHERE Name = 'Пётр'), (SELECT Id FROM Groups WHERE Name = 'Взрослые'))");
            migrationBuilder
                .Sql("INSERT INTO StudentGroups (StudentId, GroupId) VALUES ((SELECT Id FROM Students WHERE Name = 'Татьяна'), (SELECT Id FROM Groups WHERE Name = 'Взрослые'))");            
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder
                .Sql("DELETE FROM Students");

            migrationBuilder
                .Sql("DELETE FROM Groups");

            migrationBuilder
                .Sql("DELETE FROM StudentGroups");
        }
    }
}
