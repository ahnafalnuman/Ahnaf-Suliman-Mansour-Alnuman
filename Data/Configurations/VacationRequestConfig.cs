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
    public class VacationRequestConfig : IEntityTypeConfiguration<VacationRequest>
    {
        public void Configure(EntityTypeBuilder<VacationRequest> builder)
        {
            builder.HasKey(vr => vr.RequestId);
            builder.Property(vr => vr.RequestId).UseIdentityColumn();

            builder.Property(vr => vr.Description).HasMaxLength(100).IsRequired();
            builder.Property(vr => vr.SubmissionDate).IsRequired();
            builder.Property(vr => vr.StartDate).IsRequired();
            builder.Property(vr => vr.EndDate).IsRequired();
            builder.Property(vr => vr.TotalVacationDays).IsRequired();


            builder.HasOne(vr => vr.Employee)
                .WithMany(e => e.VacationRequests)
                .HasForeignKey(vr => vr.EmployeeNumber)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(vr => vr.VacationType)
                .WithMany(vt => vt.VacationRequests)
                .HasForeignKey(vr => vr.VacationTypeCode)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(vr => vr.RequestState)
                .WithMany(rs => rs.VacationRequests)
                .HasForeignKey(vr => vr.RequestStateId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(vr => vr.ApprovedByEmployee)
                .WithMany()
                .HasForeignKey(vr => vr.ApprovedByEmployeeNumber)
                .HasPrincipalKey(e => e.EmployeeNumber)
                .IsRequired(false)  // (nullable)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(vr => vr.DeclinedByEmployee)
                .WithMany()
                .HasForeignKey(vr => vr.DeclinedByEmployeeNumber)
                .HasPrincipalKey(e => e.EmployeeNumber)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }

}