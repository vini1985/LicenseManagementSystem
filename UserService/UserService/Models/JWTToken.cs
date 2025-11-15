namespace UserService.Models
{
    public interface JWTToken
    {
        string GenerateToken(string userEmail, string role);
    }
}
