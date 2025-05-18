using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TechnoparkProj.Contracts;
using TechnoparkProj.Core.Models;
using TechnoparkProj.DataAccess;

namespace TechnoparkProj.Controllers
{
    [ApiController]
    [Route("student")]
    public class StudentController : ControllerBase
    {
        private readonly TechnoparkProjDbContext _context;

        public StudentController(TechnoparkProjDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Student>>> GetStudents()
        {
            var students = await _context.Students
                .Select(i => new StudentDto(i.Id, i.Name+" "+i.Surname))
                .ToListAsync();

            return Ok(new GetStudentsResponse(students));
        }

        [HttpGet("students-by-id")]
        public async Task<ActionResult<IEnumerable<Student>>> GetStudentsBySchool([FromQuery] int id)
        {
            var students = await _context.Students
                .Where(i => i.School.Id == id)
                .Select(i => new StudentDto(i.Id, i.Name + " " + i.Surname))
                .ToListAsync();

            return Ok(new GetStudentsResponse(students));
        }
    }
}