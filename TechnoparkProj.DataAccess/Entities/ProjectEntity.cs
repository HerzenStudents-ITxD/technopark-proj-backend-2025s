namespace TechnoparkProj.DataAccess.Entities
{
    public class ProjectEntity
    {
        public Guid Id { get; set; }
        public Guid ProfessorId { get; set; }
        public Guid InstituteId { get; set; }
        public string Description { get; set; } = string.Empty;
    }
}
