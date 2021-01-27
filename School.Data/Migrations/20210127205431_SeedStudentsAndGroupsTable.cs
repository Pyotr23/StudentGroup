using Microsoft.EntityFrameworkCore.Migrations;

namespace School.Data.Migrations
{
    public partial class SeedStudentsAndGroupsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder
                .Sql("INSERT INTO public.\"Students\" (\"Sex\", \"LastName\", \"Name\", \"MiddleName\", \"Nickname\") VALUES ('Мужской', 'Олегов', 'Пётр', 'Олегович', 'Чилаверт')");
            migrationBuilder
                .Sql("INSERT INTO public.\"Students\" (\"Sex\", \"LastName\", \"Name\", \"MiddleName\", \"Nickname\") VALUES ('Женский', 'Олегова', 'Татьяна', 'Александровна', 'Матаня')");
            migrationBuilder
                .Sql("INSERT INTO public.\"Students\" (\"Sex\", \"LastName\", \"Name\", \"MiddleName\", \"Nickname\") VALUES ('Мужской', 'Олегов', 'Михаил', 'Петрович', 'Мишаня')");
            migrationBuilder
                .Sql("INSERT INTO public.\"Students\" (\"Sex\", \"LastName\", \"Name\", \"MiddleName\", \"Nickname\") VALUES ('Мужской', 'Олегов', 'Александр', 'Петрович', 'Сашуля')");

            migrationBuilder
                .Sql("INSERT INTO public.\"Groups\" (\"Name\") VALUES ('Малыши')");
            migrationBuilder
                .Sql("INSERT INTO public.\"Groups\" (\"Name\") VALUES ('Взрослые')");

            migrationBuilder
                .Sql("INSERT INTO public.\"GroupStudent\" (\"StudentsId\", \"GroupsId\") VALUES ((SELECT \"Id\" FROM public.\"Students\" WHERE \"Name\" = 'Александр'), (SELECT \"Id\" FROM public.\"Groups\" WHERE \"Name\" = 'Малыши'))");
            migrationBuilder
                .Sql("INSERT INTO public.\"GroupStudent\" (\"StudentsId\", \"GroupsId\") VALUES ((SELECT \"Id\" FROM public.\"Students\" WHERE \"Name\" = 'Пётр'), (SELECT \"Id\" FROM public.\"Groups\" WHERE \"Name\" = 'Взрослые'))");
            migrationBuilder
                .Sql("INSERT INTO public.\"GroupStudent\" (\"StudentsId\", \"GroupsId\") VALUES ((SELECT \"Id\" FROM public.\"Students\" WHERE \"Name\" = 'Татьяна'), (SELECT \"Id\" FROM public.\"Groups\" WHERE \"Name\" = 'Взрослые'))");
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
