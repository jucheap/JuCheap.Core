using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using JuCheap.Core.Data;
using JuCheap.Core.Data.Entity;
using JuCheap.Core.Infrastructure;
using JuCheap.Core.Interfaces;
using JuCheap.Core.Models;
using JuCheap.Core.Infrastructure.Enums;
using JuCheap.Core.Models.Filters;
using JuCheap.Core.Infrastructure.Extentions;
using JuCheap.Core.Infrastructure.Utilities;
using Microsoft.EntityFrameworkCore;
using System;

namespace JuCheap.Core.Services.AppServices
{
    /// <summary>
    /// 菜单契约服务
    /// </summary>
    public class MenuService : IMenuService
    {
        private readonly JuCheapContext _context;
        private readonly IMapper _mapper;
        private readonly IPathCodeService _pathCodeService;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="context"></param>
        /// <param name="mapper"></param>
        /// <param name="pathCodeService"></param>
        public MenuService(JuCheapContext context, 
            IMapper mapper,
            IPathCodeService pathCodeService)
        {
            _mapper = mapper;
            _pathCodeService = pathCodeService;
            _context = context;
        }

        /// <summary>
        /// 添加菜单
        /// </summary>
        /// <param name="dto">菜单模型</param>
        /// <returns></returns>
        public async Task<string> AddAsync(MenuDto dto)
        {
            var entity = _mapper.Map<MenuDto, MenuEntity>(dto);
            entity.Init();
            var dbSet = _context.Menus;
            var pathCodeDbSet = _pathCodeService.GetPathCodes();

            var existsCode = await dbSet.Where(item => item.ParentId == dto.ParentId)
                .Select(item => item.Code).ToListAsync();
            var pathCode = pathCodeDbSet.FirstOrDefault(item => !existsCode.Contains(item));
            entity.Code = pathCode.Trim();
            if (entity.ParentId.IsNotBlank())
            {
                var parent = await dbSet.FirstOrDefaultAsync(m => m.Id == entity.ParentId);
                entity.PathCode = string.Concat(parent.PathCode.Trim(), entity.Code.Trim());
                entity.Type = parent.Type == MenuType.Module ? MenuType.Menu : MenuType.Action;
            }
            else
            {
                entity.PathCode = entity.Code.Trim();
                entity.Type = MenuType.Module;
            }
            dbSet.Add(entity);

            return await _context.SaveChangesAsync() > 0 ? entity.Id : string.Empty;
        }

        /// <summary>
        /// 更新菜单
        /// </summary>
        /// <param name="dto">菜单模型</param>
        /// <returns></returns>
        public async Task<bool> UpdateAsync(MenuDto dto)
        {
            var dbSet = _context.Menus;

            var entity = await dbSet.FirstOrDefaultAsync(m => m.Id == dto.Id);
            entity.Name = dto.Name;
            entity.Url = dto.Url;
            entity.Order = dto.Order;
            entity.Icon = dto.Icon;
            return await _context.SaveChangesAsync() > 0;
        }

        /// <summary>
        /// 根据主键查询模型
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns></returns>
        public async Task<MenuDto> FindAsync(string id)
        {
            var dbSet = _context.Menus;
            var entity = await dbSet.FirstOrDefaultAsync(m => m.Id == id);
            var dto = _mapper.Map<MenuEntity, MenuDto>(entity);
            if (dto.ParentId.IsNotBlank())
            {
                var parent = await dbSet.FirstOrDefaultAsync(m => m.Id == dto.ParentId);
                dto.ParentName = parent.Name;
            }
            return dto;
        }

        /// <summary>
        /// 批量逻辑删除
        /// </summary>
        /// <param name="ids">主键ID集合</param>
        /// <returns></returns>
        public async Task<bool> DeleteAsync(IEnumerable<string> ids)
        {
            var dbSet = _context.Menus;
            var entities = await dbSet.Where(item => ids.Contains(item.Id)).ToListAsync();
            foreach (var menuEntity in entities)
            {
                menuEntity.IsDeleted = true;
            }
            return await _context.SaveChangesAsync() > 0;
        }

        /// <summary>
        /// 分页搜索
        /// </summary>
        /// <param name="filters">查询过滤参数</param>
        /// <returns></returns>
        public async Task<PagedResult<MenuDto>> SearchAsync(MenuFilters filters)
        {
            if (filters == null)
                return new PagedResult<MenuDto>();

            var query = _context.Menus.AsQueryable();

            if (filters.keywords.IsNotBlank())
                query = query.Where(x => x.Name.Contains(filters.keywords));
            if (filters.ExcludeType.HasValue)
                query = query.Where(x => x.Type != filters.ExcludeType.Value);

            return await query.OrderByDescending(item => item.CreateDateTime)
                .Select(item => new MenuDto
                {
                    Id = item.Id,
                    ParentId = item.ParentId,
                    Name = item.Name,
                    Url = item.Url,
                    Order = item.Order,
                    Type = (MenuType)item.Type
                }).PagingAsync(filters.page, filters.rows);
        }

