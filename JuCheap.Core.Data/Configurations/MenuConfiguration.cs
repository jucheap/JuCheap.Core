using JuCheap.Core.Data.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JuCheap.Core.Data.Configurations
{
    /// <summary>
    /// Menu表信息配置
    /// </summary>
    public class MenuConfiguration : BaseConfiguration<MenuEntity>
    {
        public override void Configure(EntityTypeBuilder<MenuEntity> builder)
        {
            base.Configure(builder);
            builder.ToTable("Menus");
            builder.Property(e => e.ParentId).HasMaxLength(36);
            builder.Property(e => e.Code).HasMaxLength(20).IsRequired();
            builder.Property(e => e.PathCode).HasMaxLength(100).IsRequired();
            builder.Property(e => e.Name).IsUnicode(true).HasMaxLength(20).IsRequired();
            builder.Property(e => e.Url).HasMaxLength(300).IsRequired();
            builder.Property(e => e.Order).IsRequired();
            builder.Property(e => e.Icon).HasMaxLength(50);
            builder.Property(e => e.Type).IsRequired();
        }
    }
}
