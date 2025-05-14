using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TechnoparkProj.Contracts;
using TechnoparkProj.Core.Models;
using TechnoparkProj.DataAccess;

namespace TechnoparkProj.Controllers
{
    [ApiController]
    [Route("school")]
    public class SchoolController : ControllerBase
    {
        private readonly TechnoparkProjDbContext _context;

        public SchoolController(TechnoparkProjDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<School>>> GetSchoolsByInstitute(int InstituteId)
        {
            var schools = await _context.Schools
                .Where(s => s.InstituteId == InstituteId)
                .Select(s => new SchoolDto(s.Id, s.Name))
                .ToListAsync();

            return Ok(new GetSchoolsResponse(schools));
        }
    }
}
