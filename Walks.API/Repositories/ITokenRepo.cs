using Microsoft.AspNetCore.Identity;

namespace Walks.API.Repositories
{
    public interface ITokenRepo
    {
        string CreateJwtToken(IdentityUser user, List<string> roles); 
    }
}
