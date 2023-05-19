using IK_Project.Domain.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IK_Project.Infrastructure.EntitityMapping
{
    public class AppRoleMapping : BaseEntityMapping<AppRole>
    {
        public override void Configure(EntityTypeBuilder<AppRole> builder)
        {

            builder.HasKey(x => x.Id);
            //builder.Property(x => x.CreatedBy).HasDefaultValue("Admin");
            //builder.Property(x => x.CreatedDate).HasDefaultValue(DateTime.Now);
            //builder.Property(x => x.ModifiedDate).HasDefaultValue(DateTime.Now);
            builder.Property(x => x.Status).HasDefaultValue(Domain.Enums.Status.Active);
            //builder.HasData(
            //new AppRole()
            //{ 
            //    Id = Guid.NewGuid(), 
            //    Name = "Admin" 
            //}, 

            //new AppRole()
            //{
            //    Id = Guid.NewGuid(),
            //    Name = "CompanyManager"
            //},

            //new AppRole()
            //{
            //    Id = Guid.NewGuid(),
            //    Name = "Employee"
            //});
            //base.Configure(builder);  
            builder.HasData(new AppRole() { Id = Guid.NewGuid(), Name = "Admin", Status = Domain.Enums.Status.Active, CreatedBy = "Admin", CreatedDate = DateTime.Now, ModifiedBy = null, ModifiedDate = null, ConcurrencyStamp = null, NormalizedName = "ADMIN" },
                 new AppRole() { Id = Guid.NewGuid(), Name = "CompanyManager", Status = Domain.Enums.Status.Active, CreatedBy = "Admin", CreatedDate = DateTime.Now, ModifiedBy = null, ModifiedDate = null, ConcurrencyStamp = null, NormalizedName = "COMPANYMANAGER" },
                   new AppRole() { Id = Guid.NewGuid(), Name = "Employee", Status = Domain.Enums.Status.Active, CreatedBy = "Admin", CreatedDate = DateTime.Now, ModifiedBy = null, ModifiedDate = null, ConcurrencyStamp = null, NormalizedName = "EMPLOYEE" });
        }
    }
}
