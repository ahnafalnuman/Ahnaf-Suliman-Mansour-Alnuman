using EmployeeManagementSystem.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.Data.Configurations
{
    public class PositionConfig : IEntityTypeConfiguration<Position>
    {
        public void Configure(EntityTypeBuilder<Position> builder)
        {
            builder.HasKey(d => d.PositionId);
            builder.Property(d => d.PositionId).UseIdentityColumn();
            builder.Property(d => d.PositionName).HasMaxLength(30).IsRequired();
        }

    }
}
