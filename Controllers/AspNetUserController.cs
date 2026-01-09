using BuildWeek2.Data;
using BuildWeek2.Models.Dto;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BuildWeek2.Controllers
{
    [Tags("Auth - Login & Register")]
    [Route("api/[controller]")]
    [ApiController]
    public class AspNetUserController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _config; // Serve a leggere i valori di appsettings.json

        public AspNetUserController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            RoleManager<IdentityRole> roleManager,
            IConfiguration config)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _config = config;
        }

        // ENDPOINTS
        // REGISTRAZIONE
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequestDto registerRequestDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var user = new ApplicationUser
                {
                    UserName = registerRequestDto.Email,
                    Email = registerRequestDto.Email,
                    NomeCompleto = registerRequestDto.NomeCompleto,
                    CodiceFiscale = registerRequestDto.CodiceFiscale,
                    EmailConfirmed = true,
                    LockoutEnabled = false
                };

                var result = await _userManager.CreateAsync(user, registerRequestDto.Password);

                if (!result.Succeeded)
                    return BadRequest(result.Errors);

                // CREA RUOLO SE NON ESISTE
                if (!await _roleManager.RoleExistsAsync("User"))
                    await _roleManager.CreateAsync(new IdentityRole("User"));

                if (!await _roleManager.RoleExistsAsync("Farmacista"))
                    await _roleManager.CreateAsync(new IdentityRole("Farmacista"));

                await _userManager.AddToRoleAsync(user, "User");

                return Ok("Registrazione completata con successo");
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    error = ex.InnerException?.Message,
                    fullError = ex.Message
                });
            }
        }


        //Login + Generazione token       
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequestDto dto)
        {
            try
            {
                var user = await _userManager.FindByNameAsync(dto.Username);

                if (user == null)
                    return Unauthorized("Credenziali non valide");

                var passwordValid = await _signInManager.CheckPasswordSignInAsync(
                    user, dto.Password, false);

                if (!passwordValid.Succeeded)
                    return Unauthorized("Credenziali non valide");

                var roles = await _userManager.GetRolesAsync(user);

                // CLAIMS
                var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim(ClaimTypes.Name, user.UserName),
            new Claim(ClaimTypes.Email, user.Email)
        };

                foreach (var role in roles)
                    claims.Add(new Claim(ClaimTypes.Role, role));

                // JWT
                var key = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(_config["Jwt:Key"])
                );

                var credentials = new SigningCredentials(
                    key, SecurityAlgorithms.HmacSha256);

                var expiration = DateTime.UtcNow.AddMinutes(30);

                var token = new JwtSecurityToken(
                    issuer: _config["Jwt:Issuer"],
                    audience: _config["Jwt:Audience"],
                    claims: claims,
                    expires: expiration,
                    signingCredentials: credentials
                );

                return Ok(new LoginResponseDto
                {
                    Token = new JwtSecurityTokenHandler().WriteToken(token),
                    Expiration = expiration
                });
            }
            catch (Exception ex)
            {
                // LOG opzionale: ex.Message
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Errore interno durante il login");
            }
        }

    }
}
