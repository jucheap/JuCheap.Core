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
using Microsoft.EntityFrameworkCore;

namespace JuCheap.Core.Services.AppServices
{
    /// <summary>
    /// 菜单契约服务
    /// </summary>
    public class MenuService : IMenuService
    {
        private readonly JuCheapContext _context;
        private readonly IMapper _mapper;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="context"></param>
        public MenuService(JuCheapContext context)
        {
            _context = context;
            _mapper = AutoMapperConfig.GetMapperConfiguration().CreateMapper();
        }

        /// <summary>
        /// 添加菜单
        /// </summary>
        /// <param name="dto">菜单模型</param>
        /// <returns></returns>
        public int Add(MenuDto dto)
        {
            var entity = _mapper.Map<MenuDto, MenuEntity>(dto);
            var dbSet = _context.Menu;
            var pathCodeDbSet = _context.PathCodes;

            var existsCode = dbSet.Where(item => item.ParentId == dto.ParentId)
                .Select(item => item.Code).ToList();
            var pathCode = pathCodeDbSet.FirstOrDefault(item => !existsCode.Contains(item.Code));
            entity.Code = pathCode.Code.Trim();
            if (entity.ParentId > 0)
            {
                var parent = dbSet.FirstOrDefault(m => m.Id == entity.ParentId);
                entity.PathCode = string.Concat(parent.PathCode.Trim(), entity.Code.Trim());
                entity.Type = parent.Type == 1 ? (byte)MenuType.Menu : (byte)MenuType.Button;
            }
            else
            {
                entity.PathCode = entity.Code.Trim();
                entity.Type = (byte)MenuType.Module;
            }
            dbSet.Add(entity);

            return _context.SaveChanges() > 0 ? entity.Id : 0;
        }

        /// <summary>
        /// 更新菜单
        /// </summary>
        /// <param name="dto">菜单模型</param>
        /// <returns></returns>
        public bool Update(MenuDto dto)
        {
            var dbSet = _context.Menu;

            var entity = dbSet.FirstOrDefault(m => m.Id == dto.Id);
            entity.Name = dto.Name;
            entity.Url = dto.Url;
            entity.Order = dto.Order;

            return _context.SaveChanges() > 0;
        }

        /// <summary>
        /// 根据主键查询模型
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns></returns>
        public MenuDto Find(int id)
        {
            var dbSet = _context.Menu;
            var entity = dbSet.FirstOrDefault(m => m.Id == id);
            var dto = _mapper.Map<MenuEntity, MenuDto>(entity);
            if (dto.ParentId > 0)
            {
                var parent = dbSet.FirstOrDefault(m => m.Id == dto.ParentId);
                dto.ParentName = parent.Name;
            }
            return dto;
        }

        /// <summary>
        /// 批量逻辑删除
        /// </summary>
        /// <param name="ids">主键ID集合</param>
        /// <returns></returns>
        public bool Delete(IEnumerable<int> ids)
        {
            var dbSet = _context.Menu;
            var entities = dbSet.Where(item => ids.Contains(item.Id)).ToList();
            foreach (var menuEntity in entities)
            {
                menuEntity.IsDeleted = true;
            }
            return _context.SaveChanges() > 0;
        }

        /// <summary>
        /// 分页搜索
        /// </summary>
        /// <param name="filters">查询过滤参数</param>
        /// <returns></returns>
        public PagedResult<MenuDto> Search(MenuFilters filters)
        {
            if (filters == null)
                return new PagedResult<MenuDto>();

            var dbSet = _context.Menu;
            var query = dbSet.Where(item => !item.IsDeleted);

            if (filters.keywords.IsNotBlank())
                query = query.Where(item => item.Name.Contains(filters.keywords));
            if (filters.ExcludeType.HasValue)
                query = query.Where(item => item.Type != (byte)filters.ExcludeType.Value);

            return query.OrderBy(item => item.CreateDateTime)
                .Select(item => new MenuDto
                {
                    Id = item.Id,
                    ParentId = item.ParentId,
                    Name = item.Name,
                    Url = item.Url,
                    Order = item.Order,
                    Type = (MenuType)item.Type
                }).Paging(filters.page, filters.rows);
        }

