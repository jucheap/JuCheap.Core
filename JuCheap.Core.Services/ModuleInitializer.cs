using AutoMapper;

namespace JuCheap.Core.Services
{
    /// <summary>
    /// 模块初始化
    /// </summary>
    public abstract class ModuleInitializer
    {

        /// <summary>
        /// 加载AutoMapper配置
        /// </summary>
        /// <param name="cofig"></param>
        public abstract void LoadAutoMapper(IMapperConfiguration cofig);
    }
}
