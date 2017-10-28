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
    public class EmployeeRoleRepository : BaseRepository
    {
        #region Constructors
        public EmployeeRoleRepository(int userID) : base(userID)
        {

        }
        #endregion

        #region Public Methods   

        #region Create Methods
        /// <summary>
        /// Create EmployeeRole and at least one EmployeeRole sign in.
        /// </summary>
        /// <param name="EmployeeRole"></param>
        /// <returns></returns>
        public EmployeeRole CreateEmployeeRole(EmployeeRole EmployeeRole)
        {
            ctx.EmployeeRole.Add(EmployeeRole);
            ctx.SaveChanges();
            return EmployeeRole;
        }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="EmployeeRole"></param>
    /// <returns></returns>
    public EmployeeRole CreateEmployeeRole(string EmployeeRole)
    {
        EmployeeRole newEmployeeRole = new EmployeeRole();
        newEmployeeRole.Name = EmployeeRole;
        return CreateEmployeeRole(newEmployeeRole);
    }
    #endregion

    #region Delete Methods
    public bool DeleteEmployeeRole(int id)
    {
        var EmployeeRole = ctx.EmployeeRole.Where(x => x.ID == id).First();
        ctx.EmployeeRole.Remove(EmployeeRole);
        if (ctx.SaveChanges() > 0)
        {
            return true;
        }
        return false;
    }
    #endregion

    #region Update Methods
    public EmployeeRole UpdateEmployeeRole(EmployeeRole EmployeeRole)
    {
        var c = ctx.EmployeeRole.Where(x => x.ID == EmployeeRole.ID).First();

        if (c != null)
        {
            c.Name = EmployeeRole.Name;
            ctx.SaveChanges();
        }
        return c;
    }


    #endregion

    #region Get Methods

    public EmployeeRole GetEmployeeRole(int id)
    {
        return ctx.EmployeeRole.Where(x => x.ID == id).First();
    }

    public EmployeeRole GetEmployeeRole(string name)
    {
        return ctx.EmployeeRole.Where(x => x.Name == name).First();
    }


    #endregion

    #region API Methods

    public List<EmployeeRole> GetEmployeeRoles()
    {
        return (from p in ctx.EmployeeRole
                orderby p.Name
                select p).ToList();
    }

    public List<EmployeeRole> GetEmployeeRolesByFilter(string filter)
    {
            string[] splitData = filter.Split(new char[] { ',' });

            return (from p in ctx.EmployeeRole
                    where splitData.Any(c=>p.Name.StartsWith(c))
                    orderby p.Name
                    select p).ToList();

        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="EmployeeRoleList"></param>
    public void SaveEmployeeRoles(List<EmployeeRole> EmployeeRoles, string filter)
    {
        string[] splitData = filter.Split(new char[] { ',' });


        List<EmployeeRole> updateEmployeeRoles = new List<EmployeeRole>();
        List<EmployeeRole> newEmployeeRoles = new List<EmployeeRole>();
        List<int> updateIds = new List<int>();
        List<EmployeeRole> deleteEmployeeRoles = new List<EmployeeRole>();

        if (EmployeeRoles != null)
        {
            updateEmployeeRoles = (from m in EmployeeRoles where  m.ID > 0 select m).ToList();
            newEmployeeRoles = (from m in EmployeeRoles where m.ID == 0 select m).ToList();
            updateIds = (from m in EmployeeRoles where splitData.Any(c => m.Name.StartsWith(c)) && m.ID > 0 select m.ID).ToList();
        }

        deleteEmployeeRoles = (from m in ctx.EmployeeRole
                               where splitData.Any(c => m.Name.StartsWith(c)) && !updateIds.Contains(m.ID)
                               select m).ToList();

        //Delete EmployeeRole
        foreach (EmployeeRole EmployeeRole in deleteEmployeeRoles)
        {
            ctx.EmployeeRole.Remove(EmployeeRole);
        }


        //Update EmployeeRoles
        foreach (EmployeeRole EmployeeRole in updateEmployeeRoles)
        {
            ctx.EmployeeRole.Attach(EmployeeRole);
            ctx.Entry(EmployeeRole).State = EntityState.Modified;
        }

        //Insert new EmployeeRoles
        foreach (EmployeeRole EmployeeRole in newEmployeeRoles)
        {
            ctx.EmployeeRole.Add(EmployeeRole);
        }

        ctx.SaveChanges();

    }
    #endregion

    #endregion

}
}