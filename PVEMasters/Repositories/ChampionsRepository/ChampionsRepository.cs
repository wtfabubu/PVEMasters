using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PVEMasters.Models;

namespace PVEMasters.Services.ChampionsRepository
{
    public class ChampionsRepository : IChampionsRepository
    {
        private UserDbContext _context;
        private UserManager<ApplicationUser> _userManager;

        public ChampionsRepository(UserDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public int AddChampion(ChampionsOwned champ)
        {
            _context.Add(champ);
            _context.SaveChanges();

            return champ.Id;
        }

        public async Task<IEnumerable<ChampionsOwned>> getAccountChampions(String userName)
        {
            
            return await _context.ChampionsOwned.Include("Champions").Where(champion => champion.AccountUsername.Equals(userName)).ToListAsync();
        }

        public async Task<IEnumerable<Champions>> getAvailableChampionsForAccount(String userName)
        {

            return await _context.Champions.Include("Champions").OrderBy(a => a.Name).ToListAsync();
        }

        public Champions getChampion(int champId)
        {
            return _context.Champions.Where(champion => champion.Id == champId).FirstOrDefault();
        }

        public async Task<IEnumerable<Champions>> getChampions()
        {
            return await _context.Champions.OrderBy(a => a.Name).ToListAsync();
        }
    }
}
