using JuCheap.Core.Data.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JuCheap.Core.Data.Configurations
{
    /// <summary>
    /// 站内信接收人表信息配置
    /// </summary>
    public class MessageReceiverConfiguration : BaseConfiguration<MessageReceiverEntity>
    {
        public override void Configure(EntityTypeBuilder<MessageReceiverEntity> builder)
        {
            base.Configure(builder);

            builder.ToTable("MessageReceivers");
            builder.Property(x => x.UserId).IsRequired().HasMaxLength(36);
            builder.Property(x => x.MessageId).IsRequired().HasMaxLength(36);
            builder.Property(x => x.IsReaded).IsRequired();
            builder.HasOne(x => x.Message).WithMany(x => x.MessageReceivers).HasForeignKey(x => x.MessageId);
        }
    }
}
