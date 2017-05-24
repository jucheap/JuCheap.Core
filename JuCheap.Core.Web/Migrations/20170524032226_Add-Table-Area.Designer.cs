using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using JuCheap.Core.Data;

namespace JuCheap.Core.Web.Migrations
{
    [DbContext(typeof(JuCheapContext))]
    [Migration("20170524032226_Add-Table-Area")]
    partial class AddTableArea
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2");

            modelBuilder.Entity("JuCheap.Core.Data.Entity.AreaEntity", b =>
                {
                    b.Property<Guid>("Id");

                    b.Property<DateTime>("CreateDateTime");

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("Areas");
                });

            modelBuilder.Entity("JuCheap.Core.Data.Entity.LoginLogEntity", b =>
                {
                    b.Property<Guid>("Id");

                    b.Property<DateTime>("CreateDateTime");

                    b.Property<string>("IP")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("LoginName")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.Property<string>("Mac")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.Property<Guid>("UserId");

                    b.HasKey("Id");

                    b.ToTable("LoginLogs");
                });

            modelBuilder.Entity("JuCheap.Core.Data.Entity.MenuEntity", b =>
                {
                    b.Property<Guid>("Id");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(6);

                    b.Property<DateTime>("CreateDateTime");

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.Property<int>("Order");

                    b.Property<Guid?>("ParentId");

                    b.Property<string>("PathCode")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.Property<byte>("Type");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasMaxLength(300);

                    b.HasKey("Id");

                    b.ToTable("Menus");
                });

            modelBuilder.Entity("JuCheap.Core.Data.Entity.PageViewEntity", b =>
                {
                    b.Property<Guid>("Id");

                    b.Property<DateTime>("CreateDateTime");

                    b.Property<string>("IP")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("LoginName")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasMaxLength(300);

                    b.Property<Guid?>("UserId")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.HasKey("Id");

                    b.ToTable("PageViews");
                });

            modelBuilder.Entity("JuCheap.Core.Data.Entity.PathCodeEntity", b =>
                {
                    b.Property<Guid>("Id");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(4);

                    b.Property<DateTime>("CreateDateTime");

                    b.Property<bool>("IsDeleted");

                    b.Property<int>("Len");

                    b.HasKey("Id");

                    b.ToTable("PathCodes");
                });

            modelBuilder.Entity("JuCheap.Core.Data.Entity.RoleEntity", b =>
                {
                    b.Property<Guid>("Id");

                    b.Property<DateTime>("CreateDateTime");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.HasKey("Id");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("JuCheap.Core.Data.Entity.RoleMenuEntity", b =>
                {
                    b.Property<Guid>("Id");

                    b.Property<DateTime>("CreateDateTime");

                    b.Property<bool>("IsDeleted");

                    b.Property<Guid>("MenuId");

                    b.Property<Guid>("RoleId");

                    b.HasKey("Id");

                    b.HasIndex("MenuId");

                    b.HasIndex("RoleId");

                    b.ToTable("RoleMenus");
                });

            modelBuilder.Entity("JuCheap.Core.Data.Entity.SystemConfigEntity", b =>
                {
                    b.Property<Guid>("Id");

                    b.Property<DateTime>("CreateDateTime");

                    b.Property<DateTime>("DataInitedDate");

                    b.Property<bool>("IsDataInited");

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("SystemName")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("SystemConfigs");
                });

            modelBuilder.Entity("JuCheap.Core.Data.Entity.UserEntity", b =>
                {
                    b.Property<Guid>("Id");

                    b.Property<DateTime>("CreateDateTime");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(36);

                    b.Property<bool>("IsDeleted");

                    b.Property<bool>("IsSuperMan");

                    b.Property<string>("LoginName")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("RealName")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("JuCheap.Core.Data.Entity.UserRoleEntity", b =>
                {
                    b.Property<Guid>("Id");

                    b.Property<DateTime>("CreateDateTime");

                    b.Property<bool>("IsDeleted");

                    b.Property<Guid>("RoleId");

                    b.Property<Guid>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.HasIndex("UserId");

                    b.ToTable("UserRoles");
                });

            modelBuilder.Entity("JuCheap.Core.Data.Entity.RoleMenuEntity", b =>
                {
                    b.HasOne("JuCheap.Core.Data.Entity.MenuEntity", "Menu")
                        .WithMany("RoleMenus")
                        .HasForeignKey("MenuId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("JuCheap.Core.Data.Entity.RoleEntity", "Role")
                        .WithMany("RoleMenus")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("JuCheap.Core.Data.Entity.UserRoleEntity", b =>
                {
                    b.HasOne("JuCheap.Core.Data.Entity.RoleEntity", "Role")
                        .WithMany("UserRoles")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("JuCheap.Core.Data.Entity.UserEntity", "User")
                        .WithMany("UserRoles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
