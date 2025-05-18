using TechnoparkProj.Core.Models;

namespace TechnoparkProj.Contracts
{
    public record GetProjectByIdResponse(List<ProjectDto> Projects);
    public record ProjectDto(
        int ProjectId,
        string ProjectName,
        string Description,
        string InstituteName,
        string SchoolName,
        Course Course,
        Semester Semester,
        ICollection<StudentData> StudentsData,
        int Year,
        ICollection<Sprint> Sprints
    );
}