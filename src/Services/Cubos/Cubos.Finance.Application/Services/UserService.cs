using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

namespace Cubos.Finance.Application
{
    public class UserService : IUserService
    {
        public Guid GetPeopleId(ClaimsPrincipal user)
        {
            var peopleIdClaim = user.FindFirst(ClaimTypes.NameIdentifier);

            if (peopleIdClaim == null)
                throw new UnauthorizedAccessException("User not authenticated");

            return Guid.Parse(peopleIdClaim.Value);
        }
    }

}
