namespace Otus_19_authorization;

public interface IUserService
{
    bool IsValidUser(UserCredentials userCredentials);
    IEnumerable<string> GetAllowedOperations(string username);
}
