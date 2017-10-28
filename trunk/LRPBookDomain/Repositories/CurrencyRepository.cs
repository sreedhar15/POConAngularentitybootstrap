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
    public class CurrencyRepository : BaseRepository
    {
        #region Constructors
        public CurrencyRepository(int userID) : base(userID)
        {

        }
        #endregion

        #region Public Methods   

        #region Create Methods
        /// <summary>
        /// Create Currency and at least one Currency sign in.
        /// </summary>
        /// <param name="Currency"></param>
        /// <returns></returns>
        public Currency CreateCurrency(Currency Currency)
        {
            ctx.Currency.Add(Currency);
            ctx.SaveChanges();
            return Currency;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Currency"></param>
        /// <returns></returns>
        public Currency CreateCurrency(string Currency)
        {
            Currency newCurrency = new Currency();
            newCurrency.Name = Currency;
            return CreateCurrency(newCurrency);
        }
        #endregion

        #region Delete Methods
        public bool DeleteCurrency(int id)
        {
            var Currency = ctx.Currency.Where(x => x.ID == id).First();
            ctx.Currency.Remove(Currency);
            if (ctx.SaveChanges() > 0)
            {
                return true;
            }
            return false;
        }
        #endregion

        #region Update Methods
        public Currency UpdateCurrency(Currency Currency)
        {
            var c = ctx.Currency.Where(x => x.ID == Currency.ID).First();

            if (c != null)
            {
                c.Name = Currency.Name;
                ctx.SaveChanges();
            }
            return c;
        }


        #endregion

        #region Get Methods

        public Currency GetCurrency(int id)
        {
            return ctx.Currency.Where(x => x.ID == id).First();
        }

        public Currency GetCurrency(string name)
        {
            return ctx.Currency.Where(x => x.Name == name).First();
        }


        #endregion

        #region API Methods

        public List<Currency> GetCurrencys()
        {
            return (from p in ctx.Currency
                    orderby p.Name
                    select p).ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="CurrencyList"></param>
        public void SaveCurrencys(List<Currency> Currencys)
        {
            List<Currency> updateCurrencys = new List<Currency>();
            List<Currency> newCurrencys = new List<Currency>();
            List<int> updateIds = new List<int>();
            List<Currency> deleteCurrencys = new List<Currency>();

            if (Currencys != null)
            {

                updateCurrencys = (from m in Currencys where m.ID > 0 select m).ToList();
                newCurrencys = (from m in Currencys where m.ID == 0 select m).ToList();
                updateIds = (from m in Currencys where m.ID > 0 select m.ID).ToList();
            }

            deleteCurrencys = (from m in ctx.Currency
                               where !updateIds.Contains(m.ID)
                               select m).ToList();

            //Delete Currency
            foreach (Currency Currency in deleteCurrencys)
            {
                ctx.Currency.Remove(Currency);
            }


            //Update Currencys
            foreach (Currency Currency in updateCurrencys)
            {
                ctx.Currency.Attach(Currency);
                ctx.Entry(Currency).State = EntityState.Modified;
            }

            //Insert new Currencys
            foreach (Currency Currency in newCurrencys)
            {
                ctx.Currency.Add(Currency);
            }

            ctx.SaveChanges();

        }
        #endregion

        #endregion

    }
}