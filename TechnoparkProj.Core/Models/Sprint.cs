// Спринт
namespace TechnoparkProj.Core.Models
{
    public enum Duration
    {
        NONE = 0,
        ONEWEEK = 7,
        TWOWEEKS = 14
    }

    public class Sprint
    {
        public Sprint()
        {
            Id = 0;
            Duration = Duration.NONE;
            StartDate = DateTime.Parse("10-10-2010");
        }

        public int Id { get; set; }
        public Duration Duration { get; set; }
        public DateTime StartDate { get; set; }

        public ICollection<Ticket>? Tickets { get; set; }
    }
}
