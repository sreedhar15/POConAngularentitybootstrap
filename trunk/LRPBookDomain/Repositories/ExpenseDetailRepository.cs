using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using LRPBookDomain.Entities;
using LRPBookTypes.DTO;
using LRPBookLibrary;


namespace LRPBookDomain.Repositories
{
    public class ExpenseDetailRepository : BaseRepository
    {
        #region Constructors
        public ExpenseDetailRepository(int userID) : base(userID)
        {

        }
        #endregion

        #region Public Methods   

        #region Create Methods
        /// <summary>
        /// Create ExpenseDetail and at least one ExpenseDetail sign in.
        /// </summary>
        /// <param name="ExpenseDetail"></param>
        /// <returns></returns>
        public ExpenseDetail CreateExpenseDetail(ExpenseDetail expenseDetail)
        {
            ctx.ExpenseDetail.Add(expenseDetail);
            ctx.SaveChanges();
            return expenseDetail;
        }


        #endregion

        #region Delete Methods
        public bool DeleteExpenseDetail(int id)
        {
            var ExpenseDetail = ctx.ExpenseDetail.Where(x => x.ID == id).First();
            ctx.ExpenseDetail.Remove(ExpenseDetail);
            if (ctx.SaveChanges() > 0)
            {
                return true;
            }
            return false;
        }
        #endregion

