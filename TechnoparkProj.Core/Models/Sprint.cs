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

        public Sprint (int id, bool isBacklog, SprintStatus status, DateTime deadline, int projectId)
        {
            Id = id;
            IsBacklog = isBacklog;
            Status = status;
            Deadline = deadline;
            ProjectId = projectId;
        }

        public int Id { get; set; }
        public bool IsBacklog { get; set; }
        public SprintStatus Status { get; set; }
        public DateTime Deadline { get; set; }

        // внешние ключи
        public int ProjectId { get; set; }

        public ICollection<Ticket>? Tickets { get; set; }
        public Project Project { get; set; }
    }
}
