using TechnoparkProj.Core.Models;

namespace TechnoparkProj.Contracts
{
    public record UpdateTicketRequest(
        string Name,
        TicketStatus Status,
        string Description,
        int? SprintId = null
    );
}
