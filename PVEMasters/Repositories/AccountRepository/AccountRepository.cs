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
                return await _context.Users.Include("AccountStatistics").Include(account => account.ChampionsOwned).ThenInclude(champOwned => champOwned.Champions).Include(account => account.ChampionsOwned).ThenInclude(champOwned => champOwned.ChampionOwnedStats).ThenInclude(champOwnedStat => champOwnedStat.Stat).Where(user => user.UserName == userName).FirstOrDefaultAsync();
        }

        public string GetAccountIdByUserName(string userName)
        {
            return _context.Users.Include("AccountStatistics").Where(user => user.UserName == userName).FirstOrDefault().Id;
        }

        public async Task UpdateUser(ApplicationUser usr)
        {
                _context.Update(usr);
                await _context.SaveChangesAsync();
        }

        public async Task<ICollection<ApplicationUser>> GetAvailablePVPAccounts(string userName)
        {
            var currentUser = await getUserByUsername(userName);
            return await _context.Users.Include("AccountStatistics")
                                       .Include(account => account.ChampionsOwned)
                                       .ThenInclude(champOwned => champOwned.ChampionOwnedStats)
                                       .ThenInclude(champOwnedStat => champOwnedStat.Stat)
                                       .Include(account => account.ChampionsOwned)
                                       .ThenInclude(champOwned => champOwned.Champions)
                                       .Where(user => user.UserName != userName && (user.AccountStatistics.Lvl <= currentUser.AccountStatistics.Lvl+2 && user.AccountStatistics.Lvl >= currentUser.AccountStatistics.Lvl - 2) && user.ChampionsOwned.Where(champ => champ.Equipped == true).Count() > 2)
                                       .ToListAsync();
        }

        public int CreateAccountStatistic(AccountStatistic accountStatistic)
        {
            _context.Add(accountStatistic);
            _context.SaveChanges();

            return accountStatistic.Id;
        }

        public async Task<int> UpdateAccount()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
