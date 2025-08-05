using Cubos.Finance.Domain;
using Cubos.Finance.Shared;

namespace Cubos.Finance.Application
{
    public class AuthService : ServiceBase, IAuthService
    {
        private readonly IPeopleRepository _repository;
        private readonly IJwtService _jwtService;

        public AuthService(INotifier notifier, IPeopleRepository repository, IJwtService jwtService) : base(notifier)
        {
            _repository = repository;
            _jwtService = jwtService;
        }

        public async Task<BearerToken> AuthenticateAsync(LoginRequest request)
        {
            var document = DocumentHelper.CleanDocument(request.Document);

            var people = await _repository.GetByDocumentAsync(document);
            if (people == null)
            {
                Notify(CubosErrorMessages.INVALID_DOCUMENT);
                return null;
            }

            var validPassword = PasswordHasher.Verify(request.Password, people.Password);
            if (!validPassword)
            {
                Notify(CubosErrorMessages.INVALID_PASSWORD);
                return null;
            }

            return _jwtService.GenerateAccessToken(people.Id.ToString());
        }
    }

}
