namespace JuCheap.Core.Data.Entity
{
    /// <summary>
    /// 路径码实体
    /// </summary>
    public class PathCodeEntity : BaseEntity
    {
        /// <summary>
        /// 路径码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 路径码长度
        /// </summary>
        public int Len { get; set; }
    }
}
