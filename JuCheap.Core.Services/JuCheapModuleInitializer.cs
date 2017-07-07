using AutoMapper;
using JuCheap.Core.Data.Entity;
using JuCheap.Core.Models;

namespace JuCheap.Core.Services
{
    /// <summary>
    /// 模块初始化
    /// </summary>
    public class JuCheapModuleInitializer : ModuleInitializer
    {
        /// <summary>
        /// 加载AutoMapper配置
        /// </summary>
        /// <param name="config"></param>
        public override void LoadAutoMapper(IMapperConfigurationExpression config)
        {
            config.CreateMap<UserEntity, UserDto>().ReverseMap();
            config.CreateMap<UserEntity, UserAddDto>().ReverseMap();
            config.CreateMap<UserEntity, UserUpdateDto>().ReverseMap();
            config.CreateMap<UserDto, UserUpdateDto>().ReverseMap();
        }
    }
}
