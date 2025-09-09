using Core.Models;

namespace Backend.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(AppUser user);
    }
}
