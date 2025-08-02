using Cubos.Finance.Shared.Utils;
using Cubos.Finance.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cubos.Finance.Domain;
using Cubos.Finance.External;

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
                Notify("Documento inválido.");

            var validPassword = PasswordHasher.Verify(request.Password, people.Password);
            if (!validPassword)
                Notify("Senha inválida.");

            if (IsInvalidOperation())
                return null;

            return _jwtService.GenerateAccessToken(people.Id.ToString());
        }
    }

}
