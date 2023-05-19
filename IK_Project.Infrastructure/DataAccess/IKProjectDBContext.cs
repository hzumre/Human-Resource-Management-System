using IK_Project.Domain.Entities.Abstract;
using IK_Project.Domain.Entities.Concrete;
using IK_Project.Infrastructure.EntitityMapping;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IK_Project.Infrastructure.DataAccess
{
    public class IKProjectDBContext : IdentityDbContext<AppUser, AppRole, Guid>
    {
        public IKProjectDBContext(DbContextOptions options) : base(options) { }



        public DbSet<AppUser> AppUser { get; set; }
        public DbSet<AppRole> Roles { get; set; }
        public DbSet<CompanyManager> CompanyManagers { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<Expense> Expenses { get; set; }

        public DbSet<Permission> Permissions { get; set; }
        public DbSet<Advance> Advances { get; set; }


        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entity = ChangeTracker.Entries<IBaseEntity>();
            foreach (var item in entity)
            {
                if (item.State == EntityState.Added)
                {
                    item.Entity.CreatedDate = DateTime.Now;
                    item.Entity.CreatedBy = "Admin";
                    //item.Entity.ModifiedBy = "Admin";
                    //item.Entity.ModifiedDate = DateTime.Now;
                    item.Entity.Status = Domain.Enums.Status.Active;
                }
                else if (item.State == EntityState.Modified || item.State == EntityState.Deleted)
                {
                    item.Entity.ModifiedDate = DateTime.Now;
                    item.Entity.ModifiedBy = "Admin";
                }
            }


            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {


            builder.ApplyConfiguration(new AppUserMapping());
            builder.ApplyConfiguration(new CompanyMapping());
            builder.ApplyConfiguration(new AdminMapping());
            builder.ApplyConfiguration(new CompanyManagerMapping());
            builder.ApplyConfiguration(new AppRoleMapping());
            builder.ApplyConfiguration(new EmployeeMapping());
            builder.ApplyConfiguration(new MenuMapping());
            base.OnModelCreating(builder);

            base.OnModelCreating(builder);
        }
    }

    


}
