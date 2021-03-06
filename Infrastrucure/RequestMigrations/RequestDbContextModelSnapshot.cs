﻿// <auto-generated />
using System;
using Infrastrucure.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Infrastrucure.RequestMigrations
{
    [DbContext(typeof(RequestDbContext))]
    partial class RequestDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.2-servicing-10034")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Data.Entities.Requests.AXRequestAccess", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AxRequestId")
                        .HasColumnName("AXRequest");

                    b.Property<string>("Company")
                        .IsRequired();

                    b.Property<bool>("Create");

                    b.Property<bool>("FullControl");

                    b.Property<string>("MenuItem")
                        .IsRequired();

                    b.Property<bool>("NoAccess");

                    b.Property<bool>("View");

                    b.Property<bool>("Write");

                    b.HasKey("Id");

                    b.HasIndex("AxRequestId");

                    b.ToTable("AXRequestAccesses");
                });

            modelBuilder.Entity("Data.Entities.Requests.Request", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DateAdded");

                    b.Property<string>("Department");

                    b.Property<string>("DepartmentManager")
                        .IsRequired();

                    b.Property<string>("Description")
                        .IsRequired();

                    b.Property<string>("Director");

                    b.Property<bool?>("DirectorSigned");

                    b.Property<DateTime?>("DirectorSignedDate");

                    b.Property<string>("Discriminator")
                        .IsRequired();

                    b.Property<bool>("ManagerSigned");

                    b.Property<DateTime?>("ManagerSignedDate");

                    b.Property<string>("RequestedBy")
                        .IsRequired();

                    b.Property<bool>("Signed");

                    b.Property<string>("Title")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Requests");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Request");
                });

            modelBuilder.Entity("Data.Entities.Requests.RequestItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("EmployeeRequestId");

                    b.Property<int?>("ITRequestId");

                    b.Property<int>("RequestItemType");

                    b.HasKey("Id");

                    b.HasIndex("EmployeeRequestId");

                    b.HasIndex("ITRequestId");

                    b.ToTable("RequestItems");
                });

            modelBuilder.Entity("Data.Entities.Requests.AXRequest", b =>
                {
                    b.HasBaseType("Data.Entities.Requests.Request");

                    b.Property<bool>("NeedsInstall");

                    b.HasDiscriminator().HasValue("AXRequest");
                });

            modelBuilder.Entity("Data.Entities.Requests.EmployeeRequest", b =>
                {
                    b.HasBaseType("Data.Entities.Requests.Request");

                    b.Property<DateTime?>("DateRequestCompletion");

                    b.Property<int>("EmployeeRequestType");

                    b.Property<string>("Office");

                    b.HasDiscriminator().HasValue("EmployeeRequest");
                });

            modelBuilder.Entity("Data.Entities.Requests.ITRequest", b =>
                {
                    b.HasBaseType("Data.Entities.Requests.Request");

                    b.HasDiscriminator().HasValue("ITRequest");
                });

            modelBuilder.Entity("Data.Entities.Requests.AXRequestAccess", b =>
                {
                    b.HasOne("Data.Entities.Requests.AXRequest", "AxRequest")
                        .WithMany("AxRequestAccesses")
                        .HasForeignKey("AxRequestId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Data.Entities.Requests.RequestItem", b =>
                {
                    b.HasOne("Data.Entities.Requests.EmployeeRequest")
                        .WithMany("RequestItems")
                        .HasForeignKey("EmployeeRequestId");

                    b.HasOne("Data.Entities.Requests.ITRequest")
                        .WithMany("RequestItems")
                        .HasForeignKey("ITRequestId");
                });
#pragma warning restore 612, 618
        }
    }
}
