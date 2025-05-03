using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TechnoparkProj.Core.Models;
using TechnoparkProj.DataAccess;

namespace TechnoparkProj.Controllers
{
    [ApiController]
    [Route("controller")]
    public class ProjectController : ControllerBase
    {
        private readonly TechnoparkProjDbContext _context;

        public ProjectController(TechnoparkProjDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Project>>> GetProjects()
        {
            return await _context.Projects
                .Include(p => p.School)
                    .ThenInclude(s => s.Institute)
                .Include(p => p.Tickets)
                    .ThenInclude(t=>t.Sprint)
                .ToListAsync();
        }


    }
}

//using Microsoft.AspNetCore.Mvc;
//using System.Linq;
//using TechnoparkProj.Application.Services;
//using TechnoparkProj.Contracts;
//using TechnoparkProj.Core.Models;

//namespace TechnoparkProj.Controllers
//{
//    [ApiController]
//    [Route("controller")]
//    public class ProjectController : ControllerBase
//    {
//        private readonly IProjectService _projectService;

//        public ProjectController(IProjectService projectService)
//        {
//            _projectService = projectService;
//        }

//        [HttpGet]
//        public async Task<ActionResult<List<ProjectResponse>>> GetProject()
//        {
//            var projects = await _projectService.GetAllProjects();

//            var response = projects.Select(b => new ProjectResponse(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), ""));

//            return Ok(response);
//        }

//        [HttpPost]
//        public async Task<ActionResult<Guid>> CreateProject([FromBody] ProjectRequest request)
//        {
//            var project = new Project();

//            var projectId = await _projectService.CreateProject(project);

//            return Ok(projectId);
//        }

//        [HttpPut("{id:guid}")]
//        public async Task<ActionResult<Guid>> UpdateProject(Guid id, Guid professorId, Guid instituteId, [FromBody] ProjectRequest request)
//        {
//            var projectId = await _projectService.UpdateProject(id, professorId, instituteId, request.Description);

//            return Ok(projectId);
//        }

//        [HttpDelete("{id:guid}")]
//        public async Task<ActionResult<Guid>> DeleteProject(Guid id)
//        {
//            return Ok(await _projectService.DeleteProject(id));
//        }

//    }
//}
