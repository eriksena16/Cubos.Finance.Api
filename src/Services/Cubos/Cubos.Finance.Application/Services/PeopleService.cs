using Cubos.Finance.Domain;
using Cubos.Finance.External;
using Cubos.Finance.Shared;

namespace Cubos.Finance.Application
{
    public class PeopleService : ServiceBase, IPeopleService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPeopleRepository _peopleRepository;
        private readonly IComplianceFacade _compliance;
        public PeopleService(INotifier notifier, IUnitOfWork unitOfWork, IPeopleRepository peopleRepository, IComplianceFacade compliance) : base(notifier)
        {
            _unitOfWork = unitOfWork;
            _peopleRepository = peopleRepository;
            _compliance = compliance;
        }

        public async Task<PeopleResponse> CreateAsync(PeopleRequest request)
        {
            var people = request.Map();

            var isValid = await _compliance.IsDocumentValidAsync(people.Document);

            if (!isValid)
                Notify("Documento inválido.");

            if (await _peopleRepository.HasPeopleAsync(people.Document))
                Notify("Já existe uma pessoa com este documento.");

            if (IsInvalidOperation())
                return null;

            await _peopleRepository.CreateAsync(people);
            await _unitOfWork.CommitAsync(); 

            return people.MapToResponse();
        }
    }
}
