using Microsoft.AspNetCore.Mvc;
using PVEMasters.ApiModels;
using PVEMasters.Services.MissionsService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PVEMasters.Controllers
{

    [Produces("application/json")]
    [Route("api/Missions")]
    public class MissionController : ControllerBase
    {
        private IMissionsService _missionsService;

        public MissionController(IMissionsService missionsService)
        {
            _missionsService = missionsService;
        }

        [HttpGet("AvailableMissions")]
        public async Task<IActionResult> getAvailableMissions()
        {
            return Ok(new List<ApiMission>(_missionsService.getAllAvailableMissions().ToList()));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]ApiModels.ApiMission mission)
        {
            _missionsService.StartMission(mission);
            return Ok();
        }

    }
}
