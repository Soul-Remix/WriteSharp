using WriteSharp.API.DTO.Auth;

namespace WriteSharp.API.Interfaces;

public interface IAuthService
{
    public Task RegisterUserAsync(RegisterDto model);
    public Task<LoginResponseDto> LoginUserAsync(LoginDto model);
}