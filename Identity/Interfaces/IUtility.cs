using Domain.Entities;

namespace Identity.Interfaces
{
    public interface IUtility
    {
        string EncryptSHA256(string input);
        string GenerateJwtToken(User user);
    }
}
