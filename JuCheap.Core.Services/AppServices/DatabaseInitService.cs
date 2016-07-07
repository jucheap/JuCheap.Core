using System;
using System.Collections.Generic;
using System.Linq;
using JuCheap.Core.Data;
using JuCheap.Core.Data.Entity;
using JuCheap.Core.Infrastructure.Extentions;
using JuCheap.Core.Infrastructure.Utilities;
using JuCheap.Core.Interfaces;
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
                LoginName = "jucheap",
                RealName = "超级管理员",
                Password = "qwaszx".ToMd5(),
                Email = "service@jucheap.com",
                IsSuperMan = true,
                CreateDateTime = _now
            };
            var guest = new UserEntity
            {
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
                Id = 1,
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
                Id = 2,
                ParentId = 1,
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
                Id = 3,
                ParentId = 1,
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
                Id = 4,
                ParentId = 1,
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
                Id = 5,
                ParentId = 1,
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
                Id = 6,
                ParentId = 1,
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
                Id = 7,
                ParentId = 1,
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
                Id = 8,
                ParentId = 7,
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
                Id = 9,
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
                Id = 10,
                ParentId = 9,
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
                Id = 11,
                ParentId = 9,
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
            var menuBtns = GetMenuButtons(2, "Menu", "菜单", "AAAA", 12);//14
            var rolwBtns = GetMenuButtons(3, "Role", "角色", "AAAB", 15);//17
            var userBtns = GetMenuButtons(4, "User", "用户", "AAAC", 18);//20

            menus.AddRange(menuBtns);//14
            menus.AddRange(rolwBtns);//17
            menus.AddRange(userBtns);//20

            #endregion

            #region 角色

            var superAdminRole = new RoleEntity { Name = "超级管理员", Description = "超级管理员" };
            var guestRole = new RoleEntity { Name = "guest", Description = "游客" };
            var roles = new List<RoleEntity>
            {
                superAdminRole,
                guestRole
            };

            #endregion

            #region 用户角色关系

            var userRoles = new List<UserRoleEntity>
            {
                new UserRoleEntity { UserId = 1, RoleId = 1, CreateDateTime = _now },
                new UserRoleEntity { UserId = 2, RoleId = 2, CreateDateTime = _now }
            };

            #endregion

            #region 角色菜单权限关系
            //超级管理员授权/游客授权
            var roleMenus = new List<RoleMenuEntity>();
            var len = menus.Count;
            for (var i = 0; i < len; i++)
            {
                roleMenus.Add(new RoleMenuEntity { RoleId = 1, MenuId = i + 1, CreateDateTime = _now });
                roleMenus.Add(new RoleMenuEntity { RoleId = 2, MenuId = i + 1, CreateDateTime = _now });
            }

            #endregion

            #region 系统配置

            var systemConfig = new SystemConfigEntity
            {
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
            _context.Database.OpenConnection();
            try
            {
                _context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT Menus ON");
                _context.SaveChanges();
                _context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT Menus OFF");
            }
            finally
            {
                _context.Database.CloseConnection();
            }
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
                    Code = string.Join(string.Empty, item),
                    Len = len
                });
                list.Add(new PathCodeEntity
                {
                    Code = string.Join(string.Empty, item.Reverse()),
                    Len = len
                });
            });
            Func<IEnumerable<PathCodeEntity>> getSameKeyFunc = () =>
            {
                return codes.Select(key => new PathCodeEntity
                {
                    Code = string.Join(string.Empty, key, key),
                    Len = len
                });
            };
            list.AddRange(getSameKeyFunc());
            list = list.OrderBy(item => item.Code).ToList();

            _context.Database.ExecuteSqlCommand("TRUNCATE TABLE PathCodes");
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
        private IEnumerable<MenuEntity> GetMenuButtons(int parentId, string controllerName, string controllerShowName, string parentPathCode,int order)
        {
            return new List<MenuEntity>
            {
                new MenuEntity
                {
                    Id = order,
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
                    Id = order + 1,
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
                    Id = order + 2,
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
