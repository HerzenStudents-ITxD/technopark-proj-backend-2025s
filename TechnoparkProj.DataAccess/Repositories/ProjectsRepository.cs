//using TechnoparkProj.Core.Models;
//using Microsoft.EntityFrameworkCore;
//using TechnoparkProj.DataAccess.Entities;

//namespace TechnoparkProj.DataAccess.Repositories
//{
//    public class ProjectsRepository : IProjectsRepository
//    {
//        public readonly TechnoparkProjDbContext _context;

//        public ProjectsRepository(TechnoparkProjDbContext context)
//        {
//            _context = context;
//        }

//        public async Task<List<Project>> Get()
//        {
//            //var projectEntities = await _context.Projects
//            //    .AsNoTracking()
//            //    .ToListAsync();

//            //var projects = projectEntities
//            //    .Select(b => new Project())
//            //    .ToList();

//            return null;
//        }

//        public async Task<int> Create(Project project)
//        {
//            //var projectEntity = new ProjectEntity
//            //{
//            //    Id = project.Id,
//            //    ProfessorId = project.ProfessorId,
//            //    InstituteId = project.InstituteId,
//            //    Description = project.Description
//            //};

//            //await _context.Projects.AddAsync(projectEntity);
//            //await _context.SaveChangesAsync();

//            //return projectEntity.Id;
//            return 0;
//        }

//        public async Task<int> Update(Guid projectId, Guid professorId, Guid instituteId, string description)
//        {
//            //await _context.Projects
//            //    .Where(b => b.Id == projectId)
//            //    .ExecuteUpdateAsync(s => s
//            //        .SetProperty(b => b.Description, b => description));
//            //// внешние ключи?

//            return 0;
//        }

//        public async Task<int> Delete(Guid projectId)
//        {
//            //await _context.Projects
//            //    .Where(b => b.Id == projectId)
//            //    .ExecuteDeleteAsync();

//            return 0;
//        }
//    }
//}
