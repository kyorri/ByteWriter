using ByteWriter.Models;

public class UserProfileViewModel
{
    public string UserName { get; set; }
    public string Email { get; set; }
    public string Bio { get; set; }
    public bool IsAdministrator { get; set; }
    public string StatusMessage { get; set; }
    public bool IsFollowing { get; set; }
    public bool IsCurrentUser { get; set; }
    public string UserId { get; set; }
    public List<Article> UserArticles { get; set; }
    public List<User> Friends { get; set; }
}
