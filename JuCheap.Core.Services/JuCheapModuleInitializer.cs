using AutoMapper;
using JuCheap.Core.Data.Entity;
using JuCheap.Core.Models;
using JuCheap.Core.Models.Enum;

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
        public override void LoadAutoMapper(IMapperConfiguration config)
        {
            config.CreateMap<UserEntity, UserDto>().ReverseMap();
            config.CreateMap<UserEntity, UserAddDto>().ReverseMap();
            config.CreateMap<UserEntity, UserUpdateDto>().ReverseMap();
            config.CreateMap<UserDto, UserUpdateDto>().ReverseMap();
            config.CreateMap<UserRoleEntity, UserRoleDto>().ReverseMap();
            config.CreateMap<RoleEntity, RoleDto>().ReverseMap();
            config.CreateMap<PageViewEntity, VisitDto>()
                .ForMember(v => v.VisitDate, e => e.MapFrom(pv => pv.CreateDateTime))
                .ReverseMap();
            config.CreateMap<LoginLogEntity, LoginLogDto>().ReverseMap();
            config.CreateMap<MenuEntity, MenuDto>()
                .ForMember(m => m.Type, e => e.MapFrom(item => (MenuType) item.Type))
                .ReverseMap();
            config.CreateMap<MenuEntity, TreeDto>()
                .ForMember(m => m.id, e => e.MapFrom(item => item.Id))
                .ForMember(m => m.pId, e => e.MapFrom(item => item.ParentId))
                .ForMember(m => m.name, e => e.MapFrom(item => item.Name));
            config.CreateMap<RoleEntity, TreeDto>()
                .ForMember(m => m.id, e => e.MapFrom(item => item.Id))
                .ForMember(m => m.name, e => e.MapFrom(item => item.Name));
        }
    }
}
