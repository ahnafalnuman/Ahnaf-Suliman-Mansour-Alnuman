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
    public class VacationTypeConfig : IEntityTypeConfiguration<VacationType>
    {
        public void Configure(EntityTypeBuilder<VacationType> builder)
        {
            builder.HasKey(vt => vt.VacationTypeCode);

            builder.Property(vt => vt.VacationTypeCode)
                .HasMaxLength(1)
                .IsRequired();

            builder.Property(vt => vt.VacationTypeName)
                .HasMaxLength(20)
                .IsRequired();
        }
    }
}
