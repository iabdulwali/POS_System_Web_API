using POS_System_API.Entities.Models;

namespace POS_System_API.Token
{
    public interface ITokenService
    {
        public string CreateToken(AppUser user);
    }
}
