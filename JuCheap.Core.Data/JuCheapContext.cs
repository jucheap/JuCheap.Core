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
        /// 系统配置
        /// </summary>
        public DbSet<SystemConfigEntity> SystemConfigs { get; set; } 

        #endregion
    }
}
