using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
    }
}
