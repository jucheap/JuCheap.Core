using JuCheap.Core.Data.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JuCheap.Core.Data.Configurations
{
    /// <summary>
    /// 任务流模板步骤表信息配置
    /// </summary>
    public class TaskTemplateStepConfiguration : BaseConfiguration<TaskTemplateStepEntity>
    {
        public override void Configure(EntityTypeBuilder<TaskTemplateStepEntity> builder)
        {
            base.Configure(builder);

            builder.ToTable("TaskTemplateStep");
            builder.Property(x => x.TemplateId).IsRequired().HasMaxLength(36);
            builder.Property(x => x.StepName).IsRequired().IsUnicode(true).HasMaxLength(20);
            builder.Property(x => x.Order).IsRequired();
        }
    }
}
