using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using TypeLibrary.Data.Models;

namespace TypeLibrary.Data.Configurations
{
    public class AttributeGroupAttributesConfiguration
    {
        public void Configure(EntityTypeBuilder<AttributeGroupAttributesLibDm> builder)
        {
            
            builder.ToTable("Attribute_Group_Attribute");
            builder.Property(p => p.Id).HasColumnName("Id").ValueGeneratedOnAdd().IsRequired();
            builder.HasOne(x => x.Attribute).WithMany().HasForeignKey(x => x.Attribute);                  
            builder.Property(p => p.AttributeGroup).HasColumnName("AttributeGroupId").IsRequired().HasMaxLength(63);
            builder.Property(p => p.AttributeId).HasColumnName("AttributeId").IsRequired().HasMaxLength(63);                       
        }
    }
}
