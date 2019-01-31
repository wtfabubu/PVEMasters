using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
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

        public void AddMissionRewardsToAccount(List<MissionRwards> missionRewards)
        {
            throw new NotImplementedException();
        }

        public ApplicationUser getUserByUsername(string userName)
        {
                var usr = _context.Users.Where(user => user.UserName == "Test3@abv.bg").FirstOrDefault();
                usr.AccountStatistics = _context.AccountStatistic.Where(stats => stats.Id == usr.AccountStatisticsId).FirstOrDefault();
                return usr;
        }

        public void UpdateUser(ApplicationUser usr)
        {
                _context.Update(usr);
                _context.SaveChanges();
        }
    }
}
