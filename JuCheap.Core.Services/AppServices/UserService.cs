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
        public UserService(JuCheapContext context)
        {
            _context = context;
            _mapper = AutoMapperConfig.GetMapperConfiguration().CreateMapper();
        }

        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public int Add(UserAddDto user)
        {
            var entity = _mapper.Map<UserAddDto, UserEntity>(user);
            if (entity.UserRoles.AnyOne())
            {
                entity.UserRoles.ForEach(r => r.UserId = entity.Id);
            }
            entity.Password = entity.Password.ToMd5();
            var dbSet = _context.Users;
            dbSet.Add(entity);

            return _context.SaveChanges() > 0 ? entity.Id : 0;
        }

        /// <summary>
        /// 更新菜单
        /// </summary>
        /// <param name="dto">菜单模型</param>
        /// <returns></returns>
        public bool Update(UserUpdateDto dto)
        {
            var dbSet = _context.Users;
            var entity = dbSet.FirstOrDefault(u => u.Id == dto.Id);
            entity.LoginName = dto.LoginName;
            entity.RealName = dto.RealName;
            entity.Email = dto.Email;
            if (dto.Password.IsNotBlank())
                entity.Password = dto.Password.ToMd5();
            //_mapper.Map(dto, entity);
            return _context.SaveChanges() > 0;
        }

        /// <summary>
        /// 根据主键查询模型
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns></returns>
        public UserDto Find(int id)
        {
            var dbSet = _context.Users;
            var entity = dbSet.FirstOrDefault(u => u.Id == id);
            var dto = _mapper.Map<UserEntity, UserDto>(entity);
            return dto;
        }

        /// <summary>
        /// 批量逻辑删除
        /// </summary>
        /// <param name="ids">主键ID集合</param>
        /// <returns></returns>
        public bool Delete(IEnumerable<int> ids)
        {
            var dbSet = _context.Users;
            var entities = dbSet.Where(item => ids.Contains(item.Id));
            entities.ForEach(item => item.IsDeleted = true);
            return _context.SaveChanges() > 0;
        }

        /// <summary>
        /// 登陆
        /// </summary>
        /// <param name="dto">登录信息</param>
        /// <returns></returns>
        public UserLoginDto Login(LoginDto dto)
        {
            var reslt = new UserLoginDto();
            var dbSet = _context.Users;
            var logDbSet = _context.LoginLog;
            var entity = dbSet.FirstOrDefault(item => item.LoginName == dto.LoginName.Trim());
            var loginLog = new LoginLogEntity
            {
                LoginName = dto.LoginName,
                IP = dto.LoginIP
            };
            if (entity == null)
            {
                reslt.Message = "账号不存在";
                reslt.Result = LoginResult.AccountNotExists;
                loginLog.UserId = "0";
            }
            else
            {
                if (entity.Password == dto.Password.ToMd5())
                {
                    reslt.LoginSuccess = true;
                    reslt.Message = "登陆成功";
                    reslt.Result = LoginResult.Success;
                    reslt.User = _mapper.Map<UserEntity, UserDto>(entity);
                }
                else
                {
                    reslt.Message = "登陆密码错误";
                    reslt.Result = LoginResult.WrongPassword;
                }
                loginLog.UserId = entity.Id.ToString();
            }
            loginLog.Mac = reslt.Message;
            logDbSet.Add(loginLog);
            _context.SaveChanges();
            return reslt;
        }

        /// <summary>
        /// 用户角色授权
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="roleId">角色ID</param>
        /// <returns></returns>
        public bool Give(int userId, int roleId)
        {
            var dbSet = _context.UserRole;
            if (dbSet.Any(item => item.UserId == userId && item.RoleId == roleId))
                return true;
            dbSet.Add(new UserRoleEntity
            {
                UserId = userId,
                RoleId = roleId
            });
            return _context.SaveChanges() > 0;
        }

        /// <summary>
        /// 用户角色取消
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="roleId">角色ID</param>
        /// <returns></returns>
        public bool Cancel(int userId, int roleId)
        {
            var dbSet = _context.UserRole;
            var userRole = dbSet.FirstOrDefault(item => item.UserId == userId && item.RoleId == roleId);
            dbSet.Remove(userRole);
            return _context.SaveChanges() > 0;
        }

        /// <summary>
        /// 分页搜索
        /// </summary>
        /// <param name="filters">查询过滤参数</param>
        /// <returns></returns>
        public PagedResult<UserDto> Search(UserFilters filters)
        {
            if (filters == null)
                return new PagedResult<UserDto>(1, 0);

            var dbSet = _context.Users;
            var query = dbSet.Where(item => !item.IsDeleted);

            if (filters.keywords.IsNotBlank())
                query =
                    query.Where(item => item.LoginName.Contains(filters.keywords) ||
                                        item.RealName.Contains(filters.keywords));

            return query.OrderBy(item => item.CreateDateTime)
                .Select(item => new UserDto
                {
                    Id = item.Id,
                    LoginName = item.LoginName,
                    RealName = item.RealName,
                    Email = item.Email,
                    CreateDateTime = item.CreateDateTime
                }).Paging(filters.page, filters.rows);
        }

        /// <summary>
        /// 是否拥有此权限
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="url">url地址</param>
        /// <returns></returns>
        public bool HasRight(int userId, string url)
        {
            var dbSet = _context.Menu;
            var dbSetUserRoles = _context.UserRole;
            var dbSetRoleMenus = _context.RoleMenu;
            var query = dbSet.Where(item => !item.IsDeleted);
            var roleIds = dbSetUserRoles.Where(item => item.UserId == userId)
                .Select(item => item.RoleId).ToList();
            var menuIds = dbSetRoleMenus.Where(item => roleIds.Contains(item.RoleId))
                .Select(item => item.MenuId)
                .ToList();
            return query.Any(item => menuIds.Contains(item.Id) && item.Url.StartsWith(url));
        }

        /// <summary>
        /// 记录访问记录
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public bool Visit(VisitDto dto)
        {
            var dbSet = _context.PageView;
            var entity = _mapper.Map<VisitDto, PageViewEntity>(dto);
            dbSet.Add(entity);
            return _context.SaveChanges() > 0;
        }

        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<int> AddAsync(UserAddDto user)
        {
            var entity = _mapper.Map<UserAddDto, UserEntity>(user);
            if (entity.UserRoles.AnyOne())
            {
                entity.UserRoles.ForEach(r => r.UserId = entity.Id);
            }
            entity.Password = entity.Password.ToMd5();
            var dbSet = _context.Users;
            dbSet.Add(entity);

            return await _context.SaveChangesAsync() > 0 ? entity.Id : 0;
        }

        /// <summary>
        /// 更新菜单
        /// </summary>
        /// <param name="dto">菜单模型</param>
        /// <returns></returns>
        public async Task<bool> UpdateAsync(UserUpdateDto dto)
        {
            var dbSet = _context.Users;
            var entity = dbSet.FirstOrDefault(u => u.Id == dto.Id);
            entity.LoginName = dto.LoginName;
            entity.RealName = dto.RealName;
            entity.Email = dto.Email;
            if (dto.Password.IsNotBlank())
                entity.Password = dto.Password.ToMd5();
            //_mapper.Map(dto, entity);
            return await _context.SaveChangesAsync() > 0;
        }

        /// <summary>
        /// 根据主键查询模型
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns></returns>
        public async Task<UserDto> FindAsync(int id)
        {
            var dbSet = _context.Users;
            var entity = await dbSet.FirstOrDefaultAsync(u => u.Id == id);
            var dto = _mapper.Map<UserEntity, UserDto>(entity);
            return dto;
        }

        /// <summary>
        /// 批量逻辑删除
        /// </summary>
        /// <param name="ids">主键ID集合</param>
        /// <returns></returns>
        public async Task<bool> DeleteAsync(IEnumerable<int> ids)
        {
            var dbSet = _context.Users;
            var entities = dbSet.Where(item => ids.Contains(item.Id));
            entities.ForEach(item => item.IsDeleted = true);
            return await _context.SaveChangesAsync() > 0;
        }

        /// <summary>
        /// 登陆
        /// </summary>
        /// <param name="dto">登录信息</param>
        /// <returns></returns>
        public async Task<UserLoginDto> LoginAsync(LoginDto dto)
        {
            var reslt = new UserLoginDto();
            var dbSet = _context.Users;
            var logDbSet = _context.LoginLog;
            var entity = await dbSet.FirstOrDefaultAsync(item => item.LoginName == dto.LoginName.Trim());
            var loginLog = new LoginLogEntity
            {
                LoginName = dto.LoginName,
                IP = dto.LoginIP
            };
            if (entity == null)
            {
                reslt.Message = "账号不存在";
                reslt.Result = LoginResult.AccountNotExists;
                loginLog.UserId = "0";
            }
            else
            {
                if (entity.Password == dto.Password.ToMd5())
                {
                    reslt.LoginSuccess = true;
                    reslt.Message = "登陆成功";
                    reslt.Result = LoginResult.Success;
                    reslt.User = _mapper.Map<UserEntity, UserDto>(entity);
                }
                else
                {
                    reslt.Message = "登陆密码错误";
                    reslt.Result = LoginResult.WrongPassword;
                }
                loginLog.UserId = entity.Id.ToString();
            }
            loginLog.Mac = reslt.Message;
            logDbSet.Add(loginLog);
            await _context.SaveChangesAsync();
            return reslt;
        }

        /// <summary>
        /// 用户角色授权
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="roleId">角色ID</param>
        /// <returns></returns>
        public async Task<bool> GiveAsync(int userId, int roleId)
        {
            var dbSet = _context.UserRole;
            if (await dbSet.AnyAsync(item => item.UserId == userId && item.RoleId == roleId))
                return true;
            dbSet.Add(new UserRoleEntity
            {
                UserId = userId,
                RoleId = roleId
            });
            return await _context.SaveChangesAsync() > 0;
        }

        /// <summary>
        /// 用户角色取消
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="roleId">角色ID</param>
        /// <returns></returns>
        public async Task<bool> CancelAsync(int userId, int roleId)
        {
            var dbSet = _context.UserRole;
            var userRole = await dbSet.FirstOrDefaultAsync(item => item.UserId == userId && item.RoleId == roleId);
            dbSet.Remove(userRole);
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

            var dbSet = _context.Users;
            var query = dbSet.Where(item => !item.IsDeleted);

            if (filters.keywords.IsNotBlank())
                query =
                    query.Where(item => item.LoginName.Contains(filters.keywords) ||
                                        item.RealName.Contains(filters.keywords));

            return await query.OrderBy(item => item.CreateDateTime)
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
        /// 是否拥有此权限
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="url">url地址</param>
        /// <returns></returns>
        public async Task<bool> HasRightAsync(int userId, string url)
        {
            var dbSet = _context.Menu;
            var dbSetUserRoles = _context.UserRole;
            var dbSetRoleMenus = _context.RoleMenu;
            var query = dbSet.Where(item => !item.IsDeleted);
            var roleIds = await dbSetUserRoles.Where(item => item.UserId == userId)
                .Select(item => item.RoleId).ToListAsync();
            var menuIds = await dbSetRoleMenus.Where(item => roleIds.Contains(item.RoleId))
                .Select(item => item.MenuId)
                .ToListAsync();
            return await query.AnyAsync(item => menuIds.Contains(item.Id) && item.Url.StartsWith(url));
        }

        /// <summary>
        /// 记录访问记录
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<bool> VisitAsync(VisitDto dto)
        {
            var dbSet = _context.PageView;
            var entity = _mapper.Map<VisitDto, PageViewEntity>(dto);
            dbSet.Add(entity);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
