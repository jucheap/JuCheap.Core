using AutoMapper;
using AutoMapper.QueryableExtensions;
using JuCheap.Core.Data;
using JuCheap.Core.Data.Entity;
using JuCheap.Core.Infrastructure;
using JuCheap.Core.Infrastructure.Extentions;
using JuCheap.Core.Interfaces;
using JuCheap.Core.Models;
using JuCheap.Core.Models.Filters;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JuCheap.Core.Services.AppServices
{
    /// <summary>
    /// 站内信服务
    /// </summary>
    public class MessageService : IMessageService
    {
        private readonly JuCheapContext _context;
        private readonly IMapper _mapper;
        private readonly IConfigurationProvider _configurationProvider;

        public MessageService(JuCheapContext context, IMapper mapper, IConfigurationProvider configurationProvider)
        {
            _context = context;
            _mapper = mapper;
            _configurationProvider = configurationProvider;
        }

        /// <summary>
        /// 发送站内信
        /// </summary>
        /// <param name="message"></param>
        public async Task Send(MessageDto message)
        {
            if (message == null)
            {
                throw new ArgumentNullException(nameof(message));
            }            
            var entity = _mapper.Map<MessageDto, MessageEntity>(message);
            var userIds = new List<string>();
            if (message.IsToAll)
            {
                var ids = await _context.Users.Select(x => x.Id).ToListAsync();
                userIds.AddRange(ids);
            }
            else
            {
                userIds.AddRange(message.UserIds);
            }
            entity.MessageReceivers = userIds.Distinct().Select(userId => new MessageReceiverEntity
            {
                UserId = userId
            }).ToList();
            entity.MessageReceivers.ForEach(x => x.Init());
            entity.Init();
            entity.SetNumber();
            await _context.Messages.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// 获取站内信
        /// </summary>
        /// <param name="id">站内信Id</param>
        /// <param name="userId">用户Id</param>
        /// <returns></returns>
        public async Task<MessageDto> GetMessage(string id, string userId)
        {
            var query = _context.Messages.Where(x => x.Id == id)
                .WhereIf(userId.IsNotBlank(), x => x.MessageReceivers.Any(m => m.UserId == userId));
            var message = await query.FirstOrDefaultAsync();
            return _mapper.Map<MessageDto>(message);
        }

        /// <summary>
        /// 查看站内信
        /// </summary>
        /// <param name="id">站内信Id</param>
        /// <param name="userId">用户Id</param>
        public async Task Read(string id, string userId)
        {
            var message = await _context.Messages.FindAsync(id);
            if (message != null)
            {
                var receiver = message.MessageReceivers.FirstOrDefault(x => x.UserId == userId);
                receiver?.Read();
            }
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// 获取站内信列表
        /// </summary>
        /// <param name="filters">过滤器</param>
        /// <returns></returns>
        public async Task<PagedResult<MessageQueryDto>> SearchAsync(BaseFilter filters)
        {
            var query = _context.Messages
                .WhereIf(filters.keywords.IsNotBlank(), x => x.Title.Contains(filters.keywords));

            return await query.OrderByDescending(x => x.CreateDateTime)
                .ProjectTo<MessageQueryDto>(_configurationProvider)
                .PagingAsync(filters.page, filters.rows);
        }

        /// <summary>
        /// 批量逻辑删除
        /// </summary>
        /// <param name="ids">主键ID集合</param>
        /// <returns></returns>
        public async Task<bool> DeleteAsync(IList<string> ids)
        {
            var messages = await _context.Messages.Where(x => ids.Contains(x.Id)).ToListAsync();
            foreach (var x in messages)
            {
                x.IsDeleted = true;
            }
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
