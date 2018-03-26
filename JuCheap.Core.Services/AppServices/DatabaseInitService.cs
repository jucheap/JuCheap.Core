using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using JuCheap.Core.Data;
using JuCheap.Core.Data.Entity;
using JuCheap.Core.Infrastructure.Extentions;
using JuCheap.Core.Interfaces;
using JuCheap.Core.Models.Enum;
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

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="context"></param>
        public DatabaseInitService(JuCheapContext context)
        {
            _context = context;
        }
        
        public DateTime Now => new DateTime(2016, 06, 06, 0, 0, 0);

        /// <summary>
        /// 初始化
        /// </summary>
        public async Task<bool> InitAsync()
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

                var system = new MenuEntity
                {
                    Id = Guid.NewGuid().ToString("N"),
                    Name = "系统设置",
                    Url = "#",
                    CreateDateTime = Now,
                    Order = 1,
                    Code = "AA",
                    PathCode = "AA",
                    Type = 1
                };//1
                var menuMgr = new MenuEntity
                {
                    Id = Guid.NewGuid().ToString("N"),
                    ParentId = system.Id,
                    Name = "菜单管理",
                    Url = "/Menu/Index",
                    CreateDateTime = Now,
                    Order = 2,
                    Code = "AA",
                    PathCode = "AAAA",
                    Type = 2
                };//2
                var roleMgr = new MenuEntity
                {
                    Id = Guid.NewGuid().ToString("N"),
                    ParentId = system.Id,
                    Name = "角色管理",
                    Url = "/Role/Index",
                    CreateDateTime = Now,
                    Order = 3,
                    Code = "AB",
                    PathCode = "AAAB",
                    Type = 2
                };//3
                var userMgr = new MenuEntity
                {
                    Id = Guid.NewGuid().ToString("N"),
                    ParentId = system.Id,
                    Name = "用户管理",
                    Url = "/User/Index",
                    CreateDateTime = Now,
                    Order = 4,
                    Code = "AC",
                    PathCode = "AAAC",
                    Type = 2
                };//4
                var departmentMgr = new MenuEntity
                {
                    Id = Guid.NewGuid().ToString("N"),
                    ParentId = system.Id,
                    Name = "部门管理",
                    Url = "/Department/Index",
                    CreateDateTime = Now,
                    Order = 3,
                    Code = "AG",
                    PathCode = "AAAG",
                    Type = 2
                };
                var userRoleMgr = new MenuEntity
                {
                    Id = Guid.NewGuid().ToString("N"),
                    ParentId = userMgr.Id,
                    Name = "用户授权",
                    Url = "/User/Authen",
                    CreateDateTime = Now,
                    Order = 5,
                    Code = "AD",
                    PathCode = "AAAD",
                    Type = 2
                };//5
                var giveRight = new MenuEntity
                {
                    Id = Guid.NewGuid().ToString("N"),
                    ParentId = userRoleMgr.Id,
                    Name = "授权",
                    Url = "/User/GiveRight",
                    CreateDateTime = Now,
                    Order = 1,
                    Code = "AA",
                    PathCode = "AAADAA",
                    Type = 3
                };
                var cancelRight = new MenuEntity
                {
                    Id = Guid.NewGuid().ToString("N"),
                    ParentId = userRoleMgr.Id,
                    Name = "取消授权",
                    Url = "/User/CancelRight",
                    CreateDateTime = Now,
                    Order = 2,
                    Code = "AB",
                    PathCode = "AAADAB",
                    Type = 3
                };
                var roleMenuMgr = new MenuEntity
                {
                    Id = Guid.NewGuid().ToString("N"),
                    ParentId = system.Id,
                    Name = "角色授权",
                    Url = "/Role/Authen",
                    CreateDateTime = Now,
                    Order = 6,
                    Code = "AE",
                    PathCode = "AAAE",
                    Type = 2
                };//6
                var sysConfig = new MenuEntity
                {
                    Id = Guid.NewGuid().ToString("N"),
                    ParentId = system.Id,
                    Name = "系统配置",
                    Url = "/System/Index",
                    CreateDateTime = Now,
                    Order = 7,
                    Code = "AF",
                    PathCode = "AAAF",
                    Type = 2
                };//7
                var areaConfig = new MenuEntity
                {
                    Id = Guid.NewGuid().ToString("N"),
                    ParentId = system.Id,
                    Name = "省市区管理",
                    Url = "/Area/Index",
                    CreateDateTime = Now,
                    Order = 5,
                    Code = "AH",
                    PathCode = "AAAH",
                    Type = 2
                };
                var sysConfigReloadPathCode = new MenuEntity
                {
                    Id = Guid.NewGuid().ToString("N"),
                    ParentId = sysConfig.Id,
                    Name = "重置路径码",
                    Url = "/System/ReloadPathCode",
                    CreateDateTime = Now,
                    Order = 8,
                    Code = "AAAF",
                    PathCode = "AAAFAA",
                    Type = 3
                };//8
                var log = new MenuEntity
                {
                    Id = Guid.NewGuid().ToString("N"),
                    Name = "日志查看",
                    Url = "#",
                    CreateDateTime = Now,
                    Order = 2,
                    Code = "AB",
                    PathCode = "AB",
                    Type = 1
                };//9
                var logLogin = new MenuEntity
                {
                    Id = Guid.NewGuid().ToString("N"),
                    ParentId = log.Id,
                    Name = "登录日志",
                    Url = "/Log/Logins",
                    CreateDateTime = Now,
                    Order = 1,
                    Code = "AA",
                    PathCode = "ABAA",
                    Type = 2
                };//10
                var logView = new MenuEntity
                {
                    Id = Guid.NewGuid().ToString("N"),
                    ParentId = log.Id,
                    Name = "访问日志",
                    Url = "/Log/Visits",
                    CreateDateTime = Now,
                    Order = 2,
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
                    departmentMgr,
                    userRoleMgr,
                    giveRight,
                    cancelRight,
                    roleMenuMgr,
                    sysConfig,
                    areaConfig,
                    sysConfigReloadPathCode,
                    log,
                    logLogin,
                    logView
                };
                var menuBtns = GetMenuButtons(menuMgr.Id, "Menu", "菜单", "AAAA", 12);
                var rolwBtns = GetMenuButtons(roleMgr.Id, "Role", "角色", "AAAB", 15);
                var userBtns = GetMenuButtons(userMgr.Id, "User", "用户", "AAAC", 18);
                var departmentBtns = GetMenuButtons(departmentMgr.Id, "Department", "部门", "AAAG", 19);
                var areaBtns = GetMenuButtons(areaConfig.Id, "Area", "部门", "AAAH", 20);

                menus.AddRange(menuBtns);
                menus.AddRange(rolwBtns);
                menus.AddRange(userBtns);
                menus.AddRange(departmentBtns);
                menus.AddRange(areaBtns);
                menus.Add(new MenuEntity
                {
                    Id = Guid.NewGuid().ToString("N"),
                    ParentId = roleMenuMgr.Id,
                    Order = 6,
                    Name = "授权",
                    Type = (byte)MenuType.Action,
                    Url = "/Role/SetRoleMenus",
                    CreateDateTime = Now,
                    Code = "AA",
                    PathCode = "AAACAA"
                });
                menus.Add(new MenuEntity
                {
                    Id = Guid.NewGuid().ToString("N"),
                    ParentId = roleMenuMgr.Id,
                    Order = 6,
                    Name = "清空权限",
                    Type = (byte)MenuType.Action,
                    Url = "/Role/ClearRoleMenus",
                    CreateDateTime = Now,
                    Code = "AB",
                    PathCode = "AAACAB"
                });
                //示例页面
                var page = new MenuEntity
                {
                    Id = Guid.NewGuid().ToString("N"),
                    Name = "示例页面",
                    Url = "#",
                    CreateDateTime = Now,
                    Order = 3,
                    Code = "AC",
                    PathCode = "AC",
                    Type = 1
                };
                var pageButton = new MenuEntity
                {
                    Id = Guid.NewGuid().ToString("N"),
                    ParentId = page.Id,
                    Name = "按钮",
                    Url = "/Pages/Buttons",
                    CreateDateTime = Now,
                    Order = 0,
                    Code = "AA",
                    PathCode = "ACAA",
                    Type = 2
                };
                var pageForm = new MenuEntity
                {
                    Id = Guid.NewGuid().ToString("N"),
                    ParentId = page.Id,
                    Name = "表单",
                    Url = "/Pages/Form",
                    CreateDateTime = Now,
                    Order = 1,
                    Code = "AB",
                    PathCode = "ACAB",
                    Type = 2
                };
                var pageFormAdvance = new MenuEntity
                {
                    Id = Guid.NewGuid().ToString("N"),
                    ParentId = page.Id,
                    Name = "高级表单",
                    Url = "/Pages/FormAdvance",
                    CreateDateTime = Now,
                    Order = 2,
                    Code = "AC",
                    PathCode = "ACAC",
                    Type = 2
                };
                var pageTable = new MenuEntity
                {
                    Id = Guid.NewGuid().ToString("N"),
                    ParentId = page.Id,
                    Name = "表格",
                    Url = "/Pages/Tables",
                    CreateDateTime = Now,
                    Order = 3,
                    Code = "AD",
                    PathCode = "ACAD",
                    Type = 2
                };
                var pageTabs = new MenuEntity
                {
                    Id = Guid.NewGuid().ToString("N"),
                    ParentId = page.Id,
                    Name = "选项卡",
                    Url = "/Pages/Tabs",
                    CreateDateTime = Now,
                    Order = 4,
                    Code = "AE",
                    PathCode = "ACAE",
                    Type = 2
                };
                var pageFonts = new MenuEntity
                {
                    Id = Guid.NewGuid().ToString("N"),
                    ParentId = page.Id,
                    Name = "字体",
                    Url = "/Pages/FontAwesome",
                    CreateDateTime = Now,
                    Order = 5,
                    Code = "AF",
                    PathCode = "ACAF",
                    Type = 2
                };

                menus.Add(page);
                menus.Add(pageButton);
                menus.Add(pageForm);
                menus.Add(pageFormAdvance);
                menus.Add(pageTable);
                menus.Add(pageTabs);
                menus.Add(pageFonts);

                #endregion

                #region 角色

                var superAdminRole = new RoleEntity
                {
                    Id = Guid.NewGuid().ToString("N"),
                    Name = "超级管理员",
                    Description = "超级管理员"
                };
                var guestRole = new RoleEntity
                {
                    Id = Guid.NewGuid().ToString("N"),
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
                //管理员授权(管理员有所有权限)
                menus.ForEach(m =>
                {
                    roleMenus.Add(new RoleMenuEntity
                    {
                        Id = Guid.NewGuid().ToString("N"),
                        RoleId = superAdminRole.Id,
                        MenuId = m.Id,
                        CreateDateTime = Now
                    });
                });
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
