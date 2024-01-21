namespace Otus_project_authorization;

public interface IUserService
{
    bool IsValidUser(UserCredentials userCredentials);
    IEnumerable<string> GetAllowedOperations(string username);
}
