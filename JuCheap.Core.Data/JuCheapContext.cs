/*******************************************************************************
* Copyright (C) JuCheap.Com
* 
* Author: dj.wong
* Create Date: 2015/8/21
* Description: Automated building by service@jucheap.com 
* 
* Revision History:
* Date         Author               Description
*
*********************************************************************************/

using JuCheap.Core.Data.Entity;
using Microsoft.EntityFrameworkCore;

namespace JuCheap.Core.Data
{
    /// <summary>
    /// JuCheapContext
    /// </summary>
    public class JuCheapContext : DbContext
    {

        /// <summary>
        /// JuCheapContext
        /// </summary>
        public JuCheapContext(DbContextOptions<JuCheapContext> options):
            base(options)
        {
            
        }

        /// <summary>
        /// OnModelCreating
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<MenuEntity>(m =>
            {
                m.HasKey(e => e.Id);
                m.Property(e => e.Id).ValueGeneratedNever();
                m.Property(e => e.Code).HasMaxLength(6).IsRequired();
                m.Property(e => e.PathCode).HasMaxLength(20).IsRequired();
                m.Property(e => e.Name).HasMaxLength(20).IsRequired();
                m.Property(e => e.Url).HasMaxLength(300).IsRequired();
                m.Property(e => e.Order).IsRequired();
                m.Property(e => e.Type).IsRequired();
                m.Property(e => e.CreateDateTime).IsRequired();
                m.Property(e => e.IsDeleted).IsRequired();
                m.ToTable("Menus");
            });
            modelBuilder.Entity<RoleEntity>(m =>
            {
                m.HasKey(e => e.Id);
                m.Property(e => e.Id).ValueGeneratedNever();
                m.Property(e => e.Name).HasMaxLength(20).IsRequired();
                m.Property(e => e.Description).HasMaxLength(50).IsRequired();
                m.Property(e => e.CreateDateTime).IsRequired();
                m.Property(e => e.IsDeleted).IsRequired();
                m.ToTable("Roles");
            });
            modelBuilder.Entity<LoginLogEntity>(m =>
            {
                m.HasKey(e => e.Id);
                m.Property(e => e.Id).ValueGeneratedNever();
                m.Property(e => e.LoginName).HasMaxLength(20).IsRequired();
                m.Property(e => e.IP).HasMaxLength(20).IsRequired();
                m.Property(e => e.Mac).HasMaxLength(200).IsRequired();
                m.Property(e => e.UserId).IsRequired();
                m.Property(e => e.CreateDateTime).IsRequired();
                m.Property(e => e.IsDeleted).IsRequired();
                m.ToTable("LoginLogs");
            });
            modelBuilder.Entity<PageViewEntity>(m =>
            {
                m.HasKey(e => e.Id);
                m.Property(e => e.Id).ValueGeneratedNever();
                m.Property(e => e.LoginName).HasMaxLength(20).IsRequired();
                m.Property(e => e.IP).HasMaxLength(20).IsRequired();
                m.Property(e => e.Url).HasMaxLength(300).IsRequired();
                m.Property(e => e.UserId).HasMaxLength(20).IsRequired();
                m.Property(e => e.CreateDateTime).IsRequired();
                m.Property(e => e.IsDeleted).IsRequired();
                m.ToTable("PageViews");
            });
            modelBuilder.Entity<PathCodeEntity>(m =>
            {
                m.HasKey(e => e.Id);
                m.Property(e => e.Id).ValueGeneratedNever();
                m.Property(e => e.Code).HasMaxLength(4).IsRequired();
                m.Property(e => e.Len).IsRequired();
                m.Property(e => e.CreateDateTime).IsRequired();
                m.Property(e => e.IsDeleted).IsRequired();
                m.ToTable("PathCodes");
            });
            modelBuilder.Entity<RoleMenuEntity>(m =>
            {
                m.HasKey(e => e.Id);
                m.Property(e => e.Id).ValueGeneratedNever();
                m.Property(e => e.RoleId).IsRequired();
                m.Property(e => e.MenuId).IsRequired();
                m.Property(e => e.CreateDateTime).IsRequired();
                m.Property(e => e.IsDeleted).IsRequired();
                m.ToTable("RoleMenus");
            });
            modelBuilder.Entity<UserRoleEntity>(m =>
            {
                m.HasKey(e => e.Id);
                m.Property(e => e.Id).ValueGeneratedNever();
                m.Property(e => e.RoleId).IsRequired();
                m.Property(e => e.UserId).IsRequired();
                m.Property(e => e.CreateDateTime).IsRequired();
                m.Property(e => e.IsDeleted).IsRequired();
                m.ToTable("UserRoles");
            });
            modelBuilder.Entity<UserEntity>(m =>
            {
                m.HasKey(e => e.Id);
                m.Property(e => e.Id).ValueGeneratedNever();
                m.Property(e => e.LoginName).HasMaxLength(20).IsRequired();
                m.Property(e => e.RealName).HasMaxLength(20).IsRequired();
                m.Property(e => e.Email).HasMaxLength(36).IsRequired();
                m.Property(e => e.Password).HasMaxLength(50).IsRequired();
                m.Property(e => e.IsSuperMan).IsRequired();
                m.Property(e => e.CreateDateTime).IsRequired();
                m.Property(e => e.IsDeleted).IsRequired();
                m.ToTable("Users");
            });
            modelBuilder.Entity<SystemConfigEntity>(m =>
            {
                m.HasKey(e => e.Id);
                m.Property(e => e.Id).ValueGeneratedNever();
                m.Property(e => e.SystemName).HasMaxLength(50).IsRequired();
                m.Property(e => e.IsDataInited).IsRequired();
                m.Property(e => e.DataInitedDate).IsRequired();
                m.Property(e => e.CreateDateTime).IsRequired();
                m.Property(e => e.IsDeleted).IsRequired();
                m.ToTable("SystemConfigs");
            });
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        #region DbSets

        /// <summary>
        /// 用户
        /// </summary>
        public DbSet<UserEntity> Users { get; set; }

        /// <summary>
        /// 角色
        /// </summary>
        public DbSet<RoleEntity> Roles { get; set; }

        /// <summary>
        /// 菜单
        /// </summary>
        public DbSet<MenuEntity> Menus { get; set; }

        /// <summary>
        /// 用户角色关系
        /// </summary>
        public DbSet<UserRoleEntity> UserRoles { get; set; }

        /// <summary>
        /// 角色菜单关系
        /// </summary>
        public DbSet<RoleMenuEntity> RoleMenus { get; set; }

        /// <summary>
        /// 路径码
        /// </summary>
        public DbSet<PathCodeEntity> PathCodes { get; set; }

        /// <summary>
        /// 页面访问记录
        /// </summary>
        public DbSet<PageViewEntity> PageViews { get; set; }

        /// <summary>
        /// 登录日志
        /// </summary>
        public DbSet<LoginLogEntity> LoginLogs { get; set; }

        /// <summary>
        /// 系统配置
        /// </summary>
        public DbSet<SystemConfigEntity> SystemConfigs { get; set; } 

        #endregion
    }
}
