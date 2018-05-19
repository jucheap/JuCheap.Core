using JuCheap.Core.Data.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JuCheap.Core.Data.Configurations
{
    /// <summary>
    /// Area表信息配置
    /// </summary>
    public class AreaConfiguration : BaseConfiguration<AreaEntity>
    {
        public override void Configure(EntityTypeBuilder<AreaEntity> builder)
        {
            base.Configure(builder);

            builder.ToTable("Areas");
            builder.Property(x => x.Name).IsRequired().IsUnicode(true).HasMaxLength(50);
            builder.Property(x => x.ParentId).HasMaxLength(36);
            builder.Property(x => x.FullSpelling).HasMaxLength(100);
            builder.Property(x => x.SimpleSpelling).HasMaxLength(20);
            builder.Property(x => x.PathCode).HasMaxLength(20);
            builder.Property(x => x.Enabled).IsRequired();
        }
    }
}
