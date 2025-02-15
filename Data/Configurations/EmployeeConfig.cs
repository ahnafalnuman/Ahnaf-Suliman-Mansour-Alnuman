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
    public class EmployeeConfig : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.HasKey(e => e.EmployeeNumber);
            builder.Property(e => e.EmployeeNumber).HasMaxLength(6).IsRequired().ValueGeneratedNever();
            builder.Property(e => e.EmployeeName).HasMaxLength(20).IsRequired();
            builder.Property(e => e.GenderCode).HasMaxLength(1);
            builder.Property(e => e.VacationDaysLeft).IsRequired().HasDefaultValue(24); ;
            builder.ToTable(t => t.HasCheckConstraint("CK_Employee_VacationDaysLeft", "[VacationDaysLeft] <= 24")); //check constraint that ensures the value of VacationDaysLeft is less than or equal to 24
            builder.Property(e => e.Salary).HasColumnType("decimal(18,2)");

            // Relationships
            builder.HasOne(e => e.Department)  // Department 1 : M Employee
                   .WithMany(d => d.Employees)
                   .HasForeignKey(e => e.DepartmentId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(e => e.Position)  // Position 1 : M Employee
                .WithMany(p => p.Employees)
                .HasForeignKey(e => e.PositionId)
                .OnDelete(DeleteBehavior.Restrict);

            // Employee 1 : M Employee (self referencing )
            builder.HasOne(e => e.ReportedToEmployee) // An employee has one reporting manager   
                   .WithMany(e => e.Subordinates)   //A manager can have many subordinates
                   .HasForeignKey(e => e.ReportedToEmployeeNumber)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
