using System;
using System.Collections.Generic;
using System.Linq;
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
        public void Init()
        {
            _context.Database.Migrate();
            if (_context.SystemConfigs.Any(item => item.IsDataInited)) return;

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
                ParentId = system.Id,
                Name = "用户授权",
                Url = "/User/Authen",
                CreateDateTime = _now,
                Order = 5,
                Code = "AC",
                PathCode = "AAAC",
                Type = 2
            };//5
            var roleMenuMgr = new MenuEntity
            {
                Id = _instance.GetId(),
                ParentId = system.Id,
                Name = "角色授权",
                Url = "/Role/Authen",
                CreateDateTime = _now,
                Order = 6,
                Code = "AC",
                PathCode = "AAAC",
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
                Code = "AD",
                PathCode = "AAAD",
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
                Code = "AAAD",
                PathCode = "AAADAA",
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
                    UserId = admin.Id, RoleId = superAdminRole.Id, CreateDateTime = _now
                },
                new UserRoleEntity
                {
                    Id = _instance.GetId(),
                    UserId = guest.Id, RoleId = guestRole.Id, CreateDateTime = _now
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
                    RoleId = superAdminRole.Id, MenuId = m.Id, CreateDateTime = _now
                });
            });
            //guest授权(guest只有查看权限，没有按钮操作权限)
            menus.Where(item=>item.Type!=(byte)MenuType.Button).ForEach(m =>
            {
                roleMenus.Add(new RoleMenuEntity
                {
                    Id = _instance.GetId(),
                    RoleId = guestRole.Id, MenuId = m.Id, CreateDateTime = _now
                });
            });

            #endregion

            #region 路径码

            InitPathCode();

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
            _context.SaveChanges();
            _context.UserRoles.AddRange(userRoles);
            _context.RoleMenus.AddRange(roleMenus);
            _context.SystemConfigs.Add(systemConfig);
            _context.SaveChanges();
        }

        /// <summary>
        /// 初始化路径码
        /// </summary>
        public bool InitPathCode()
        {
            //生成路径码
            var codes = new List<string>(26);
            for (var i = 65; i <= 90; i++)
            {
                codes.Add(((char)i).ToString());
            }
            var len = 2;
            //求组合
            var ermutation = PermutationAndCombination<string>.GetCombination(codes.ToArray(), len);
            var list = new List<PathCodeEntity>();
            ermutation.ForEach(item =>
            {
                list.Add(new PathCodeEntity
                {
                    Id = _instance.GetId(),
                    Code = string.Join(string.Empty, item),
                    Len = len
                });
                list.Add(new PathCodeEntity
                {
                    Id = _instance.GetId(),
                    Code = string.Join(string.Empty, item.Reverse()),
                    Len = len
                });
            });
            Func<IEnumerable<PathCodeEntity>> getSameKeyFunc = () =>
            {
                return codes.Select(key => new PathCodeEntity
                {
                    Id = _instance.GetId(),
                    Code = string.Join(string.Empty, key, key),
                    Len = len
                });
            };
            list.AddRange(getSameKeyFunc());
            list = list.OrderBy(item => item.Code).ToList();

            _context.Database.ExecuteSqlCommand("DELETE FROM PathCodes");
            _context.PathCodes.AddRange(list);

            return _context.SaveChanges() > 0;
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
        private IEnumerable<MenuEntity> GetMenuButtons(string parentId, string controllerName, string controllerShowName, string parentPathCode,int order)
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
