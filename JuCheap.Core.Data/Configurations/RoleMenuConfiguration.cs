using JuCheap.Core.Data.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JuCheap.Core.Data.Configurations
{
    /// <summary>
    /// RoleMenu表信息配置
    /// </summary>
    public class RoleMenuConfiguration : IEntityTypeConfiguration<RoleMenuEntity>
    {
        public void Configure(EntityTypeBuilder<RoleMenuEntity> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).HasMaxLength(36).ValueGeneratedNever();
            builder.Property(e => e.RoleId).IsRequired();
            builder.Property(e => e.MenuId).IsRequired();
            builder.Property(e => e.CreateDateTime).IsRequired();
            builder.Property(e => e.IsDeleted).IsRequired();
            builder.ToTable("RoleMenus");
        }
    }
}
