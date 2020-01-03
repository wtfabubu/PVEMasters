using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PVEMasters.ApiModels;
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
            champ.ChampionOwnedStats = CreateChampionOwnedStats(champ.Id);
            _context.Update(champ);
            _context.SaveChanges();
            return champ.Id;
        }

        public async Task<string> BuyChampionForAccount(ChampionsOwned champion)
        {
            var championId = AddChampion(champion);
            return "Champion added to your collection";
        }

        public async Task<IEnumerable<ChampionsOwned>> getAccountChampions(String userName)
        {
            
            return await _context.ChampionsOwned.Include("Champions").Include(champ => champ.ChampionOwnedStats).ThenInclude(stat => stat.Stat).Where(champion => champion.AccountUsername.Equals(userName)).ToListAsync();
        }

        public async Task<IEnumerable<Champions>> getAvailableChampionsForAccount(String userName)
        {
            return await _context.Champions
                                 .Where(champions => !_context.ChampionsOwned
                                                              .Where(championsOwned => championsOwned.AccountUsername == userName)
                                                              .Any(championsOwned => championsOwned.ChampionsId == champions.Id)
                                       ).Include(champ => champ.ChampionsStats).ThenInclude(championStat => championStat.Stat).OrderBy(a => a.Name).ToListAsync();
        }

        public Champions getChampion(int champId)
        {
            return _context.Champions.Where(champion => champion.Id == champId).FirstOrDefault();
        }

        public async Task<Champions> getChampionByName(string name)
        {
            return await _context.Champions.Include(champ => champ.ChampionsStats).ThenInclude(championStat => championStat.Stat).OrderBy(a => a.Name).Where(champion => champion.Name == name).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Champions>> getChampions()
        {
            return await _context.Champions.Include(champ => champ.ChampionsStats).ThenInclude(championStat => championStat.Stat).OrderBy(a => a.Name).ToListAsync();
        }

        private List<ChampionOwnedStats> CreateChampionOwnedStats(int champId)
        {
            List<ChampionOwnedStats> statList = new List<ChampionOwnedStats>();

            foreach(var stat in _context.Stat.Include(championStats => championStats.ChampionsStats ).OrderBy(stat => stat.Name).ToList())
            {
                statList.Add(new ChampionOwnedStats { StatId = stat.Id, ChampionsOwnedId = champId, Amount = stat.ChampionsStats.First(element => element.Stat.Name == stat.Name).Amount });
            }
            return statList;
        }
    }
}
