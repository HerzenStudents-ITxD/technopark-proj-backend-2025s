using Azure.Core;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;
using System.Net.NetworkInformation;
using TechnoparkProj.Contracts;
using TechnoparkProj.Core.Models;
using TechnoparkProj.DataAccess;

namespace TechnoparkProj.Controllers
{
    [ApiController]
    [Route("controller/project")]
    public class ProjectController : ControllerBase
    {
        private readonly TechnoparkProjDbContext _context;

        public ProjectController(TechnoparkProjDbContext context)
        {
            _context = context;
        }

        private static int CalculateProgress(ICollection<Sprint> sprints)
        {
            int total = 0;
            int totalCompleted = 0;
            foreach(Sprint sprint in sprints)
            {
                if (sprint.Tickets == null || sprint.Tickets.Count == 0)
                    continue;
                total += sprint.Tickets.Count;
                totalCompleted += sprint.Tickets.Count(t => t.Status == TicketStatus.DONE);
            }
            if (total == 0)
                return 0;

            return (int)((double)totalCompleted / total * 100);
        }

        //private static List<StudentData> GetStudentData(ICollection<StudProjLink> links)
        //{
        //    List<StudentData> returnList = new List<StudentData>();

        //    foreach (StudProjLink link in links)
        //    {
        //        StudentData studentData = new StudentData(0, );
        //        studentData.Id = link.StudentId;
        //        studentData.FullName = link.Student.Surname + " " + link.Student.Name;
        //        returnList.Add(studentData);
        //    }

        //    return returnList;
        //}

        //[HttpGet("db-test")]
        //public async Task<ActionResult> TestDatabaseConnection()
        //{
        //    try
        //    {
        //        // Test raw connection
        //        await _context.Database.OpenConnectionAsync();
        //        _context.Database.CloseConnection();

        //        // Test if ANY table exists
        //        var tables = await _context.Database.SqlQueryRaw<string>(
        //            "SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = 'BASE TABLE'"
        //        ).ToListAsync();

        //        return Ok(new
        //        {
        //            ConnectionSuccessful = true,
        //            TablesInDatabase = tables
        //        });
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, new
        //        {
        //            Error = ex.Message,
        //            Details = ex.InnerException?.Message,
        //            StackTrace = ex.StackTrace
        //        });
        //    }
        //}

        [HttpGet("all-projects")]
        public async Task<IActionResult> GetProjects([FromQuery] GetProjectsRequest request)
        {

            var projects = _context.Projects
                .Where(n => string.IsNullOrWhiteSpace(request.Search) ||
                            n.Name.ToLower().Contains(request.Search.ToLower()))
                .Include(p => p.School)
                    .ThenInclude(s => s.Institute)
                .Include(p => p.Sprints)
                    .ThenInclude(s => s.Tickets);


            var projectsDto = await projects
                .Select(p => new ProjectsDto(
                                            p.Id,
                                            p.Name,
                                            p.Description,
                                            p.School.Institute.Name,
                                            p.School.Name,
                                            p.Course,
                                            p.Semester,
                                            p.StudProjLinks.Select(spl => new StudentData(
                                                spl.Student.Id,
                                                spl.Student.Surname + " " + spl.Student.Name
                                            )).ToList(),
                                            p.Year,
                                            CalculateProgress(p.Sprints)
                                            )
                )
                .ToListAsync();

            return Ok(new GetProjectsResponse(projectsDto));
        }

        [HttpGet("proj-by-id")]
        public async Task<ActionResult> GetProjectById([FromQuery]int id)
        {
            var projects = _context.Projects
                .Where(p => p.Id == id)
                .Include(p => p.School)
                    .ThenInclude(s => s.Institute)
                .Include(p => p.Sprints)
                    .ThenInclude(s => s.Tickets);


            var projectDto = await projects
                .Select(p => new ProjectDto(
                                            p.Id,
                                            p.Name,
                                            p.Description,
                                            p.School.Institute.Name,
                                            p.School.Name,
                                            p.Course,
                                            p.Semester,
                                            p.StudProjLinks.Select(spl => new StudentData(
                                                spl.Student.Id,
                                                spl.Student.Surname + " " + spl.Student.Name
                                            )).ToList(),
                                            p.Year,
                                            p.Sprints
                                            )
                )
                .ToListAsync();

            return Ok(new GetProjectByIdResponse(projectDto));
        }

