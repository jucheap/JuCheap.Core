using System;

namespace JuCheap.Core.Data.Entity
{
    /// <summary>
    /// 基础实体
    /// </summary>
    public class BaseEntity
    {
        /// <summary>
        /// 主键
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 创建日期
        /// </summary>
        public DateTime CreateDateTime { get; set; }

        /// <summary>
        /// 是否删除
        /// </summary>
        public bool IsDeleted { get; set; }
    }

    /// <summary>
    /// 基础实体扩展
    /// </summary>
    public static class BaseEntityExtention
    {
        /// <summary>
        /// 默认值初始化
        /// </summary>
        /// <param name="entity"></param>
        public static void Init(this BaseEntity entity)
        {
            entity.Id = Guid.NewGuid();
            entity.CreateDateTime = DateTime.Now;
            entity.IsDeleted = false;
        }
    }
}
