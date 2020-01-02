using Microsoft.AspNetCore.Mvc;
using PVEMasters.ApiModels;
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

        public ShopController(IChampionsService championsService, IEquipmentService equipmentService)
        {
            _championsService = championsService;
            _equipmentService = equipmentService;
        }

        [HttpGet("champions")]
        public async Task<IActionResult> getChampions()
        {
            var result = await _championsService.getChampions();
            return Ok(new List<ApiChampions>(result.ToList()));
        }

        [HttpGet("champions")]
        public async Task<IActionResult> getAvailableChampionsForAccount(String userName)
        {
            var result = await _championsService.getAvailableChampionsForAccount(userName);
            return Ok(new List<ApiChampions>(result.ToList()));
        }

        [HttpGet("equipments")]
        public async Task<IActionResult> getEquipments()
        {
            var result = await _equipmentService.getAllEquipments();
            return Ok(new List<ApiEquipment>(result.ToList()));
        }

    }
}
