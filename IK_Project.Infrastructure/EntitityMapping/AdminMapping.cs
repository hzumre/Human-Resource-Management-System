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
    public class AdminMapping:BaseEntityMapping<Admin>
    {
        public override void Configure(EntityTypeBuilder<Admin> builder)
        {

            
         builder.Property(x => x.ProfilePhoto).HasDefaultValue("myLogo.jpg");
           
            base.Configure(builder);  
        }
    }
}
