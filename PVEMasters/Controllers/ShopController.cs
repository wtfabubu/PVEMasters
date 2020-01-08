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
    [Route("api/Shop")]
    public class ShopController : ControllerBase
    {
        private IChampionsService _championsService;
        private IEquipmentService _equipmentService;
        readonly UserManager<ApplicationUser> _userManager;


        public ShopController(IChampionsService championsService, IEquipmentService equipmentService, UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _championsService = championsService;
            _equipmentService = equipmentService;
        }

        [HttpGet("champions")]
        public async Task<IActionResult> getChampions()
        {
            var result = await _championsService.getChampions();
            return Ok(new List<ApiChampions>(result.ToList()));
        }

        [HttpGet("availableChampionsForAccount")]
        public async Task<IActionResult> getAvailableChampionsForAccount()
        {
            var user = await _userManager.GetUserAsync(User);
            var result = await _championsService.getAvailableChampionsForAccount(user.UserName);
            return Ok(new List<ApiChampions>(result.ToList()));
        }

        [HttpGet("equipments")]
        public async Task<IActionResult> getEquipments()
        {
            var user = await _userManager.GetUserAsync(User);
            var result = await _equipmentService.getAllEquipmentsAvailableForAccount(user.Id);
            return Ok(new List<ApiEquipment>(result.ToList()));
        }

        [HttpPost("buyChampion")]
        public async Task<IActionResult> BuyChampion([FromBody]ApiChampions champion)
        {
            var user = await _userManager.GetUserAsync(User);
            var result = await _championsService.BuyChampionForUser(champion, user.UserName);
            return Ok(result);
        }

        [HttpPost("buyItem")]
        public async Task<IActionResult> BuyEquipment([FromBody]ApiEquipment equipment)
        {
            var user = await _userManager.GetUserAsync(User);
            var result = await _equipmentService.BuyEquipmentForUser(equipment, user.UserName);
            return Ok(result);
        }
    }
}
