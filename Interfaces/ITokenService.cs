using apis.Models;

namespace apis.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(AppUser user);
    }
}
