//Направление
namespace TechnoparkProj.Core.Models
{
    public class School
    {
        public int Id { get; set; }
        public string Name { get; set; }

        // внешний ключ
        public int InstituteId { get; set; }

        public Institute Institute { get; set; }
        public ICollection<Project> Projects { get; set; }
        public ICollection<Student> Students { get; set; }
    }
}
