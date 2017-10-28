using LRPBookDomain.Entities;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace LRPBookDomain.Repositories
{
    public class ProjectGroupRepository : BaseRepository
    {
        #region Constructors
        public ProjectGroupRepository(int userID) : base(userID)
        {
        }
        #endregion

        #region Public Methods 

        #region Create Methods
        /// <summary>
        /// Create ProjectGroup and at least one ProjectGroup sign in.
        /// </summary>
        /// <param name="ProjectGroup"></param>
        /// <returns></returns>
        public ProjectGroup CreateProjectGroup(ProjectGroup ProjectGroup)
        {
            ctx.ProjectGroup.Add(ProjectGroup);
            ctx.SaveChanges();
            return ProjectGroup;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ProjectGroup"></param>
        /// <returns></returns>
        public ProjectGroup CreateProjectGroup(string ProjectGroup)
        {
            ProjectGroup newProjectGroup = new ProjectGroup();
            newProjectGroup.Name = ProjectGroup;
            return CreateProjectGroup(newProjectGroup);
        }
        #endregion

        #region Delete Methods
        public bool DeleteProjectGroup(int id)
        {
            var projectGroup = ctx.ProjectGroup.Where(pg => pg.ID == id).First();
            ctx.ProjectGroup.Remove(projectGroup);
            if (ctx.SaveChanges() > 0)
            {
                return true;
            }
            return false;
        }
        #endregion

        #region Update Methods
        public ProjectGroup UpdateProjectGroup(ProjectGroup ProjectGroup)
        {
            var pg = ctx.ProjectGroup.Where(x => x.ID == ProjectGroup.ID).First();
            if (pg != null)
            {
                pg.Name = ProjectGroup.Name;
                ctx.SaveChanges();
            }
            return pg;
        }

        #endregion

        #region Get Methods

        public ProjectGroup GetProjectGroup(int id)
        {
            return ctx.ProjectGroup.Where(pg => pg.ID == id).First();
        }

        public ProjectGroup ProjectGroup(string name)
        {
            return ctx.ProjectGroup.Where(pg => pg.Name == name).First();
        }

        #endregion

        #region API Methods

        public List<ProjectGroup> GetProjectGroups()
        {
            return (from pg in ctx.ProjectGroup
                    orderby pg.Name
                    select pg).ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="projectGroups"></param>
        public void SaveProjectGroups(List<ProjectGroup> projectGroups)
        {
            List<ProjectGroup> updateProjectGroups = new List<ProjectGroup>();
            List<ProjectGroup> newProjectGroups = new List<ProjectGroup>();
            List<int> updateIds = new List<int>();
            List<ProjectGroup> deleteProjectGroups = new List<ProjectGroup>();

            if (projectGroups != null)
            {
                updateProjectGroups = (from pg in projectGroups where pg.ID > 0 select pg).ToList();
                newProjectGroups = (from pg in projectGroups where pg.ID == 0 select pg).ToList();
                updateIds = (from pg in projectGroups where pg.ID > 0 select pg.ID).ToList();
            }

            deleteProjectGroups = (from pg in ctx.ProjectGroup
                           where !updateIds.Contains(pg.ID)
                           select pg).ToList();

            //Delete ProjectGroups
            foreach (ProjectGroup ProjectGroup in deleteProjectGroups)
            {
                ctx.ProjectGroup.Remove(ProjectGroup);
            }
            
            //Update ProjectGroups
            foreach (ProjectGroup ProjectGroup in updateProjectGroups)
            {
                ctx.ProjectGroup.Attach(ProjectGroup);
                ctx.Entry(ProjectGroup).State = EntityState.Modified;
            }

            //Insert new ProjectGroups
            foreach (ProjectGroup ProjectGroup in newProjectGroups)
            {
                ctx.ProjectGroup.Add(ProjectGroup);
            }

            ctx.SaveChanges();
        }

        #endregion

        #endregion
    }
}
