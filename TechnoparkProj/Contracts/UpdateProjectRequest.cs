using TechnoparkProj.Core.Models;

namespace TechnoparkProj.Contracts
{
    public record UpdateProjectRequest(
        string Name,
        string Description,
        Course Course,
        int Year,
        Semester Semester, // 1 - весна, 2 - осень
        Duration SprintDuration,
        DateTime StartDate,
        int SchoolId,
        List<int> StudentIds
    );
}
