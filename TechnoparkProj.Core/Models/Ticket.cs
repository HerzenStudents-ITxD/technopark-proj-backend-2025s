// Задача
namespace TechnoparkProj.Core.Models
{
    public enum TicketStatus
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
            SprintId = 0;
            Name = "start name";
        }

        public Ticket(int id, string name, TicketStatus status, string description, int sprintId)
        {
            Id = id;
            Name = name;
            Status = status;
            Description = description;
            SprintId = sprintId;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public TicketStatus Status { get; set; }
        public string? Description { get; set; }

        // внешние ключи
        public int SprintId { get; set; }

        public Sprint Sprint { get; set; }
    }
}
