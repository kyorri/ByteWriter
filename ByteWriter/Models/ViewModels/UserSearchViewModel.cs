namespace ByteWriter.Models.ViewModels
{
    public class UserSearchViewModel
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Bio { get; set; }
        public bool IsFollowing { get; set; }
        public string UserId { get; set; }
        public bool IsCurrentUser { get; set; }
    }
}
