
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PVEMasters.ApiModels;
using PVEMasters.Models;
using PVEMasters.Services.EquipmentService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PVEMasters.Controllers
{
    [Produces("application/json")]
    [Route("api/equipment")]
    public class EquipmentController: ControllerBase
    {
        private IEquipmentService _equipmentService;
        readonly UserManager<ApplicationUser> _userManager;

        public EquipmentController(IEquipmentService equipmentService, UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _equipmentService = equipmentService;
        }

        [HttpGet("accountEquipments")]
        public async Task<IActionResult> GetEquipments()
        {
            var user = await _userManager.GetUserAsync(User);
            var userName = user.UserName;
            var result = await _equipmentService.getAccountEquipments(userName);
            return Ok(new List<ApiEquipment>(result.ToList()));
        }

    }
}
