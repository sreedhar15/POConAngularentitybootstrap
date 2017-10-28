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
    public class FranchiseRepository : BaseRepository
    {
        #region Constructors
        public FranchiseRepository(int userID) : base(userID)
        {

        }
        #endregion

        #region Public Methods   

        #region Create Methods
        /// <summary>
        /// Create Country and at least one Country sign in.
        /// </summary>
        /// <param name="Franchise"></param>
        /// <returns></returns>
        public Franchise CreateFranchise(Franchise Franchise)
        {
            ctx.Franchise.Add(Franchise);
            ctx.SaveChanges();
            return Franchise;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Country"></param>
        /// <returns></returns>
        public Franchise CreateFranchise(string Franchise)
        {
            Franchise newFranchise = new Franchise();
            newFranchise.Name = Franchise;
            return CreateFranchise(newFranchise);
        }
        #endregion

        #region Delete Methods
        public bool DeleteFranchise(int id)
        {
            var Franchise = ctx.Franchise.Where(x => x.ID == id).First();
            ctx.Franchise.Remove(Franchise);
            if (ctx.SaveChanges() > 0)
            {
                return true;
            }
            return false;
        }
        #endregion

        #region Update Methods
        public Franchise UpdateFranchise(Franchise Franchise)
        {
            var c = ctx.Franchise.Where(x => x.ID == Franchise.ID).First();

            if (c != null)
            {
                c.Name = Franchise.Name;
                ctx.SaveChanges();
            }
            return c;
        }


        #endregion

        #region Get Methods

        public Franchise GetFranchise(int id)
        {
            return ctx.Franchise.Where(x => x.ID == id).First();
        }

        public Franchise GetFranchise(string name)
        {
            return ctx.Franchise.Where(x => x.Name == name).First();
        }


        #endregion

        #region API Methods

        public List<Franchise> GetFranchises()
        {
            return (from p in ctx.Franchise
                    orderby p.Name
                    select p).ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="CountryList"></param>
        public void SaveFranchises(List<Franchise> Franchises)
        {
            List<Franchise> updateFranchises = new List<Franchise>();
            List<Franchise> newFranchises = new List<Franchise>();
            List<int> updateIds = new List<int>();
            List<Franchise> deleteFranchises = new List<Franchise>();

            if (Franchises != null)
            {

                updateFranchises = (from m in Franchises where m.ID > 0 select m).ToList();
                newFranchises = (from m in Franchises where m.ID == 0 select m).ToList();
                updateIds = (from m in Franchises where m.ID > 0 select m.ID).ToList();
            }

            deleteFranchises = (from m in ctx.Franchise
                               where !updateIds.Contains(m.ID)
                              select m).ToList();

            //Delete Franchise
            foreach (Franchise Franchise in deleteFranchises)
            {
                ctx.Franchise.Remove(Franchise);
            }


            //Update Countrys
            foreach (Franchise Franchise in updateFranchises)
            {
                ctx.Franchise.Attach(Franchise);
                ctx.Entry(Franchise).State = EntityState.Modified;
            }

            //Insert new Countrys
            foreach (Franchise Franchise in newFranchises)
            {
                ctx.Franchise.Add(Franchise);
            }

            ctx.SaveChanges();

        }
        #endregion

        #endregion

    }
}