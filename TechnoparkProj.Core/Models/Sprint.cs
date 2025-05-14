// Спринт
namespace TechnoparkProj.Core.Models
{
    public enum SprintStatus
    {
        PLANNED,
        WORKING,
        DONE,
        OVERDUE
    }

    public class Sprint
    {
        public Sprint()
        {
            Id = 0;
        }

        public int Id { get; set; }
        public bool IsBacklog { get; set; }
        public SprintStatus Status { get; set; }

        // внешние ключи
        public int ProjectId { get; set; }

        public ICollection<Ticket>? Tickets { get; set; }
        public Project Project { get; set; }
    }
}
