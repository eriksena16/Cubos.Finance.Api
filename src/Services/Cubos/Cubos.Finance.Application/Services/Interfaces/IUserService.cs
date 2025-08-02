using System.Security.Claims;

namespace Cubos.Finance.Application
{
        public interface IUserService
        {
            Guid GetPeopleId(ClaimsPrincipal user);
        }
}
