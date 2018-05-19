using JuCheap.Core.Data.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JuCheap.Core.Data.Configurations
{
    /// <summary>
    /// 任务流模板表单表信息配置
    /// </summary>
    public class TaskTemplateFormConfiguration : BaseConfiguration<TaskTemplateFormEntity>
    {
        public override void Configure(EntityTypeBuilder<TaskTemplateFormEntity> builder)
        {
            base.Configure(builder);

            builder.ToTable("TaskTemplateForm");
            builder.Property(x => x.TemplateId).IsRequired().HasMaxLength(36);
            builder.Property(x => x.ControlType).IsRequired();
            builder.Property(x => x.ControlName).IsRequired().IsUnicode(true).HasMaxLength(50);
            builder.HasOne(x => x.Template).WithMany(x => x.Forms).HasForeignKey(x => x.TemplateId);
        }
    }
}
