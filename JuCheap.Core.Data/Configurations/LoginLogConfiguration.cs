using JuCheap.Core.Data.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JuCheap.Core.Data.Configurations
{
    /// <summary>
    ///LoginLog表信息配置
    /// </summary>
    public class LoginLogConfiguration : IEntityTypeConfiguration<LoginLogEntity>
    {
        public void Configure(EntityTypeBuilder<LoginLogEntity> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).ValueGeneratedNever();
            builder.Property(e => e.LoginName).HasMaxLength(20).IsRequired();
            builder.Property(e => e.IP).HasMaxLength(20).IsRequired();
            builder.Property(e => e.Mac).HasMaxLength(200).IsRequired();
            builder.Property(e => e.UserId).IsRequired();
            builder.Property(e => e.CreateDateTime).IsRequired();
            builder.Property(e => e.IsDeleted).IsRequired();
            builder.ToTable("LoginLogs");
        }
    }
}
