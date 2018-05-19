using JuCheap.Core.Data.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JuCheap.Core.Data.Configurations
{
    /// <summary>
    /// Role表信息配置
    /// </summary>
    public class RoleConfiguration : BaseConfiguration<RoleEntity>
    {
        public override void Configure(EntityTypeBuilder<RoleEntity> builder)
        {
            base.Configure(builder);
            builder.Property(e => e.Name).IsRequired().IsUnicode(true).HasMaxLength(20);
            builder.Property(e => e.Description).IsRequired().IsUnicode(true).HasMaxLength(50);
            builder.ToTable("Roles");
        }
    }
}
