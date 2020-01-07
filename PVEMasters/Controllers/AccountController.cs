using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PVEMasters.ApiModels;
using PVEMasters.ModelMappers;
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

            var user = new ApplicationUser { UserName = credentials.Username, Email = credentials.Username, Gender = credentials.Gender, InGameName = credentials.InGameName, AccountStatisticsId = accStatId };


            var result = await _userManager.CreateAsync(user, credentials.Password);
            ApiResponse response = new ApiResponse();

            if (!result.Succeeded)
            {                
                result.Errors.ToList().ForEach(err => response.Error += err.Description + " ");
                return Ok(response);
            }

            ChampionsOwned champ1 = new ChampionsOwned { AccountId = accountService.GetAccountIdByUserName(credentials.Username), ChampionsId = 4, Experience = 0, Lvl = 1, Equipped = true };
            ChampionsOwned champ2 = new ChampionsOwned { AccountId = accountService.GetAccountIdByUserName(credentials.Username), ChampionsId = 5, Experience = 0, Lvl = 1, Equipped = true };
            ChampionsOwned champ3 = new ChampionsOwned { AccountId = accountService.GetAccountIdByUserName(credentials.Username), ChampionsId = 6, Experience = 0, Lvl = 1, Equipped = true };
            championsService.AddChampion(champ1);
            championsService.AddChampion(champ2);
            championsService.AddChampion(champ3);
            await signInManager.SignInAsync(user, isPersistent: false);

            response.auth = new ApiAuth { Token = CreateToken(user), UserName = credentials.Username };
            return Ok(response);

        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] Credentials credentials)
        {
            var result = await signInManager.PasswordSignInAsync(credentials.Username, credentials.Password, false, false);

            ApiResponse response = new ApiResponse();

            if (!result.Succeeded)
            {
                response.Error += "Username or password is wrong!";
                return Ok(response);
            }

            var user = await _userManager.FindByEmailAsync(credentials.Username);

            response.auth = new ApiAuth { Token = CreateToken(user), UserName = credentials.Username };
            return Ok(response);
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

        [HttpGet("getAvailablePVPAccounts")]
        public async Task<IActionResult> GetAvailablePVPAccounts()
        {
            var user = await _userManager.GetUserAsync(User);
            var userName = user.UserName;
            var userProfile = await accountService.GetAvailablePVPAccounts(userName);
            return Ok(userProfile);
        }

        [HttpPost("attackOpponent")]
        public async Task<IActionResult> AttackOpponent([FromBody]ApiProfile account)
        {
            var user = await _userManager.GetUserAsync(User);
            var result = await accountService.AttackOpponent(account, user.UserName);
            return Ok(result);
        }

        private List<ChampionOwnedStats> CreateChampionOwnedStats(int champId, int agility, int str, int health, int magicPower)
        {
            List<ChampionOwnedStats> statList = new List<ChampionOwnedStats>();
            statList.Add(new ChampionOwnedStats { StatId = 1, ChampionsOwnedId = champId, Amount = agility });
            statList.Add(new ChampionOwnedStats { StatId = 2, ChampionsOwnedId = champId, Amount = str });
            statList.Add(new ChampionOwnedStats { StatId = 3, ChampionsOwnedId = champId, Amount = health });
            statList.Add(new ChampionOwnedStats { StatId = 4, ChampionsOwnedId = champId, Amount = magicPower });
            return statList;
        }
    }
}
