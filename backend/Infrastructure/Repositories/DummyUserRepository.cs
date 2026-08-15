using Domain.Interfaces;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Repositories
{
    public class DummyUserRepository : IDummyUserRepository
    {
        private readonly AppDbContext _dbContext;

        public DummyUserRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> UserExists(int userId)
        {
            var user = await _dbContext.Users.AsNoTracking().SingleOrDefaultAsync(x => x.Id == userId).ConfigureAwait(false);
            return user != null ? true : false;
        }
    }
}
