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

        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<Ticket>>> GetTickets()
        //{
        //    return await _context.Tickets
        //        .ToListAsync();
        //}

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

        [HttpPost]
        public async Task<IActionResult> CreateTicket([FromBody] CreateTicketRequest request)
        {
            var sprint = await _context.Sprints
                .Include(s => s.Project)
                .FirstOrDefaultAsync(s => s.Id == request.SprintId);

            if (sprint == null)
            {
                return NotFound("Sprint not found");
            }

            var ticket = new Ticket(0,
                                    request.Name,
                                    request.Status,
                                    request.Description,
                                    request.SprintId);

            await _context.Tickets.AddAsync(ticket);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                success = true,
                ticketId = ticket.Id,
                ticketName = ticket.Name,
                ticketStatus = ticket.Status,
                ticketDescription = ticket.Description,
                sprintId = ticket.SprintId,
                projectId = sprint.ProjectId
            });
        }

        [HttpPut("update-ticket/{id}")]
        public async Task<IActionResult> UpdateTicket(int id, [FromBody] UpdateTicketRequest request)
        {
            try
            {
                var ticket = await _context.Tickets.FindAsync(id);
                if (ticket == null)
                    return NotFound($"Ticket with id {id} not found");

                ticket.Name = request.Name;
                ticket.Status = request.Status;
                ticket.Description = request.Description;

                if (request.SprintId.HasValue)
                {
                    var sprint = await _context.Sprints.FindAsync(request.SprintId.Value);
                    if (sprint != null)
                        ticket.SprintId = request.SprintId.Value;
                }

                _context.Tickets.Update(ticket);
                await _context.SaveChangesAsync();

                return Ok(new
                {
                    success = true,
                    ticket = new
                    {
                        ticket.Id,
                        ticket.Name,
                        ticket.Status,
                        ticket.Description,
                        ticket.SprintId
                    }
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


    }
}
