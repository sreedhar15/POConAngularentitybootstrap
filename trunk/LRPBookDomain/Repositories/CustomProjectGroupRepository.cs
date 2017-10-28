using LRPBookDomain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LRPBookDomain.Repositories
{
    public class CustomProjectGroupRepository : BaseRepository
    {
        #region Constructors
        public CustomProjectGroupRepository(int ProjectGroupUserID)
            : base(ProjectGroupUserID)
        {

        }
        #endregion Constructors

        #region Public Methods

        #region Create Methods
        public CustomProjectGroup CreateCustomProjectGroup(CustomProjectGroup customProjectGroup)
        {
            ctx.CustomProjectGroup.Add(customProjectGroup);
            ctx.SaveChanges();
            return customProjectGroup;
        }

        #endregion Create Methods

        #region Delete Methods
        public bool DeleteProjectGroupUser(int id)
        {
            var customProjectGroup = ctx.CustomProjectGroup.Where(x => x.ID == id).First();
            ctx.CustomProjectGroup.Remove(customProjectGroup);
            if (ctx.SaveChanges() > 0)
            {
                return true;
            }
            return false;
        }

        #endregion Delete Methods

        #region Update Methods
        public CustomProjectGroup UpdateCustomProjectGroup(CustomProjectGroup customProjectGroup)
        {
            var c = ctx.CustomProjectGroup.Where(x => x.ID == customProjectGroup.ID).First();

            if (c != null)
            {
                c.ID = customProjectGroup.ID;
                c.ProjectGroupID = customProjectGroup.ProjectGroupID;
                c.ProjectID = customProjectGroup.ProjectID;
                c.CustomGroupID = customProjectGroup.CustomGroupID;
                ctx.SaveChanges();
            }
            return c;
        }

        #endregion Update Methods

        #region Get Methods

        public CustomProjectGroup GetCustomProjectGroup(int id)
        {
            return ctx.CustomProjectGroup.Where(x => x.ID == id).First();
        }

        #endregion Get Methods

        #region API Methods

        public List<CustomProjectGroup> GetCustomProjectGroups()
        {
            return (from p in ctx.CustomProjectGroup
                    orderby p.ID
                    select p).ToList();
        }
        public List<CustomProjectGroup> GetCustomProjectGroups(int customGroupId, int projectGroupId)
        {
            List<CustomProjectGroup> CustomProjectGroupsbyId = new List<CustomProjectGroup>();
            //if (projectGroupId <= 0)
            //{
            //    CustomProjectGroupsbyId=(from p in ctx.CustomProjectGroup
            //            where p.CustomGroupID == customGroupId
            //            orderby p.ID
            //            select p).ToList();
            //}
            //else if (customGroupId<=0)
            //{
            //    CustomProjectGroupsbyId = (from p in ctx.CustomProjectGroup
            //                               where p.ProjectGroupID == projectGroupId
            //                               orderby p.ID
            //                               select p).ToList();
            //}
            //else
            if(projectGroupId > 0 && customGroupId > 0)
            {
                CustomProjectGroupsbyId = (from p in ctx.CustomProjectGroup
                                           where p.ProjectGroupID == projectGroupId
                                           && p.CustomGroupID == customGroupId
                                           orderby p.ID
                                           select p).ToList();
            }
            return CustomProjectGroupsbyId;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        //public List<int> GetProjectGroups(int projectID)
        //{
        //    return (from p in ctx.CustomProjectGroup
        //            where p.ProjectID == projectID
        //            select p.ProjectGroupID).ToList();
        //}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ProjectGroupUserList"></param>
        public void SaveCustomProjectGroups(List<CustomProjectGroup> customProjectGroups, int customGroupId, int projectGroupId)
        {
            List<CustomProjectGroup> updateCustomProjectGroups = new List<CustomProjectGroup>();
            List<CustomProjectGroup> newCustomProjectGroups = new List<CustomProjectGroup>();
            List<int> updateIds = new List<int>();
            List<CustomProjectGroup> deleteCustomProjectGroups = new List<CustomProjectGroup>();

            if (customProjectGroups != null)
            {

                updateCustomProjectGroups = (from m in customProjectGroups where m.ID > 0 select m).ToList();
                newCustomProjectGroups = (from m in customProjectGroups where m.ID == 0 select m).ToList();
                updateIds = (from m in customProjectGroups where m.ID > 0 select m.ID).ToList();
            }

            deleteCustomProjectGroups = (from m in ctx.CustomProjectGroup
                                       where m.CustomGroupID==customGroupId && m.ProjectGroupID == projectGroupId && !updateIds.Contains(m.ID)
                                       select m).ToList();

            //Delete CustomProjectGroups
            foreach (CustomProjectGroup customProjectGroup in deleteCustomProjectGroups)
            {
                ctx.CustomProjectGroup.Remove(customProjectGroup);
            }


            //Update CustomProjectGroup
            foreach (CustomProjectGroup customProjectGroup in updateCustomProjectGroups)
            {
                ctx.CustomProjectGroup.Attach(customProjectGroup);
                ctx.Entry(customProjectGroup).State = EntityState.Modified;
            }

            //Insert new CustomProjectGroup
            foreach (CustomProjectGroup customProjectGroup in newCustomProjectGroups)
            {
                customProjectGroup.ProjectGroupID = projectGroupId;
                customProjectGroup.CustomGroupID = customGroupId;
                ctx.CustomProjectGroup.Add(customProjectGroup);
            }

            ctx.SaveChanges();

        }
        #endregion API Methods

        #endregion Public Methods

    }
}
