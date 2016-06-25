using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using JuCheap.Core.Data;

namespace JuCheap.Core.Web.Migrations
{
    [DbContext(typeof(JuCheapContext))]
    [Migration("20160625051306_JuCheapCore001")]
    partial class JuCheapCore001
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.0-rc2-20901")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("JuCheap.Core.Data.Entity.LoginLogEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreateDateTime");

                    b.Property<string>("IP")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 20);

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("LoginName")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 20);

                    b.Property<string>("Mac")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 200);

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("LoginLogs");
                });

            modelBuilder.Entity("JuCheap.Core.Data.Entity.MenuEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 6);

                    b.Property<DateTime>("CreateDateTime");

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 20);

                    b.Property<int>("Order");

                    b.Property<int>("ParentId");

                    b.Property<string>("PathCode")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 20);

                    b.Property<byte>("Type");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 300);

                    b.HasKey("Id");

                    b.ToTable("Menus");
                });

            modelBuilder.Entity("JuCheap.Core.Data.Entity.PageViewEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreateDateTime");

                    b.Property<string>("IP")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 20);

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("LoginName")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 20);

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 300);

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 20);

                    b.HasKey("Id");

                    b.ToTable("PageViews");
                });

            modelBuilder.Entity("JuCheap.Core.Data.Entity.PathCodeEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 4);

                    b.Property<DateTime>("CreateDateTime");

                    b.Property<bool>("IsDeleted");

                    b.Property<int>("Len");

                    b.HasKey("Id");

                    b.ToTable("PathCodes");
                });

            modelBuilder.Entity("JuCheap.Core.Data.Entity.RoleEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreateDateTime");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 50);

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 20);

                    b.HasKey("Id");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("JuCheap.Core.Data.Entity.RoleMenuEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreateDateTime");

                    b.Property<bool>("IsDeleted");

                    b.Property<int>("MenuId");

                    b.Property<int>("RoleId");

                    b.HasKey("Id");

                    b.HasIndex("MenuId");

                    b.HasIndex("RoleId");

                    b.ToTable("RoleMenus");
                });

            modelBuilder.Entity("JuCheap.Core.Data.Entity.SystemConfigEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreateDateTime");

                    b.Property<DateTime>("DataInitedDate");

                    b.Property<bool>("IsDataInited");

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("SystemName")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 50);

                    b.HasKey("Id");

                    b.ToTable("SystemConfigs");
                });

            modelBuilder.Entity("JuCheap.Core.Data.Entity.UserEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreateDateTime");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 36);

                    b.Property<bool>("IsDeleted");

                    b.Property<bool>("IsSuperMan");

                    b.Property<string>("LoginName")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 20);

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 50);

                    b.Property<string>("RealName")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 20);

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("JuCheap.Core.Data.Entity.UserRoleEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreateDateTime");

                    b.Property<bool>("IsDeleted");

                    b.Property<int>("RoleId");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.HasIndex("UserId");

                    b.ToTable("UserRoles");
                });

            modelBuilder.Entity("JuCheap.Core.Data.Entity.RoleMenuEntity", b =>
                {
                    b.HasOne("JuCheap.Core.Data.Entity.MenuEntity")
                        .WithMany()
                        .HasForeignKey("MenuId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("JuCheap.Core.Data.Entity.RoleEntity")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("JuCheap.Core.Data.Entity.UserRoleEntity", b =>
                {
                    b.HasOne("JuCheap.Core.Data.Entity.RoleEntity")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("JuCheap.Core.Data.Entity.UserEntity")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
