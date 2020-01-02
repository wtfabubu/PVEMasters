using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PVEMasters.ApiModels;
using PVEMasters.Models;
using PVEMasters.Services.MissionsService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PVEMasters.Controllers
{

    [Produces("application/json")]
    [Route("api/Missions")]
    public class MissionController : ControllerBase
    {
        private IMissionsService _missionsService;
        readonly UserManager<ApplicationUser> _userManager;

        public MissionController(IMissionsService missionsService, UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _missionsService = missionsService;
        }

        [HttpGet("AvailableMissions")]
        public async Task<IActionResult> getAvailableMissions()
        {
            var result = await _missionsService.getAllAvailableMissions();
            return Ok(new List<ApiMission>(result.ToList()));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]ApiModels.ApiMission mission)
        {
            var user = await _userManager.GetUserAsync(User);
            var userName = user.UserName;
            await _missionsService.StartMission(mission, userName);
            return Ok();
        }

    }
}
