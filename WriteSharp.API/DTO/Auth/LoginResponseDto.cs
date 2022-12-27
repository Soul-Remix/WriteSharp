namespace WriteSharp.API.DTO.Auth;

public class LoginResponseDto
{
    public string Token { get; set; }
    public int ExpiresIn { get; set; }
    public DateTime ExpiryDate { get; set; }
}