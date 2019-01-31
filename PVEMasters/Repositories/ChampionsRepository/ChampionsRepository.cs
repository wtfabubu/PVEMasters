using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PVEMasters.Models;

namespace PVEMasters.Services.ChampionsRepository
{
    public class ChampionsRepository : IChampionsRepository
    {
        private UserDbContext _context;

        public ChampionsRepository(UserDbContext context)
        {
            _context = context;
        }

        public Champions getChampion(int champId)
        {
            return _context.Champions.Where(champion => champion.Id == champId).FirstOrDefault();
        }

        public IEnumerable<Champions> getChampions()
        {
            return _context.Champions.OrderBy(a => a.Name).ToList();
        }
    }
}
