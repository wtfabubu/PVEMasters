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

            var user = await _userManager.GetUserAsync(User);
            var result = await _missionsService.getAllAvailableMissions(user.Id);
            return Ok(new List<ApiMission>(result.ToList()));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]ApiMission mission)
        {
            var user = await _userManager.GetUserAsync(User);
            await _missionsService.StartMission(mission, user.Id);
            return Ok();
        }

        [HttpPost("postCompleteMission")]
        public async Task<IActionResult> PostCompleteMission([FromBody]ApiMission mission)
        {
            var user = await _userManager.GetUserAsync(User);
            await _missionsService.CompleteMission(mission, user.UserName);
            return Ok();
        }
        

       [HttpGet("getAllInProgressMissionForAccount")]
        public async Task<IActionResult> GetAllInProgressMissionForAccount()
        {
            var user = await _userManager.GetUserAsync(User);
            var result = await _missionsService.GetAllMissionsWithGivenStatus(user.Id, "In Progress");
            return Ok(result);
        }

        [HttpGet("getAllCompletedMissionForAccount")]
        public async Task<IActionResult> GetAllCompletedMissionForAccount()
        {
            var user = await _userManager.GetUserAsync(User);
            var result = await _missionsService.GetAllMissionsWithGivenStatus(user.Id, "Completed");
            return Ok(result);
        }

    }
}
