namespace TechnoparkProj.Contracts
{
    public record GetStudentsResponse(List<StudentDto> Students);
    public record StudentDto(
        int StudentId,
        string FullName
    );
}
