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
    public class UserSecurityRoleRepository : BaseRepository
    {
        #region Constructors
        public UserSecurityRoleRepository(int UserSecurityRoleID)
            : base(UserSecurityRoleID)
        {

        }
        #endregion

        #region Public Methods

        #region Create Methods
        /// <summary>
        /// Create UserSecurityRole and at least one UserSecurityRole sign in.
        /// </summary>
        /// <param name="UserSecurityRole"></param>
        /// <returns></returns>
        public UserSecurityRole CreateUserSecurityRole(UserSecurityRole UserSecurityRole)
        {
            ctx.UserSecurityRole.Add(UserSecurityRole);
            ctx.SaveChanges();
            return UserSecurityRole;
        }

        
        #endregion

        #region Delete Methods
        public bool DeleteUserSecurityRole(int id)
        {
            var UserSecurityRole = ctx.UserSecurityRole.Where(x => x.ID == id).First();
            ctx.UserSecurityRole.Remove(UserSecurityRole);
            if (ctx.SaveChanges() > 0)
            {
                return true;
            }
            return false;
        }
        #endregion

        #region Update Methods
        public UserSecurityRole UpdateUserSecurityRole(UserSecurityRole UserSecurityRole)
        {
            var c = ctx.UserSecurityRole.Where(x => x.ID == UserSecurityRole.ID).First();

            if (c != null)
            {
                c.UserID = UserSecurityRole.UserID;
                c.SecurityRoleID = UserSecurityRole.SecurityRoleID;
                ctx.SaveChanges();
            }
            return c;
        }


        #endregion

        #region Get Methods

        public UserSecurityRole GetUserSecurityRole(int id)
        {
            return ctx.UserSecurityRole.Where(x => x.ID == id).First();
        }

      

        #endregion

        #region API Methods

        public List<UserSecurityRole> GetUserSecurityRoles()
        {
            return (from p in ctx.UserSecurityRole
                    orderby p.ID
                    select p).ToList();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public List<int> GetUserSecurityRoles(int userID)
        {
            return (from p in ctx.UserSecurityRole where p.UserID == userID
                    select p.SecurityRoleID).ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="UserSecurityRoleList"></param>
        public void SaveUserSecurityRoles(List<UserSecurityRole> UserSecurityRoles)
        {
            List<UserSecurityRole> updateUserSecurityRoles = new List<UserSecurityRole>();
            List<UserSecurityRole> newUserSecurityRoles = new List<UserSecurityRole>();
            List<int> updateIds = new List<int>();
            List<UserSecurityRole> deleteUserSecurityRoles = new List<UserSecurityRole>();

            if (UserSecurityRoles != null)
            {

                updateUserSecurityRoles = (from m in UserSecurityRoles where m.ID > 0 select m).ToList();
                newUserSecurityRoles = (from m in UserSecurityRoles where m.ID == 0 select m).ToList();
                updateIds = (from m in UserSecurityRoles where m.ID > 0 select m.ID).ToList();
            }

            deleteUserSecurityRoles = (from m in ctx.UserSecurityRole
                                       where !updateIds.Contains(m.ID)
                                       select m).ToList();

            //Delete UserSecurityRole
            foreach (UserSecurityRole UserSecurityRole in deleteUserSecurityRoles)
            {
                ctx.UserSecurityRole.Remove(UserSecurityRole);
            }


            //Update UserSecurityRoles
            foreach (UserSecurityRole UserSecurityRole in updateUserSecurityRoles)
            {
                ctx.UserSecurityRole.Attach(UserSecurityRole);
                ctx.Entry(UserSecurityRole).State = EntityState.Modified;
            }

            //Insert new UserSecurityRoles
            foreach (UserSecurityRole UserSecurityRole in newUserSecurityRoles)
            {
                ctx.UserSecurityRole.Add(UserSecurityRole);
            }

            ctx.SaveChanges();

        }
        #endregion

        #endregion

    }
}