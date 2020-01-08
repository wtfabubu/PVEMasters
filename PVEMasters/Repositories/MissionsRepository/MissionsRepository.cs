using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using PVEMasters.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace PVEMasters.Repositories.MissionsRepository
{
    public class MissionsRepository : IMissionsRepository
    {
        private UserDbContext _context;
        private IHttpContextAccessor _httpContext;
        readonly SignInManager<ApplicationUser> signInManager;

        public MissionsRepository(UserDbContext context, IHttpContextAccessor httpContext, SignInManager<ApplicationUser> signInManager)
        {
            _context = context;
            _httpContext = httpContext;
            this.signInManager = signInManager;
        }

        public async Task<IEnumerable<Mission>> getAllAvailableMissions(String userId)
        {
            //Edit this query(this is an example of bad code)
            var usr = await _context.ApplicationUsers.Include("AccountStatistics").Where(user => user.Id == userId).FirstOrDefaultAsync();
            List<Mission> missions = _context.Mission.Where(mission => mission.AccountLvlRequired <= usr.AccountStatistics.Lvl && _context.MissionsForAccount.Where(miss => miss.MissionId == mission.Id && miss.AccountId == userId).Count() == 0).ToList();
            missions.ForEach(mission => mission.MissionRwards = _context.MissionRwards.Where(mr => mr.MissionId == mission.Id).ToList());
            return missions;
        }

        public async Task<IEnumerable<MissionsForAccount>> GetAllMissionsWithGivenStatus(String userId, String status)
        {
            return await _context.MissionsForAccount.Include(mission => mission.Mission)
                                 .ThenInclude(mission => mission.MissionRwards)
                                 .Where(mission => mission.AccountId == userId)
                                 .Where(mission => mission.StatusId == _context.MissionStatus.Where(stat => stat.Status == status).FirstOrDefault().Id)
                                 .ToListAsync();
        }

        public async Task StartMission(Mission mission, String userId)
        {
            //Edit this query(this is an example of bad code)
            var missionStatus = _context.MissionStatus.Where(status => status.Status == "In Progress").FirstOrDefault();
            MissionsForAccount missionForAccount = new MissionsForAccount { AccountId = userId, MissionId = mission.Id, StatusId = missionStatus.Id, Status = missionStatus, StartDateTime = DateTimeOffset.Now, EndDateTime = DateTimeOffset.Now.AddSeconds(mission.Duration)};
            _context.MissionsForAccount.Add(missionForAccount);
            try
            {
                await _context.SaveChangesAsync();
            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public async Task CompleteMission(int missionId)
        {
            //Edit this query(this is an example of bad code)
            var missionStatus = _context.MissionStatus.Where(status => status.Status == "Completed").FirstOrDefault();
            MissionsForAccount missionForAccount = await _context.MissionsForAccount.Where(miss => miss.Id == missionId).FirstOrDefaultAsync();
            missionForAccount.StatusId = missionStatus.Id;
            _context.MissionsForAccount.Update(missionForAccount);
            await _context.SaveChangesAsync();
        }
    }
}
