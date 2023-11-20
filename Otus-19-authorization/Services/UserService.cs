namespace Otus_19_authorization;

public class UserService : IUserService
{
    public bool IsValidUser(UserCredentials userCredentials)
    {
        var validUsers = new[]{ ("admin", "adminpassword"), ("user", "userpassword") };

        return validUsers.Contains((userCredentials.Username, userCredentials.Password));
    }

    public IEnumerable<string> GetAllowedOperations(string username)
    {
        var userOperations = new Dictionary<string, string[]>
        {
            {"admin", new[]{ "ChangeState", "PrintState"}},
            {"user", new[]{ "PrintState"}},
        };
        
        return userOperations.ContainsKey(username) ? userOperations[username] : Array.Empty<string>();
    }
}