        /// <summary>
        /// 获取用户拥有的权限菜单（不包含按钮）
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <returns></returns>
        public async Task<List<MenuDto>> GetMyMenusAsync(string userId)
        {
            var query = _context.Menus.Where(x => x.Type != MenuType.Action);
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == userId);
            if (user == null)
            {
                return new List<MenuDto>();
            }
            var dbSetUserRoles = _context.UserRoles;
            var dbSetRoleMenus = _context.RoleMenus;
            var roleIds = await dbSetUserRoles.Where(x => x.UserId == userId)
                .Select(x => x.RoleId).ToListAsync();
            var menuIds = await dbSetRoleMenus.Where(x => roleIds.Contains(x.RoleId))
                .Select(x => x.MenuId)
                .ToListAsync();
            //如果是超级管理员,则默认有所有的权限
            return await query.WhereIf(user.IsSuperMan == false, x => menuIds.Contains(x.Id))
                .Select(x => new MenuDto
                {
                    Id = x.Id,
                    ParentId = x.ParentId,
                    Name = x.Name,
                    Url = x.Url,
                    Order = x.Order,
                    Type = (MenuType)x.Type,
                    Icon = x.Icon
                }).ToListAsync();
        }

        /// <summary>
        /// 获取菜单树
        /// </summary>
        /// <returns></returns>
        public async Task<List<TreeDto>> GetTreesAsync()
        {
            var list = await _context.Menus.ToListAsync();
            return _mapper.Map<List<MenuEntity>, List<TreeDto>>(list);
        }

        /// <summary>
        /// 获取菜单树
        /// </summary>
        /// <returns></returns>
        public async Task<List<MenuDto>> GetMenusByRoleIdAsync(string roleId)
        {
            var list = await _context.Menus
                .Join(_context.RoleMenus, m => m.Id, rm => rm.MenuId, (menu, roleMenu) => new { menu, roleMenu })
                .Where(item => item.roleMenu.RoleId == roleId)
                .Select(item => item.menu).ToListAsync();
            return _mapper.Map<List<MenuEntity>, List<MenuDto>>(list);
        }

        /// <summary>
        /// 重置系统的所有菜单
        /// </summary>
        /// <returns></returns>
        public async Task<bool> ReInitMenuesAsync(List<MenuDto> list)
        {
            if (!list.AnyOne())
            {
                return false;
            }

            //删除以前所有的菜单和菜单的角色关系
            var oldMenues = await _context.Menus.ToListAsync();
            oldMenues.ForEach(x => x.IsDeleted = true);
            var oldMenuRoles = await _context.RoleMenus.ToListAsync();
            _context.RoleMenus.RemoveRange(oldMenuRoles);
            //删除已经存在的相同id的菜单
            var ids = list.Select(x => x.Id).Distinct().ToList();
            var sameIdMenus = await _context.Menus.Where(x => ids.Contains(x.Id)).ToListAsync();
            _context.Menus.RemoveRange(sameIdMenus);
            await _context.SaveChangesAsync();
            //重置新的菜单
            var moduleIds = list.Where(x => x.ParentId.IsBlank()).Select(x => x.Id);
            var menues = _mapper.Map<List<MenuDto>, List<MenuEntity>>(list);
            foreach(var menu in menues)
            {
                //设置菜单类型
                if (menu.ParentId.IsBlank())
                {
                    menu.Type = MenuType.Module;
                }
                else if (moduleIds.Contains(menu.ParentId))
                {
                    menu.Type = MenuType.Menu;
                }
                else
                {
                    menu.Type = MenuType.Action;
                }

                //设置菜单的路径(层级关系) 父类的PathCode+当前类别的Id
                var parent = menues.FirstOrDefault(x => x.Id == menu.ParentId);
                menu.Code = menu.Id;
                if (parent == null)
                {
                    menu.PathCode = menu.Id;
                }
                else
                {
                    menu.PathCode = $"{parent.PathCode}-{menu.Id}";
                }
                if (menu.Url.IsBlank())
                {
                    menu.Url = "#";
                }
                menu.CreateDateTime = DateTime.Now;
            }
            _context.Menus.AddRange(menues);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
