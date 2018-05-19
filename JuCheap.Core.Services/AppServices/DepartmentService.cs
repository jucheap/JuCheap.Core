using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using JuCheap.Core.Data;
using JuCheap.Core.Data.Entity;
using JuCheap.Core.Infrastructure;
using JuCheap.Core.Infrastructure.Exceptions;
using JuCheap.Core.Infrastructure.Extentions;
using JuCheap.Core.Interfaces;
using JuCheap.Core.Models;
using JuCheap.Core.Models.Filters;
using Microsoft.EntityFrameworkCore;

namespace JuCheap.Core.Services.AppServices
{
    /// <summary>
    /// 部门契约服务
    /// </summary>
    public class DepartmentService : IDepartmentService
    {
        private readonly JuCheapContext _context;
        private readonly IMapper _mapper;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="dbContextScopeFactory"></param>
        /// <param name="mapper"></param>
        public DepartmentService(JuCheapContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        /// <summary>
        /// 添加部门
        /// </summary>
        /// <param name="dto">部门模型</param>
        /// <returns></returns>
        public async Task<string> Add(DepartmentDto dto)
        {
            var entity = _mapper.Map<DepartmentDto, DepartmentEntity>(dto);
            entity.Init();
            await SetDepartment(entity);
            _context.Departments.Add(entity);
            await _context.SaveChangesAsync();
            return entity.Id;
        }

        /// <summary>
        /// 更新部门
        /// </summary>
        /// <param name="dto">部门模型</param>
        /// <returns></returns>
        public async Task<bool> Update(DepartmentDto dto)
        {
            var entity = await _context.Departments.FindAsync(dto.Id);
            _mapper.Map(dto, entity);
            if (entity.ParentId.IsNotBlank())
            {
                var parent = await _context.Departments.FindAsync(entity.ParentId);
                entity.FullName = string.Format("{0}-{1}", parent.FullName, entity.Name);
            }
            else
            {
                entity.FullName = entity.Name;
            }
            await _context.SaveChangesAsync();
            return true;
        }

        /// <summary>
        /// 根据主键查询模型
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns></returns>
        public async Task<DepartmentDto> Find(string id)
        {
            if (id.IsBlank())
            {
                return null;
            }
            var entity = await _context.Departments.FindAsync(id);
            var result = _mapper.Map<DepartmentEntity, DepartmentDto>(entity);
            if (result?.ParentId.IsBlank() == false)
            {
                var parent = await _context.Departments.FindAsync(entity.ParentId);
                result.ParentName = parent.FullName;
            }
            return result;
        }

        /// <summary>
        /// 根据父部门ID查询
        /// </summary>
        /// <param name="parentId">父部门ID</param>
        /// <returns></returns>
        public async Task<IList<TreeDto>> FindByParentId(string parentId)
        {
            var query = _context.Departments
                .WhereIf(parentId.IsBlank(), x => x.ParentId == null || x.ParentId == string.Empty)
                .WhereIf(parentId.IsNotBlank(), x => x.ParentId == parentId);
            return await query.Select(x => new TreeDto
            {
                id = x.Id,
                name = x.Name,
                pId = x.ParentId,
                isParent = _context.Departments.Any(c => c.ParentId == x.Id)
            }).ToListAsync();
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="ids">主键ID集合</param>
        /// <returns></returns>
        public async Task<bool> Delete(IList<string> ids)
        {
            var departments = _context.Departments.Where(x => ids.Contains(x.Id)).ToList();

            if (!departments.AnyOne())
            {
                throw new BusinessException("没有找到相关的部门数据");
            }
            if (await _context.Users.AnyAsync(x => ids.Contains(x.DepartmentId)))
            {
                throw new BusinessException("该部门下还存在用户，请先将用户移除该部门");
            }
            //标记成逻辑删除
            foreach (var department in departments)
            {
                department.IsDeleted = true;
            }
            await _context.SaveChangesAsync();
            return true;
        }

        /// <summary>
        /// 分页搜索
        /// </summary>
        /// <param name="filters">查询过滤参数</param>
        /// <returns></returns>
        public async Task<PagedResult<DepartmentDto>> Search(BaseFilter filters)
        {
            if (filters == null)
                return new PagedResult<DepartmentDto>();

            var query = _context.Departments
                    .WhereIf(filters.keywords.IsNotBlank(),
                        x => x.Name.Contains(filters.keywords) || x.FullName.Contains(filters.keywords));

            return await query.OrderByDescending(x => x.CreateDateTime)
                .Select(x => new DepartmentDto
                {
                    Id = x.Id,
                    ParentId = x.ParentId,
                    Name = x.Name,
                    FullName = x.FullName
                }).PagingAsync(filters.page, filters.rows);
        }

        /// <summary>
        /// 获取路径码
        /// </summary>
        private async Task SetDepartment(DepartmentEntity dept)
        {
            //顶级页面
            List<string> existCodes;
            var parentPathCode = string.Empty;
            var parentId = dept.ParentId;
            if (parentId.IsBlank())
            {
                var list = await _context.Departments
                    .Where(x => (x.ParentId == null || x.ParentId == string.Empty))
                    .Select(x => x.PathCode).ToListAsync();
                existCodes = list.Select(x => x.Trim()).ToList();
                dept.FullName = dept.Name;
            }
            else
            {
                var department = await _context.Departments.FindAsync(parentId);
                parentPathCode = department.PathCode;

                var list = await _context.Departments.Where(x => x.ParentId == parentId && x.PathCode != string.Empty)
                    .Select(x => x.PathCode).ToListAsync();
                existCodes = list.Select(x => x.Substring(department.PathCode.Trim().Length, 2)).ToList();
                dept.FullName = string.Format("{0}-{1}", department.FullName, dept.Name);
            }
            var pathCode = await _context.PathCodes
                .OrderBy(x => x.Code)
                .FirstOrDefaultAsync(x => !existCodes.Contains(x.Code));
            dept.PathCode = parentPathCode.Trim() + pathCode.Code.Trim();
        }
    }
}
