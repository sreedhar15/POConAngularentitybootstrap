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
    public class EmployeeRoleTypeRepository : BaseRepository
    {
        #region Constructors
        public EmployeeRoleTypeRepository(int userID) : base(userID)
        {

        }
        #endregion

        #region Public Methods   

        #region Create Methods
        /// <summary>
        /// Create EmployeeRoleType and at least one EmployeeRoleType sign in.
        /// </summary>
        /// <param name="EmployeeRoleType"></param>
        /// <returns></returns>
        public EmployeeRoleType CreateEmployeeRoleType(EmployeeRoleType EmployeeRoleType)
        {
            ctx.EmployeeRoleType.Add(EmployeeRoleType);
            ctx.SaveChanges();
            return EmployeeRoleType;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="EmployeeRoleType"></param>
        /// <returns></returns>
        public EmployeeRoleType CreateEmployeeRoleType(string EmployeeRoleType)
        {
            EmployeeRoleType newEmployeeRoleType = new EmployeeRoleType();
            newEmployeeRoleType.Name = EmployeeRoleType;
            return CreateEmployeeRoleType(newEmployeeRoleType);
        }
        #endregion

        #region Delete Methods
        public bool DeleteEmployeeRoleType(int id)
        {
            var EmployeeRoleType = ctx.EmployeeRoleType.Where(x => x.ID == id).First();
            ctx.EmployeeRoleType.Remove(EmployeeRoleType);
            if (ctx.SaveChanges() > 0)
            {
                return true;
            }
            return false;
        }
        #endregion

        #region Update Methods
        public EmployeeRoleType UpdateEmployeeRoleType(EmployeeRoleType EmployeeRoleType)
        {
            var c = ctx.EmployeeRoleType.Where(x => x.ID == EmployeeRoleType.ID).First();

            if (c != null)
            {
                c.Name = EmployeeRoleType.Name;
                ctx.SaveChanges();
            }
            return c;
        }

        
        #endregion

        #region Get Methods

        public EmployeeRoleType GetEmployeeRoleType(int id)
        {
            return ctx.EmployeeRoleType.Where(x => x.ID == id).First();
        }

        public EmployeeRoleType GetEmployeeRoleType(string name)
        {
            return ctx.EmployeeRoleType.Where(x => x.Name == name).First();
        }

       
        #endregion

        #region API Methods

        public List<EmployeeRoleType> GetEmployeeRoleTypes()
        {
            return (from p in ctx.EmployeeRoleType
                    orderby p.Name
                    select p).ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="EmployeeRoleTypeList"></param>
        public void SaveEmployeeRoleTypes(List<EmployeeRoleType> EmployeeRoleTypes)
        {
            List<EmployeeRoleType> updateEmployeeRoleTypes = new List<EmployeeRoleType>();
            List<EmployeeRoleType> newEmployeeRoleTypes = new List<EmployeeRoleType>();
            List<int> updateIds = new List<int>();
            List<EmployeeRoleType> deleteEmployeeRoleTypes = new List<EmployeeRoleType>();

            if (EmployeeRoleTypes != null)
            {

                updateEmployeeRoleTypes = (from m in EmployeeRoleTypes where m.ID > 0 select m).ToList();
                newEmployeeRoleTypes = (from m in EmployeeRoleTypes where m.ID == 0 select m).ToList();
                updateIds = (from m in EmployeeRoleTypes where m.ID > 0 select m.ID).ToList();
            }

            deleteEmployeeRoleTypes = (from m in ctx.EmployeeRoleType
                                       where !updateIds.Contains(m.ID)
                                       select m).ToList();

            //Delete EmployeeRoleType
            foreach (EmployeeRoleType EmployeeRoleType in deleteEmployeeRoleTypes)
            {
                ctx.EmployeeRoleType.Remove(EmployeeRoleType);
            }


            //Update EmployeeRoleTypes
            foreach (EmployeeRoleType EmployeeRoleType in updateEmployeeRoleTypes)
            {
                ctx.EmployeeRoleType.Attach(EmployeeRoleType);
                ctx.Entry(EmployeeRoleType).State = EntityState.Modified;
            }

            //Insert new EmployeeRoleTypes
            foreach (EmployeeRoleType EmployeeRoleType in newEmployeeRoleTypes)
            {
                ctx.EmployeeRoleType.Add(EmployeeRoleType);
            }

            ctx.SaveChanges();

        }
        #endregion

        #endregion

    }
}