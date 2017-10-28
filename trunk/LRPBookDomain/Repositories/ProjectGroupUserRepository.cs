using LRPBookDomain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LRPBookDomain.Repositories
{
    public class ProjectGroupUserRepository : BaseRepository
    {
        #region Constructors
        public ProjectGroupUserRepository(int ProjectGroupUserID)
            : base(ProjectGroupUserID)
        {

        }
        #endregion Constructors

        #region Public Methods

        #region Create Methods
        public ProjectGroupUser CreateProjectGroupUser(ProjectGroupUser projectGroupUser)
        {
            ctx.ProjectGroupUser.Add(projectGroupUser);
            ctx.SaveChanges();
            return projectGroupUser;
        }

        #endregion Create Methods

        #region Delete Methods
        public bool DeleteProjectGroupUser(int id)
        {
            var projectGroupUser = ctx.ProjectGroupUser.Where(x => x.ID == id).First();
            ctx.ProjectGroupUser.Remove(projectGroupUser);
            if (ctx.SaveChanges() > 0)
            {
                return true;
            }
            return false;
        }

        #endregion Delete Methods

        #region Update Methods
        public ProjectGroupUser UpdateProjectGroupUser(ProjectGroupUser projectGroupUser)
        {
            var c = ctx.ProjectGroupUser.Where(x => x.ID == projectGroupUser.ID).First();

            if (c != null)
            {
                c.UserID = projectGroupUser.UserID;
                c.ProjectGroupID = projectGroupUser.ProjectGroupID;
                c.SecurityRoleID = projectGroupUser.SecurityRoleID;
                ctx.SaveChanges();
            }
            return c;
        }

        #endregion Update Methods

        #region Get Methods

        public ProjectGroupUser GetProjectGroupUser(int id)
        {
            return ctx.ProjectGroupUser.Where(x => x.ID == id).First();
        }

        #endregion Get Methods

        #region API Methods

        public List<ProjectGroupUser> GetProjectGroupUsers()
        {
            return (from p in ctx.ProjectGroupUser
                    orderby p.ID
                    select p).ToList();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public List<int> GetProjectGroupUsers(int userID)
        {
            return (from p in ctx.ProjectGroupUser
                    where p.UserID == userID
                    select p.ProjectGroupID).ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ProjectGroupUserList"></param>
        public void SaveProjectGroupUsers(List<ProjectGroupUser> projectGroupUsers)
        {
            List<ProjectGroupUser> updateProjectGroupUsers = new List<ProjectGroupUser>();
            List<ProjectGroupUser> newProjectGroupUsers = new List<ProjectGroupUser>();
            List<int> updateIds = new List<int>();
            List<ProjectGroupUser> deleteProjectGroupUsers = new List<ProjectGroupUser>();

            if (projectGroupUsers != null)
            {

                updateProjectGroupUsers = (from m in projectGroupUsers where m.ID > 0 select m).ToList();
                newProjectGroupUsers = (from m in projectGroupUsers where m.ID == 0 select m).ToList();
                updateIds = (from m in projectGroupUsers where m.ID > 0 select m.ID).ToList();
            }

            deleteProjectGroupUsers = (from m in ctx.ProjectGroupUser
                                       where !updateIds.Contains(m.ID)
                                       select m).ToList();

            //Delete ProjectGroupUsers
            foreach (ProjectGroupUser projectGroupUser in deleteProjectGroupUsers)
            {
                ctx.ProjectGroupUser.Remove(projectGroupUser);
            }


            //Update ProjectGroupUsers
            foreach (ProjectGroupUser projectGroupUser in updateProjectGroupUsers)
            {
                ctx.ProjectGroupUser.Attach(projectGroupUser);
                ctx.Entry(projectGroupUser).State = EntityState.Modified;
            }

            //Insert new ProjectGroupUsers
            foreach (ProjectGroupUser projectGroupUser in newProjectGroupUsers)
            {
                ctx.ProjectGroupUser.Add(projectGroupUser);
            }

            ctx.SaveChanges();

        }
        #endregion API Methods

        #endregion Public Methods

    }
}