        [HttpPost]
        public async Task<IActionResult> CreateProject([FromBody] CreateProjectRequest request)
        {
            var project = new Project(0,
                                        request.Name,
                                        request.Description,
                                        request.Course,
                                        request.Semester,
                                        request.Year,
                                        request.SprintDuration,
                                        request.StartDate,
                                        request.SchoolId);

            await _context.Projects.AddAsync(project);
            await _context.SaveChangesAsync();

            foreach (int studentid in request.StudentIds)
            {
                StudProjLink link = new StudProjLink();
               
                link.StudentId = studentid;
                link.ProjectId = project.Id;
                await _context.StudProjLinks.AddAsync(link);
                await _context.SaveChangesAsync();
            }


            if (project.Duration != 0)
            {
                var backlog = new Sprint(0, true, 0, new DateTime(), project.Id);

                await _context.Sprints.AddAsync(backlog);
                await _context.SaveChangesAsync();

                var deadline = project.StartDate;
                for(int i = 0; i < (17*7/(int)project.Duration); i++)
                {
                    int dur = project.Duration == Duration.ONEWEEK ? 7 : 14;
                    deadline = deadline.AddDays(dur);
                    var sprint = new Sprint(0, false, 0, deadline, project.Id);

                    await _context.Sprints.AddAsync(sprint);
                    await _context.SaveChangesAsync();
                }
            }

            return Ok(new
            {
                success = true,
                projectId = project.Id
            });
        }

        [HttpGet("edit/{id}")]
        public async Task<IActionResult> GetProjectForEdit(int id)
        {
            var project = await _context.Projects
                .Include(p => p.School)
                .Include(p => p.StudProjLinks)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (project == null)
                return NotFound();

            return Ok(new
            {
                project = new
                {
                    project.Id,
                    project.Name,
                    project.Description,
                    project.Course,
                    project.Year,
                    Semester = project.Semester == Semester.SPRING ? 1 : 2,
                    project.Duration,
                    StartDate = project.StartDate.ToString("yyyy-MM-dd"),
                    InstituteId = project.School?.InstituteId,
                    project.SchoolId,
                    StudentIds = project.StudProjLinks.Select(l => l.StudentId)
                }
            });
        }

        [HttpPut("update-project/{id}")]
        public async Task<IActionResult> UpdateProject(int id, [FromBody] UpdateProjectRequest request)
        {
            try
            {
                var project = await _context.Projects
                    .Include(p => p.StudProjLinks)
                    .FirstOrDefaultAsync(p => p.Id == id);

                if (project == null)
                    return NotFound($"Project with id {id} not found");

                // Обновляем основные данные проекта
                project.Name = request.Name;
                project.Description = request.Description;
                project.Course = request.Course;
                project.Year = request.Year;
                project.Semester = request.Semester; // 1 - весна, 2 - осень
                project.Duration = request.SprintDuration;
                project.StartDate = request.StartDate;
                project.SchoolId = request.SchoolId;

                // Обновляем связи со студентами
                var currentStudentIds = project.StudProjLinks.Select(l => l.StudentId).ToList();
                var newStudentIds = request.StudentIds;

                // Удаляем отсутствующих студентов
                var toRemove = project.StudProjLinks
                    .Where(l => !newStudentIds.Contains(l.StudentId))
                    .ToList();

                _context.StudProjLinks.RemoveRange(toRemove);

                // Добавляем новых студентов
                var toAdd = newStudentIds
                    .Where(id => !currentStudentIds.Contains(id))
                    .Select(id => new StudProjLink { StudentId = id, ProjectId = project.Id })
                    .ToList();

                await _context.StudProjLinks.AddRangeAsync(toAdd);

                await _context.SaveChangesAsync();

                return Ok(new
                {
                    success = true,
                    project = new
                    {
                        project.Id,
                        project.Name,
                        project.Description,
                        project.Course,
                        project.Year,
                        Semester = project.Semester == Semester.SPRING ? 1 : 2,
                        project.Duration,
                        project.StartDate,
                        project.SchoolId,
                        StudentIds = project.StudProjLinks.Select(l => l.StudentId)
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
