using Microsoft.VisualBasic;
using TechnoparkProj.Core.Models;

namespace TechnoparkProj.Contracts
{
    public record CreateProjectRequest(
      string Name,
      string Description,
      int InstituteId,
      int SchoolId,
      Course Course,
      Semester Semester,
      int Year,
      int StudentId,
      Duration SprintDuration,
      DateTime StartDate
    );
}
