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

        public async Task<IEnumerable<Mission>> getAllAvailableMissions(String userName)
        {
            //Edit this query(this is an example of bad code)
            var usr = await _context.ApplicationUsers.Include("AccountStatistics").Where(user => user.UserName == userName).FirstOrDefaultAsync();
            List<Mission> missions = _context.Mission.Where(mission => mission.AccountLvlRequired <= usr.AccountStatistics.Lvl).ToList();
            missions.ForEach(mission => mission.MissionRwards = _context.MissionRwards.Where(mr => mr.MissionId == mission.Id).ToList());
            return missions;
        }

        public IEnumerable<Mission> getAllCompletedMissions()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Mission> getAllInProgressMissions()
        {
            throw new NotImplementedException();
        }

        public async Task StartMission(Mission mission, String userName)
        {
            //Edit this query(this is an example of bad code)
            var missionStatus = _context.MissionStatus.Where(status => status.Status == "Completed").FirstOrDefault();
            MissionsForAccount missionForAccount = new MissionsForAccount { AccountUsername = userName, MissionId = mission.Id, StatusId = missionStatus.Id, Status = missionStatus };
            _context.MissionsForAccount.Add(missionForAccount);
            await _context.SaveChangesAsync();
        }
    }
}
