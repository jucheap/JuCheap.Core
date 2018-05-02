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

using JuCheap.Core.Data.Configurations;
using JuCheap.Core.Data.Entity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace JuCheap.Core.Data.Configurations
{
    /// <summary>
    /// 访问量表配置
    /// </summary>
    public class SiteViewConfiguration : BaseConfiguration<SiteViewEntity>
    {
        public override void Configure(EntityTypeBuilder<SiteViewEntity> builder)
        {
            base.Configure(builder);

            builder.ToTable("SiteViews");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasMaxLength(36);
            builder.Property(x => x.Number).IsRequired();
            builder.Property(x => x.Day).IsRequired();
        }
    }
}
