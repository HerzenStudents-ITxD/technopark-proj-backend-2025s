using System.ComponentModel;
using TechnoparkProj.Core.Models;

namespace TechnoparkProj.DataAccess
{
    public static class DbItitializer
    {
        public static void Initialize(TechnoparkProjDbContext context)
        {

            context.Database.EnsureCreated();

            if (context.Projects.Any())
            {
                return;
            }

            var institutes = new Institute[]
            {
                new Institute{Name="Первый институт"},
                new Institute{Name="Второй институт"}
            };
            foreach (Institute i in institutes)
            {
                context.Institutes.Add(i);
            }
            context.SaveChanges();

            var schools = new School[]
            {
                new School{InstituteId=1, Name="Направление первого института"},
                new School{InstituteId=2, Name="Направление второго института"}
            };
            foreach (School s in schools)
            {
                context.Schools.Add(s);
            }
            context.SaveChanges();

            var projects = new Project[]
            {
                new Project{SchoolId=1, Name="Первый проект", Description="Описание первого проекта"},
                new Project{SchoolId=2, Name="Второй проект", Description="Описание второго проекта"}
            };
            foreach (Project p in projects)
            {
                context.Projects.Add(p);
            }
            context.SaveChanges();

            var sprints = new Sprint[]
            {
                new Sprint{Duration=Duration.ONEWEEK, StartDate=DateTime.Parse("01-02-2025")},
                new Sprint{Duration=Duration.ONEWEEK, StartDate=DateTime.Parse("01-02-2025")},
                new Sprint{Duration=Duration.TWOWEEKS, StartDate=DateTime.Parse("05-02-2025")},
                new Sprint{Duration=Duration.TWOWEEKS, StartDate=DateTime.Parse("05-02-2025")}
            };
            foreach (Sprint s in sprints)
            {
                context.Sprints.Add(s);
            }
            context.SaveChanges();

            var tickets = new Ticket[]
            {
                new Ticket{ProjectID=1, SprintID=1, Name="Задача 1", Status=Status.WORKING, Description="Первая задача"},
                new Ticket{ProjectID=1, SprintID=1, Name="Задача 2", Status=Status.WORKING, Description="Вторая задача"},
                new Ticket{ProjectID=1, SprintID=2,Name="Задача 3", Status=Status.WORKING, Description="Третья задача"},
                new Ticket{ProjectID=2, SprintID=3, Name="Задача 4", Status=Status.WORKING, Description="Четвертая задача"},
                new Ticket{ProjectID=2, SprintID=4, Name="Задача 5", Status=Status.WORKING, Description="Пятая задача"},
                new Ticket{ProjectID=2, SprintID=4,Name="Задача 6", Status=Status.WORKING, Description="Шестая задача"}
            };
            foreach (Ticket t in tickets)
            {
                context.Tickets.Add(t);
            }
            context.SaveChanges();

        }
    }
}
