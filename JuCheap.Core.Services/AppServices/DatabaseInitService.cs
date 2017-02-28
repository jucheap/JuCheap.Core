using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JuCheap.Core.Data;
using JuCheap.Core.Data.Entity;
using JuCheap.Core.Infrastructure.Extentions;
using JuCheap.Core.Infrastructure.Utilities;
using JuCheap.Core.Interfaces;
using JuCheap.Core.Models.Enum;
using Microsoft.EntityFrameworkCore;

namespace JuCheap.Core.Services.AppServices
{
    /// <summary>
    /// 数据库初始化
    /// </summary>
    public class DatabaseInitService : IDatabaseInitService
    {
        private readonly JuCheapContext _context;
        private readonly DateTime _now = new DateTime(2016, 06, 06, 0, 0, 0);
        private readonly BaseIdGenerator _instance = BaseIdGenerator.Instance;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="context"></param>
        public DatabaseInitService(JuCheapContext context)
        {
            _context = context;
        }

        /// <summary>
        /// 初始化
        /// </summary>
        public async Task<bool> InitAsync()
        {
            try
            {
                await _context.Database.EnsureCreatedAsync();
                //await _context.Database.MigrateAsync();
                if (await _context.SystemConfigs.AnyAsync(item => item.IsDataInited))
                    return false;

                #region 用户

                var admin = new UserEntity
                {
                    Id = _instance.GetId(),
                    LoginName = "jucheap",
                    RealName = "超级管理员",
                    Password = "qwaszx12".ToMd5(),
                    Email = "service@jucheap.com",
                    IsSuperMan = true,
                    CreateDateTime = _now
                };
                var guest = new UserEntity
                {
                    Id = _instance.GetId(),
                    LoginName = "admin",
                    RealName = "游客",
                    Password = "qwaszx".ToMd5(),
                    Email = "service@jucheap.com",
                    CreateDateTime = _now
                };
                //用户
                var user = new List<UserEntity>
                {
                    admin,
                    guest
                };
                #endregion

                #region 菜单

                var system = new MenuEntity
                {
                    Id = _instance.GetId(),
                    Name = "系统设置",
                    Url = "#",
                    CreateDateTime = _now,
                    Order = 1,
                    Code = "AA",
                    PathCode = "AA",
                    Type = 1
                };//1
                var menuMgr = new MenuEntity
                {
                    Id = _instance.GetId(),
                    ParentId = system.Id,
                    Name = "菜单管理",
                    Url = "/Menu/Index",
                    CreateDateTime = _now,
                    Order = 2,
                    Code = "AA",
                    PathCode = "AAAA",
                    Type = 2
                };//2
                var roleMgr = new MenuEntity
                {
                    Id = _instance.GetId(),
                    ParentId = system.Id,
                    Name = "角色管理",
                    Url = "/Role/Index",
                    CreateDateTime = _now,
                    Order = 3,
                    Code = "AB",
                    PathCode = "AAAB",
                    Type = 2
                };//3
                var userMgr = new MenuEntity
                {
                    Id = _instance.GetId(),
                    ParentId = system.Id,
                    Name = "用户管理",
                    Url = "/User/Index",
                    CreateDateTime = _now,
                    Order = 4,
                    Code = "AC",
                    PathCode = "AAAC",
                    Type = 2
                };//4
                var userRoleMgr = new MenuEntity
                {
                    Id = _instance.GetId(),
                    ParentId = userMgr.Id,
                    Name = "用户授权",
                    Url = "/User/Authen",
                    CreateDateTime = _now,
                    Order = 5,
                    Code = "AD",
                    PathCode = "AAAD",
                    Type = 2
                };//5
                var giveRight = new MenuEntity
                {
                    Id = _instance.GetId(),
                    ParentId = userRoleMgr.Id,
                    Name = "授权",
                    Url = "/User/GiveRight",
                    CreateDateTime = _now,
                    Order = 1,
                    Code = "AA",
                    PathCode = "AAADAA",
                    Type = 3
                };
                var cancelRight = new MenuEntity
                {
                    Id = _instance.GetId(),
                    ParentId = userRoleMgr.Id,
                    Name = "取消授权",
                    Url = "/User/CancelRight",
                    CreateDateTime = _now,
                    Order = 2,
                    Code = "AB",
                    PathCode = "AAADAB",
                    Type = 3
                };
                var roleMenuMgr = new MenuEntity
                {
                    Id = _instance.GetId(),
                    ParentId = system.Id,
                    Name = "角色授权",
                    Url = "/Role/Authen",
                    CreateDateTime = _now,
                    Order = 6,
                    Code = "AE",
                    PathCode = "AAAE",
                    Type = 2
                };//6
                var sysConfig = new MenuEntity
                {
                    Id = _instance.GetId(),
                    ParentId = system.Id,
                    Name = "系统配置",
                    Url = "/System/Index",
                    CreateDateTime = _now,
                    Order = 7,
                    Code = "AF",
                    PathCode = "AAAF",
                    Type = 2
                };//7
                var sysConfigReloadPathCode = new MenuEntity
                {
                    Id = _instance.GetId(),
                    ParentId = sysConfig.Id,
                    Name = "重置路径码",
                    Url = "/System/ReloadPathCode",
                    CreateDateTime = _now,
                    Order = 8,
                    Code = "AAAF",
                    PathCode = "AAAFAA",
                    Type = 3
                };//8
                var log = new MenuEntity
                {
                    Id = _instance.GetId(),
                    Name = "日志查看",
                    Url = "#",
                    CreateDateTime = _now,
                    Order = 9,
                    Code = "AB",
                    PathCode = "AB",
                    Type = 1
                };//9
                var logLogin = new MenuEntity
                {
                    Id = _instance.GetId(),
                    ParentId = log.Id,
                    Name = "登录日志",
                    Url = "/Log/Logins",
                    CreateDateTime = _now,
                    Order = 10,
                    Code = "AA",
                    PathCode = "ABAA",
                    Type = 2
                };//10
                var logView = new MenuEntity
                {
                    Id = _instance.GetId(),
                    ParentId = log.Id,
                    Name = "访问日志",
                    Url = "/Log/Visits",
                    CreateDateTime = _now,
                    Order = 11,
                    Code = "AB",
                    PathCode = "ABAB",
                    Type = 2
                };//11

                //菜单
                var menus = new List<MenuEntity>
                {
                    system,
                    menuMgr,
                    roleMgr,
                    userMgr,
                    userRoleMgr,
                    giveRight,
                    cancelRight,
                    roleMenuMgr,
                    sysConfig,
                    sysConfigReloadPathCode,
                    log,
                    logLogin,
                    logView
                };
                var menuBtns = GetMenuButtons(menuMgr.Id, "Menu", "菜单", "AAAA", 12);//14
                var rolwBtns = GetMenuButtons(roleMgr.Id, "Role", "角色", "AAAB", 15);//17
                var userBtns = GetMenuButtons(userMgr.Id, "User", "用户", "AAAC", 18);//20

                menus.AddRange(menuBtns);//14
                menus.AddRange(rolwBtns);//17
                menus.AddRange(userBtns);//20
                menus.Add(new MenuEntity
                {
                    Id = _instance.GetId(),
                    ParentId = roleMenuMgr.Id,
                    Order = 6,
                    Name = "授权",
                    Type = (byte)MenuType.Button,
                    Url = "/Role/SetRoleMenus",
                    CreateDateTime = _now,
                    Code = "AA",
                    PathCode = "AAACAA"
                });
                menus.Add(new MenuEntity
                {
                    Id = _instance.GetId(),
                    ParentId = roleMenuMgr.Id,
                    Order = 6,
                    Name = "清空权限",
                    Type = (byte)MenuType.Button,
                    Url = "/Role/ClearRoleMenus",
                    CreateDateTime = _now,
                    Code = "AB",
                    PathCode = "AAACAB"
                });

                #endregion

                #region 角色

                var superAdminRole = new RoleEntity
                {
                    Id = _instance.GetId(),
                    Name = "超级管理员",
                    Description = "超级管理员"
                };
                var guestRole = new RoleEntity
                {
                    Id = _instance.GetId(),
                    Name = "guest",
                    Description = "游客"
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
                        Id = _instance.GetId(),
                        UserId = admin.Id,
                        RoleId = superAdminRole.Id,
                        CreateDateTime = _now
                    },
                    new UserRoleEntity
                    {
                        Id = _instance.GetId(),
                        UserId = guest.Id,
                        RoleId = guestRole.Id,
                        CreateDateTime = _now
                    }
                };

                #endregion

                #region 角色菜单权限关系

                var roleMenus = new List<RoleMenuEntity>();
                //管理员授权(管理员有所有权限)
                menus.ForEach(m =>
                {
                    roleMenus.Add(new RoleMenuEntity
                    {
                        Id = _instance.GetId(),
                        RoleId = superAdminRole.Id,
                        MenuId = m.Id,
                        CreateDateTime = _now
                    });
                });
                //guest授权(guest只有查看权限，没有按钮操作权限)
                menus.Where(item => item.Type != (byte)MenuType.Button).ForEach(m =>
                {
                    roleMenus.Add(new RoleMenuEntity
                    {
                        Id = _instance.GetId(),
                        RoleId = guestRole.Id,
                        MenuId = m.Id,
                        CreateDateTime = _now
                    });
                });

                #endregion

                #region 系统配置

                var systemConfig = new SystemConfigEntity
                {
                    Id = _instance.GetId(),
                    SystemName = "JuCheap Core",
                    IsDataInited = true,
                    DataInitedDate = _now,
                    CreateDateTime = _now,
                    IsDeleted = false
                };

                #endregion

                _context.Menus.AddRange(menus.OrderBy(m => m.Order).ToArray());
                _context.Roles.AddRange(roles);
                _context.Users.AddRange(user);
                _context.UserRoles.AddRange(userRoles);
                _context.RoleMenus.AddRange(roleMenus);
                _context.SystemConfigs.Add(systemConfig);
                return await _context.SaveChangesAsync() > 0;
            }
            catch(Exception ex)
            {
                //todo log
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

            await _context.Database.ExecuteSqlCommandAsync("DELETE FROM PathCodes");
            _context.PathCodes.AddRange(list);

            return await _context.SaveChangesAsync() > 0;
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
                    Id = _instance.GetId(),
                    ParentId = parentId,
                    Name = string.Concat("添加",controllerShowName),
                    Url = string.Format("/{0}/Add",controllerName),
                    CreateDateTime = _now,
                    Order = order,
                    Code = "AA",
                    PathCode = parentPathCode+"AA",
                    Type = 3
                },
                new MenuEntity
                {
                    Id = _instance.GetId(),
                    ParentId = parentId,
                    Name = string.Concat("修改",controllerShowName),
                    Url = string.Format("/{0}/Edit",controllerName),
                    CreateDateTime = _now,
                    Order = order+1,
                    Code = "AB",
                    PathCode = parentPathCode+"AB",
                    Type = 3
                },
                new MenuEntity
                {
                    Id = _instance.GetId(),
                    ParentId = parentId,
                    Name = string.Concat("删除",controllerShowName),
                    Url = string.Format("/{0}/Delete",controllerName),
                    CreateDateTime = _now,
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
