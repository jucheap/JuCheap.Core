using JuCheap.Core.Data.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JuCheap.Core.Data.Configurations
{
    /// <summary>
    /// PageView表信息配置
    /// </summary>
    public class PageViewConfiguration : IEntityTypeConfiguration<PageViewEntity>
    {
        public void Configure(EntityTypeBuilder<PageViewEntity> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).ValueGeneratedNever();
            builder.Property(e => e.LoginName).HasMaxLength(20).IsRequired();
            builder.Property(e => e.IP).HasMaxLength(20).IsRequired();
            builder.Property(e => e.Url).HasMaxLength(300).IsRequired();
            builder.Property(e => e.UserId).HasMaxLength(20).IsRequired();
            builder.Property(e => e.CreateDateTime).IsRequired();
            builder.Property(e => e.IsDeleted).IsRequired();
            builder.ToTable("PageViews");
        }
    }
}
