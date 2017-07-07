using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using JuCheap.Core.Data;

namespace JuCheap.Core.Web.Migrations
{
    [DbContext(typeof(JuCheapContext))]
    partial class JuCheapContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("JuCheap.Core.Data.Entity.AppEntity", b =>
                {
                    b.Property<Guid>("Id");

                    b.Property<string>("ClientId")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("ClientName")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("ClientUri")
                        .IsRequired()
                        .HasMaxLength(256);

                    b.Property<DateTime>("CreateDateTime");

                    b.Property<bool>("Enabled");

                    b.Property<bool>("IsDeleted");

                    b.Property<Guid>("UserId");

                    b.HasKey("Id");

                    b.ToTable("Apps");
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
        }
    }
}
