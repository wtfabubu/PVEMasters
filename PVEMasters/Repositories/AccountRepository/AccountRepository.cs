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
                                       .Where(user => user.UserName != userName && (user.AccountStatistics.Lvl <= currentUser.AccountStatistics.Lvl+2 && user.AccountStatistics.Lvl >= currentUser.AccountStatistics.Lvl - 2) && user.ChampionsOwned.Where(champ => champ.Equipped == true).Count() > 2 && 
                                       _context.PvPSafeList.Where(safelist => safelist.AttackerId == _context.ApplicationUsers.Where(usr => usr.UserName == userName).FirstOrDefault().Id).Where(safe => safe.DeffenderId == user.Id).Count() == 0)
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

        private async Task<bool> canAttack(string DefenderId,string userName)
        {
            var attackerId = await _context.ApplicationUsers.Where(user => user.UserName == userName).FirstOrDefaultAsync();
            var attackerVictims = await _context.PvPSafeList.Where(safelist => safelist.AttackerId == attackerId.Id).ToListAsync();
            foreach(var victim in attackerVictims)
            {
                if(victim.DeffenderId == DefenderId)
                {
                    return false;
                }
            }
            return true;
        }

        public async Task AddAccountToSafeList(string opponendUsername, string attackerUserName)
        {
            var opponent = await _context.Users.Where(user => user.UserName == opponendUsername).FirstAsync();
            var attacker = await _context.Users.Where(user => user.UserName == attackerUserName).FirstAsync();
            PvPSafeList safeList = new PvPSafeList { AttackerId = attacker.Id, DeffenderId = opponent.Id};
            _context.Add(safeList);
            try
            {
                await _context.SaveChangesAsync();
            } catch (Exception ex)
            {
                Console.Write(ex.Message);
            }
        }
    }
}
