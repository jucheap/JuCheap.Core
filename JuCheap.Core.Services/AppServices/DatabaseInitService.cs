using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using JuCheap.Core.Data;
using JuCheap.Core.Data.Entity;
using JuCheap.Core.Infrastructure.Extentions;
using JuCheap.Core.Infrastructure.Utilities;
using JuCheap.Core.Interfaces;
using JuCheap.Core.Models;
using JuCheap.Core.Models.Enum;
using log4net;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace JuCheap.Core.Services.AppServices
{
    /// <summary>
    /// 数据库初始化
    /// </summary>
    public class DatabaseInitService : IDatabaseInitService
    {
        private readonly JuCheapContext _context;
        private readonly IMenuService _menuService;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="context"></param>
        public DatabaseInitService(JuCheapContext context,IMenuService menuService)
        {
            _context = context;
            _menuService = menuService;
        }
        
        public DateTime Now => new DateTime(2016, 06, 06, 0, 0, 0);

        /// <summary>
        /// 初始化
        /// </summary>
        public async Task<bool> InitAsync(List<MenuDto> menues)
        {
            try
            {
                await _context.Database.MigrateAsync();
                if (await _context.SystemConfigs.AnyAsync(item => item.IsDataInited))
                    return false;

                #region 用户

                var admin = new UserEntity
                {
                    Id = Guid.NewGuid().ToString("N"),
                    LoginName = "jucheap",
                    RealName = "超级管理员",
                    Password = "qwaszx12".ToMd5(),
                    Email = "service@jucheap.com",
                    IsSuperMan = true,
                    CreateDateTime = Now
                };
                var guest = new UserEntity
                {
                    Id = Guid.NewGuid().ToString("N"),
                    LoginName = "admin",
                    RealName = "游客",
                    Password = "qwaszx".ToMd5(),
                    Email = "service@jucheap.com",
                    CreateDateTime = Now
                };
                //用户
                var user = new List<UserEntity>
                {
                    admin,
                    guest
                };
                #endregion

                #region 菜单

                await _menuService.ReInitMenuesAsync(menues);

                var menus = await _context.Menus.ToListAsync();
                #endregion

                #region 角色

                var superAdminRole = new RoleEntity
                {
                    Id = Guid.NewGuid().ToString("N"),
                    Name = "超级管理员",
                    Description = "超级管理员",
                    CreateDateTime = Now
                };
                var guestRole = new RoleEntity
                {
                    Id = Guid.NewGuid().ToString("N"),
                    Name = "guest",
                    Description = "游客",
                    CreateDateTime = Now
                };
                var roles = new List<RoleEntity>
            {
                superAdminRole,
                guestRole
            };

                #endregion

                #region 用户角色关系

                var userRoles = new List<UserRoleEntity>
                {
                    new UserRoleEntity
                    {
                        Id = Guid.NewGuid().ToString("N"),
                        UserId = admin.Id,
                        RoleId = superAdminRole.Id,
                        CreateDateTime = Now
                    },
                    new UserRoleEntity
                    {
                        Id = Guid.NewGuid().ToString("N"),
                        UserId = guest.Id,
                        RoleId = guestRole.Id,
                        CreateDateTime = Now
                    }
                };

                #endregion

                #region 角色菜单权限关系

                var roleMenus = new List<RoleMenuEntity>();
                //guest授权(guest只有查看权限，没有按钮操作权限)
                menus.Where(item => item.Type != (byte)MenuType.Action).ForEach(m =>
                {
                    roleMenus.Add(new RoleMenuEntity
                    {
                        Id = Guid.NewGuid().ToString("N"),
                        RoleId = guestRole.Id,
                        MenuId = m.Id,
                        CreateDateTime = Now
                    });
                });

                #endregion

                #region 系统配置

                var systemConfig = new SystemConfigEntity
                {
                    Id = Guid.NewGuid().ToString("N"),
                    SystemName = "JuCheap Core",
                    IsDataInited = true,
                    DataInitedDate = Now,
                    CreateDateTime = Now,
                    IsDeleted = false
                };

                #endregion

                _context.Roles.AddRange(roles);
                _context.Users.AddRange(user);
                _context.UserRoles.AddRange(userRoles);
                _context.RoleMenus.AddRange(roleMenus);
                _context.SystemConfigs.Add(systemConfig);
                await _context.SaveChangesAsync();
                await InitPathCodeAsync();
                return true;
            }
            catch (Exception ex)
            {
                Log.Logger.Error(ex);
            }
            return false;
        }

        /// <summary>
        /// 初始化路径码
        /// </summary>
        public async Task<bool> InitPathCodeAsync()
        {
            //生成路径码
            var codes = new List<string>(26);
            for (var i = 65; i <= 90; i++)
            {
                codes.Add(((char)i).ToString());
            }
            //求组合
            var list = (from a in codes
                        from b in codes
                        select new PathCodeEntity
                        {
                            Code = a + b,
                            Len = 2
                        }).OrderBy(item => item.Code).ToList();
            list.ForEach(x => x.Init());
            await _context.Database.ExecuteSqlCommandAsync("DELETE FROM PathCodes");
            _context.PathCodes.AddRange(list);

            return await _context.SaveChangesAsync() > 0;
        }

        /// <summary>
        /// 初始化省市区数据
        /// </summary>
        /// <returns></returns>
        public async Task<bool> InitAreas()
        {
            //删除以前的数据
            var olds = await _context.Areas.ToListAsync();
            _context.Areas.RemoveRange(olds);
            await _context.SaveChangesAsync();
            var filePath = string.Format("{0}areas-json.json", AppDomain.CurrentDomain.BaseDirectory);
            if (File.Exists(filePath))
            {
                var areas = JsonConvert.DeserializeObject<IList<AreaEntity>>(File.ReadAllText(filePath));
                if (areas.AnyOne())
                {
                    areas.ForEach(x => x.CreateDateTime = DateTime.Now);
                }
                _context.Areas.AddRange(areas);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        #region Private

        /// <summary>
        /// 获取菜单的基础按钮
        /// </summary>
        /// <param name="parentId">父级ID</param>
        /// <param name="controllerName">控制器名称</param>
        /// <param name="controllerShowName">菜单显示名称</param>
        /// <param name="parentPathCode">父级路径码</param>
        /// <param name="order">排序</param>
        /// <returns></returns>
        private IEnumerable<MenuEntity> GetMenuButtons(string parentId, string controllerName, string controllerShowName, string parentPathCode, int order)
        {
            return new List<MenuEntity>
            {
                new MenuEntity
                {
                    Id = Guid.NewGuid().ToString("N"),
                    ParentId = parentId,
                    Name = string.Concat("添加",controllerShowName),
                    Url = string.Format("/{0}/Add",controllerName),
                    CreateDateTime = Now,
                    Order = order,
                    Code = "AA",
                    PathCode = parentPathCode+"AA",
                    Type = 3
                },
                new MenuEntity
                {
                    Id = Guid.NewGuid().ToString("N"),
                    ParentId = parentId,
                    Name = string.Concat("修改",controllerShowName),
                    Url = string.Format("/{0}/Edit",controllerName),
                    CreateDateTime = Now,
                    Order = order+1,
                    Code = "AB",
                    PathCode = parentPathCode+"AB",
                    Type = 3
                },
                new MenuEntity
                {
                    Id = Guid.NewGuid().ToString("N"),
                    ParentId = parentId,
                    Name = string.Concat("删除",controllerShowName),
                    Url = string.Format("/{0}/Delete",controllerName),
                    CreateDateTime = Now,
                    Order = order+2,
                    Code = "AC",
                    PathCode = parentPathCode+"AC",
                    Type = 3
                }
            };
        }

        #endregion
    }
}
