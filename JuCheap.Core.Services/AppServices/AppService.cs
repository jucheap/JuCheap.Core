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
    /// 应用服务
    /// </summary>
    public class AppService : IAppService
    {
        private readonly JuCheapContext _context;
        private readonly IMapper _mapper;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="context"></param>
        /// <param name="mapper"></param>
        public AppService(JuCheapContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        /// <summary>
        /// 添加编辑
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<Guid> AddOrUpdateAsync(AppDto app)
        {
            var id = Guid.Empty;
            if (app.Id == Guid.Empty)
            {
                var entity = _mapper.Map<AppDto, AppEntity>(app);
                entity.Init();
                entity.Enabled = true;
                _context.Apps.Add(entity);
                id = entity.Id;
            }
            else
            {
                var entity = await _context.Apps.FirstOrDefaultAsync(x => x.Id == app.Id);
                entity.ClientName = app.ClientName;
                entity.ClientUri = app.ClientUri;
            }

            await _context.SaveChangesAsync();
            return id;
        }

        public async Task<AppDto> GetAsync(Guid id)
        {
            var entity = await _context.Apps.FirstOrDefaultAsync(x => x.Id == id);
            return _mapper.Map<AppEntity, AppDto>(entity);
        }

        public async Task<List<AppDto>> GetByUserId(Guid userId)
        {
            var list = await _context.Apps.Where(x => x.UserId == userId).ToListAsync();
            return _mapper.Map<List<AppEntity>, List<AppDto>>(list);
        }
    }
}
