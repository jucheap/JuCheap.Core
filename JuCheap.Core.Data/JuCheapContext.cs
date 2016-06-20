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
    //[DbConfigurationType(typeof(MySql.Data.Entity.MySqlEFConfiguration))]
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

        }

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
        public DbSet<MenuEntity> Menu { get; set; }

        /// <summary>
        /// 用户角色关系
        /// </summary>
        public DbSet<UserRoleEntity> UserRole { get; set; }

        /// <summary>
        /// 角色菜单关系
        /// </summary>
        public DbSet<RoleMenuEntity> RoleMenu { get; set; }

        /// <summary>
        /// 路径码
        /// </summary>
        public DbSet<PathCodeEntity> PathCodes { get; set; }

        /// <summary>
        /// 页面访问记录
        /// </summary>
        public DbSet<PageViewEntity> PageView { get; set; }

        /// <summary>
        /// 登录日志
        /// </summary>
        public DbSet<LoginLogEntity> LoginLog { get; set; }
    }
}
