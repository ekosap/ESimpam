﻿// <auto-generated />
using CoreSimpam.Model.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CoreSimpam.Model.Migrations
{
    [DbContext(typeof(SimpamDBContext))]
    partial class SimpamDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.4")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("CoreSimpam.Model.CustomerModel", b =>
                {
                    b.Property<long>("CustomerID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CustomerAddress")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CustomerName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CustomerNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("ResidentID")
                        .HasColumnType("bigint");

                    b.HasKey("CustomerID");

                    b.ToTable("Customer", "Customer");
                });

            modelBuilder.Entity("CoreSimpam.Model.ResidentModel", b =>
                {
                    b.Property<long>("ResidentID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("ResidentName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ResidentNumber")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ResidentID");

                    b.ToTable("Resident", "Customer");
                });

            modelBuilder.Entity("CoreSimpam.Model.RoleModel", b =>
                {
                    b.Property<long>("RoleID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("IsEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("RoleName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("RoleID");

                    b.ToTable("Roles", "Common");

                    b.HasData(
                        new
                        {
                            RoleID = 1L,
                            IsEnabled = true,
                            RoleName = "root"
                        });
                });

            modelBuilder.Entity("CoreSimpam.Model.RoleScreenModel", b =>
                {
                    b.Property<long>("RoleScreenID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("DeleteFlag")
                        .HasColumnType("bit");

                    b.Property<bool>("ReadFlag")
                        .HasColumnType("bit");

                    b.Property<long>("RoleID")
                        .HasColumnType("bigint");

                    b.Property<long>("ScreenID")
                        .HasColumnType("bigint");

                    b.Property<bool>("WriteFlag")
                        .HasColumnType("bit");

                    b.HasKey("RoleScreenID");

                    b.HasIndex("RoleID");

                    b.HasIndex("ScreenID");

                    b.ToTable("RoleScreen", "Common");
                });

            modelBuilder.Entity("CoreSimpam.Model.ScreenModel", b =>
                {
                    b.Property<long>("ScreenID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ActionName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ControllerName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<bool>("IsMenu")
                        .HasColumnType("bit");

                    b.Property<long>("ParentID")
                        .HasColumnType("bigint");

                    b.Property<string>("ScreenName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ScreenID");

                    b.ToTable("Screen", "Common");
                });

            modelBuilder.Entity("CoreSimpam.Model.UserModel", b =>
                {
                    b.Property<long>("UserID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("RoleID")
                        .HasColumnType("bigint");

                    b.Property<string>("Salt")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserID");

                    b.HasIndex("RoleID")
                        .IsUnique();

                    b.ToTable("Users", "Common");
                });

            modelBuilder.Entity("CoreSimpam.Model.RoleScreenModel", b =>
                {
                    b.HasOne("CoreSimpam.Model.RoleModel", "Role")
                        .WithMany("RoleScreens")
                        .HasForeignKey("RoleID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CoreSimpam.Model.ScreenModel", "Screen")
                        .WithMany()
                        .HasForeignKey("ScreenID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");

                    b.Navigation("Screen");
                });

            modelBuilder.Entity("CoreSimpam.Model.UserModel", b =>
                {
                    b.HasOne("CoreSimpam.Model.RoleModel", "Role")
                        .WithOne("User")
                        .HasForeignKey("CoreSimpam.Model.UserModel", "RoleID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");
                });

            modelBuilder.Entity("CoreSimpam.Model.RoleModel", b =>
                {
                    b.Navigation("RoleScreens");

                    b.Navigation("User");
                });
#pragma warning restore 612, 618
        }
    }
}
