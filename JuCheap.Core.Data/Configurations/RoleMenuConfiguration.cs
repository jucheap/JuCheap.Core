using JuCheap.Core.Data.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JuCheap.Core.Data.Configurations
{
    /// <summary>
    /// RoleMenu表信息配置
    /// </summary>
    public class RoleMenuConfiguration : BaseConfiguration<RoleMenuEntity>
    {
        public override void Configure(EntityTypeBuilder<RoleMenuEntity> builder)
        {
            base.Configure(builder);
            builder.ToTable("RoleMenus");
            builder.Property(e => e.RoleId).HasMaxLength(36).IsRequired();
            builder.Property(e => e.MenuId).HasMaxLength(36).IsRequired();
        }
    }
}
