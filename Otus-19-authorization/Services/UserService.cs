namespace Otus_19_authorization;

public class UserService : IUserService
{
    private readonly (string, string)[] users = new[]{
        ("admin", "adminpassword"), 
        ("user", "userpassword") 
    };
    private readonly Dictionary<string, string[]> userRoles = new Dictionary<string, string[]>
    {
        {"admin", new[]{ "player", "viewer"}},
        {"user", new[]{ "viewer"}},
    };
    
    private readonly Dictionary<string, string[]> roleOperations = new Dictionary<string, string[]>
    {
        {"player", new[]{ "ChangeState", "StartMove", "StopMove", "Shot", "PrintObject"}},
        {"viewer", new[]{ "PrintObject"}},
    };

    public bool IsValidUser(UserCredentials userCredentials) 
    {
        return users.Contains((userCredentials.Username, userCredentials.Password));
    }
    
    public IEnumerable<string> GetAllowedOperations(string username)
    {
        if (!userRoles.ContainsKey(username)) return Array.Empty<string>();

        return roleOperations.Where(x => userRoles[username].Contains(x.Key)).SelectMany(y => y.Value);
    }
}
