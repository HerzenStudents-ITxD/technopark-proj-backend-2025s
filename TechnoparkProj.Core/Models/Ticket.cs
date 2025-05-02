// Задача
namespace TechnoparkProj.Core.Models
{
    public enum Status
    {
        PLANNED,
        WORKING,
        DONE
    }

    public class Ticket
    {
        public Ticket()
        {
            Id = 0;
            ProjectID = 0;
            SprintID = 0;
            Name = "start name";
            Status = Status.PLANNED;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public Status Status { get; set; }
        public string? Description { get; set; }

        // внешние ключи
        public int ProjectID { get; set; }
        public int SprintID { get; set; }

        public Project Project { get; set; }
        public Sprint? Sprint { get; set; }
    }
}
