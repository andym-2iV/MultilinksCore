﻿using System;
using System.Threading;
using System.Threading.Tasks;
using Multilinks.ApiService.Models;
using Multilinks.ApiService.Data;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using System.Collections.Generic;
using AutoMapper.QueryableExtensions;
using System.Linq;

namespace Multilinks.ApiService.Services
{
   public class EndpointService : IEndpointService
   {
      private readonly ApplicationDbContext _context;

      public EndpointService(ApplicationDbContext context)
      {
         _context = context;
      }

      public async Task<EndpointViewModel> GetEndpointAsync(Guid id, CancellationToken ct)
      {
         var entity = await _context.Endpoints.SingleOrDefaultAsync(r => r.EndpointId == id, ct);
         if(entity == null) return null;

         return Mapper.Map<EndpointViewModel>(entity);
      }

      public async Task<PagedResults<EndpointViewModel>> GetEndpointsAsync(
         PagingOptions pagingOptions,
         SortOptions<EndpointViewModel, EndpointEntity> sortOptions,
         CancellationToken ct)
      {
         IQueryable<EndpointEntity> query = _context.Endpoints;
         query = sortOptions.Apply(query);

         var size = await query.CountAsync(ct);

         var items = await query
                .Skip(pagingOptions.Offset.Value)
                .Take(pagingOptions.Limit.Value)
                .ProjectTo<EndpointViewModel>()
                .ToArrayAsync(ct);

         return new PagedResults<EndpointViewModel>
         {
            Items = items,
            TotalSize = size
         };
      }
   }
}
