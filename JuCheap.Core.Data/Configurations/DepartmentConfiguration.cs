/*******************************************************************************
* Copyright (C) JuCheap.Com
* 
* Author: dj.wong
* Create Date: 09/04/2015 11:47:14
* Description: Automated building by service@JuCheap.com 
* 
* Revision History:
* Date         Author               Description
*
*********************************************************************************/

using JuCheap.Core.Data.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JuCheap.Data.Config
{
    /// <summary>
    /// 部门表表配置
    /// </summary>
    public class DepartmentConfiguration : IEntityTypeConfiguration<DepartmentEntity>
    {
        public void Configure(EntityTypeBuilder<DepartmentEntity> builder)
        {
            builder.ToTable("Departments");
            builder.HasKey(item => item.Id);
            builder.Property(x => x.Id).HasMaxLength(36);
            builder.Property(item => item.Name).IsRequired().HasMaxLength(50);
            builder.Property(item => item.FullName).HasMaxLength(500);
        }
    }
}
