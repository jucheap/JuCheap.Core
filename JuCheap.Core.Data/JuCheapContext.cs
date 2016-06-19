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

        public DbSet<UserEntity> Users { get; set; }

        public DbSet<RoleEntity> Roles { get; set; }

        public DbSet<MenuEntity> Menu { get; set; }

        public DbSet<UserRoleEntity> UserRole { get; set; }

        public DbSet<RoleMenuEntity> RoleMenu { get; set; }

        public DbSet<PathCodeEntity> PathCodes { get; set; }

        public DbSet<PageViewEntity> PageView { get; set; }

        public DbSet<LoginLogEntity> LoginLog { get; set; }
    }
}
