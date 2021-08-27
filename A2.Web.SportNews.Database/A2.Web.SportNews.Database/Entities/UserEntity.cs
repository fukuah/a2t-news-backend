namespace A2.Web.SportNews.Database.Entities
{
    public class UserEntity : Entity
    {
        public string Username { get; set; }
        public string PasswordHash { get; set; }
    }
}
