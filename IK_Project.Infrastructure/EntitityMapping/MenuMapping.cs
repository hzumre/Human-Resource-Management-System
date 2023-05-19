using IK_Project.Domain.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace IK_Project.Infrastructure.EntitityMapping
{
    public class MenuMapping : BaseEntityMapping<Menu>
    {
        public override void Configure(EntityTypeBuilder<Menu> builder)
        {
            builder.Property(x => x.Name).IsRequired(false).HasMaxLength(35).HasColumnType("varchar");
            builder.Property(x=>x.Period).IsRequired(false).HasColumnType("varchar");
            builder.Property(x => x.UnitPrice).IsRequired(false);
            builder.Property(x => x.DeletedDate).IsRequired(false);
            builder.Property(x => x.ReleaseDate).IsRequired(false);
            builder.Property(x => x.UserAmount).IsRequired(false);
            builder.Property(x => x.Currecy).IsRequired(false);

            builder.HasData(new Menu() { Id = 1, Name = "Basic", UnitPrice = 1000, Currecy = Domain.Enums.CurrecyUnit.TL, UserAmount = 100, Period = 3, ReleaseDate=DateTime.Now, Status = Domain.Enums.Status.Active, CreatedBy = "Admin", CreatedDate = DateTime.Now, ModifiedBy = null, ModifiedDate = null, DeletedDate = null },
                
                new Menu() { Id = 2, Name = "Standart", UnitPrice = 2000, Currecy = Domain.Enums.CurrecyUnit.TL, UserAmount = 200, Period = 3, ReleaseDate = DateTime.Now, Status = Domain.Enums.Status.Active, CreatedBy = "Admin", CreatedDate = DateTime.Now, ModifiedBy = null, ModifiedDate = null, DeletedDate = null },

                new Menu() { Id = 3, Name = "Premium", UnitPrice = 3000, Currecy = Domain.Enums.CurrecyUnit.TL, UserAmount = 300, Period = 3, ReleaseDate = DateTime.Now, Status = Domain.Enums.Status.Active, CreatedBy = "Admin", CreatedDate = DateTime.Now, ModifiedBy = null, ModifiedDate = null, DeletedDate = null });


            base.Configure(builder);
        }
    }
}
