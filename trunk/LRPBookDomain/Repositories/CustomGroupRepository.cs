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
    public class CustomGroupRepository : BaseRepository
    {

        #region Constructors
        public CustomGroupRepository(int userID) : base(userID)
        {

        }
        #endregion

        #region Public Methods 

        #region Create Methods
        /// <summary>
        /// Create Plan and at least one Plan sign in.
        /// </summary>
        /// <param name="CustomGroup"></param>
        /// <returns></returns>
        public CustomGroup CreateCustomGroup(CustomGroup CustomGroup)
        {
            ctx.CustomGroup.Add(CustomGroup);
            ctx.SaveChanges();
            return CustomGroup;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="CustomGroup"></param>
        /// <returns></returns>
        public CustomGroup CreateCustomGroup(string CustomGroup)
        {
            CustomGroup newCustomGroup = new CustomGroup();
            newCustomGroup.Name = CustomGroup;
            return CreateCustomGroup(newCustomGroup);
        }
        #endregion

        #region Delete Methods
        public bool DeleteCustomGroup(int id)
        {
            var CustomGroup = ctx.CustomGroup.Where(x => x.ID == id).First();
            ctx.CustomGroup.Remove(CustomGroup);
            if (ctx.SaveChanges() > 0)
            {
                return true;
            }
            return false;
        }
        #endregion

        #region Update Methods
        public CustomGroup UpdateCustomGroup(CustomGroup CustomGroup)
        {
            var c = ctx.CustomGroup.Where(x => x.ID == CustomGroup.ID).First();

            if (c != null)
            {
                c.Name = CustomGroup.Name;
                ctx.SaveChanges();
            }
            return c;
        }

        #endregion

        #region Get Methods

        public CustomGroup GetCustomGroup(int id)
        {
            return ctx.CustomGroup.Where(x => x.ID == id).First();
        }

        public CustomGroup CustomGroup(string name)
        {
            return ctx.CustomGroup.Where(x => x.Name == name).First();
        }

        #endregion

        #region API Methods

        public List<CustomGroup> GetCustomGroups()
        {
            return (from p in ctx.CustomGroup
                    orderby p.Name
                    select p).ToList();
        }

        

        /// <summary>
        /// 
        /// </summary>
        /// <param name="CustomGroupList"></param>
        public void SaveCustomGroups(List<CustomGroup> PlanGgroups)
        {
            List<CustomGroup> updatePlans = new List<CustomGroup>();
            List<CustomGroup> newPlans = new List<CustomGroup>();
            List<int> updateIds = new List<int>();
            List<CustomGroup> deletePlans = new List<CustomGroup>();

            if (PlanGgroups != null)
            {

                updatePlans = (from m in PlanGgroups where m.ID > 0 select m).ToList();
                newPlans = (from m in PlanGgroups where m.ID == 0 select m).ToList();
                updateIds = (from m in PlanGgroups where m.ID > 0 select m.ID).ToList();
            }

            deletePlans = (from m in ctx.CustomGroup
                           where !updateIds.Contains(m.ID)
                           select m).ToList();

            //Delete Plan
            foreach (CustomGroup CustomGroup in deletePlans)
            {
                ctx.CustomGroup.Remove(CustomGroup);
            }


            //Update Plans
            foreach (CustomGroup CustomGroup in updatePlans)
            {
                ctx.CustomGroup.Attach(CustomGroup);
                ctx.Entry(CustomGroup).State = EntityState.Modified;
            }

            //Insert new Plans
            foreach (CustomGroup CustomGroup in newPlans)
            {
                ctx.CustomGroup.Add(CustomGroup);
            }

            ctx.SaveChanges();

        }


        #endregion

        #endregion
    }
}
