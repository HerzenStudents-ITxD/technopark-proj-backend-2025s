namespace TechnoparkProj.Contracts
{
    public record ProjectResponse(
        Guid ProjectId,
        Guid ProfessorId,
        Guid InstituteId,
        string Description);
}
