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
    public class EmployeeRoleByTypeRepository : BaseRepository
    {
        #region Constructors
        public EmployeeRoleByTypeRepository(int userID) : base(userID)
        {

        }
        #endregion

        #region Public Methods   

        #region Create Methods
        /// <summary>
        /// Create EmployeeRoleByType and at least one EmployeeRoleByType sign in.
        /// </summary>
        /// <param name="EmployeeRoleByType"></param>
        /// <returns></returns>
        public EmployeeRoleByType CreateEmployeeRoleByType(EmployeeRoleByType EmployeeRoleByType)
        {
            ctx.EmployeeRoleByType.Add(EmployeeRoleByType);
            ctx.SaveChanges();
            return EmployeeRoleByType;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="EmployeeRoleByType"></param>
        /// <returns></returns>
        public EmployeeRoleByType CreateEmployeeRoleByType(int employeeRoleTypeID, int employeeRoleID)
        {
            EmployeeRoleByType newEmployeeRoleByType = new EmployeeRoleByType();
            newEmployeeRoleByType.EmployeeRoleTypeID = employeeRoleTypeID;
            newEmployeeRoleByType.EmployeeRoleID = employeeRoleID;
            return CreateEmployeeRoleByType(newEmployeeRoleByType);
        }
        #endregion

        #region Delete Methods
        public bool DeleteEmployeeRoleByType(int id)
        {
            var EmployeeRoleByType = ctx.EmployeeRoleByType.Where(x => x.ID == id).First();
            ctx.EmployeeRoleByType.Remove(EmployeeRoleByType);
            if (ctx.SaveChanges() > 0)
            {
                return true;
            }
            return false;
        }
        #endregion

        #region Update Methods
        public EmployeeRoleByType UpdateEmployeeRoleByType(EmployeeRoleByType EmployeeRoleByType)
        {
            var c = ctx.EmployeeRoleByType.Where(x => x.ID == EmployeeRoleByType.ID).First();

            if (c != null)
            {
                c.EmployeeRoleTypeID = EmployeeRoleByType.EmployeeRoleTypeID;
                c.EmployeeRoleID = EmployeeRoleByType.EmployeeRoleID;
                ctx.SaveChanges();
            }
            return c;
        }


        #endregion

        #region Get Methods

        public EmployeeRoleByType GetEmployeeRoleByType(int id)
        {
            return ctx.EmployeeRoleByType.Where(x => x.ID == id).First();
        }
        
        #endregion

        #region API Methods

        public List<EmployeeRoleByType> GetEmployeeRoleByTypes()
        {
            return (from p in ctx.EmployeeRoleByType
                    orderby p.ID
                    select p).ToList();
        }

        public List<EmployeeRoleByType> GetEmployeeRoleByTypes(int employeeRoleTypeID)
        {
            return (from p in ctx.EmployeeRoleByType
                    where p.EmployeeRoleTypeID == employeeRoleTypeID
                    orderby p.ID
                    select p).ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="employeeRoleTypeID"></param>
        /// <returns></returns>
        public List<BaseList> GetEmployeeRoleByTypesList(int employeeRoleTypeID)
        {
            return (from p in ctx.EmployeeRoleByType
                    join q in ctx.EmployeeRole on p.EmployeeRoleID equals q.ID
                    where p.EmployeeRoleTypeID == employeeRoleTypeID
                    orderby p.ID
                    select new BaseList
                    {
                        ID = p.ID,
                        Name = q.Name
                    }
            ).ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="EmployeeRoleByTypeList"></param>
        public void SaveEmployeeRoleByTypes(List<EmployeeRoleByType> EmployeeRoleByTypes, int employeeRoleTypeID)
        {
            List<EmployeeRoleByType> updateEmployeeRoleByTypes = new List<EmployeeRoleByType>();
            List<EmployeeRoleByType> newEmployeeRoleByTypes = new List<EmployeeRoleByType>();
            List<int> updateIds = new List<int>();
            List<EmployeeRoleByType> deleteEmployeeRoleByTypes = new List<EmployeeRoleByType>();

            if (EmployeeRoleByTypes != null)
            {
                updateEmployeeRoleByTypes = (from m in EmployeeRoleByTypes where m.ID > 0 select m).ToList();
                newEmployeeRoleByTypes = (from m in EmployeeRoleByTypes where m.ID == 0 select m).ToList();
                // by filter.
                updateIds = (from m in EmployeeRoleByTypes where m.EmployeeRoleTypeID==employeeRoleTypeID &&  m.ID > 0 select m.ID).ToList();
            }

            // by filter.
            deleteEmployeeRoleByTypes = (from m in ctx.EmployeeRoleByType
                                         where m.EmployeeRoleTypeID==employeeRoleTypeID && !updateIds.Contains(m.ID)
                                         select m).ToList();

            //Delete EmployeeRoleByType
            foreach (EmployeeRoleByType EmployeeRoleByType in deleteEmployeeRoleByTypes)
            {
                ctx.EmployeeRoleByType.Remove(EmployeeRoleByType);
            }


            //Update EmployeeRoleByTypes
            foreach (EmployeeRoleByType EmployeeRoleByType in updateEmployeeRoleByTypes)
            {
                ctx.EmployeeRoleByType.Attach(EmployeeRoleByType);
                ctx.Entry(EmployeeRoleByType).State = EntityState.Modified;
            }

            //Insert new EmployeeRoleByTypes
            foreach (EmployeeRoleByType EmployeeRoleByType in newEmployeeRoleByTypes)
            {
                EmployeeRoleByType.EmployeeRoleTypeID = employeeRoleTypeID;
                ctx.EmployeeRoleByType.Add(EmployeeRoleByType);
            }

            ctx.SaveChanges();

        }
        #endregion

        #endregion

    }
}