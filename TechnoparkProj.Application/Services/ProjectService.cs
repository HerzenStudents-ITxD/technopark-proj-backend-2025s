//using TechnoparkProj.Core.Models;
//using TechnoparkProj.DataAccess.Repositories;

//namespace TechnoparkProj.Application.Services
//{
//    public class ProjectService : IProjectService
//    {
//        private readonly IProjectsRepository _projectsRepository;

//        public ProjectService(IProjectsRepository projectsRepository)
//        {
//            _projectsRepository = projectsRepository;
//        }

//        public async Task<List<Project>> GetAllProjects()
//        {
//            return await _projectsRepository.Get();
//        }

//        public async Task<int> CreateProject(Project project)
//        {
//            return await _projectsRepository.Create(project);
//        }

//        public async Task<int> UpdateProject(Guid projectId, Guid professorId, Guid instituteId, string description)
//        {
//            return await _projectsRepository.Update(projectId, professorId, instituteId, description);
//        }

//        public async Task<int> DeleteProject(Guid projectId)
//        {
//            return await _projectsRepository.Delete(projectId);
//        }
//    }
//}
