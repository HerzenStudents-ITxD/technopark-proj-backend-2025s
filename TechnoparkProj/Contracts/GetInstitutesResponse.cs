namespace TechnoparkProj.Contracts
{
    public record GetInstitutesResponse(List<InstituteDto> Institutes);
    public record InstituteDto(
        int InstituteId,
        string InstituteName
    );
}
