﻿using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using LivrariaVirtualApp.Domain.Models;
using LivrariaVirtualApp.Domain.Repositories;
using System.Threading.Tasks;
using System.Linq;

namespace LivrariaVirtualApp.Infrastructure.Repositories
{
    public class WishlistRepository : Repository<Wishlist>, IWishlistRepository
    {
        public WishlistRepository(LivrariaVirtualDbContext dbContext) : base(dbContext)
        {
        }

        public Task<List<Wishlist>> FindAllByNameStartWithAsync(string name_wishlist)
        {
            return _dbContext.Wishlists.Where(c => c.Name.StartsWith(name_wishlist))
                .OrderBy(c => c.Name)
                .ToListAsync();
        }

    
        public Task<Wishlist> FindByNameAsync(string name_wishlist)
        {
            return _dbContext.Wishlists.SingleOrDefaultAsync(c => c.Name == name_wishlist);
        }

        public override async Task<Wishlist> FindOrCreate(Wishlist e)
        {
            var c = await _dbContext.Wishlists.SingleOrDefaultAsync(i => i.Name == e.Name);
            if (c == null)
            {
                c = await CreateAsync(e);
                await _dbContext.SaveChangesAsync();
            }
            return c;
        }


        public override async Task<Wishlist> UpsertAsync(Wishlist e)
        {
            Wishlist f = null;
            Wishlist existing = await FindByNameAsync(e.Name);

            if (existing == null)
            {
                if (e.Id == 0)
                {
                    f = await CreateAsync(e);
                }
                else
                {
                    f = await UpdateAsync(e);
                }
            }
            else if (existing.Id == e.Id)
            {
                _dbContext.Entry(existing).State = EntityState.Detached;
                f = await UpdateAsync(e);
            }
            await _dbContext.SaveChangesAsync();

            return f;
        }
    }
}