        #region Update Methods
        public ExpenseDetail UpdateExpenseDetail(ExpenseDetail ExpenseDetail)
        {
            var c = ctx.ExpenseDetail.Where(x => x.ID == ExpenseDetail.ID).First();

            if (c != null)
            {
                c.ExpenseID = ExpenseDetail.ExpenseID;
                c.ExpenseTypeID = ExpenseDetail.ExpenseTypeID;
                c.CountryID = ExpenseDetail.CountryID;
                c.Comment = ExpenseDetail.Comment;
                c.Year = ExpenseDetail.Year;
                c.Month01 = ExpenseDetail.Month01;
                c.Month02 = ExpenseDetail.Month02;
                c.Month03 = ExpenseDetail.Month03;
                c.Month04 = ExpenseDetail.Month04;
                c.Month05 = ExpenseDetail.Month05;
                c.Month06 = ExpenseDetail.Month06;
                c.Month07 = ExpenseDetail.Month07;
                c.Month08 = ExpenseDetail.Month08;
                c.Month09 = ExpenseDetail.Month09;
                c.Month08 = ExpenseDetail.Month10;
                c.Month10 = ExpenseDetail.Month11;
                c.Month11 = ExpenseDetail.Month12;
                ctx.SaveChanges();
            }
            return c;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ExpenseDetail"></param>
        /// <returns></returns>
        public int UpdateExpenseDetail(List<ExpenseDetailDTO> target, int typeID, int year, int projectID)
        {
            var source = GetExpenseDetailByType(typeID, year, projectID);

            ExpenseRepository expenseRepository = new ExpenseRepository(BaseRepository.SystemUserID);
            CountryRepository countryRepository = new CountryRepository(BaseRepository.SystemUserID);

            // find and insert new items.
            var newItems = target.Except(source, new ExpenseDetailDTOComparer());
            foreach (ExpenseDetailDTO ExpenseDetailDTO in newItems)
            {
                ExpenseDetail ExpenseDetail = new ExpenseDetail();

                ExpenseDetail.ProjectID = projectID;
                ExpenseDetail.ExpenseTypeID = typeID;
                ExpenseDetail.Year = year;

                ExpenseDetail.ExpenseID = expenseRepository.GetExpense(ExpenseDetailDTO.Expense).ID;
                ExpenseDetail.CountryID = countryRepository.GetCountry(ExpenseDetailDTO.Country).ID;

                ExpenseDetail.Comment = ExpenseDetailDTO.Comment;
                ExpenseDetail.Month01 = DataUtility.GetDecimalValue(ExpenseDetailDTO.Month01);
                ExpenseDetail.Month02 = DataUtility.GetDecimalValue(ExpenseDetailDTO.Month02);
                ExpenseDetail.Month03 = DataUtility.GetDecimalValue(ExpenseDetailDTO.Month03);
                ExpenseDetail.Month04 = DataUtility.GetDecimalValue(ExpenseDetailDTO.Month04);
                ExpenseDetail.Month05 = DataUtility.GetDecimalValue(ExpenseDetailDTO.Month05);
                ExpenseDetail.Month06 = DataUtility.GetDecimalValue(ExpenseDetailDTO.Month06);
                ExpenseDetail.Month07 = DataUtility.GetDecimalValue(ExpenseDetailDTO.Month07);
                ExpenseDetail.Month08 = DataUtility.GetDecimalValue(ExpenseDetailDTO.Month08);
                ExpenseDetail.Month09 = DataUtility.GetDecimalValue(ExpenseDetailDTO.Month09);
                ExpenseDetail.Month10 = DataUtility.GetDecimalValue(ExpenseDetailDTO.Month10);
                ExpenseDetail.Month11 = DataUtility.GetDecimalValue(ExpenseDetailDTO.Month11);
                ExpenseDetail.Month12 = DataUtility.GetDecimalValue(ExpenseDetailDTO.Month12);

                CreateExpenseDetail(ExpenseDetail);
            }


            // find and insert new items.
            var updateItems = target.Union(source, new ExpenseDetailDTOComparer());
            foreach (ExpenseDetailDTO ExpenseDetailDTO in updateItems)
            {

                int expenseID = expenseRepository.GetExpense(ExpenseDetailDTO.Expense).ID;

                ExpenseDetail ExpenseDetail = GetExpenseDetail(typeID, year, projectID, expenseID);

                ExpenseDetail.ExpenseTypeID = typeID;
                ExpenseDetail.Year = year;
                ExpenseDetail.ProjectID = projectID;

                ExpenseDetail.ExpenseID = expenseID;
                ExpenseDetail.CountryID = countryRepository.GetCountry(ExpenseDetailDTO.Country).ID;

                ExpenseDetail.Comment = ExpenseDetailDTO.Comment;
                ExpenseDetail.Month01 = DataUtility.GetDecimalValue(ExpenseDetailDTO.Month01);
                ExpenseDetail.Month02 = DataUtility.GetDecimalValue(ExpenseDetailDTO.Month02);
                ExpenseDetail.Month03 = DataUtility.GetDecimalValue(ExpenseDetailDTO.Month03);
                ExpenseDetail.Month04 = DataUtility.GetDecimalValue(ExpenseDetailDTO.Month04);
                ExpenseDetail.Month05 = DataUtility.GetDecimalValue(ExpenseDetailDTO.Month05);
                ExpenseDetail.Month06 = DataUtility.GetDecimalValue(ExpenseDetailDTO.Month06);
                ExpenseDetail.Month07 = DataUtility.GetDecimalValue(ExpenseDetailDTO.Month07);
                ExpenseDetail.Month08 = DataUtility.GetDecimalValue(ExpenseDetailDTO.Month08);
                ExpenseDetail.Month09 = DataUtility.GetDecimalValue(ExpenseDetailDTO.Month09);
                ExpenseDetail.Month10 = DataUtility.GetDecimalValue(ExpenseDetailDTO.Month10);
                ExpenseDetail.Month11 = DataUtility.GetDecimalValue(ExpenseDetailDTO.Month11);
                ExpenseDetail.Month12 = DataUtility.GetDecimalValue(ExpenseDetailDTO.Month12);

                UpdateExpenseDetail(ExpenseDetail);
            }


            // find and delete items
            var deleteItems = source.Except(target, new ExpenseDetailDTOComparer());
            foreach (ExpenseDetailDTO ExpenseDetailDTO in deleteItems)
            {
                int expenseID = expenseRepository.GetExpense(ExpenseDetailDTO.Expense).ID;

                ExpenseDetail ExpenseDetail = GetExpenseDetail(typeID, year, projectID, expenseID);

                DeleteExpenseDetail(ExpenseDetail.ID);
            }

            ctx.SaveChanges();

            return 0;

        }
        #endregion

        #region Get Methods
        public ExpenseDetail GetExpenseDetail(int id)
        {
            return ctx.ExpenseDetail.Where(x => x.ID == id).First();
        }

        public ExpenseDetail GetExpenseDetail(int typeID, int year, int projectID, int expenseID)
        {
            return ctx.ExpenseDetail.Where(x => x.ExpenseTypeID == typeID && x.Year == year && x.ProjectID == projectID && x.ExpenseID == expenseID).First();
        }

        public List<ExpenseDetailDTO> GetExpenseDetailByType(int typeID, int year, int projectID)
        {
            var list = (from p in ctx.ExpenseDetail
                        join er in ctx.Expense on p.ExpenseID equals er.ID
                        join c in ctx.Country on p.CountryID equals c.ID
                        where p.ExpenseTypeID == typeID && p.Year == year && p.ProjectID == projectID
                        orderby er.Name, c.Name
                        select new ExpenseDetailDTO
                        {
                            Expense = er.Name,
                            Country = c.Name,
                            Comment = p.Comment,
                            Month01 = p.Month01.ToString(),
                            Month02 = p.Month02.ToString(),
                            Month03 = p.Month03.ToString(),
                            Month04 = p.Month04.ToString(),
                            Month05 = p.Month05.ToString(),
                            Month06 = p.Month06.ToString(),
                            Month07 = p.Month07.ToString(),
                            Month08 = p.Month08.ToString(),
                            Month09 = p.Month09.ToString(),
                            Month10 = p.Month10.ToString(),
                            Month11 = p.Month11.ToString(),
                            Month12 = p.Month12.ToString()
                        }).ToList();

            return list;
        }
        #endregion


        #endregion
    }
}