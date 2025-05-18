using TechnoparkProj.Core.Models;

namespace TechnoparkProj.Contracts
{
    public record CreateTicketRequest(
        string Name,
        TicketStatus Status,
        string Description,
        int SprintId
    );
}
