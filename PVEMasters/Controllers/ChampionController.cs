using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PVEMasters.ApiModels;
using PVEMasters.Models;
using PVEMasters.Services.ChampionsService;
using PVEMasters.Services.EquipmentService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PVEMasters.Controllers
{
    [Produces("application/json")]
    [Route("api/champion")]
    public class ChampionController : ControllerBase
    {
        private IChampionsService _championsService;
        private IEquipmentService _equipmentService;
        private readonly UserManager<ApplicationUser> _userManager;

        public ChampionController(IChampionsService championsService, IEquipmentService equipmentService, UserManager<ApplicationUser> userManager)
        {
            _equipmentService = equipmentService;
            _userManager = userManager;
            _championsService = championsService;
        }

        [HttpGet("accountChampions")]
        public async Task<IActionResult> getChampions()
        {
            var user = await _userManager.GetUserAsync(User);
            var userName = user.UserName;
            var result = await _championsService.getAccountChampions(userName);
            return Ok(new List<ApiChampionsOwned>(result.ToList()));
        }
    }
}
