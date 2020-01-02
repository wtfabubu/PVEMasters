using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PVEMasters.Models;
using PVEMasters.Services.AccountService;
using PVEMasters.Services.ChampionsService;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace PVEMasters.Controllers
{
    public class Credentials
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Gender { get; set; }
        public string InGameName { get; set; }
    }

    [Produces("application/json")]
    [Route("api/Account")]
    public class AccountController : ControllerBase
    {
        readonly UserManager<ApplicationUser> _userManager;
        readonly SignInManager<ApplicationUser> signInManager;
        readonly IAccountService accountService;
        readonly IChampionsService championsService;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IAccountService accountService, IChampionsService championsService)
        {
            _userManager = userManager;
            this.signInManager = signInManager;
            this.accountService = accountService;
            this.championsService = championsService;
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] Credentials credentials)
        {
            int accStatId = accountService.CreateAccountStatistic(new AccountStatistic());
            ChampionsOwned champ1 = new ChampionsOwned { AccountUsername = credentials.Username, ChampionsId = 4, Agility = 10, Strength = 5, MagicPower = 2, Experience = 0, Health = 100, Lvl = 1};
            ChampionsOwned champ2 = new ChampionsOwned { AccountUsername = credentials.Username, ChampionsId = 5, Agility = 5, Strength = 10, MagicPower = 2, Experience = 0, Health = 100, Lvl = 1 };
            ChampionsOwned champ3 = new ChampionsOwned { AccountUsername = credentials.Username, ChampionsId = 6, Agility = 2, Strength = 5, MagicPower = 10, Experience = 0, Health = 100, Lvl = 1 };
            championsService.AddChampion(champ1);
            championsService.AddChampion(champ2);
            championsService.AddChampion(champ3);
            var user = new ApplicationUser { UserName = credentials.Username, Email = credentials.Username, Gender = credentials.Gender, InGameName = credentials.InGameName, AccountStatisticsId = accStatId};

            var result = await _userManager.CreateAsync(user, credentials.Password);

            if (!result.Succeeded)
                return BadRequest(result.Errors);

            await signInManager.SignInAsync(user, isPersistent: false);

            return Ok(CreateToken(user));

        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] Credentials credentials)
        {
            var result = await signInManager.PasswordSignInAsync(credentials.Username, credentials.Password, false, false);

            if (!result.Succeeded)
                return BadRequest();

            var user = await _userManager.FindByEmailAsync(credentials.Username);

            return Ok(CreateToken(user));
        }

        string CreateToken(ApplicationUser user)
        {
            var claims = new Claim[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id)
            };

            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("this is the secret phrase"));
            var signingCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);

            var jwt = new JwtSecurityToken(signingCredentials: signingCredentials, claims: claims);
            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }

        [HttpGet("getUserProfile")]
        public async Task<IActionResult> GetUserProfile()
        {
            var user = await _userManager.GetUserAsync(User);
            var userName = user.UserName;
            var userProfile = await accountService.GetUserProfileByUserName(userName);
            return Ok(userProfile);
        }
    }
}
