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
    public class UserRepository : BaseRepository
    {
        #region Constructors
        public UserRepository(int userID)
            : base(userID)
        {

        }
        #endregion

        #region Public Methods

        #region Create Methods
        /// <summary>
        /// Create User and at least one User sign in.
        /// </summary>
        /// <param name="User"></param>
        /// <returns></returns>
        public User CreateUser(User User)
        {
            ctx.User.Add(User);
            ctx.SaveChanges();
            return User;
        }

        #endregion

        #region Delete Methods
        public bool DeleteUser(int id)
        {
            var User = ctx.User.Where(x => x.ID == id).First();
            ctx.User.Remove(User);
            if (ctx.SaveChanges() > 0)
            {
                return true;
            }
            return false;
        }
        #endregion

        #region Update Methods
        public User UpdateUser(User User)
        {
            var c = ctx.User.Where(x => x.ID == User.ID).First();

            if (c != null)
            {
                c.LoginName = User.LoginName;
                ctx.SaveChanges();
            }
            return c;
        }


        #endregion

        #region Get Methods

        public User GetUser(int id)
        {
            return ctx.User.Find(id);
        }
        public List<User> GetUsers()
        {
            return ctx.User.ToList();
        }
        public User GetUser(string loginName)
        {
            //return ctx.User.Where(x => x.LoginName == loginName).First();
            return ctx.User.Find(loginName);
        }
        public List<User> GetUsers(string loginName, string firstName, string lastName)
        {
            var queryUser = (from u in ctx.User
                                              where (
                                                                                                            (u.LoginName.Contains(loginName == null ? u.LoginName : loginName)) &&
                                                      (u.FirstName.Contains(firstName == null ? u.FirstName : firstName)) &&
                                                      (u.LastName.Contains(lastName == null ? u.LastName : lastName)) 
                                                    )
                                              select u);

            List<User> usersMatching = queryUser.ToList();

            return usersMatching;
        }

        public List<User> GetUsersByFilter(string filter)
        {
            string[] splitData = filter.Split(new char[] { ',' });

            return (from p in ctx.User
                    where splitData.Any(c => p.FirstName.StartsWith(c))
                    orderby p.FirstName
                    select p).ToList();

        }


        #endregion

        #region API Methods

        //public List<User> GetUsers()
        //{
        //    return (from p in ctx.User
        //            orderby p.LoginName
        //            select p).ToList();
        //}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="UserList"></param>
        public void SaveUsers(List<User> Users, string filter)
        {
            string[] splitData = filter.Split(new char[] { ',' });
            List<User> updateUsers = new List<User>();
            List<User> newUsers = new List<User>();
            List<int> updateIds = new List<int>();
            List<User> deleteUsers = new List<User>();

            if (Users != null)
            {

                updateUsers = (from m in Users where m.ID > 0 select m).ToList();
                newUsers = (from m in Users where m.ID == 0 select m).ToList();
                updateIds = (from m in Users where splitData.Any(c => m.FirstName.StartsWith(c)) && m.ID > 0 select m.ID).ToList();
            }

            deleteUsers = (from m in ctx.User
                                   where splitData.Any(c => m.FirstName.StartsWith(c)) && !updateIds.Contains(m.ID)
                                   select m).ToList();

            //Delete User
            foreach (User User in deleteUsers)
            {
                ctx.User.Remove(User);
            }


            //Update Users
            foreach (User User in updateUsers)
            {
                ctx.User.Attach(User);
                ctx.Entry(User).State = EntityState.Modified;
            }

            //Insert new Users
            foreach (User User in newUsers)
            {
                ctx.User.Add(User);
            }

            ctx.SaveChanges();

        }
        #endregion

       
        /// <summary>
        /// 
        /// </summary>
        /// <param name="loginName"></param>
        /// <param name="accessLevel"></param>
        /// <returns></returns>
        public int GetUserIDInRole(string loginName, string accessLevel)
        {
           var p = (from u in ctx.User
                     join usr in ctx.UserSecurityRole on u.ID equals usr.UserID
                     join sr in ctx.SecurityRole on usr.SecurityRoleID equals sr.ID
                     where u.LoginName.ToLower() == loginName.ToLower() &&
                     sr.Name.ToLower() == accessLevel.ToLower()
                     select u.ID).FirstOrDefault();

           if (p != null)
           {
               return (int)p;
           }
           else return 0;
        }

        #endregion

    }
}