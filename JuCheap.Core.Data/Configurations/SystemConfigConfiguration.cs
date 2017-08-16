using JuCheap.Core.Data.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JuCheap.Core.Data.Configurations
{
    /// <summary>
    /// SystemConfig表信息配置
    /// </summary>
    public class SystemConfigConfiguration : IEntityTypeConfiguration<SystemConfigEntity>
    {
        public void Configure(EntityTypeBuilder<SystemConfigEntity> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).ValueGeneratedNever();
            builder.Property(e => e.SystemName).HasMaxLength(50).IsRequired();
            builder.Property(e => e.IsDataInited).IsRequired();
            builder.Property(e => e.DataInitedDate).IsRequired();
            builder.Property(e => e.CreateDateTime).IsRequired();
            builder.Property(e => e.IsDeleted).IsRequired();
            builder.ToTable("SystemConfigs");
        }
    }
}
