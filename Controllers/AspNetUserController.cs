using BuildWeek2.Data;
using BuildWeek2.Models.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace BuildWeek2.Controllers
{
    //[ApiExplorerSettings(GroupName = "Auth")]
    //[Tags("Auth - Login & Register")]
    [Route("api/[controller]")]
    [ApiController]
    public class AspNetUserController : ControllerBase
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;


        public AspNetUserController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }


        //ENDPOINTS 
        //REGISTRAZIONE 
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequestDto registerRequestDto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ApplicationUser user = new ApplicationUser()
                    {
                        UserName = registerRequestDto.Email,
                        Email = registerRequestDto.Email,
                        NomeCompleto = registerRequestDto.NomeCompleto,
                        EmailConfirmed = true,
                        LockoutEnabled = false

                    };
                    IdentityResult result = await _userManager.CreateAsync(user, registerRequestDto.Password);
                    if (result.Succeeded)
                    {
                        var roleExists = await _roleManager.RoleExistsAsync("User");
                        if (!roleExists)
                        {
                            await _roleManager.CreateAsync(new IdentityRole("User"));
                        }
                        await _userManager.AddToRoleAsync(user, "User");
                        return Ok("Utente registrato con successo!");
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            return BadRequest("Errore nella registrazione dell'utente.");
        }



        //LOGIN 
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequestDto loginRequestDto)
        {
            try
            {
                ApplicationUser user = await _userManager.FindByNameAsync(loginRequestDto.Username);
                //controllo se l'utente esiste
                if (user is null)
                { return BadRequest(); }

                //controllo login 
                var result = await _signInManager.PasswordSignInAsync(user, loginRequestDto.Password, false, false);

                if (!result.Succeeded)
                {
                    return StatusCode(StatusCodes.Status401Unauthorized);
                }

                //Ruoli 
                List<string> roles = (await this._userManager.GetRolesAsync(user)).ToList();

                //Claims
                List<Claim> userClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id),
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.Email, user.Email)
                };
                foreach (string roleName in roles)
                {
                    userClaims.Add(new Claim(ClaimTypes.Role, roleName));
                }


                //Generazione token
                var key = System.Text.Encoding.UTF8.GetBytes("7134e3acff05c9585aefc36e7067bffaf24ac5784d06925b456a1544060dc5c9f5b0a5b1");
                SigningCredentials cred = new SigningCredentials(new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256);

                var tokenExpiration = DateTime.Now.AddMinutes(30);

                JwtSecurityToken jwt = new JwtSecurityToken(
                    "https://", //Issuer
                    "https://", //Audience
                    claims: userClaims,
                    expires: tokenExpiration,
                    signingCredentials: cred
                    );

                string token = new JwtSecurityTokenHandler().WriteToken(jwt);

                return Ok(new LoginResponseDto()
                {
                    Token = token,
                    Expiration = tokenExpiration

                });

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);

            }
        }
    }
}


