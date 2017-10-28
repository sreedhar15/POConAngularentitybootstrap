using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Conventions;
using LRPBookDomain.Entities;

namespace LRPBookDomain.Concrete
{
    /// <summary>
    /// A class for LRPDbContext
    /// </summary>
    public class LRPDbContext : DbContext
    {
        public int ContextUserID { get; set; }
        public string ConnectionString { get; set; }
        public DbSet<Country> Country { get; set; }
        public DbSet<Currency> Currency { get; set; }
        public DbSet<EmployeeRoleType> EmployeeRoleType { get; set; }
        public DbSet<EmployeeRole> EmployeeRole { get; set; }
        public DbSet<EmployeeRoleByType> EmployeeRoleByType { get; set; }
        public DbSet<ExpenseType> ExpenseType { get; set; }
        public DbSet<DVP> DVP { get; set; }
        public DbSet<ExpenseByType> ExpenseByType { get; set; }
        public DbSet<Expense> Expense { get; set; }
        public DbSet<HeadCount> HeadCount { get; set; }
        public DbSet<ExpenseDetail> ExpenseDetail { get; set; }
        public DbSet<Project> Project { get; set; }
        public DbSet<Plan> Plan { get; set; }
        public DbSet<PlanGroup> PlanGroup { get; set; }
        public DbSet<PlanDetail> PlanDetail { get; set; }
        public DbSet<PlanDetailHeadCount> PlanDetailHeadCount { get; set; }
        public DbSet<PlanDetailExpense> PlanDetailExpense { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<SecurityRole> SecurityRole { get; set; }
        public DbSet<UserSecurityRole> UserSecurityRole { get; set; }
        public DbSet<CostCenter> CostCenter { get; set; }
        public DbSet<Franchise> Franchise { get; set; }
        public DbSet<PLLine> PLLine { get; set; }
        public DbSet<ProjectGroup> ProjectGroup { get; set; }
        public DbSet<ProjectGroupUser> ProjectGroupUser { get; set; }
        public DbSet<ProjectUser> ProjectUser { get; set; }
        public DbSet<CustomGroup> CustomGroup { get; set; }
        public DbSet<CustomProjectGroup> CustomProjectGroup { get; set; }

        public LRPDbContext()
        {
            Database.SetInitializer<LRPDbContext>(null);
            ContextUserID = 65536;
        }
        public LRPDbContext(int contextUserID)
        {
            Database.SetInitializer<LRPDbContext>(null);
            ContextUserID = contextUserID;
           
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Country>().ToTable("Country");
            modelBuilder.Entity<Currency>().ToTable("Currency");

            modelBuilder.Entity<EmployeeRoleType>().ToTable("EmployeeRoleType");
            modelBuilder.Entity<ExpenseType>().ToTable("ExpenseType");
            modelBuilder.Entity<DVP>().ToTable("DVP");

            modelBuilder.Entity<EmployeeRole>().ToTable("EmployeeRole");
            modelBuilder.Entity<Expense>().ToTable("Expense");
            modelBuilder.Entity<EmployeeRoleByType>().ToTable("EmployeeRoleByType");
            modelBuilder.Entity<ExpenseByType>().ToTable("ExpenseByType");


            modelBuilder.Entity<HeadCount>().ToTable("HeadCount");
            modelBuilder.Entity<ExpenseDetail>().ToTable("ExpenseDetail");
            modelBuilder.Entity<Project>().ToTable("Project");
            modelBuilder.Entity<Plan>().ToTable("Plan");
            modelBuilder.Entity<PlanGroup>().ToTable("PlanGroup");
            modelBuilder.Entity<PlanDetail>().ToTable("PlanDetail");
            modelBuilder.Entity<PlanDetailHeadCount>().ToTable("PlanDetailHeadCount");
            modelBuilder.Entity<PlanDetailExpense>().ToTable("PlanDetailEXpense");
            modelBuilder.Entity<CustomGroup>().ToTable("CustomGroup");
            modelBuilder.Entity<CustomProjectGroup>().ToTable("CustomProjectGroup");

            modelBuilder.Entity<ProjectGroupUser>().ToTable("ProjectGroupUser", "Admin");
            modelBuilder.Entity<ProjectUser>().ToTable("ProjectUser", "Admin");
            modelBuilder.Entity<SecurityRole>().ToTable("SecurityRole", "Admin");
            modelBuilder.Entity<User>().ToTable("User", "Admin");            
            modelBuilder.Entity<UserSecurityRole>().ToTable("UserSecurityRole", "Admin");
            modelBuilder.Entity<CostCenter>().ToTable("CostCenter");
            modelBuilder.Entity<PLLine>().ToTable("PLLine");
            modelBuilder.Entity<ProjectGroup>().ToTable("ProjectGroup");
            

            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
       }

        public override int SaveChanges()
        {
            foreach (var entry in ChangeTracker.Entries<Auditable>())
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.Created = DateTime.Now;
                    entry.Entity.CreatedBy = ContextUserID;
                }
                else if (entry.State == EntityState.Modified)
                {
                    if (entry.Property("Created") != null)
                        entry.Property("Created").IsModified = false;

                    if (entry.Property("CreatedBy") != null)
                        entry.Property("CreatedBy").IsModified = false;

                    entry.Entity.Updated = DateTime.Now;
                    entry.Entity.UpdatedBy = ContextUserID;
                }
            }
            return base.SaveChanges();
        }

    }
}
