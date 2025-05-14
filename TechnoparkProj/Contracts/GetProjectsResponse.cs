using TechnoparkProj.Core.Models;

namespace TechnoparkProj.Contracts
{
    public record GetProjectsResponse(List<ProjectsDto> Projects);
    public record ProjectsDto(
        int ProjectId,
        string ProjectName,
        string Description,
        string InstituteName,
        string SchoolName,
        Course Course,
        Semester Semester,
        ICollection<StudentData> StudentsData,
        int Year,
        int ProjectProgress
    );
}

