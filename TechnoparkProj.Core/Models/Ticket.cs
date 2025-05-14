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
            SprintID = 0;
            Name = "start name";
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public TicketStatus Status { get; set; }
        public string? Description { get; set; }

        // внешние ключи
        public int SprintID { get; set; }

        public Sprint Sprint { get; set; }
    }
}
