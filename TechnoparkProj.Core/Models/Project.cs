// Проект
namespace TechnoparkProj.Core.Models
{
    public class Project
    {
        public Project()
        {
            Id = 0;
            Name = "start name";
            Description = "start name";
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; } = string.Empty;

        // внешний ключ
        public int SchoolId { get; set; }

        public ICollection<Ticket>? Tickets { get; set; }
        public School School { get; set; }
    }
}
