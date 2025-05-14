using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TechnoparkProj.Contracts;
using TechnoparkProj.Core.Models;
using TechnoparkProj.DataAccess;

namespace TechnoparkProj.Controllers
{
    [ApiController]
    [Route("institute")]
    public class InstituteController : ControllerBase
    {
        private readonly TechnoparkProjDbContext _context;

        public InstituteController(TechnoparkProjDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Institute>>> GetInstitutes()
        {
            var institutes = await _context.Institutes
                .Select(i => new InstituteDto(i.Id, i.Name))
                .ToListAsync();

            return Ok(new GetInstitutesResponse(institutes));
        }
    }
}
