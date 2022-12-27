using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using WriteSharp.API.DTO.Auth;
using WriteSharp.API.Interfaces;

namespace WriteSharp.API.Services;

public class AuthService : IAuthService
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;

    public AuthService(UserManager<IdentityUser> userManager,
        SignInManager<IdentityUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    public async Task RegisterUserAsync(RegisterDto model)
    {
        var user = new IdentityUser
        {
            Email = model.Email,
            UserName = model.UserName
        };

        var result = await _userManager.CreateAsync(user, model.Password);

        if (!result.Succeeded)
        {
            throw new Exception("Failed to create user account");
        }
    }

    public async Task<LoginResponseDto> LoginUserAsync(LoginDto model)
    {
        var user = await _userManager.FindByEmailAsync(model.Email);

        if (user == null)
        {
            throw new Exception("User not found");
        }

        var roles = await _userManager.GetRolesAsync(user);

        var signin = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);

        if (!signin.Succeeded)
        {
            throw new Exception("Failed to login");
        }

        var token = CreateJwt(user, roles);
        return new LoginResponseDto
        {
            Token = token,
            ExpiryDate = DateTime.Now.AddDays(7),
            ExpiresIn = 604800,
        };
    }

    private static string CreateJwt(IdentityUser user, IList<string>? roles)
    {
        var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("this is my secret"));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>
        {
            new(ClaimTypes.Name, user.UserName),
            new(ClaimTypes.NameIdentifier, user.Id),
            new(JwtRegisteredClaimNames.Jti, user.Id)
        };
        if (roles != null)
        {
            foreach (var role in roles)
            {
                claims.Add(
                    new Claim(ClaimTypes.Role, role)
                );
            }
        }

        var token = new JwtSecurityToken(
            issuer: "Soul-Remix",
            audience: "all",
            claims: claims,
            expires: DateTime.UtcNow.AddDays(7),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}