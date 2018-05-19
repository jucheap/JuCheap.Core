using JuCheap.Core.Data.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JuCheap.Core.Data.Configurations
{
    /// <summary>
    /// 任务流模板步骤表信息配置
    /// </summary>
    public class TaskTemplateStepOperateConfiguration : BaseConfiguration<TaskTemplateStepOperateEntity>
    {
        public override void Configure(EntityTypeBuilder<TaskTemplateStepOperateEntity> builder)
        {
            base.Configure(builder);

            builder.ToTable("TaskTemplateStepOperate");
            builder.Property(x => x.StepId).IsRequired().HasMaxLength(36);
            builder.Property(x => x.Name).IsRequired().IsUnicode(true).HasMaxLength(20);
            builder.Property(x => x.OperateDirection).IsRequired();
            builder.HasOne(x => x.Step).WithMany(x => x.Operates).HasForeignKey(x => x.StepId);
        }
    }
}
