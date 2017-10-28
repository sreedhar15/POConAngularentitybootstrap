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
    public class SecurityRoleRepository : BaseRepository
    {
        #region Constructors
        public SecurityRoleRepository(int SecurityRoleID)
            : base(SecurityRoleID)
        {

        }
        #endregion

        #region Public Methods

        #region Create Methods
        /// <summary>
        /// Create SecurityRole and at least one SecurityRole sign in.
        /// </summary>
        /// <param name="SecurityRole"></param>
        /// <returns></returns>
        public SecurityRole CreateSecurityRole(SecurityRole SecurityRole)
        {
            ctx.SecurityRole.Add(SecurityRole);
            ctx.SaveChanges();
            return SecurityRole;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="SecurityRole"></param>
        /// <returns></returns>
        public SecurityRole CreateSecurityRole(string SecurityRole)
        {
            SecurityRole newSecurityRole = new SecurityRole();
            newSecurityRole.Name = SecurityRole;
            return CreateSecurityRole(newSecurityRole);
        }
        #endregion

        #region Delete Methods
        public bool DeleteSecurityRole(int id)
        {
            var SecurityRole = ctx.SecurityRole.Where(x => x.ID == id).First();
            ctx.SecurityRole.Remove(SecurityRole);
            if (ctx.SaveChanges() > 0)
            {
                return true;
            }
            return false;
        }
        #endregion

        #region Update Methods
        public SecurityRole UpdateSecurityRole(SecurityRole SecurityRole)
        {
            var c = ctx.SecurityRole.Where(x => x.ID == SecurityRole.ID).First();

            if (c != null)
            {
                ctx.SaveChanges();
            }
            return c;
        }


        #endregion

        #region Get Methods



        public SecurityRole GetSecurityRole(int id)
        {
            return ctx.SecurityRole.Where(x => x.ID == id).First();
        }

        public SecurityRole GetSecurityRole(string name)
        {
            return ctx.SecurityRole.Where(x => x.Name == name).First();
        }


        #endregion

        #region API Methods

        public List<SecurityRole> GetSecurityRoles()
        {
            return (from p in ctx.SecurityRole
                    orderby p.Name
                    select p).ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="SecurityRoleList"></param>
        public void SaveSecurityRoles(List<SecurityRole> SecurityRoles)
        {
            List<SecurityRole> updateSecurityRoles = new List<SecurityRole>();
            List<SecurityRole> newSecurityRoles = new List<SecurityRole>();
            List<int> updateIds = new List<int>();
            List<SecurityRole> deleteSecurityRoles = new List<SecurityRole>();

            if (SecurityRoles != null)
            {

                updateSecurityRoles = (from m in SecurityRoles where m.ID > 0 select m).ToList();
                newSecurityRoles = (from m in SecurityRoles where m.ID == 0 select m).ToList();
                updateIds = (from m in SecurityRoles where m.ID > 0 select m.ID).ToList();
            }

            deleteSecurityRoles = (from m in ctx.SecurityRole
                                   where !updateIds.Contains(m.ID)
                                   select m).ToList();

            //Delete SecurityRole
            foreach (SecurityRole SecurityRole in deleteSecurityRoles)
            {
                ctx.SecurityRole.Remove(SecurityRole);
            }


            //Update SecurityRoles
            foreach (SecurityRole SecurityRole in updateSecurityRoles)
            {
                ctx.SecurityRole.Attach(SecurityRole);
                ctx.Entry(SecurityRole).State = EntityState.Modified;
            }

            //Insert new SecurityRoles
            foreach (SecurityRole SecurityRole in newSecurityRoles)
            {
                ctx.SecurityRole.Add(SecurityRole);
            }

            ctx.SaveChanges();

        }
        #endregion

        #endregion

    }
}