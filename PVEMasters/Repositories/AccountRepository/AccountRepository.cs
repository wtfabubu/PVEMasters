using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PVEMasters.Models;

namespace PVEMasters.Repositories.AccountRepository
{
    public class AccountRepository : IAccountRepository
    {
        private UserDbContext _context;

        public AccountRepository(UserDbContext context)
        {
            _context = context;
        }

        public async Task AddMissionRewardsToAccount(List<MissionRwards> missionRewards)
        {
            throw new NotImplementedException();
        }

        public async Task<ApplicationUser> getUserByUsername(string userName)
        {
                return await _context.Users.Include("AccountStatistics").Where(user => user.UserName == "real@abv.bg").FirstOrDefaultAsync();
        }

        public async Task UpdateUser(ApplicationUser usr)
        {
                _context.Update(usr);
                await _context.SaveChangesAsync();
        }

        public int CreateAccountStatistic(AccountStatistic accountStatistic)
        {
            _context.Add(accountStatistic);
            _context.SaveChanges();

            return accountStatistic.Id;
        }
    }
}
