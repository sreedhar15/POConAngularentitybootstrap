using LRPBookDomain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LRPBookDomain.Repositories
{
    public class ProjectUserRepository : BaseRepository
    {
        #region Constructors
        public ProjectUserRepository(int projectUserID)
            : base(projectUserID)
        {

        }
        #endregion Constructors

        #region Public Methods

        #region Create Methods
        public ProjectUser CreateProjectUser(ProjectUser projectUser)
        {
            ctx.ProjectUser.Add(projectUser);
            ctx.SaveChanges();
            return projectUser;
        }

        #endregion Create Methods

        #region Delete Methods
        public bool DeleteProjectUser(int id)
        {
            var projectUser = ctx.ProjectUser.Where(x => x.ID == id).First();
            ctx.ProjectUser.Remove(projectUser);
            if (ctx.SaveChanges() > 0)
            {
                return true;
            }
            return false;
        }

        #endregion Delete Methods

        #region Update Methods
        public ProjectUser UpdateProjectUser(ProjectUser projectUser)
        {
            var c = ctx.ProjectUser.Where(x => x.ID == projectUser.ID).First();

            if (c != null)
            {
                c.UserID = projectUser.UserID;
                c.ProjectID = projectUser.ProjectID;
                c.SecurityRoleID = projectUser.SecurityRoleID;
                ctx.SaveChanges();
            }
            return c;
        }

        #endregion Update Methods

        #region Get Methods

        public ProjectUser GetProjectUser(int id)
        {
            return ctx.ProjectUser.Where(x => x.ID == id).First();
        }

        #endregion Get Methods

        #region API Methods

        public List<ProjectUser> GetProjectUsers()
        {
            return (from p in ctx.ProjectUser
                    orderby p.ID
                    select p).ToList();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public List<int> GetProjectUsers(int userID)
        {
            return (from p in ctx.ProjectUser
                    where p.UserID == userID
                    select p.ProjectID).ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ProjectUserList"></param>
        public void SaveProjectUsers(List<ProjectUser> projectUsers)
        {
            List<ProjectUser> updateProjectUsers = new List<ProjectUser>();
            List<ProjectUser> newProjectUsers = new List<ProjectUser>();
            List<int> updateIds = new List<int>();
            List<ProjectUser> deleteProjectUsers = new List<ProjectUser>();

            if (projectUsers != null)
            {

                updateProjectUsers = (from m in projectUsers where m.ID > 0 select m).ToList();
                newProjectUsers = (from m in projectUsers where m.ID == 0 select m).ToList();
                updateIds = (from m in projectUsers where m.ID > 0 select m.ID).ToList();
            }

            deleteProjectUsers = (from m in ctx.ProjectUser
                                  where !updateIds.Contains(m.ID)
                                       select m).ToList();

            //Delete ProjectUser
            foreach (ProjectUser projectUser in deleteProjectUsers)
            {
                ctx.ProjectUser.Remove(projectUser);
            }


            //Update ProjectUser
            foreach (ProjectUser projectUser in updateProjectUsers)
            {
                ctx.ProjectUser.Attach(projectUser);
                ctx.Entry(projectUser).State = EntityState.Modified;
            }

            //Insert new ProjectUser
            foreach (ProjectUser projectUser in newProjectUsers)
            {
                ctx.ProjectUser.Add(projectUser);
            }

            ctx.SaveChanges();

        }
        #endregion API Methods

        #endregion Public Methods
    }
}
