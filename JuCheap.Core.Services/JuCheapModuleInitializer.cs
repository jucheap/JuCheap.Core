using AutoMapper;
using JuCheap.Core.Data.Entity;
using JuCheap.Core.Infrastructure.Extentions;
using JuCheap.Core.Models;
using JuCheap.Core.Infrastructure.Enums;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace JuCheap.Core.Services
{
    /// <summary>
    /// 模块初始化
    /// </summary>
    public class JuCheapModuleInitializer : ModuleInitializer
    {
        /// <summary>
        /// 加载AutoMapper配置(AutoMapper映射关系配置)
        /// </summary>
        /// <param name="config"></param>
        public override void LoadAutoMapper(IMapperConfigurationExpression config)
        {
            config.CreateMap<UserEntity, UserDto>().ReverseMap();
            config.CreateMap<UserEntity, UserAddDto>().ReverseMap();
            config.CreateMap<UserEntity, UserUpdateDto>().ReverseMap();
            config.CreateMap<UserDto, UserUpdateDto>().ReverseMap();
            config.CreateMap<UserRoleEntity, UserRoleDto>().ReverseMap();
            config.CreateMap<RoleEntity, RoleDto>().ReverseMap();
            config.CreateMap<RoleMenuDto, RoleMenuEntity>().ReverseMap();
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

            config.CreateMap<DepartmentEntity, DepartmentDto>().ReverseMap();
            config.CreateMap<AreaEntity, AreaDto>().ReverseMap();
            config.CreateMap<MessageEntity, MessageDto>().ReverseMap();
            config.CreateMap<MessageEntity, MessageQueryDto>();
            config.CreateMap<TaskTemplateFormEntity, TaskTemplateFormDto>().ReverseMap();
        }
    }

    /// <summary>
    /// 依赖注入模块初始化
    /// </summary>
    public static class JuCheapServiceModuleInitialize
    {
        /// <summary>
        /// 添加需要依赖注入的服务
        /// </summary>
        /// <param name="services"></param>
        public static void UseJuCheapService(this IServiceCollection services)
        {
            //AutoMapper配置
            services.AddScoped<IConfigurationProvider>(_ => AutoMapperConfig.GetMapperConfiguration());
            services.AddScoped(_ => AutoMapperConfig.GetMapperConfiguration().CreateMapper());

            //通过反射，批量取出需要注入的接口和实现类
            var registrations =
                from type in typeof(JuCheapModuleInitializer).Assembly.GetTypes()
                where type.Namespace != null && (type.Namespace.IsNotBlank() &&
                                               type.Namespace.StartsWith("JuCheap.Core.Services") &&
                                               type.GetInterfaces().Any(x => x.Name.EndsWith("Service")) &&
                                               type.GetInterfaces().Any())
                select new { Service = type.GetInterfaces().First(), Implementation = type };

            foreach (var t in registrations)
            {
                services.AddScoped(t.Service, t.Implementation);
            }
        }
    }
}
