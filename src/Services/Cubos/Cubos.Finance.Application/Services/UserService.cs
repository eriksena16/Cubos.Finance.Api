using Cubos.Finance.Shared;
using System.Security.Claims;

namespace Cubos.Finance.Application
{
    public class UserService : IUserService
    {
        public Guid GetPeopleId(ClaimsPrincipal user)
        {
            var peopleIdClaim = user.FindFirst(ClaimTypes.NameIdentifier);

            if (peopleIdClaim == null)
                throw new UnauthorizedAccessException(CubosErrorMessages.USER_NOT_AUTHENTICATED);

            return Guid.Parse(peopleIdClaim.Value);
        }
    }

}
