using Cubos.Finance.Domain;
using Cubos.Finance.External;
using Cubos.Finance.Shared;

namespace Cubos.Finance.Application
{
    public class CardService(INotifier notifier, IUnitOfWork unitOfWork, IBankAccountRepository peopleRepository, ICardRepository cardRepository, IComplianceFacade compliance) : ServiceBase(notifier), ICardService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IBankAccountRepository _accountRepository = peopleRepository;
        private readonly ICardRepository _cardRepository = cardRepository;
        private readonly IComplianceFacade _compliance = compliance;

        public async Task<CardResponse> CreateCardAsync(Guid bankAccountId, CardRequest request)
        {
            if (!request.Number.IsValidCardNumber())
            {
                Notify(CubosErrorMessages.INVALID_CARD_NUMBER);
                return null;
            }

            var card = request.Map(bankAccountId);

            if (await _cardRepository.HasCardAsync(card.Number))
            {
                Notify(CubosErrorMessages.CARD_ALREADY_EXISTS);
                return null;
            }
            if (card.Type.ToLower() == "physical")
            {
                var alreadyHasPhysical = await _cardRepository.HasCardPhysicalAsync(bankAccountId);
                if (alreadyHasPhysical)
                {
                    Notify(CubosErrorMessages.PHYSICAL_CARD_ALREADY_EXISTS);
                    return null;
                }

            }
            await _cardRepository.CreateAsync(card);

            await _unitOfWork.CommitAsync();

            return card.MapToCreateResponse();
        }
        public async Task<List<CardResponse>> GetCardsAsync(Guid bankAccountId)
        {
            var cards = (await _cardRepository.GetCardsAsync(bankAccountId)).MapToResponse();

            return cards;
        }
        public async Task<QueryBaseResponse<CardResponse>> GetCardsFromPeopleAsync(Guid peopleId, CardPaginationRequest filterRequest)
        {
            var filter = filterRequest.Map(peopleId);

            var cardsResult = await _cardRepository.GetCardsFromPeopleAsync(filter);

            var response = cardsResult.MapItems(card => card.MapToGetResponse());

            return response;
        }
    }  
}
