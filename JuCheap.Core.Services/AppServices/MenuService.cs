using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using JuCheap.Core.Data;
using JuCheap.Core.Data.Entity;
using JuCheap.Core.Infrastructure;
using JuCheap.Core.Interfaces;
using JuCheap.Core.Models;
using JuCheap.Core.Models.Enum;
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
                entity.Type = parent.Type == 1 ? (byte)MenuType.Menu : (byte)MenuType.Action;
            }
            else
            {
                entity.PathCode = entity.Code.Trim();
                entity.Type = (byte)MenuType.Module;
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

            var dbSet = _context.Menus;
            var query = dbSet.Where(item => !item.IsDeleted);

            if (filters.keywords.IsNotBlank())
                query = query.Where(item => item.Name.Contains(filters.keywords));
            if (filters.ExcludeType.HasValue)
                query = query.Where(item => item.Type != (byte)filters.ExcludeType.Value);

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
            var dbSet = _context.Menus;
            var dbSetUserRoles = _context.UserRoles;
            var dbSetRoleMenus = _context.RoleMenus;
            var query = dbSet.Where(x => !x.IsDeleted && x.Type != (byte)MenuType.Action);
            var roleIds = await dbSetUserRoles.Where(x => x.UserId == userId)
                .Select(x => x.RoleId).ToListAsync();
            var menuIds = await dbSetRoleMenus.Where(x => roleIds.Contains(x.RoleId))
                .Select(x => x.MenuId)
                .ToListAsync();
            return await query.Where(x => menuIds.Contains(x.Id))
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
            var list = await _context.Menus.Where(m => !m.IsDeleted).ToListAsync();
            return _mapper.Map<List<MenuEntity>, List<TreeDto>>(list);
        }

        /// <summary>
        /// 获取菜单树
        /// </summary>
        /// <returns></returns>
        public async Task<List<MenuDto>> GetMenusByRoleIdAsync(string roleId)
        {
            var list = await _context.Menus.Where(m => !m.IsDeleted)
                .Join(_context.RoleMenus, m => m.Id, rm => rm.MenuId, (menu, roleMenu) => new { menu, roleMenu })
                .Where(item => item.roleMenu.RoleId == roleId)
                .Select(item => item.menu).ToListAsync();
            return _mapper.Map<List<MenuEntity>, List<MenuDto>>(list);
        }
    }
}
