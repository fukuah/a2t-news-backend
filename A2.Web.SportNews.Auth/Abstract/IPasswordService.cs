namespace A2.Web.SportNews.Auth.Abstract
{
    public interface IPasswordService
    {
        string GenerateHash(string password);
        (bool IsValid, string NewPasswordHash) CheckPassword(string passwordToCheck, string userPasswordHash);
    }
}