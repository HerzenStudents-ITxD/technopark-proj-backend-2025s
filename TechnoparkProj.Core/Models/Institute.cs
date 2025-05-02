// Институт
namespace TechnoparkProj.Core.Models
{
    public class Institute
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<School> Schools { get; set; }
    }
}
