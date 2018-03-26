using JuCheap.Core.Data.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JuCheap.Core.Data.Configurations
{
    /// <summary>
    /// UserRole表信息配置
    /// </summary>
    public class UserRoleConfiguration : IEntityTypeConfiguration<UserRoleEntity>
    {
        public void Configure(EntityTypeBuilder<UserRoleEntity> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).HasMaxLength(36).ValueGeneratedNever();
            builder.Property(e => e.RoleId).IsRequired();
            builder.Property(e => e.UserId).IsRequired();
            builder.Property(e => e.CreateDateTime).IsRequired();
            builder.Property(e => e.IsDeleted).IsRequired();
            builder.ToTable("UserRoles");
        }
    }
}
