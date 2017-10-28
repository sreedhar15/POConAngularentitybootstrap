using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using LRPBookDomain.Entities;
using LRPBookTypes.DTO;
using LRPBookTypes.List;

namespace LRPBookDomain.Repositories
{
    public class ProjectRepository : BaseRepository
    {
        #region Constructors
        public ProjectRepository(int userID) : base(userID)
        {

        }
        #endregion

        #region Public Methods   

        #region Create Methods
        /// <summary>
        /// Create Project and at least one Project sign in.
        /// </summary>
        /// <param name="Project"></param>
        /// <returns></returns>
        public Project CreateProject(Project Project)
        {
            ctx.Project.Add(Project);
            ctx.SaveChanges();
            return Project;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Project"></param>
        /// <returns></returns>
        public Project CreateProject(string Project)
        {
            Project newProject = new Project();
            newProject.Name = Project;
            return CreateProject(newProject);
        }
        #endregion

        #region Delete Methods
        public bool DeleteProject(int id)
        {
            var Project = ctx.Project.Where(x => x.ID == id).First();
            ctx.Project.Remove(Project);
            if (ctx.SaveChanges() > 0)
            {
                return true;
            }
            return false;
        }
        #endregion

        #region Update Methods
        public Project UpdateProject(Project Project)
        {
            var c = ctx.Project.Where(x => x.ID == Project.ID).First();

            if (c != null)
            {
                c.Name = Project.Name;
                ctx.SaveChanges();
            }
            return c;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Project"></param>
        /// <returns></returns>
        public int UpdateProject(List<ProjectDTO> target)
        {
            var source = GetProject();

            // find and insert new items.
            var newItems = target.Except(source, new BaseDTOComparer());
            foreach (ProjectDTO ProjectDTO in newItems)
            {
                Project Project = new Project();
                Project.Name = ProjectDTO.KeyName;
                CreateProject(Project);
            }

            // find and delete items
            var deleteItems = source.Except(target, new BaseDTOComparer());
            foreach (ProjectDTO ProjectDTO in deleteItems)
            {
                Project Project = GetProject(ProjectDTO.KeyName);
                DeleteProject(Project.ID);
            }

            ctx.SaveChanges();

            return 0;

        }
        #endregion

        #region Get Methods

        public Project GetProject(int id)
        {
            return ctx.Project.Where(x => x.ID == id).First();
        }

        public Project GetProject(string name)
        {
            return ctx.Project.Where(x => x.Name == name).First();
        }

        public List<ProjectDTO> GetProject()
        {
            return (from p in ctx.Project
                    orderby p.Name
                    select new ProjectDTO
                    {
                        KeyName = p.Name
                    }).ToList();
        }
        public List<Project> GetProjectsByFilter(string filter)
        {
            string[] splitData = filter.Split(new char[] { ',' });

            return (from p in ctx.Project
                    where splitData.Any(c => p.Name.StartsWith(c))
                    orderby p.Name
                    select p).ToList();

        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<ProjectList> GetProjectList()
        {
            var list = (from p in ctx.Project
                    orderby p.Name
                    select new ProjectList
                    {
                        ID = p.ID,
                        Name = p.Name
                    }).ToList();

            return list;
        }
        #endregion
        
        #region API Methods

        public List<Project> GetProjects()
        {
            return (from p in ctx.Project
                    orderby p.Name
                    select p).ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="projectList"></param>
        public void SaveProjects(List<Project> projects, string filter)
        {
            string[] splitData = filter.Split(new char[] { ',' });

            List<Project> updateProjects = new List<Project>();
            List<Project> newProjects = new List<Project>();
            List<int> updateIds = new List<int>();
            List<Project> deleteProjects = new List<Project>();

            if (projects != null)
            {

                updateProjects = (from m in projects where m.ID > 0 select m).ToList();
                newProjects = (from m in projects where m.ID == 0 select m).ToList();
                //updateIds = (from m in projects where m.ID > 0 select m.ID).ToList();
                updateIds = (from m in projects where splitData.Any(c => m.Name.StartsWith(c)) && m.ID > 0 select m.ID).ToList();
            }

            deleteProjects = (from m in ctx.Project
                              where splitData.Any(c => m.Name.StartsWith(c)) && !updateIds.Contains(m.ID)
                              select m).ToList();

            //Delete Project
            foreach (Project project in deleteProjects)
            {
                ctx.Project.Remove(project);
            }


            //Update Projects
            foreach (Project project in updateProjects)
            {
                ctx.Project.Attach(project);
                ctx.Entry(project).State = EntityState.Modified;
            }

            //Insert new Projects
            foreach (Project project in newProjects)
            {
                ctx.Project.Add(project);
            }

            ctx.SaveChanges();

        }
        #endregion

        #endregion

    }
}