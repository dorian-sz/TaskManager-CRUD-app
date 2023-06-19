namespace TaskManagerApi.Service;

public class AuthService : IAuthService
{
    public bool VerifyUserPassword(string loginPassword, string userPassword)
    {
        return BCrypt.Net.BCrypt.Verify(loginPassword, userPassword);
    }
}