using System.ComponentModel;
using TechnoparkProj.Core.Models;

namespace TechnoparkProj.DataAccess
{
    public static class DbItitializer
    {
        public static void Initialize(TechnoparkProjDbContext context)
        {
            //context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            if (context.Projects.Any())
            {
                return;
            }

            var institutes = new Institute[]
            {
                new Institute{Name="ИИТТО"},
                new Institute{Name="ИХО"},
            };
            foreach (Institute i in institutes)
            {
                context.Institutes.Add(i);
            }
            context.SaveChanges();

            var schools = new School[]
            {
                new School{InstituteId=1, Name="ИТВД"},
                new School{InstituteId=1, Name="ИИС"},
                new School{InstituteId=1, Name="ИВТ"},
                new School{InstituteId=2, Name="ИИД"},
                new School{InstituteId=2, Name="ХО-ИЗО"},
                new School{InstituteId=2, Name="ХО-ДИД"}

            };
            foreach (School s in schools)
            {
                context.Schools.Add(s);
            }
            context.SaveChanges();

            var students = new Student[]
            {
                new Student{SchoolId=1, Name="Екатерина", Surname="Житомирская"},
                new Student{SchoolId=1, Name="Олег", Surname="Гриненко"},
                new Student{SchoolId=1, Name="Элина", Surname="Талантова"},
                new Student{SchoolId=2, Name="Ринат", Surname="Куимов"},
                new Student{SchoolId=2, Name="Петр", Surname="Антипенков"},
                new Student{SchoolId=2, Name="Юлия", Surname="Шулятикова"},
                new Student{SchoolId=3, Name="Ефим", Surname="Эрдинев"},
                new Student{SchoolId=4, Name="Глеб", Surname="Паничкин"},
                new Student{SchoolId=5, Name="Алла", Surname="Гандлевская"},
                new Student{SchoolId=6, Name="Константин", Surname="Оборский"}
            };
            foreach (Student s in students)
            {
                context.Students.Add(s);
            }
            context.SaveChanges();

            var projects = new Project[]
            {
                new Project{SchoolId=1, Name="Первый проект", Description="Описание первого проекта", Course=Course.ONE, Semester=Semester.SPRING, Year=2022, Duration=Duration.ONEWEEK, StartDate=DateTime.Parse("01-02-2025")},
                new Project{SchoolId=2, Name="Второй проект", Description="Описание второго проекта", Course=Course.TWO, Semester=Semester.AUTUMN, Year=2023, Duration=Duration.TWOWEEKS, StartDate=DateTime.Parse("05-02-2025")}
            };
            foreach (Project p in projects)
            {
                context.Projects.Add(p);
            }
            context.SaveChanges();

            var links = new StudProjLink[]
            {
                new StudProjLink{StudentId=1, ProjectId=1},
                new StudProjLink{StudentId=2, ProjectId=1},
                new StudProjLink{StudentId=3, ProjectId=1},
                new StudProjLink{StudentId=4, ProjectId=2},
                new StudProjLink{StudentId=5, ProjectId=2},
                new StudProjLink{StudentId=6, ProjectId=2}
            };
            foreach (StudProjLink l in links)
            {
                context.StudProjLinks.Add(l);
            }
            context.SaveChanges();

            var sprints = new Sprint[]
            {
                new Sprint{ProjectId=1, IsBacklog=true, Status=SprintStatus.WORKING},
                new Sprint{ProjectId=1, IsBacklog=false, Status=SprintStatus.WORKING},
                new Sprint{ProjectId=2, IsBacklog=true, Status=SprintStatus.WORKING},
                new Sprint{ProjectId=2, IsBacklog=false, Status=SprintStatus.WORKING},
                new Sprint{ProjectId=2, IsBacklog=false, Status=SprintStatus.WORKING}
            };
            foreach (Sprint s in sprints)
            {
                context.Sprints.Add(s);
            }
            context.SaveChanges();

            var tickets = new Ticket[]
            {
                new Ticket{SprintId=1, Name="Задача 1", Status=TicketStatus.DONE, Description="Первая задача"},
                new Ticket{SprintId=1, Name="Задача 2", Status=TicketStatus.DONE, Description="Вторая задача"},
                new Ticket{SprintId=2, Name="Задача 3", Status=TicketStatus.WORKING, Description="Третья задача"},
                new Ticket{SprintId=3, Name="Задача 4", Status=TicketStatus.DONE, Description="Четвертая задача"},
                new Ticket{SprintId=4, Name="Задача 5", Status=TicketStatus.WORKING, Description="Пятая задача"},
                new Ticket{SprintId=5, Name="Задача 6", Status=TicketStatus.WORKING, Description="Шестая задача"}
            };
            foreach (Ticket t in tickets)
            {
                context.Tickets.Add(t);
            }
            context.SaveChanges();

        }
    }
}
