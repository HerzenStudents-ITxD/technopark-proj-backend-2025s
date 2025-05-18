using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TechnoparkProj.Contracts;
using TechnoparkProj.Core.Models;
using TechnoparkProj.DataAccess;

namespace TechnoparkProj.Controllers
{
    [ApiController]
    [Route("controller/ticket")]
    public class TicketController : ControllerBase
    {
        private readonly TechnoparkProjDbContext _context;

        public TicketController(TechnoparkProjDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Ticket>>> GetTickets()
        {
            return await _context.Tickets
                .ToListAsync();
        }

        [HttpPut("update-sprint")]
        public async Task<ActionResult<IEnumerable<Ticket>>> UpdateSprintId([FromQuery] int ticketId, [FromBody] int? sprintId)
        {
            var ticket = await _context.Tickets
                .Include(t => t.Sprint)
                .FirstOrDefaultAsync(t => t.Id == ticketId);

            //var sprint = await _context.Sprints.FindAsync(sprintId);
            var sprint = await _context.Sprints.FindAsync(sprintId);
            if (sprint == null)
            {
                return BadRequest($"Sprint with ID {sprintId} not found");
            }
            ticket.Sprint = sprint;

            await _context.SaveChangesAsync();

            return Ok(ticket);
        }
    }
}
