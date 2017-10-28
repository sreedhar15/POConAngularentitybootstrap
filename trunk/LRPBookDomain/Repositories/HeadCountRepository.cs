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
    public class HeadCountRepository : BaseRepository
    {
        #region Constructors
        public HeadCountRepository(int userID) : base(userID)
        {

        }
        #endregion

        #region Public Methods   

        #region Create Methods
        /// <summary>
        /// Create HeadCount and at least one HeadCount sign in.
        /// </summary>
        /// <param name="HeadCount"></param>
        /// <returns></returns>
        public HeadCount CreateHeadCount(HeadCount HeadCount)
        {
            ctx.HeadCount.Add(HeadCount);
            ctx.SaveChanges();
            return HeadCount;
        }


        #endregion

        #region Delete Methods
        public bool DeleteHeadCount(int id)
        {
            var HeadCount = ctx.HeadCount.Where(x => x.ID == id).First();
            ctx.HeadCount.Remove(HeadCount);
            if (ctx.SaveChanges() > 0)
            {
                return true;
            }
            return false;
        }
        #endregion

        #region Update Methods
        public HeadCount UpdateHeadCount(HeadCount headCount)
        {
            var c = ctx.HeadCount.Where(x => x.ID == headCount.ID).First();

            if (c != null)
            {
                c.EmployeeRoleID = headCount.EmployeeRoleID;
                c.EmployeeRoleTypeID = headCount.EmployeeRoleTypeID;
                c.CountryID = headCount.CountryID;
                c.Comment = headCount.Comment;
                c.Year = headCount.Year;
                c.Month01 = headCount.Month01;
                c.Month02 = headCount.Month02;
                c.Month03 = headCount.Month03;
                c.Month04 = headCount.Month04;
                c.Month05 = headCount.Month05;
                c.Month06 = headCount.Month06;
                c.Month07 = headCount.Month07;
                c.Month08 = headCount.Month08;
                c.Month09 = headCount.Month09;
                c.Month08 = headCount.Month10;
                c.Month10 = headCount.Month11;
                c.Month11 = headCount.Month12;
                ctx.SaveChanges();
            }
            return c;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="HeadCount"></param>
        /// <returns></returns>
        public int UpdateHeadCount(List<HeadCountDTO> target, int typeID, int year, int projectID)
        {
            var source = GetHeadCountByType(typeID, year, projectID);

            EmployeeRoleRepository employeeRepository = new EmployeeRoleRepository(BaseRepository.SystemUserID);
            CountryRepository countryRepository = new CountryRepository(BaseRepository.SystemUserID);

            // find and insert new items.
            var newItems = target.Except(source, new HeadCountDTOComparer());
            foreach (HeadCountDTO headCountDTO in newItems)
            {
                HeadCount HeadCount = new HeadCount();

                HeadCount.ProjectID = projectID;
                HeadCount.EmployeeRoleTypeID = typeID;
                HeadCount.Year = year;
                
                HeadCount.EmployeeRoleID = employeeRepository.GetEmployeeRole(headCountDTO.EmployeeRole).ID;
                HeadCount.CountryID = countryRepository.GetCountry(headCountDTO.Country).ID;

                HeadCount.Incremental = DataUtility.GetBooleanString(headCountDTO.Incremental);

                HeadCount.Comment = headCountDTO.Comment;
                HeadCount.Month01 = DataUtility.GetDecimalValue(headCountDTO.Month01);
                HeadCount.Month02 = DataUtility.GetDecimalValue(headCountDTO.Month02);
                HeadCount.Month03 = DataUtility.GetDecimalValue(headCountDTO.Month03);
                HeadCount.Month04 = DataUtility.GetDecimalValue(headCountDTO.Month04);
                HeadCount.Month05 = DataUtility.GetDecimalValue(headCountDTO.Month05);
                HeadCount.Month06 = DataUtility.GetDecimalValue(headCountDTO.Month06);
                HeadCount.Month07 = DataUtility.GetDecimalValue(headCountDTO.Month07);
                HeadCount.Month08 = DataUtility.GetDecimalValue(headCountDTO.Month08);
                HeadCount.Month09 = DataUtility.GetDecimalValue(headCountDTO.Month09);
                HeadCount.Month10 = DataUtility.GetDecimalValue(headCountDTO.Month10);
                HeadCount.Month11 = DataUtility.GetDecimalValue(headCountDTO.Month11);
                HeadCount.Month12 = DataUtility.GetDecimalValue(headCountDTO.Month12);

                CreateHeadCount(HeadCount);
            }


            // find and insert new items.
            var updateItems = target.Union(source, new HeadCountDTOComparer());
            foreach (HeadCountDTO headCountDTO in updateItems)
            {
                
                int employeeRoleID = employeeRepository.GetEmployeeRole(headCountDTO.EmployeeRole).ID;

                HeadCount HeadCount = GetHeadCount(typeID, year, projectID, employeeRoleID);

                HeadCount.EmployeeRoleTypeID = typeID;
                HeadCount.Year = year;
                HeadCount.ProjectID = projectID;

                HeadCount.EmployeeRoleID = employeeRoleID;
                HeadCount.CountryID = countryRepository.GetCountry(headCountDTO.Country).ID;

                HeadCount.Incremental = DataUtility.GetBooleanString(headCountDTO.Incremental);

                HeadCount.Comment = headCountDTO.Comment;
                HeadCount.Month01 = DataUtility.GetDecimalValue(headCountDTO.Month01);
                HeadCount.Month02 = DataUtility.GetDecimalValue(headCountDTO.Month02);
                HeadCount.Month03 = DataUtility.GetDecimalValue(headCountDTO.Month03);
                HeadCount.Month04 = DataUtility.GetDecimalValue(headCountDTO.Month04);
                HeadCount.Month05 = DataUtility.GetDecimalValue(headCountDTO.Month05);
                HeadCount.Month06 = DataUtility.GetDecimalValue(headCountDTO.Month06);
                HeadCount.Month07 = DataUtility.GetDecimalValue(headCountDTO.Month07);
                HeadCount.Month08 = DataUtility.GetDecimalValue(headCountDTO.Month08);
                HeadCount.Month09 = DataUtility.GetDecimalValue(headCountDTO.Month09);
                HeadCount.Month10 = DataUtility.GetDecimalValue(headCountDTO.Month10);
                HeadCount.Month11 = DataUtility.GetDecimalValue(headCountDTO.Month11);
                HeadCount.Month12 = DataUtility.GetDecimalValue(headCountDTO.Month12);

                UpdateHeadCount(HeadCount);
            }


            // find and delete items
            var deleteItems = source.Except(target, new HeadCountDTOComparer());
            foreach (HeadCountDTO HeadCountDTO in deleteItems)
            {
                int employeeRoleID = employeeRepository.GetEmployeeRole(HeadCountDTO.EmployeeRole).ID;

                HeadCount HeadCount = GetHeadCount(typeID, year, projectID, employeeRoleID);

                DeleteHeadCount(HeadCount.ID);
            }

            ctx.SaveChanges();

            return 0;

        }
        #endregion

        #region Get Methods
        public HeadCount GetHeadCount(int id)
        {
            return ctx.HeadCount.Where(x => x.ID == id).First();
        }

        public HeadCount GetHeadCount(int typeID, int year, int projectID, int employeeRoleID)
        {
            return ctx.HeadCount.Where(x => x.EmployeeRoleTypeID==typeID && x.Year==year && x.ProjectID == projectID && x.EmployeeRoleID==employeeRoleID).First();
        }

        public List<HeadCountDTO> GetHeadCountByType(int typeID, int year, int projectID)
        {
            var list = (from p in ctx.HeadCount
                     join  er in ctx.EmployeeRole on p.EmployeeRoleID equals er.ID
                     join c in ctx.Country on p.CountryID equals c.ID
                     where p.EmployeeRoleTypeID == typeID && p.Year == year && p.ProjectID == projectID
                     orderby er.Name, c.Name
                     select new HeadCountDTO
                     {
                        EmployeeRole = er.Name,
                        Incremental =  p.Incremental ? "Yes" : "No",
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