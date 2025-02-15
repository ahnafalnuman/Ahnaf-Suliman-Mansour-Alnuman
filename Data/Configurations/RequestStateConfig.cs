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
    public class RequestStateConfig: IEntityTypeConfiguration<RequestState>
    {
        public void Configure(EntityTypeBuilder<RequestState> builder)
        {
            builder.HasKey(rs => rs.StateId);
            builder.Property(rs => rs.StateName)
                .HasMaxLength(10)
                .IsRequired();
        }
    }
}
