namespace TechnoparkProj.Contracts
{
    public record GetSchoolsResponse(List<SchoolDto> Schools);
    public record SchoolDto(
        int SchoolId,
        string SchoolName
    );
}
