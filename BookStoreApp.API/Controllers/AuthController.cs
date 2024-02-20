using AutoMapper;
using BookStoreApp.API.Data.DTOs.User;
using BookStoreApp.API.Data.Entities;
using BookStoreApp.API.Static;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BookStoreApp.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly ILogger<AuthController> _logger;
    private readonly IMapper _mapper;
    private readonly UserManager<ApiUser> _userManager;
    private readonly IConfiguration _configuration;

    public AuthController(ILogger<AuthController> logger, IMapper mapper, UserManager<ApiUser> userManager, IConfiguration configuration)
    {
        _logger = logger;
        _mapper = mapper;
        _userManager = userManager;
        _configuration = configuration;
    }

    [HttpPost]
    [Route("register")]
    public async Task<IActionResult> Register(UserDto userDto)
    {
        _logger.LogInformation($"Register attempt for user {userDto.Email}");


        var user = _mapper.Map<ApiUser>(userDto);
        user.UserName = userDto.Email;
        var result = await _userManager.CreateAsync(user, userDto.Password);
        if (result.Succeeded == false)
        {
            foreach (var item in result.Errors)
            {
                ModelState.AddModelError(item.Code, item.Description);
            }
            return BadRequest(ModelState);
        }

        try
        {
            await _userManager.AddToRoleAsync(user, "User");
            return Accepted();
        }
        catch (Exception ex)
        {
            _logger.LogError($"Registration error occured: {ex.Message}");
            return Problem($"Something wrong occured during the registration process", statusCode: 500);
            //throw;
        }
    }

    [HttpPost]
    [Route("login")]
    public async Task<ActionResult<AuthResponse>> Login(LoginUserDto userDto)
    {
        _logger.LogInformation($"Login attempt using {userDto.Email} address");
        try
        {
            var user = await _userManager.FindByEmailAsync(userDto.Email);

            if (user == null)
            {
                _logger.LogError($"Did not found an user {userDto.Email} while loging in");
                return Unauthorized(userDto);
            }

            var passwordValid = await _userManager.CheckPasswordAsync(user, userDto.Password);

            if (passwordValid == false)
            {
                _logger.LogError($"User found, but provided password is not correct. User: {userDto.Email}");
                return Unauthorized(userDto);
            }

            var tokenString = await GenerateTokenAsync(user);

            var response = new AuthResponse
            {
                Email = userDto.Email,
                Token = tokenString,
                UserId = user.Id,
            };

            return response;

        }
        catch (Exception ex)
        {
            _logger.LogError($"Login error occured: {ex.Message}");
            return Problem($"Something wrong occured during the login process", statusCode: 500);

            //throw;
        }

    }

    private async Task<string> GenerateTokenAsync(ApiUser user)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:Key"]!));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var roles = await _userManager.GetRolesAsync(user);
        var roleClaims = roles.Select(q => new Claim(ClaimTypes.Role, q)).ToList();

        var userClaims = await _userManager.GetClaimsAsync(user);

        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.UserName!),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email!),
            new Claim(CustomClaimTypes.Uid, user.Id),
        }
        .Union(userClaims)
        .Union(roleClaims);

        var token = new JwtSecurityToken(
            issuer:_configuration["JwtSettings:Issuer"],
            audience: _configuration["JwtSettings:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddHours(Convert.ToInt32(_configuration["JwtSettings:Duration"])),
            signingCredentials: credentials
            );
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
