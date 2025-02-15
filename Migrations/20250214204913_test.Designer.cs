﻿// <auto-generated />
using System;
using EmployeeManagementSystem.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace EmployeeManagementSystem.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20250214204913_test")]
    partial class test
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("EmployeeManagementSystem.Entities.Department", b =>
                {
                    b.Property<int>("DepartmentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DepartmentId"));

                    b.Property<string>("DepartmentName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("DepartmentId");

                    b.ToTable("Departments");
                });

            modelBuilder.Entity("EmployeeManagementSystem.Entities.Employee", b =>
                {
                    b.Property<string>("EmployeeNumber")
                        .HasMaxLength(6)
                        .HasColumnType("nvarchar(6)");

                    b.Property<int>("DepartmentId")
                        .HasColumnType("int");

                    b.Property<string>("EmployeeName")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("GenderCode")
                        .HasMaxLength(1)
                        .HasColumnType("nvarchar(1)");

                    b.Property<int>("PositionId")
                        .HasColumnType("int");

                    b.Property<string>("ReportedToEmployeeNumber")
                        .HasColumnType("nvarchar(6)");

                    b.Property<decimal>("Salary")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("VacationDaysLeft")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(24);

                    b.HasKey("EmployeeNumber");

                    b.HasIndex("DepartmentId");

                    b.HasIndex("PositionId");

                    b.HasIndex("ReportedToEmployeeNumber");

                    b.ToTable("Employees", t =>
                        {
                            t.HasCheckConstraint("CK_Employee_VacationDaysLeft", "[VacationDaysLeft] <= 24");
                        });
                });

            modelBuilder.Entity("EmployeeManagementSystem.Entities.Position", b =>
                {
                    b.Property<int>("PositionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PositionId"));

                    b.Property<string>("PositionName")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.HasKey("PositionId");

                    b.ToTable("Positions");
                });

            modelBuilder.Entity("EmployeeManagementSystem.Entities.RequestState", b =>
                {
                    b.Property<int>("StateId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("StateId"));

                    b.Property<string>("StateName")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.HasKey("StateId");

                    b.ToTable("RequestStates");
                });

            modelBuilder.Entity("EmployeeManagementSystem.Entities.VacationRequest", b =>
                {
                    b.Property<int>("RequestId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RequestId"));

                    b.Property<string>("ApprovedByEmployeeNumber")
                        .HasColumnType("nvarchar(6)");

                    b.Property<string>("DeclinedByEmployeeNumber")
                        .HasColumnType("nvarchar(6)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("EmployeeNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(6)");

                    b.Property<string>("EmployeeNumber1")
                        .HasColumnType("nvarchar(6)");

                    b.Property<DateOnly>("EndDate")
                        .HasColumnType("date");

                    b.Property<int>("RequestStateId")
                        .HasColumnType("int");

                    b.Property<DateOnly>("StartDate")
                        .HasColumnType("date");

                    b.Property<DateTime>("SubmissionDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("TotalVacationDays")
                        .HasColumnType("int");

                    b.Property<string>("VacationTypeCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(1)");

                    b.HasKey("RequestId");

                    b.HasIndex("ApprovedByEmployeeNumber");

                    b.HasIndex("DeclinedByEmployeeNumber");

                    b.HasIndex("EmployeeNumber");

                    b.HasIndex("EmployeeNumber1");

                    b.HasIndex("RequestStateId");

                    b.HasIndex("VacationTypeCode");

                    b.ToTable("VacationRequests");
                });

            modelBuilder.Entity("EmployeeManagementSystem.Entities.VacationType", b =>
                {
                    b.Property<string>("VacationTypeCode")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(1)
                        .HasColumnType("nvarchar(1)");

                    b.Property<string>("VacationTypeName")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("VacationTypeCode");

                    b.ToTable("VacationTypes");
                });

            modelBuilder.Entity("EmployeeManagementSystem.Entities.Employee", b =>
                {
                    b.HasOne("EmployeeManagementSystem.Entities.Department", "Department")
                        .WithMany("Employees")
                        .HasForeignKey("DepartmentId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("EmployeeManagementSystem.Entities.Position", "Position")
                        .WithMany("Employees")
                        .HasForeignKey("PositionId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("EmployeeManagementSystem.Entities.Employee", "ReportedToEmployee")
                        .WithMany("Subordinates")
                        .HasForeignKey("ReportedToEmployeeNumber")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("Department");

                    b.Navigation("Position");

                    b.Navigation("ReportedToEmployee");
                });

            modelBuilder.Entity("EmployeeManagementSystem.Entities.VacationRequest", b =>
                {
                    b.HasOne("EmployeeManagementSystem.Entities.Employee", "ApprovedByEmployee")
                        .WithMany()
                        .HasForeignKey("ApprovedByEmployeeNumber")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("EmployeeManagementSystem.Entities.Employee", "DeclinedByEmployee")
                        .WithMany()
                        .HasForeignKey("DeclinedByEmployeeNumber")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("EmployeeManagementSystem.Entities.Employee", "Employee")
                        .WithMany("VacationRequests")
                        .HasForeignKey("EmployeeNumber")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EmployeeManagementSystem.Entities.Employee", null)
                        .WithMany("ApprovedVacationRequests")
                        .HasForeignKey("EmployeeNumber1");

                    b.HasOne("EmployeeManagementSystem.Entities.RequestState", "RequestState")
                        .WithMany("VacationRequests")
                        .HasForeignKey("RequestStateId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("EmployeeManagementSystem.Entities.VacationType", "VacationType")
                        .WithMany("VacationRequests")
                        .HasForeignKey("VacationTypeCode")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("ApprovedByEmployee");

                    b.Navigation("DeclinedByEmployee");

                    b.Navigation("Employee");

                    b.Navigation("RequestState");

                    b.Navigation("VacationType");
                });

            modelBuilder.Entity("EmployeeManagementSystem.Entities.Department", b =>
                {
                    b.Navigation("Employees");
                });

            modelBuilder.Entity("EmployeeManagementSystem.Entities.Employee", b =>
                {
                    b.Navigation("ApprovedVacationRequests");

                    b.Navigation("Subordinates");

                    b.Navigation("VacationRequests");
                });

            modelBuilder.Entity("EmployeeManagementSystem.Entities.Position", b =>
                {
                    b.Navigation("Employees");
                });

            modelBuilder.Entity("EmployeeManagementSystem.Entities.RequestState", b =>
                {
                    b.Navigation("VacationRequests");
                });

            modelBuilder.Entity("EmployeeManagementSystem.Entities.VacationType", b =>
                {
                    b.Navigation("VacationRequests");
                });
#pragma warning restore 612, 618
        }
    }
}
