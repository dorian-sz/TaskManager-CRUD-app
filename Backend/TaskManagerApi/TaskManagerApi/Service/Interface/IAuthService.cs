namespace TaskManagerApi.Service;

public interface IAuthService
{
    bool VerifyUserPassword(string loginPassword, string userPassword);
}