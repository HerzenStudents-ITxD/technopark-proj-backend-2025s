// Проект
using System.Security.Cryptography.X509Certificates;

namespace TechnoparkProj.Core.Models
{
    public enum Course
    {
        ONE = 1,
        TWO,
        THREE,
        FOUR
    }

    public enum Duration
    {
        NONE = 0,
        ONEWEEK = 7,
        TWOWEEKS = 14
    }

    public enum Semester
    {
        SPRING,
        AUTUMN
    }

    public class Project
    {
        public Project()
        {
            Id = 0;
            Name = "start name";
            Description = "start name";
        }
        // новый конструктор
        // 
        public Project(int id, string name, string description, Course course, Semester semester, int year, Duration duration, DateTime startDate, int schoolId)
        {
            Id =id;
            Name = name;
            Description = description;
            Course = course;
            Semester = semester;
            Year = year;
            Duration = duration;
            StartDate = startDate;
            SchoolId = schoolId;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; } = string.Empty;
        public Course Course { get; set; }
        public Semester Semester { get; set; }
        public int Year { get; set; }
        public Duration Duration { get; set; }
        public DateTime StartDate { get; set; }

        // внешний ключ
        public int SchoolId { get; set; }

        public School School { get; set; }

        public ICollection<Sprint>? Sprints { get; set; }
        public ICollection<StudProjLink> StudProjLinks { get; set; }
    }
}