        /// <summary>
        /// 获取用户拥有的权限菜单（不包含按钮）
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <returns></returns>
        public List<MenuDto> GetMyMenus(int userId)
        {
            var dbSet = _context.Menu;
            var dbSetUserRoles = _context.UserRole;
            var dbSetRoleMenus = _context.RoleMenu;
            var query = dbSet.Where(item => !item.IsDeleted && item.Type != (byte)MenuType.Button);
            var roleIds = dbSetUserRoles.Where(item => item.UserId == userId)
                .Select(item => item.RoleId).ToList();
            var menuIds = dbSetRoleMenus.Where(item => roleIds.Contains(item.RoleId))
                .Select(item => item.MenuId)
                .ToList();
            return query.Where(item => menuIds.Contains(item.Id))
                .Select(item => new MenuDto
                {
                    Id = item.Id,
                    ParentId = item.ParentId,
                    Name = item.Name,
                    Url = item.Url,
                    Order = item.Order,
                    Type = (MenuType)item.Type
                }).ToList();
        }

        /// <summary>
        /// 添加菜单
        /// </summary>
        /// <param name="dto">菜单模型</param>
        /// <returns></returns>
        public async Task<int> AddAsync(MenuDto dto)
        {
            var entity = _mapper.Map<MenuDto, MenuEntity>(dto);
            var dbSet = _context.Menu;
            var pathCodeDbSet = _context.PathCodes;

            var existsCode = await dbSet.Where(item => item.ParentId == dto.ParentId)
                .Select(item => item.Code).ToListAsync();
            var pathCode = await pathCodeDbSet.FirstOrDefaultAsync(item => !existsCode.Contains(item.Code));
            entity.Code = pathCode.Code.Trim();
            if (entity.ParentId > 0)
            {
                var parent = await dbSet.FirstOrDefaultAsync(m => m.Id == entity.ParentId);
                entity.PathCode = string.Concat(parent.PathCode.Trim(), entity.Code.Trim());
                entity.Type = parent.Type == 1 ? (byte)MenuType.Menu : (byte)MenuType.Button;
            }
            else
            {
                entity.PathCode = entity.Code.Trim();
                entity.Type = (byte)MenuType.Module;
            }
            dbSet.Add(entity);

            return await _context.SaveChangesAsync() > 0 ? entity.Id : 0;
        }

        /// <summary>
        /// 更新菜单
        /// </summary>
        /// <param name="dto">菜单模型</param>
        /// <returns></returns>
        public async Task<bool> UpdateAsync(MenuDto dto)
        {
            var dbSet = _context.Menu;

            var entity = await dbSet.FirstOrDefaultAsync(m => m.Id == dto.Id);
            entity.Name = dto.Name;
            entity.Url = dto.Url;
            entity.Order = dto.Order;

            return await _context.SaveChangesAsync() > 0;
        }

        /// <summary>
        /// 根据主键查询模型
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns></returns>
        public async Task<MenuDto> FindAsync(int id)
        {
            var dbSet = _context.Menu;
            var entity = await dbSet.FirstOrDefaultAsync(m => m.Id == id);
            var dto = _mapper.Map<MenuEntity, MenuDto>(entity);
            if (dto.ParentId > 0)
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
        public async Task<bool> DeleteAsync(IEnumerable<int> ids)
        {
            var dbSet = _context.Menu;
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

            var dbSet = _context.Menu;
            var query = dbSet.Where(item => !item.IsDeleted);

            if (filters.keywords.IsNotBlank())
                query = query.Where(item => item.Name.Contains(filters.keywords));
            if (filters.ExcludeType.HasValue)
                query = query.Where(item => item.Type != (byte)filters.ExcludeType.Value);

            return await query.OrderBy(item => item.CreateDateTime)
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
        public async Task<List<MenuDto>> GetMyMenusAsync(int userId)
        {
            var dbSet = _context.Menu;
            var dbSetUserRoles = _context.UserRole;
            var dbSetRoleMenus = _context.RoleMenu;
            var query = dbSet.Where(item => !item.IsDeleted && item.Type != (byte)MenuType.Button);
            var roleIds = await dbSetUserRoles.Where(item => item.UserId == userId)
                .Select(item => item.RoleId).ToListAsync();
            var menuIds = await dbSetRoleMenus.Where(item => roleIds.Contains(item.RoleId))
                .Select(item => item.MenuId)
                .ToListAsync();
            return await query.Where(item => menuIds.Contains(item.Id))
                .Select(item => new MenuDto
                {
                    Id = item.Id,
                    ParentId = item.ParentId,
                    Name = item.Name,
                    Url = item.Url,
                    Order = item.Order,
                    Type = (MenuType)item.Type
                }).ToListAsync();
        }
    }
}
