using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using JuCheap.Core.Data;
using JuCheap.Core.Data.Entity;
using JuCheap.Core.Infrastructure;
using JuCheap.Core.Interfaces;
using JuCheap.Core.Models;
using JuCheap.Core.Models.Filters;
using JuCheap.Core.Infrastructure.Extentions;
using Microsoft.EntityFrameworkCore;
using System;

namespace JuCheap.Core.Services.AppServices
{
    /// <summary>
    /// 用户实现类
    /// </summary>
    public class UserService : IUserService
    {
        private readonly JuCheapContext _context;
        private readonly IMapper _mapper;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="context"></param>
        /// <param name="mapper"></param>
        public UserService(JuCheapContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<Guid> AddAsync(UserAddDto user)
        {
            var entity = _mapper.Map<UserAddDto, UserEntity>(user);
            entity.Init();
            entity.Password = entity.Password.ToMd5();
            _context.Users.Add(entity);

            return await _context.SaveChangesAsync() > 0 ? entity.Id : Guid.Empty;
        }

        /// <summary>
        /// 更新菜单
        /// </summary>
        /// <param name="dto">菜单模型</param>
        /// <returns></returns>
        public async Task<bool> UpdateAsync(UserUpdateDto dto)
        {
            var entity = _context.Users.FirstOrDefault(u => u.Id == dto.Id);
            entity.LoginName = dto.LoginName;
            entity.RealName = dto.RealName;
            entity.Email = dto.Email;
            if (dto.Password.IsNotBlank())
                entity.Password = dto.Password.ToMd5();
            return await _context.SaveChangesAsync() > 0;
        }

        /// <summary>
        /// 根据主键查询模型
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns></returns>
        public async Task<UserDto> FindAsync(Guid id)
        {
            var entity = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
            var dto = _mapper.Map<UserEntity, UserDto>(entity);
            return dto;
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public async Task<UserDto> LoginAsync(string userName, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.LoginName == userName);
            if (user == null)
            {
                throw new Exception("该用户不存在");
            }
            if (user.Password != password.ToMd5())
            {
                throw new Exception("登录密码不正确");
            }
            return _mapper.Map<UserDto>(user);
        }

        /// <summary>
        /// 批量逻辑删除
        /// </summary>
        /// <param name="ids">主键ID集合</param>
        /// <returns></returns>
        public async Task<bool> DeleteAsync(IEnumerable<Guid> ids)
        {
            var entities = _context.Users.Where(item => ids.Contains(item.Id));
            entities.ForEach(item => item.IsDeleted = true);
            return await _context.SaveChangesAsync() > 0;
        }

        /// <summary>
        /// 分页搜索
        /// </summary>
        /// <param name="filters">查询过滤参数</param>
        /// <returns></returns>
        public async Task<PagedResult<UserDto>> SearchAsync(UserFilters filters)
        {
            if (filters == null)
                return new PagedResult<UserDto>(1, 0);

            var query = _context.Users.Where(item => !item.IsDeleted);

            if (filters.keywords.IsNotBlank())
                query =
                    query.Where(item => item.LoginName.Contains(filters.keywords) ||
                                        item.RealName.Contains(filters.keywords));

            return await query.OrderByDescending(item => item.CreateDateTime)
                .Select(item => new UserDto
                {
                    Id = item.Id,
                    LoginName = item.LoginName,
                    RealName = item.RealName,
                    Email = item.Email,
                    CreateDateTime = item.CreateDateTime
                }).PagingAsync(filters.page, filters.rows);
        }

        /// <summary>
        /// 验证此用户是否有指定client应用的授权
        /// </summary>
        /// <param name="clientId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<bool> HasClient(string clientId, Guid userId)
        {
            var client = await _context.Apps.FirstOrDefaultAsync(x => x.ClientId == clientId);
            return client.UserId == userId;
        }
    }
}
