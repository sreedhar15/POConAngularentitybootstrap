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
    public class CountryRepository : BaseRepository
    {
        #region Constructors
        public CountryRepository(int userID) : base(userID)
        {

        }
        #endregion

        #region Public Methods   

        #region Create Methods
        /// <summary>
        /// Create Country and at least one Country sign in.
        /// </summary>
        /// <param name="Country"></param>
        /// <returns></returns>
        public Country CreateCountry(Country Country)
        {
            ctx.Country.Add(Country);
            ctx.SaveChanges();
            return Country;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Country"></param>
        /// <returns></returns>
        public Country CreateCountry(string Country)
        {
            Country newCountry = new Country();
            newCountry.Name = Country;
            return CreateCountry(newCountry);
        }
        #endregion

        #region Delete Methods
        public bool DeleteCountry(int id)
        {
            var Country = ctx.Country.Where(x => x.ID == id).First();
            ctx.Country.Remove(Country);
            if (ctx.SaveChanges() > 0)
            {
                return true;
            }
            return false;
        }
        #endregion

        #region Update Methods
        public Country UpdateCountry(Country Country)
        {
            var c = ctx.Country.Where(x => x.ID == Country.ID).First();

            if (c != null)
            {
                c.Name = Country.Name;
                ctx.SaveChanges();
            }
            return c;
        }


        #endregion

        #region Get Methods

        public Country GetCountry(int id)
        {
            return ctx.Country.Where(x => x.ID == id).First();
        }

        public Country GetCountry(string name)
        {
            return ctx.Country.Where(x => x.Name == name).First();
        }


        #endregion

        #region API Methods

        public List<Country> GetCountrys()
        {
            return (from p in ctx.Country
                    orderby p.Name
                    select p).ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="CountryList"></param>
        public void SaveCountrys(List<Country> Countrys)
        {
            List<Country> updateCountrys = new List<Country>();
            List<Country> newCountrys = new List<Country>();
            List<int> updateIds = new List<int>();
            List<Country> deleteCountrys = new List<Country>();

            if (Countrys != null)
            {

                updateCountrys = (from m in Countrys where m.ID > 0 select m).ToList();
                newCountrys = (from m in Countrys where m.ID == 0 select m).ToList();
                updateIds = (from m in Countrys where m.ID > 0 select m.ID).ToList();
            }

            deleteCountrys = (from m in ctx.Country
                              where !updateIds.Contains(m.ID)
                              select m).ToList();

            //Delete Country
            foreach (Country Country in deleteCountrys)
            {
                ctx.Country.Remove(Country);
            }


            //Update Countrys
            foreach (Country Country in updateCountrys)
            {
                ctx.Country.Attach(Country);
                ctx.Entry(Country).State = EntityState.Modified;
            }

            //Insert new Countrys
            foreach (Country Country in newCountrys)
            {
                ctx.Country.Add(Country);
            }

            ctx.SaveChanges();

        }
        #endregion

        #endregion

    }
}